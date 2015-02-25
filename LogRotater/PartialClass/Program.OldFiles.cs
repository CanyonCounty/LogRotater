using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LogRotater
{
  internal partial class Program
  {
    private static void HandleOldFiles(string fileMask)
    {
      if (DeleteFiles == true)
      {
        if (LogDirectoryExists)
        {
          string[] files = Directory.GetFiles(LogDirectory, fileMask, SearchOption.AllDirectories);
          Console.WriteLine("Number of Files: " + files.Count());
          foreach (string file in files)
          {
            Console.WriteLine("");
            Console.WriteLine("Checking: " + file);
            if (OlderThan(file, DeleteOlderThanDays))
            {
              Console.WriteLine("Deleting: " + file);
              File.Delete(file);
            }
          }
        }
        else
        {
          ConsoleError("Could not find Log Directory", 404);
        }
      }
    }
  }
}
