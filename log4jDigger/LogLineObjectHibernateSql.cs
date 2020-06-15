using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4jDigger
{
    public static class LogLineObjectHibernateSql
    {
        public static string Info(StreamingFactory streamingFactory, int index, LoglineObject loglineObject)
        {
            string messageResult;
            int pos = 0;
            Dictionary<int, LoglineObject> hibernateSqlDic = new Dictionary<int, LoglineObject>();
            hibernateSqlDic.Add(pos, loglineObject);
            LogPos logPosSql = null;
            String lineSql = null;
            String threadName = loglineObject.Threadname;
            LoglineObject loglineObjectSql;
            if (loglineObject.Classname != "org.hibernate.SQL" && index > 0)
            {
                pos--;
                logPosSql = streamingFactory.PositionList[index + pos];
                lineSql = LoglineObject.ReadLine(logPosSql);
                loglineObjectSql = LoglineObject.CreateLoglineObject(lineSql, logPosSql);

                while (loglineObjectSql.Threadname == threadName &&
                    loglineObjectSql.Classname != "org.hibernate.SQL" &&
                    (loglineObject.Classname == "org.hibernate.type.descriptor.sql.BasicBinder" ||
                    (loglineObject.Classname == "org.hibernate.type.EnumType")) && index + pos > 0)
                {
                    pos--;
                    hibernateSqlDic.Add(pos, loglineObjectSql);
                    logPosSql = streamingFactory.PositionList[index + pos];
                    lineSql = LoglineObject.ReadLine(logPosSql);
                    loglineObjectSql = LoglineObject.CreateLoglineObject(lineSql, logPosSql);
                }

                if (loglineObjectSql.Classname == "org.hibernate.SQL")
                {
                    pos--;
                    hibernateSqlDic.Add(pos, loglineObjectSql);
                }
            }

            pos = 1;
            if (index + pos < streamingFactory.PositionList.Count)
            {
                logPosSql = streamingFactory.PositionList[index + pos];
                lineSql = LoglineObject.ReadLine(logPosSql);
                loglineObjectSql = LoglineObject.CreateLoglineObject(lineSql, logPosSql);

                while ((loglineObjectSql.Classname == "org.hibernate.type.descriptor.sql.BasicBinder" ||
                       loglineObjectSql.Classname == "org.hibernate.type.EnumType") &&
                       loglineObjectSql.Threadname == threadName &&
                       index + pos < streamingFactory.PositionList.Count - 1)
                {
                    pos++;
                    hibernateSqlDic.Add(pos, loglineObjectSql);
                    logPosSql = streamingFactory.PositionList[index + pos];
                    lineSql = LoglineObject.ReadLine(logPosSql);
                    loglineObjectSql = LoglineObject.CreateLoglineObject(lineSql, logPosSql);
                }
            }

            messageResult = String.Join(Environment.NewLine, hibernateSqlDic.OrderBy(x => x.Key).Select(x => x.Value.Message));
            return messageResult.Replace(") values (", ") \r\n    values (")
                .Replace(" where ", " \r\n    where ")
                .Replace(" from ", " \r\n    from ");
        }
    }
}
