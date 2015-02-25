using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LogRotater
{
  internal partial class Program
  {
    public static void Go(string[] args)
    {
      //TestCompression();

      ExitCode = 0;
      bool done;
      HandleCommandLine(args, out done);
      if (!done)
      {
        if (LoadPreferences())
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
        else
        {
          //DefaultConfig();
          if (!parseError)
            Usage();
        }
        Console.WriteLine("Done");
      }

      // This code will only run from Visual Studio!
      if (System.Diagnostics.Debugger.IsAttached)
      {
        //SavePreferences();
        Console.ReadKey();
      }
    }
  }
}
