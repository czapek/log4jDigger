using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace log4jDigger
{
    public struct LoglineObject
    {
        public LogPos LogPos;
        public String Timestamp;
        public String Level;
        public int? Duration;
        public String Classname;
        public String ClassnameShort;
        public String Message;
        public String Threadname;

        public Color GetLevelBackColor()
        {
            switch (Level)
            {
                case "TRACE":
                    return Color.WhiteSmoke;

                case "DEBUG":
                    return Color.LightGreen;

                case "INFO":
                    return Color.Linen;

                case "WARN":
                    return Color.Salmon;

                case "ERROR":
                    return Color.OrangeRed;

                case "FATAL":
                    return Color.Red;
            }

            return Color.White;
        }

        public Color GetLevelFrontColor()
        {
            switch (Level)
            {
                case "FATAL":
                    return Color.White;
            }

            return Color.Black;
        }

        public static LoglineObject CreateLoglineObject(string line, LogPos logPos)
        {
            LoglineObject loglineObject = new LoglineObject() { LogPos = logPos };
            if (logPos.LoglineType == LoglineType.MAIN_LOG)
            {
                int headerPos = line.IndexOf(" - ");
                String header = headerPos > 0 ? line.Substring(0, line.IndexOf(" - ")) : line;
                int posThread = header.IndexOf(" [");
                int posThread2 = header.LastIndexOf("] ");
                loglineObject.Threadname = posThread > 0 && posThread2 > 0 ? header.Substring(posThread + 1, posThread2 - posThread) : String.Empty;
                int posClassname = header.LastIndexOf(' ');
                loglineObject.Classname = posClassname > 0 ? header.Substring(header.LastIndexOf(' ') + 1) : String.Empty;
                loglineObject.ClassnameShort = posClassname > 0 ? loglineObject.Classname.Substring(loglineObject.Classname.LastIndexOf('.') + 1) : String.Empty;
                loglineObject.Message = line.Length > header.Length + 3 ? line.Substring(header.Length + 3) : String.Empty;
                loglineObject.Level = line.Length > 29 ? line.Substring(24, 5).Trim() : String.Empty;
                loglineObject.Timestamp = line.Length > 23 ? line.Substring(0, 23) : String.Empty;

                int posMs2 = loglineObject.Message.LastIndexOf(" ms");
                if (posMs2 > 0)
                {
                    int posMs1 = loglineObject.Message.LastIndexOf(" ", posMs2 - 1);
                    String ms = loglineObject.Message.Substring(posMs1 + 1, posMs2 - posMs1 - 1);
                    int val;
                    if (Int32.TryParse(ms, out val))
                    {
                        loglineObject.Duration = val;
                    }
                }
            }
            else
            {
                if (logPos.LoglineType == LoglineType.CATALINA_LOG)
                {
                    int index = Math.Max(line.IndexOf("AM"), line.IndexOf("PM"));

                    loglineObject.Timestamp = logPos.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss,fff");
                    loglineObject.Classname = "catalina";
                    loglineObject.Message = index > 0 ? line.Substring(index + 3) : line;
                }
                else
                {
                    loglineObject.Message = line;
                }
            }

            return loglineObject;
        }

        public static long InfoTextFromLine(StreamingFactory streamingFactory, int index, RichTextBox infoTextBox)
        {
            LogPos logPos = streamingFactory.PositionList[index];
            logPos = logPos.Parent != null ? logPos.Parent : logPos;
            StringBuilder sb = new StringBuilder();
            String line = ReadLine(logPos);
            LoglineObject loglineObject = CreateLoglineObject(line, logPos);
            if (logPos.LoglineType == LoglineType.MAIN_LOG)
            {
                String messageResult = loglineObject.Message;
                if (loglineObject.Classname.EndsWith("HttpAuthInterceptor"))
                {
                    int index1 = loglineObject.Message.LastIndexOf("):");
                    if (index1 >= -1)
                    {
                        messageResult = loglineObject.Message.Substring(0, index1 + 1) + Environment.NewLine
                            + loglineObject.Message.Substring(index1 + 3);
                    }

                    int index2 = messageResult.IndexOf(" - ");
                    if (index2 >= -1)
                    {
                        messageResult = messageResult.Substring(0, index2) + Environment.NewLine
                            + messageResult.Substring(index2 + 3);
                    }
                }
                else if (loglineObject.Classname == "org.hibernate.type.EnumType"
                   || loglineObject.Classname == "org.hibernate.type.descriptor.sql.BasicBinder"
                   || loglineObject.Classname == "org.hibernate.SQL")
                {
                    messageResult = LogLineObjectHibernateSql.Info(streamingFactory, index, loglineObject);
                }
                else if (loglineObject.Classname == "org.jdbcdslog.StatementLogger"
                    || loglineObject.Classname == "org.jdbcdslog.SlowQueryLogger")
                {
                    messageResult = LogLineObjectStatementLoggerSql.Info(streamingFactory, index, loglineObject);
                }

                sb.AppendLine($"{loglineObject.Timestamp} {loglineObject.Level}\r\n{loglineObject.Threadname}\r\n{loglineObject.Classname}\r\n{messageResult}");
            }
            else if (logPos.LoglineType == LoglineType.CATALINA_LOG)
            {
                sb.AppendLine($"{loglineObject.Timestamp} {loglineObject.Classname}\r\n{loglineObject.Message}");
            }
            else
            {
                sb.AppendLine(line);
            }

            if (logPos.Childs != null)
            {
                foreach (LogPos lp in logPos.Childs)
                {
                    sb.AppendLine("    " + LoglineObject.ReadLine(lp));
                }
            }

            infoTextBox.Text = sb.ToString();
            int lineCounter = 0;
            foreach (string infoLine in infoTextBox.Lines)
            {
                foreach (String pattern in LogUtils.LineMarkerFont)
                {
                    if (infoLine.Contains(pattern))
                    {
                        infoTextBox.Select(infoTextBox.GetFirstCharIndexFromLine(lineCounter), infoLine.Length);
                        infoTextBox.SelectionBackColor = Color.LightGreen;
                        infoTextBox.SelectionFont = new Font(infoTextBox.Font, FontStyle.Bold);
                    }
                }
                lineCounter++;
            }
            infoTextBox.Select(0, 0);

            return index - (streamingFactory.PositionList[index].Order - logPos.Order);
        }

        public static string ReadLine(LogPos logPos)
        {
            if (logPos == null)
                return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss,fff} FATAL [log4jDigger] log4jDigger - Unvalid Request for Loline";

            if (logPos.StreamingHost.IsDisposed)
                return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss,fff} FATAL [log4jDigger] log4jDigger - Stream is disposed";

            try
            {
                logPos.StreamingHost.Reader.SetPosition(logPos.Pos);
                String line = logPos.StreamingHost.Reader.ReadLine();

                if (line == null)
                    return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss,fff} FATAL [log4jDigger] log4jDigger - Inconsistent Logdata, please refresh (F5)";
                else
                    return line;
            }
            catch
            {
                return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss,fff} FATAL [log4jDigger] log4jDigger - Error resolving Logline from Stream";
            }
        }

        public override string ToString()
        {
            return this.Timestamp + " " + this.Classname;
        }
    }
}