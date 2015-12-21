using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LogRotater
{
  internal partial class Program
  {
    private static void CalculateAge()
    {
      if (LogDirectoryExists)
      {
        Dictionary<int, int> dict = new Dictionary<int, int>();
        string[] files = Directory.GetFiles(LogDirectory, "*.*", SearchOption.AllDirectories);
        foreach (string file in files)
        {
          FileInfo info = new FileInfo(file);
          //ret += (ulong)info.Length;
          int days = (int)(DateTime.Now - info.LastWriteTime).TotalDays;
          if (dict.ContainsKey(days))
          {
            dict[days] += 1;
          }
          else
          {
            dict[days] = 1;
          }
        }

        Console.WriteLine("LogRotator.exe Calculate Age:");
        Console.Write("The for the ");
        ConsoleColor color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(LogDirectory);
        Console.ForegroundColor = color;
        Console.WriteLine(" directory\n");
        DisplayAge("Age (in days)", "count");
        foreach (KeyValuePair<int, int> item in dict.OrderByDescending(x => x.Key))
        {
          DisplayAge(item.Key, item.Value);
        }
        Console.WriteLine("");
      }
    }

    private static void DisplayAge(string age, string count)
    {
      ConsoleColor color = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Green;
      Console.Write(" " + age);
      Console.ForegroundColor = color;
      Console.WriteLine(":\t" + count);
    }

    private static void DisplayAge(int age, int count)
    {
      DisplayAge(age.ToString(), count.ToString());
    }
  }
}
