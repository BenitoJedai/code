using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace ScriptCoreLib.Desktop
{
    /// <summary>
    /// If CLR is loaded into JVM process, it will look for references where java.exe was launched.
    /// </summary>
    public static class AppDomainAssemblyResolve
    {
        static bool InitializeDone;

        public static void Initialize()
        {
            if (InitializeDone)
                return;

            InitializeDone = true;

            AppDomain.CurrentDomain.AssemblyResolve +=
                (_s, _a) =>
                {
                    var Directory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
                    var AssemblyName = new AssemblyName(_a.Name).Name;

                    var dll = Path.Combine(Directory, AssemblyName + ".dll");

                    //Console.WriteLine("looking for " + dll);

                    if (File.Exists(dll))
                        return Assembly.LoadFrom(dll);

                    var exe = Path.Combine(Directory, AssemblyName + ".exe");

                    //Console.WriteLine("looking for " + exe);

                    if (File.Exists(exe))
                        return Assembly.LoadFrom(exe);

                    //Console.WriteLine("missing!");

                    return null;
                };
        }
    }
}
