using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace HybridReflectionExample
{
    class Foo
    {
        public void Method1(Foo f)
        {

        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("this code is running inside JVM");

            CLRProgram.CLRMain(
                new TypeBroker { Target = typeof(Foo) }
            );
        }
    }

    public class TypeBroker : IBroker
    {
        public Type Target;

        public string FullName
        {
            get { return this.Target.FullName; }
        }
    }

    public interface IBroker
    {
        string FullName { get; }
    }

    [SwitchToCLRContext]
    public static class CLRProgram
    {
        [STAThread]
        public static void CLRMain(IBroker b)
        {
            Console.WriteLine("running inside CLR");
            Console.WriteLine("b: " + b.FullName);

            Console.WriteLine(Path.GetFileName(Assembly.GetExecutingAssembly().Location));

            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(Path.GetFullPath(item.Location) + " types: " + item.GetTypes().Length);
            }

            // if jsc supported pdb rewrite we could have a break point over here!

            MessageBox.Show("hello");

            Console.WriteLine("done!");
        }
    }
}
