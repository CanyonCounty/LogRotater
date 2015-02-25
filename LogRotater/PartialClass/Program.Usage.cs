using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogRotater
{
  internal partial class Program
  {
    private static void Usage()
    {
      ConsoleColor color = Console.ForegroundColor;
      Console.WriteLine("LogRotator.exe Usage");
      Console.WriteLine("By default LogRotator will use a ./LogRotator.json file for configuration");

      DisplayArg("--help", "Shows this usage");
      DisplayArg("/?", "Alias for this usage");
      DisplayArg("--config <filename>", "Use this to pass in a config file - filename required");
      DisplayArg("--default-config", "Creates a configuration file with all defaults set");
    }

    private static void DisplayArg(string arg, string help)
    {
      ConsoleColor color = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Green;
      Console.Write("  " + arg);
      Console.ForegroundColor = color;
      Console.WriteLine(" : " + help);
    }
  }
}
