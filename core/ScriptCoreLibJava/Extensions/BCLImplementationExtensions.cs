using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.lang;
using System.IO;
using java.security;
using java.net;

namespace ScriptCoreLibJava.Extensions
{
    [Script]
    public static class BCLImplementationExtensions
    {

        public static Type ToType(this Class c)
        {
            return (ScriptCoreLibJava.BCLImplementation.System.__Type)c;
        }

        public static Class ToClass(this System.Type t)
        {
            var tt = (ScriptCoreLibJava.BCLImplementation.System.__Type)t;

            return tt.InternalTypeDescription;
        }

        public static FileInfo GetDeclaringFile(this Class cls)
        {
            // for some reason void cannot be resolved...
            if (cls.getName() == "void")
                cls = typeof(object).ToClass();


            // http://stackoverflow.com/questions/5726930/location-of-javaagent-jar-in-bootclasspath
            var r = cls.getName().Replace(".", "/") + ".class";


            var cl = cls.getClassLoader();

            if (cl == null)
                cl = ClassLoader.getSystemClassLoader();

            //http://stackoverflow.com/questions/1921238/getclass-getclassloader-is-null-why

            var loc = cl.getResource(r);

            if (loc == null)
            {
                Console.WriteLine("cls: " + cls.getName());
                Console.WriteLine("r: " + r);
                Console.WriteLine("cl: " + cl);

                //return new FileInfo(@"c:\missing\missing-foo.jar");

                throw new InvalidOperationException();
            }

            //ProtectionDomain pDomain = cls.getProtectionDomain();

            //CodeSource cSource = pDomain.getCodeSource();

            //EnsureCodeSource(cSource);

            //URL loc = cSource.getLocation();




            var ff = loc.getFile();

            {
                var prefix = "file:/";

                if (prefix == ff.Substring(0, prefix.Length))
                    ff = ff.Substring(prefix.Length);
            }

            // sometimes the prefix is shorter?
            {
                var prefix = "file:";

                if (prefix == ff.Substring(0, prefix.Length))
                    ff = ff.Substring(prefix.Length);
            }


            // those jar loaders are adding !/ to the end?

            {
                var suffix = ff.IndexOf("!");

                if (suffix > 0)
                    ff = ff.Substring(0, suffix);
            }

            //global::System.Console.WriteLine("ff: " + ff);

            return new FileInfo(ff);
        }

        private static void EnsureResourceLocation(URL loc)
        {
            if (loc == null)
                throw new InvalidOperationException();
        }

        private static void EnsureCodeSource(CodeSource cSource)
        {
            if (cSource == null)
                throw new InvalidOperationException();
        }
    }
}
