using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace log4jDigger
{
    public class StreamingHost : IDisposable
    {
        public FileStream Stream;
        public StreamReader Reader;
        public String Filename;
        private DateTime lastWriteTime;
        public long LastMaxPosition { get; private set; }

        public StreamingHost(String filename)
        {
            Filename = filename;
            Stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            Reader = new StreamReader(Stream, Encoding.Default);
        }

        public void SetLastMaxPosition()
        {
            LastMaxPosition = Reader.GetPosition();
            lastWriteTime = File.GetLastWriteTime(Filename);
        }

        public bool HasChanged()
        {
            return lastWriteTime != File.GetLastWriteTime(Filename);
        }

        public void Dispose()
        {
            Stream.Dispose();
            Reader.Dispose();
        }    
    }
}