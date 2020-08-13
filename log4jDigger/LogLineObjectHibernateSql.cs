using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                       index + pos < streamingFactory.PositionList.Count)
                {
                    pos++;
                    hibernateSqlDic.Add(pos, loglineObjectSql);
                    if (index + pos < streamingFactory.PositionList.Count)
                    {
                        logPosSql = streamingFactory.PositionList[index + pos];
                        lineSql = LoglineObject.ReadLine(logPosSql);
                        loglineObjectSql = LoglineObject.CreateLoglineObject(lineSql, logPosSql);
                    }
                }
            }

            StringBuilder sqlFull = new StringBuilder();
            LoglineObject? sqlLine = hibernateSqlDic.Values.FirstOrDefault(x => x.Classname == "org.hibernate.SQL");
            if (sqlLine != null && !String.IsNullOrWhiteSpace(sqlLine.Value.Message))
            {
                var basicBinders = hibernateSqlDic.Values.Where(x => 
                (x.Classname == "org.hibernate.type.descriptor.sql.BasicBinder" || x.Classname == "org.hibernate.type.EnumType") && x.Message.ToLower().StartsWith("binding"));
                List<HqlParameter> parameters = basicBinders.Select(x => new HqlParameter(x)).Where(x => x.ParameterPos > 0).ToList();
                String[] parts = sqlLine.Value.Message.Split('?');
                for (int i = 0; i < parts.Length; i++)
                {
                    sqlFull.Append(parts[i].Replace(", ", ",\r\n\t").Replace(" set ", " set\r\n\t").Replace(" (", "\r\n\t("));
                    if (i < parts.Length - 1)
                    {
                        HqlParameter hlq = parameters.FirstOrDefault(x => x.ParameterPos == i + 1);
                        if (hlq == null) sqlFull.Append("?");
                        else sqlFull.Append(hlq.Sql);
                    }
                }             
            }

            if(sqlFull.Length > 0)
            {
                messageResult = sqlFull.ToString();
            }
            else
            {
                messageResult = String.Join(Environment.NewLine, hibernateSqlDic.OrderBy(x => x.Key).Select(x => x.Value.Message));
            }
            
            return messageResult.Replace(") values (", "\r\n) values (")
                .Replace(" where ", " \r\n    where ")
                .Replace(" from ", " \r\n    from ");
        }

        class HqlParameter
        {
            Regex expression = new Regex(@"\[[^\[\[]*\]", RegexOptions.Compiled);
            CultureInfo culture = new CultureInfo("en-US");
            public int ParameterPos;
            public String SqlType;
            public String ParameterValue;
            public String Sql;

            public HqlParameter(LoglineObject loglineObject)
            {
                MatchCollection mc = expression.Matches(loglineObject.Message);
                if (mc.Count >= 2)
                {
                    if (mc.Count == 3)
                    {
                        ParameterPos = Int32.Parse(mc[0].Value.Trim("[]".ToCharArray()));
                        SqlType = mc[1].Value.Trim("[]".ToCharArray());
                        ParameterValue = mc[2].Value.Trim("[]".ToCharArray());
                    }
                    else
                    {
                        ParameterValue = mc[0].Value.Trim("[]".ToCharArray());
                        ParameterPos = Int32.Parse(mc[1].Value.Trim("[]".ToCharArray()));
                        SqlType = "VARCHAR";
                    }

                    if (ParameterValue == "null")
                    {
                        Sql = "null";
                    }
                    else if (SqlType.Contains("CHAR"))
                    {
                        Sql = "'" + ParameterValue.Replace("'", "''") + "'";
                    }
                    else if (SqlType == "DATE" || SqlType == "TIMESTAMP")
                    {
                        DateTime date;
                        if (DateTime.TryParseExact(ParameterValue, "ddd MMM dd HH:mm:ss CET yyyy", culture, DateTimeStyles.None, out date))
                        {
                            if (SqlType == "DATE")
                                Sql = "'" + date.ToString("yyyy-MM-dd") + "'";
                            else
                                Sql = "'" + date.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                        }
                    }
                    else
                    {
                        Sql = ParameterValue;
                    }
                }
            }

            public override string ToString()
            {
                return $"{ParameterPos} {ParameterValue}";
            }
        }
    }

}
