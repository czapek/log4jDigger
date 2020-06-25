using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace log4jDigger
{
    public class LogUtils
    {
        public static List<String> LineMarkerFont = new List<string>() { "de.meona" };
        private static List<String> logDirs = new List<string>() { @"var\log\meona", @"meona\log", @"meona\log\updatelog", @"\Users\sebas\Desktop\logs" };
        public static List<FileInfo> FindLatesLogfiles()
        {
            List<FileInfo> logFiles = new List<FileInfo>();
            foreach (DriveInfo d in DriveInfo.GetDrives())
                if (d.DriveType == DriveType.Fixed)
                    foreach (String logDir in logDirs)
                        if (Directory.Exists(Path.Combine(d.Name, logDir)))
                            foreach (String file in Directory.GetFiles(Path.Combine(d.Name, logDir), "*.log"))
                                logFiles.Add(new FileInfo(file));


            return logFiles.Where(f => f.Exists && f.Length > 0).OrderByDescending(f => f.LastWriteTime).ToList();
        }

        public static List<FileInfo> FindRolloverLogfiles(DateTime date)
        {
            List<FileInfo> logFiles = new List<FileInfo>();

            foreach (DriveInfo d in DriveInfo.GetDrives())
                if (d.DriveType == DriveType.Fixed)
                    foreach (String logDir in logDirs)
                    {
                        String dir = Path.Combine(d.Name, logDir);       
                        if (Directory.Exists(dir))
                        {
                            foreach (String subDir in Directory.GetDirectories(dir))
                            {
                                foreach (String file in Directory.GetFiles(subDir, $"*.log.{date:yyyy-MM-dd_HH}"))
                                {
                                    logFiles.Add(new FileInfo(file));
                                }
                            }

                            foreach (String file in Directory.GetFiles(dir, $"*.log.{date:yyyy-MM-dd_HH}"))
                            {
                                logFiles.Add(new FileInfo(file));
                            }
                        }
                    }

            return logFiles.Where(f => f.Exists && f.Length > 0).OrderByDescending(f => f.LastWriteTime).ToList();
        }

        public static String FindLatestLogDir()
        {
            return FindLatesLogfiles().FirstOrDefault()?.DirectoryName;
        }
    }
}