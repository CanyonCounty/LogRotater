using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LogRotater
{
  internal partial class Program
  {
    public static ulong DirSize
    {
      get { return GetDirSize(); }
    }

    public static ulong GetDirSize()
    {
      ulong ret = 0;

      if (LogDirectoryExists)
      {
        string[] files = Directory.GetFiles(LogDirectory, "*.*", SearchOption.AllDirectories);
        foreach (string file in files)
        {
          FileInfo info = new FileInfo(file);
          ret += (ulong)info.Length;
        }
      }
      return ret;
    }
  }
}
