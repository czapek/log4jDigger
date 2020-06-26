using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace log4jDigger
{
    public class SearchEventArgs : EventArgs, IEquatable<SearchEventArgs>
    {
        public String SearchText;
        public int DurationFrom;
        public int DurationTo;
        public bool UseRegex = false;
        public bool IgnoreCase = false;
        public bool OnlyLinesWithStackTrace = false;
        public bool SearchDuration;
        public Regex SearchRegex;
        public bool LevelTrace = true;
        public bool LevelDebug = true;
        public bool LevelInfo = true;
        public bool LevelWarn = true;
        public bool LevelError = true;
        public bool LevelFatal = true;

        public override string ToString()
        {
            List<String> info = new List<string>();

            if (SearchText.Length > 0)
                info.Add($"search for \"{SearchText}\"");

            if (DurationFrom > 0 || DurationTo > 0)
                info.Add($"Duration from {DurationFrom:n0} ms to  {DurationTo:n0} ms");

            if (UseRegex)
                info.Add("use Regex");

            if (IgnoreCase)
                info.Add("ignore Case");

            if(OnlyLinesWithStackTrace)
                info.Add("with StackTrace");

            if (!LevelTrace)
                info.Add("no TRACE");

            if (!LevelDebug)
                info.Add("no DEBUG");

            if (!LevelInfo)
                info.Add("no INFO");

            if (!LevelWarn)
                info.Add("no WARN");

            if (!LevelError)
                info.Add("no ERROR");

            if (!LevelFatal)
                info.Add("no FATAL");

            return String.Join(" and ", info);
        }

        bool IEquatable<SearchEventArgs>.Equals(SearchEventArgs other)
        {
           return this.ToString() == other.ToString();
        }
    }
}