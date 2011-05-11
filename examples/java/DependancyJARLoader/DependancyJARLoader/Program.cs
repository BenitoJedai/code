using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using java.lang;
using ScriptCoreLib.Java;
using ScriptCoreLib.Archive.ZIP;
using ScriptCoreLib.Archive;

namespace DependancyJARLoader
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                InternalMain();
            }
            catch (csharp.ThrowableException u)
            {
                Console.WriteLine("error");

                ((Throwable)(object)u).printStackTrace();
            }


            CLRProgram.CLRMain();
        }

        private static void InternalMain()
        {
            Console.WriteLine("this code is running inside JVM 1");


            var x = new FileInfo(@"C:\util\aws-android-sdk-0.2.0\lib\aws-android-sdk-0.2.0-ec2.jar");

            Console.WriteLine(x.FullName);

            var r = new JavaArchiveReflector(x);

            r.JavaArchiveResolve +=
                name =>
                {
                    var xx = CLRProgram.JavaArchiveResolve(r.FileNameString, name);

                    if (null == xx)
                        return null;

                    return new FileInfo(xx);
                };

            ToConsole(r);
        }

        private static void ToConsole(JavaArchiveReflector r)
        {
            Console.WriteLine("files: " + r.Count);

            foreach (JavaArchiveReflector.Entry item in r)
            {
                if (item.Type != null)
                {
                    Console.WriteLine(".class " + item.Type.AssemblyQualifiedName);


                    foreach (var m in item.Methods)
                    {
                        Console.WriteLine("  .method " + m.Name);

                        Console.WriteLine("    .return " + m.ReturnType.AssemblyQualifiedName);

                        foreach (var p in m.GetParameters())
                        {
                            Console.WriteLine("    .param " + p.ParameterType.AssemblyQualifiedName);
                        }
                    }

                    Console.WriteLine("first class done!");

                    break;
                }


            }
        }
    }

    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            Console.WriteLine("running inside CLR");

            Console.WriteLine(Path.GetFileName(Assembly.GetExecutingAssembly().Location));



            // if jsc supported pdb rewrite we could have a break point over here!

            MessageBox.Show("hello");

            Console.WriteLine("done!");
        }

        public static string JavaArchiveResolve(string context, string name)
        {
            return JavaArchiveExtensions.ResolveJavaArchiveLoadRequest(context, name);
        }



    }
}
