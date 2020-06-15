using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace log4jDigger
{
    public static class LogLineObjectStatementLoggerSql
    {
        private static Regex regexParam = new Regex(Regex.Escape("?"));
        private static Regex regexDate = new Regex(@"\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d\.\d");
        public static string Info(StreamingFactory streamingFactory, int index, LoglineObject loglineObject)
        {
            String message = loglineObject.Message;
            String SqlClass;
            String Statement;
            String Entity;
            string Exception;
            int? MilliSeconds;
            List<String> Parameter = new List<string>();

            int index1 = message.IndexOf(" ");
            int index2 = message.IndexOf("parameters: {");

            if (index2 == -1)
            {
                index2 = message.IndexOf(" ms", index1);
            }

            index2 = index2 >= 0 ? index2 : 0;

            int index3 = message.IndexOf(" ms", index2);

            if (index3 < 0)
            {
                index3 = message.IndexOf(" throws exception: ", index2);
                if (index3 > 0)
                {
                    Exception = message.Substring(index3 + 1, message.Length - index3 - 1);
                    if (index2 > 0)
                        Parameter = message.Substring(index2 + 13, index3 - index2 - 14).Split(',').Select(x => x.Trim()).ToList();
                    else
                        index2 = index3;
                }
                else
                {
                    Exception = "Maximale Länge der Logzeile überschritten";
                }
            }
            else
            {
                string[] parts = message.Substring(index3 - 10, 10).Split(' ');
                string ms = parts[parts.Length - 1];
                MilliSeconds = Int32.Parse(ms);

                if (index2 != index3)
                {
                    string param = message.Substring(index2 + 13, index3 - index2 - 15 - ms.Length);
                    if (param.Length != 0)
                        Parameter = param.Split(',').Select(x => x.Trim()).ToList();
                    else
                        Parameter = new List<string>();
                }
                else
                {
                    Parameter = new List<string>();
                    index2 -= ms.Length;
                }
            }

            SqlClass = message.Substring(0, index1);

            if (index2 == 0)
            {
                Statement = message.Substring(index1 + 1);
            }
            else
            {
                Statement = message.Substring(index1 + 1, index2 - index1 - 2);

                int indexEntity = Statement.ToLower().IndexOf(" from ");
                int indexEntityStop = Statement.IndexOf(" ", indexEntity + 6);
                indexEntityStop = indexEntityStop < 0 ? Statement.Length : indexEntityStop;
                if (indexEntity > -1 && indexEntity < indexEntityStop)
                {
                    Entity = "[select] " + Statement.Substring(indexEntity + 6, indexEntityStop - indexEntity - 6).ToLower();

                    while (Statement.ToLower().IndexOf(" from ", indexEntityStop) >= 0)
                    {
                        indexEntity = Statement.ToLower().IndexOf(" from ", indexEntityStop);
                        indexEntityStop = Statement.IndexOf(" ", indexEntity + 6);

                        Entity += ", " + Statement.Substring(indexEntity + 6, indexEntityStop - indexEntity - 6).ToLower();
                    }
                }
                else
                {
                    indexEntity = Statement.ToLower().IndexOf("insert into ");
                    indexEntityStop = Statement.IndexOf(" ", indexEntity + 13);
                    if (indexEntity > -1 && indexEntity < indexEntityStop)
                    {
                        Entity = "[insert] " + Statement.Substring(indexEntity + 12, indexEntityStop - indexEntity - 12).ToLower();
                    }
                    else
                    {
                        indexEntity = Statement.ToLower().IndexOf("update ");
                        indexEntityStop = Statement.IndexOf(" ", indexEntity + 7);
                        if (indexEntity > -1 && indexEntity < indexEntityStop)
                        {
                            Entity = "[update] " + Statement.Substring(indexEntity + 7, indexEntityStop - indexEntity - 7).ToLower();
                        }
                        else
                        {
                            Entity = "unknown entity";
                        }
                    }
                }
            }

            int statementCnt = Statement.ToCharArray().Where(x => x == '?').Count();
            string paramStatement = Statement;
            for (int i = 0; i < statementCnt; i++)
            {
                int j = i + (Parameter.Count - statementCnt);
                if (regexDate.IsMatch(Parameter[j]))
                {
                    paramStatement = regexParam.Replace(paramStatement, "'" + Parameter[j] + "'", 1);
                }
                else
                {
                    paramStatement = regexParam.Replace(paramStatement, Parameter[j], 1);
                }
            }
            return paramStatement.Replace(") values (", ") \r\n    values (")
                .Replace(" where ", " \r\n    where ")
                .Replace(" from ", " \r\n    from ");
        }

    }
}
