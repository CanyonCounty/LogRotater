using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using CC.Common.Compression;
using CC.Common.JSON;

namespace LogRotater
{
  internal partial class Program
  {
    private static void ZipDirectory()
    {
      if (LogDirectoryExists)
      {
        string[] files = Directory.GetFiles(LogDirectory, FileMask, SearchOption.AllDirectories);
        Console.WriteLine("Number of Files: " + files.Count());
        foreach (string file in files)
        {
          Console.WriteLine("");
          Console.WriteLine("Checking: " + file);
          if (OlderThan(file, OlderThanDays))
          {
            Console.WriteLine("Compressing: " + file);
            string path = Path.GetDirectoryName(file) + Path.DirectorySeparatorChar;
            CCZip.SearchPattern = file.Replace(path, "");
            CCZip.CompressionLevel = CompressionLevel;
            string zipFile = Path.Combine(path, Path.GetFileNameWithoutExtension(file) + ".zip");
            CCZip.CreateZip(zipFile, path);
            //Console.WriteLine("Deleting: " + file);
            File.Delete(file);
            
            // Set all the file date flags
            File.SetLastWriteTime(zipFile, LastWriteTime);
            File.SetCreationTime(zipFile, CreationTime);
            File.SetLastAccessTime(zipFile, LastAccessTime);
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
