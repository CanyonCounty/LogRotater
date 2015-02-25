using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CC.Common.Compression;

namespace LogRotater
{
  internal partial class Program
  {
    // For These Logs CompressionLevel of 7 is almost as small as 9 but takes 1.5 seconds less
    private static void TestCompression()
    {
      string dir = @"D:\Logs\kprod1\";
      CCZip.SearchPattern = "u_ex140101.log";

      // Test compression level/speed
      for (int i = 0; i < 10; i++)
      {
        CCZip.CompressionLevel = i;
        DateTime start = DateTime.Now;
        CCZip.CreateZip(String.Format("./{0}test.zip", i), dir);
        TimeSpan duration = DateTime.Now - start;
        Console.WriteLine(String.Format("Compression: {0}\tDuration: {1}", i, duration.TotalSeconds));
      }
      Console.WriteLine("Done");
      Console.ReadKey();
    }
  }
}
