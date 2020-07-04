using System;
using System.Collections.Generic;

namespace log4jDigger
{
    public class LogPos : IEquatable<LogPos>
    {
        public long Pos;
        public long Order;
        public DateTime TimeStamp;
        public LoglineType LoglineType;
        public StreamingHost StreamingHost;
        public LogSource LogSource;
        public List<LogPos> Childs;
        public LogPos Parent;

        public bool Equals(LogPos other)
        {
            return this.TimeStamp == other.TimeStamp && this.Order == other.Order;
        }

        public override string ToString()
        {
            return $"{TimeStamp:yyyy-MM-dd HH:mm:ss,fff} {Pos:n0} ({Order:n0}) {LogSource}";
        }
    }

    public enum LoglineType
    {
        MAIN_LOG,
        CHILD_LINE,
        CATALINA_LOG
    }
}