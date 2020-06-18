using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace log4jDigger
{
    public class StreamingFactory : IDisposable
    {
        public event EventHandler NewPositions;
        public event EventHandler IsInConsistent;
        private List<StreamingHost> streamingHosts = new List<StreamingHost>();
        public List<LogPos> PositionList = new List<LogPos>();
        private bool isDisposing = false;
        private Dictionary<SearchEventArgs, List<LogPos>> searchResults = new Dictionary<SearchEventArgs, List<LogPos>>();
        private bool isBusy = false;
        Timer pollTimer;
        public StreamingFactory()
        {
            pollTimer = new Timer(2000);
            pollTimer.Elapsed += Timer_Elapsed;
            pollTimer.Enabled = false;
        }

        public void RemoveSearchResult(SearchEventArgs e)
        {
            if (e != null && searchResults.ContainsKey(e))
                searchResults.Remove(e);
        }

        public bool EnablePolling
        {

            get
            {
                return pollTimer.Enabled;
            }

            set
            {
                pollTimer.Enabled = value;
            }
        }

        public void Poll()
        {
            if (isBusy)
                return;

            System.Diagnostics.Debug.WriteLine("Poll");
            long newPositions = 0;
            foreach (StreamingHost sh in streamingHosts)
            {
                int isBigger = sh.HasChanged();
                if (isBigger > 0)
                {
                    if (sh.LastMaxLine == LoglineObject.ReadLine(sh.LastMaxLogPosition))
                    {
                        newPositions += ScanFile(null, 0, sh);
                    }
                    else
                    {
                        SetInconsistent();
                        return;
                    }
                }
                else if (isBigger < 0)
                {
                    SetInconsistent();
                    return;
                }
            }

            if (newPositions > 0 && NewPositions != null)
            {
                MainForm.FlashTrayIcon();
                NewPositions.Invoke(this, EventArgs.Empty);
            }
        }

        private void SetInconsistent()
        {
            if (IsInConsistent != null)
                IsInConsistent.Invoke(this, EventArgs.Empty);

            EnablePolling = false;
            isBusy = false;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Poll();
        }

        public List<LogPos> GetSearchResult(SearchEventArgs e)
        {
            return Search(e, null);
        }

        public List<LogPos> Search(SearchEventArgs e, BackgroundWorker worker)
        {
            if (searchResults.ContainsKey(e))
            {
                if (worker != null)
                    worker.ReportProgress(100);

                return searchResults[e];
            }

            isBusy = true;
            if (e.UseRegex)
            {
                if (e.IgnoreCase)
                    e.SearchRegex = new Regex(e.SearchText, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
                else
                    e.SearchRegex = new Regex(e.SearchText, RegexOptions.Singleline | RegexOptions.Compiled);
            }

            e.SearchDuration = (e.DurationTo > 0 && e.DurationFrom <= e.DurationTo) || (e.DurationFrom > 0 && e.DurationTo == 0);

            int cnt = 0;
            List<LogPos> resultsList = new List<LogPos>();
            foreach (LogPos logPos in PositionList)
            {
                bool addLine = MatchSearch(logPos, e);

                if (addLine)
                    resultsList.Add(logPos);

                cnt++;

                if (worker != null && worker.CancellationPending)
                    break;

                if (cnt % 10000 == 0 && worker != null)
                    worker.ReportProgress((int)(((double)cnt / (double)PositionList.Count) * 100));
            }

            if (worker != null)
                worker.ReportProgress(100);
            searchResults.Add(e, resultsList);
            isBusy = false;
            return resultsList;
        }

        private static bool MatchSearch(LogPos logPos, SearchEventArgs e)
        {
            bool addLine = true;
            String line = LoglineObject.ReadLine(logPos);
            if (e.SearchText.Length > 0)
            {
                if (e.UseRegex)
                {
                    if (!e.SearchRegex.IsMatch(line))
                        addLine &= false;
                }
                else
                {
                    if (e.IgnoreCase)
                    {
                        if (!line.ToLower().Contains(e.SearchText.ToLower()))
                            addLine &= false;
                    }
                    else
                    {
                        if (!line.Contains(e.SearchText))
                            addLine &= false;
                    }
                }
            }

            if (e.SearchDuration)
            {
                int posMs2 = line.LastIndexOf(" ms");
                if (posMs2 > 0)
                {
                    int posMs1 = line.LastIndexOf(" ", posMs2 - 1);
                    String ms = line.Substring(posMs1 + 1, posMs2 - posMs1 - 1);
                    int val;
                    if (Int32.TryParse(ms, out val))
                    {
                        if (!(val >= e.DurationFrom && (val <= e.DurationTo || e.DurationTo == 0)))
                            addLine &= false;
                    }
                    else
                    {
                        addLine &= false;
                    }
                }
                else
                {
                    addLine &= false;
                }
            }

            return addLine;
        }

        public void AddNewFile(String filename, BackgroundWorker worker, int progess)
        {
            StreamingHost sh = new StreamingHost(filename);
            streamingHosts.Add(sh);

            ScanFile(worker, progess, sh);
        }

        private long ScanFile(BackgroundWorker worker, int progess, StreamingHost sh)
        {
            int newPositions = PositionList.Count;
            isBusy = true;
            LogSource source = new LogSource() { Filename = sh.Filename };

            String line;
            int lastRelPos = 0;
            sh.Reader.SetPosition(sh.LastMaxPosition);
            long position = sh.Reader.GetPosition();
            LogPos lastMainLog = sh.LastMaxLogPosition;
            while ((line = sh.Reader.ReadLine()) != null)
            {
                if (worker != null && (worker.CancellationPending || isDisposing))
                    break;

                DateTime date;
                if (line.Length >= 23 && Char.IsDigit(line[0]) && DateTime.TryParseExact(line.Substring(0, 23), "yyyy-MM-dd HH:mm:ss,fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    lastMainLog = new LogPos() { TimeStamp = date, Pos = position, Order = PositionList.Count(), LoglineType = LoglineType.MAIN_LOG, StreamingHost = sh, LogSource = source };
                    AddToPositionList(lastMainLog);
                }
                else if (!String.IsNullOrWhiteSpace(line))
                {
                    if (line.StartsWith("#"))
                    {
                        String[] parts = line.Split('#');

                        if (parts.Length == 10)
                        {
                            int index = parts[6].IndexOf(" - ");
                            if (index > 0)
                            {
                                source = new LogSource() { Filename = parts[6].Substring(10, index - 10), Servername = parts[3].Trim() };
                            }
                        }
                    }
                    else if (CheckCatalinaLine(line, out date))
                    {
                        lastMainLog = new LogPos() { TimeStamp = date, Pos = position, Order = PositionList.Count(), LoglineType = LoglineType.CATALINA_LOG, StreamingHost = sh, LogSource = source };
                        AddToPositionList(lastMainLog);
                    }
                    else if (lastMainLog != null)
                    {
                        LogPos childLogPos = new LogPos()
                        {
                            TimeStamp = PositionList[PositionList.Count - 1].TimeStamp,
                            Pos = position,
                            Order = PositionList.Count(),
                            LoglineType = LoglineType.CHILD_LINE,
                            StreamingHost = sh,
                            LogSource = source,
                            Parent = lastMainLog
                        };

                        if (lastMainLog != null)
                        {
                            if (lastMainLog.Childs == null)
                                lastMainLog.Childs = new List<LogPos>();

                            lastMainLog.Childs.Add(childLogPos);
                        }

                        AddToPositionList(childLogPos);
                    }
                }

                position = sh.Reader.GetPosition();
                int relPos = (int)(100.0 * (double)position / (double)sh.Stream.Length);

                if (lastRelPos != relPos)
                {
                    lastRelPos = relPos;
                    if (worker != null)
                        worker.ReportProgress(progess + lastRelPos);
                }
            }
            sh.SetLastMaxPosition(lastMainLog);
            System.Diagnostics.Debug.WriteLine($"{sh.Filename}; last position  {sh.LastMaxPosition:n0}; new lines {PositionList.Count - newPositions:n0};");
            isBusy = false;

            return PositionList.Count - newPositions;
        }

        private void AddToPositionList(LogPos logPos)
        {
            PositionList.Add(logPos);

            //Suchergebnisse aktualisieren
            if (searchResults.Count > 0)
            {
                foreach (KeyValuePair<SearchEventArgs, List<LogPos>> kv in searchResults)
                {
                    if (MatchSearch(logPos, kv.Key))
                    {
                        kv.Value.Add(logPos);
                    }
                }
            }
        }

        private static bool CheckCatalinaLine(string line, out DateTime date)
        {
            String[] parts = line.Split(' ');
            if (parts.Length > 4)
            {
                String dateString = parts[0] + " " + parts[1] + " " + parts[2] + " " + parts[3] + " " + parts[4];
                if (DateTime.TryParse(dateString, out date))
                {
                    return true;
                }
            }
            date = DateTime.Now;
            return false;
        }

        public void Dispose()
        {
            pollTimer.Enabled = false;
            pollTimer.Elapsed -= Timer_Elapsed;
            pollTimer.Dispose();
            PositionList.Clear();
            streamingHosts.Clear();
            isDisposing = true;
            foreach (StreamingHost sh in streamingHosts)
                sh.Dispose();
        }
    }
}
