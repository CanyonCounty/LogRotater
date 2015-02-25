using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogRotater
{
  internal partial class Program
  {
    private static void DisplayVariable(string variable, object value, object defaultValue)
    {
      ConsoleColor color = Console.ForegroundColor;
      Console.Write(variable + ": ");
      if (value.ToString() != defaultValue.ToString())
      {
        Console.ForegroundColor = ConsoleColor.Cyan;
        // Masks won't show if you skip out on string.format
        Console.Write(String.Format("{0}", value));
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(String.Format(" ({0})", defaultValue));
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        // Masks won't show if you skip out on string.format
        Console.WriteLine(String.Format("{0}", value)); 
      }
      Console.ForegroundColor = color;
    }

    private static void DisplayHeader()
    {
      Console.WriteLine("** Config **");
      DisplayVariable("configFile", PREF_FILE, PREF_FILE);
      DisplayVariable("logDirectory", LogDirectory, DEFAULT_LOG_DIRECTORY);
      DisplayVariable("compressionLevel", CompressionLevel, DEFAULT_COMPRESSION_LEVEL);
      DisplayVariable("olderThanDays", (OlderThanDays * -1), DEFAULT_OLDER_THAN_DAYS);
      DisplayVariable("fileMask", FileMask, DEFAULT_FILE_MASK);

      DisplayVariable("deleteFiles", DeleteFiles, DEFAULT_DELETE_FILES);
      DisplayVariable("deleteFileMask", DeleteFileMask, DEFAULT_DELETE_FILE_MASK);
      DisplayVariable("deleteOlderThanDays", (DeleteOlderThanDays * -1), DEFAULT_DELETE_OLDER_THAN_DAYS);
      DisplayVariable("fastForward", FastForward, DEFAULT_FAST_FORWARD);

      Console.WriteLine("");
    }
  }
}
