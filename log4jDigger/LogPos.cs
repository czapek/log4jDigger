using System;
using System.Collections.Generic;

namespace log4jDigger
{
    public class LogPos
    {
        public long Pos;
        public long Order;
        public DateTime TimeStamp;
        public LoglineType LoglineType;
        public StreamingHost StreamingHost;
        public LogSource LogSource;
        public List<LogPos> Childs;
        public LogPos Parent;
    }

    public enum LoglineType
    {
        MAIN_LOG,
        CHILD_LINE,
        CATALINA_LOG
    }
}