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
        public void Method2()
        {
            Console.WriteLine("this is Method2");
        }

        public void Method1(Foo f)
        {

        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("this code is running inside JVM");

            ToConsole(args);
            ToConsole(CLRProgram.GetVMContinuationSupport());

            var Handlers = new Action[] { 
                new Foo().Method2
            };

            Handlers[0]();

            CLRProgram.CLRMain(
                new TypeBroker { Target = typeof(Foo) }
            );
        }

        private static void ToConsole(string[] args)
        {
            if (args == null)
                Console.WriteLine("ToConsole null");

            foreach (var item in args)
            {
                Console.WriteLine("- " + item);
                
            }

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
        public static string[] GetVMContinuationSupport()
        {
            return VMContinuationSupport.__value();
        }

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

    public static class VMContinuationSupport
    {
        public static Func<string[]> __value = () => null;
    }
}
