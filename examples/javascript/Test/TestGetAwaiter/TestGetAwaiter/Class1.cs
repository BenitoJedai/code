using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]
namespace TestGetAwaiter
{
    // see: http://msdn.microsoft.com/en-us/library/hh138386(v=vs.110).aspx
    [Script(ImplementsViaAssemblyQualifiedName = 
        "System.Runtime.CompilerServices.TaskAwaiter`1"
        //"System.Runtime.CompilerServices.TaskAwaiter`1, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        )]
    internal class __TaskAwaiter<TResult>
    {

    }

    [Script(Implements = typeof(global::System.Threading.Tasks.Task<>))]
    internal class __Task<TResult> //: __Task
    {
        // see also: http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.getawaiter(v=vs.110).aspx


        public __TaskAwaiter<TResult> GetAwaiter()
        {

            return null;
        }
    }

    [Script]
    public class Class1
    {
        public static void Main(string[] args)
        {
            //var t = typeof(System.Runtime.CompilerServices.TaskAwaiter<>);
            //Console.WriteLine(t.AssemblyQualifiedName);
            //Console.WriteLine(t.FullName);

            var x = default(Task<string>);

            // script: error JSC1000: method was found, but too late: [GetAwaiter]
            var a = x.GetAwaiter();

        }
    }
}
