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
      var action = HandleCommandLine(args);
      if (action != DefaultConfig)
      {
        if (LoadPreferences())
        {
          action();
        }
        else
        {
          //DefaultConfig();
          if (!parseError)
            Usage();
        }
      }
      else
      {
        action();
      }

      if (action == Process)
      {
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
