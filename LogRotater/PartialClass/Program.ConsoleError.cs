using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogRotater
{
  internal partial class Program
  {
    private static int ExitCode
    {
      set { Environment.ExitCode = value; }
    }

    private static void ConsoleError(string err, int exitCode)
    {
      ConsoleColor color = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Red;
      Console.Error.WriteLine("Error: " + err);
      Console.ForegroundColor = color;

      ExitCode = exitCode;
    }
  }
}
