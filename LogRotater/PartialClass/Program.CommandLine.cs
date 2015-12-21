using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogRotater
{
  internal partial class Program
  {
    private static void DefaultConfig()
    {
      // If I use the getter than the prefs.get is called
      // which will set the defaults and since the values
      // are the same as the defaults, it won't save anything
      // this is by design of CCPreferences
      prefs.Set("logDirectory", DEFAULT_LOG_DIRECTORY);
      prefs.Set("compressionLevel", DEFAULT_COMPRESSION_LEVEL);
      prefs.Set("fileMask", DEFAULT_FILE_MASK);
      prefs.Set("olderThanDays", DEFAULT_OLDER_THAN_DAYS);
      prefs.Set("deleteFiles", DEFAULT_DELETE_FILES);
      prefs.Set("deleteFileMask", DEFAULT_DELETE_FILE_MASK);
      prefs.Set("deleteOlderThanDays", DEFAULT_DELETE_OLDER_THAN_DAYS);
      prefs.Set("fastForward", DEFAULT_FAST_FORWARD);

      string file = PREF_FILE.Replace(".json", ".default.json");
      prefs.Save(file);
      Console.WriteLine("Default file written as " + file);
      Console.WriteLine("Make your changes and save it as " + PREF_FILE);
    }

    private static Action HandleCommandLine(string[] args)
    {
      if (args.Length > 0)
      {
        switch (args[0])
        {
          case "--help": return Usage;
          case "/?": return Usage;
          case "--default-config": return DefaultConfig;
          case "--calc-age": return CalculateAge;
          case "--config":
            {
              if (args.Length > 1)
              {
                PREF_FILE = args[1];
              }
              else
              {
                ConsoleError("I got a --config switch but not a config file", 42);
              }
              return Process;
            }
        }
      }

      return Process;
    }
  }
}
