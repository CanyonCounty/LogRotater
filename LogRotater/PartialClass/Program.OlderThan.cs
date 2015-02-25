using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LogRotater
{
  internal partial class Program
  {
    private static DateTime LastWriteTime;
    private static DateTime CreationTime;
    private static DateTime LastAccessTime;

    private static bool OlderThan(string fileName, int days)
    {
      bool ret = false;
      FileInfo fi = new FileInfo(fileName);
      LastWriteTime = fi.LastWriteTime;
      CreationTime = fi.CreationTime;
      LastAccessTime = fi.LastAccessTime;

      if (fi.LastWriteTime < DateTime.Now.AddDays(days))
      {
        Console.WriteLine("File is older than: " + (days * -1) + " days");
        ret = true;
      }
      return ret;
    }
  }
}
