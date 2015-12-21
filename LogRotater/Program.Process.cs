using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogRotater
{
  internal partial class Program
  {
    public static void Process()
    {
      DisplayHeader();

      ulong size = DirSize;
      Console.WriteLine("Dir Size: " + String.Format("{0:#,0} MB", ((size / 1024 / 1024))));

      if (FastForward)
      {
        Console.WriteLine("");
        Console.WriteLine("*** Fast-Forward ***");
        HandleOldFiles(FileMask);
      }

      Console.WriteLine("");
      Console.WriteLine("*** Zip Logs ***");
      ZipDirectory();

      Console.WriteLine("");
      Console.WriteLine("*** Delete Old Zips ***");
      HandleOldFiles(DeleteFileMask);

      Console.WriteLine("");
      ulong nsize = DirSize;
      Console.WriteLine("Dir Size: " + String.Format("{0:#,0} MB", ((nsize / 1024 / 1024))));
      Console.WriteLine("Diff: " + String.Format("{0:#,0} MB", (((size - nsize) / 1024 / 1024))));
    }
  }
}
