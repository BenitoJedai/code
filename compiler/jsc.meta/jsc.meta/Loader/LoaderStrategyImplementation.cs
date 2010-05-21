using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace jsc.meta.Loader
{
    public class LoaderStrategyImplementation
    {
        public static void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        public static readonly List<DirectoryInfo> Hints = new List<DirectoryInfo>();

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // already loaded? we might be loading once too many times :)
            var a = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(
                k => k.GetName().FullName == new AssemblyName(args.Name).FullName
            );

            if (a != null)
                return a;

            var bin = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;

            var r = new AssemblyName(args.Name);

            foreach (var item in Hints)
            {

                var lib_dll = Path.Combine(item.FullName, r.Name + ".dll");
                if (File.Exists(lib_dll))
                {
                    return Assembly.LoadFile(lib_dll);
                }

                var lib_exe = Path.Combine(item.FullName, r.Name + ".exe");
                if (File.Exists(lib_exe))
                {
                    return Assembly.LoadFile(lib_exe);
                }
            }

            var lib = new DirectoryInfo(Path.Combine(bin.FullName, @"..\lib"));

            if (!lib.Exists)
                lib = new DirectoryInfo(@"c:\util\jsc\lib");


            if (lib.Exists)
            {
                // why return null?

                if (File.Exists(Path.Combine(bin.FullName, r.Name + ".dll")))
                    return null;

                if (File.Exists(Path.Combine(bin.FullName, r.Name + ".exe")))
                    return null;

                var lib_dll = Path.Combine(lib.FullName, r.Name + ".dll");
                if (File.Exists(lib_dll))
                {
                    return Assembly.LoadFile(lib_dll);
                }

                var lib_exe = Path.Combine(lib.FullName, r.Name + ".exe");
                if (File.Exists(lib_exe))
                {
                    return Assembly.LoadFile(lib_exe);
                }
            }



            return null;
        }

    }
}
