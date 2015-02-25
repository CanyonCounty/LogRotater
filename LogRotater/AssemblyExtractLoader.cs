using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace LogRotater
{
  public class AssemblyExtractLoader
  {
    private static Dictionary<string, Assembly> libs = new Dictionary<string, Assembly>();
    // set this to the directory name for you assemblies (include the dots)
    // if you don't have one (just in the main bundle) set this to blank
    private const string LIBDIR = ".Libs.";

    static void Main(string[] args)
    {
      AppDomain.CurrentDomain.AssemblyResolve += FindAssembly;
      Program.Go(args);
    }

    static Assembly FindAssembly(object sender, ResolveEventArgs args)
    {
      // Find the Requested name
      string shortName = new AssemblyName(args.Name).Name;
      string fullName = shortName;
      if (libs.ContainsKey(shortName)) return libs[shortName];

      // Lookup requested name in the list of embedded resource names
      string[] realNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
      foreach (string name in realNames)
      {
        if (name.Contains(shortName))
        {
          fullName = name;
          break;
        }
      }
      
      // Load it up and remember it for next time
      using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullName))
      {
        byte[] data = new BinaryReader(s).ReadBytes((int)s.Length);
        Assembly a = Assembly.Load(data);
        libs[shortName] = a;
        return a;
      }
    }
  }
}
