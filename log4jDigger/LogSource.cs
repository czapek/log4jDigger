using System;
using System.IO;

namespace log4jDigger
{
    public class LogSource
    {
        private String fileMarker;
        public String Filename;
        public String Servername;

        public override string ToString()
        {
            if (fileMarker == null && Filename != null)
            {
                if (Path.GetDirectoryName(Filename).Length > 0)
                {
                    fileMarker = $"{Path.GetFileName(Filename)} ({Path.GetFileName(Path.GetDirectoryName(Filename))})";
                }
                else
                {
                    fileMarker = Filename;
                }
            }

            return Servername == null ? (fileMarker == null ? "No Source" : fileMarker) : (Servername + (fileMarker != null ? $" ({fileMarker})" : String.Empty));
        }
    }
}