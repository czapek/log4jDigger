﻿using System;
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
        private long lastFileLength;
        public long LastMaxPosition { get; private set; }
        public LogPos LastMaxLogPosition { get; private set; }

        public StreamingHost(String filename)
        {
            Filename = filename;
            Stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            Reader = new StreamReader(Stream, Encoding.Default);
        }

        public void SetLastMaxPosition(LogPos logPos)
        {
            LastMaxLogPosition = logPos;
            LastMaxPosition = Reader.GetPosition();
            lastFileLength = new FileInfo(Filename).Length; 
        }

        public int HasChanged()
        { 
            return new FileInfo(Filename).Length.CompareTo(lastFileLength);
        }

        public void Dispose()
        {
            Stream.Dispose();
            Reader.Dispose();
        }    
    }
}