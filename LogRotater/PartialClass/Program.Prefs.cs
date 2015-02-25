using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CC.Common.JSON;

namespace LogRotater
{
  internal partial class Program
  {
    private static string PREF_FILE = "./LogRotater.json";
    private const string DEFAULT_LOG_DIRECTORY = @"C:\inetpub\logs\LogFiles\";
    private const int DEFAULT_COMPRESSION_LEVEL = 7;
    private const string DEFAULT_FILE_MASK = "*.log";
    private const int DEFAULT_OLDER_THAN_DAYS = 30;

    private const bool DEFAULT_DELETE_FILES = false;
    private const string DEFAULT_DELETE_FILE_MASK = "*.zip";
    private const int DEFAULT_DELETE_OLDER_THAN_DAYS = 90;
    private const bool DEFAULT_FAST_FORWARD = false;

    private static CCPreferences _prefs;
    private static bool? _directoryExists = null;
    private static bool parseError = false;

    private static CCPreferences prefs
    {
      get
      {
        if (_prefs == null) _prefs = new CCPreferences();
        return _prefs;
      }
    }

    private static string LogDirectory
    {
      get 
      {
        string sep = Path.DirectorySeparatorChar.ToString();
        string alt = Path.AltDirectorySeparatorChar.ToString();
        
        string ret = prefs.Get("logDirectory", DEFAULT_LOG_DIRECTORY);
        ret = ret.Trim().Replace(alt, sep);
        if (!ret.EndsWith(sep))
          ret += sep;

        return ret; 
      }
      set { prefs.Set("logDirectory", value); }
    }

    private static int CompressionLevel
    {
      get { return prefs.Get("compressionLevel", DEFAULT_COMPRESSION_LEVEL); }
      set { prefs.Set("compressionLevel", value); }
    }

    private static string FileMask
    {
      get { return prefs.Get("fileMask", DEFAULT_FILE_MASK); }
      set { prefs.Set("fileMask", value); }
    }

    private static int OlderThanDays
    {
      get { return -prefs.Get("olderThanDays", DEFAULT_OLDER_THAN_DAYS); }
      set { prefs.Set("olderThanDays", value); }
    }

    private static int DeleteOlderThanDays
    {
      get { return -prefs.Get("deleteOlderThanDays", DEFAULT_DELETE_OLDER_THAN_DAYS); }
      set { prefs.Set("deleteOlderThanDays", value); }
    }

    private static string DeleteFileMask
    {
      get { return prefs.Get("deleteFileMask", DEFAULT_DELETE_FILE_MASK); }
      set { prefs.Set("deleteFileMask", value); }
    }

    private static bool DeleteFiles
    {
      get { return prefs.Get("deleteFiles", DEFAULT_DELETE_FILES); }
      set { prefs.Set("deleteFiles", value); }
    }

    private static bool FastForward
    {
      get { return prefs.Get("fastForward", DEFAULT_FAST_FORWARD); }
      set { prefs.Set("fastForward", value); }
    }

    private static bool LogDirectoryExists
    {
      get
      {
        if (_directoryExists == null)
        {
          _directoryExists = Directory.Exists(LogDirectory);
        }
        return _directoryExists.Value;
      }
    }

    private static bool LoadPreferences()
    {
      bool ret = true;

      if (File.Exists(PREF_FILE))
      {
        ret = prefs.Load(PREF_FILE);
        if (!ret)
        {
          parseError = true;
          ConsoleError("There is an error in your configuration file - use jslint to verify", 500);
        }
      }
      else
      {
        ConsoleError("Could not find config file", 404);
        ret = false;
      }

      return ret;
    }

    private static void SavePreferences()
    {
      prefs.Save(PREF_FILE);
    }
  }
}
