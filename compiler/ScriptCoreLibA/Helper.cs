using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Linq;

namespace ScriptCoreLib
{
    

    /// <summary>
    /// this class is shared among scriptcorelib assemblies 
    /// and provides pre runtime information, to build static
    /// files
    /// </summary>
    public static class SharedHelper
    {

        public static void DefineSpawnPoint(ITextWriter w, string alias, string data)
        {
            w.WriteLine("<input type='hidden' value='" + Convert.ToBase64String(Encoding.ASCII.GetBytes(data)) + "' class='" + alias + "' />");
        }

        public static void DefineScript(ITextWriter w, string[] Modules)
        {
            foreach (var v in Modules)
                DefineScript(w, v);
        }

        public static void PHPInclude(ITextWriter e, string src)
        {
            if (!src.EndsWith(".php"))
                src += ".php";

            e.WriteLine("require_once '" + src + "';");
        }

        public static void DefineScript(ITextWriter w, string src)
        {
            if (!src.EndsWith(".js"))
                src += ".js";

            w.WriteLine("<script type='text/javascript' src='" + src + "'></script>");
        }

        static IEnumerable<Assembly> LoadReferencedAssemblies(Assembly a)
        {
            foreach (AssemblyName z in a.GetReferencedAssemblies())
            {
                var x = AppDomain.CurrentDomain.Load(z);

                if (ScriptAttribute.Of(x) == null)
                    continue;

                yield return x;

                foreach (Assembly c in LoadReferencedAssemblies(x))
                    yield return c;
            }
        }

        class DependencyComparer : IComparer<Assembly>
        {
            public int Compare(Assembly x, Assembly y)
            {
                if (x.GetReferencedAssemblies().Select(i => i.Name).Contains(y.GetName().Name))
                {
                    System.Diagnostics.Debug.WriteLine(x.GetName().Name + " depends on " + y.GetName().Name);
                    return 1;
                }

                if (y.GetReferencedAssemblies().Select(i => i.Name).Contains(x.GetName().Name)) 
                {
                    System.Diagnostics.Debug.WriteLine(y.GetName().Name + " depends on " +x.GetName().Name);
                    return -1;
                }

                var sx = ScriptAttribute.OfProvider(x);
                var sy = ScriptAttribute.OfProvider(y);

                if (sx != null && sx.IsCoreLib && sy != null && !sy.IsCoreLib)
                    return -1;

                if (sx != null && !sx.IsCoreLib && sy != null && sy.IsCoreLib)
                    return 1;

                return 0;
            }
        }

        public static Assembly[] LoadReferencedAssemblies(Assembly a, bool includethis)
        {
            var u = LoadReferencedAssemblies(a);

            


            if (includethis)
                u = u.Concat( new [] { a } );

            
            
            var x = u.GroupBy(i => i.GetName().FullName).Distinct().Select(i => i.First()).ToArray();

            Array.Sort(x, new DependencyComparer());

            return x;
        }

        /// <summary>
        /// returns the names of the modules needed to run a assembly including a
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string[] ModulesOf(Assembly a)
        {
            return (from e in LoadReferencedAssemblies(a, true)
                    select e.ManifestModule.FullyQualifiedName).ToArray();
        }

        public static string[] Modules
        {
            get
            {
                return ModulesOf(Assembly.GetCallingAssembly());
            }
        }

        public static string[] LocalModules
        {
            get
            {
                return
                    (from i in ModulesOf(Assembly.GetCallingAssembly())
                    let f = new FileInfo(i)
                    select f.Name).Distinct().ToArray();

            }
        }


        public static void PHPInclude(ITextWriter w, string[] Modules)
        {
            foreach (var v in Modules)
                PHPInclude(w, v);
        }
    }
}
