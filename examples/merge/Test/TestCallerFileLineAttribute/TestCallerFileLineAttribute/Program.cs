using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.CompilerServices;
using System.Reflection;

[assembly: Obfuscation(Feature = "merge")]

namespace TestCallerFileLineAttribute
{
    static class Program
    {
        static void trace(
            this string message,

            [CallerFilePath] string sourceFilePath = "",

            [CallerLineNumber] int sourceLineNumber = 0,

            [CallerFileLine] string sourceFileLine = ""

        )
        {
            Console.WriteLine(
                "trace " + new { message, sourceFilePath, sourceLineNumber, sourceFileLine }
            );
        }

        static void Main(string[] args)
        {
            // RewriteToAssembly /AssemblyMerge:"TestCallerFileLineAttribute.exe" /Output:"TestCallerFileLineAttribute.exe"  /EntryPointAssembly:"TestCallerFileLineAttribute.exe"

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201502/20150220

            "hello".trace();

            new { foo = "bar" }.ToString().trace();

            //trace { message = hello, sourceFilePath = X:\jsc.svn\examples\merge\Test\TestCallerFileLineAttribute\TestCallerFileLineAttribute\Program.cs, sourceLineNumber = 36, sourceFileLine =  }
            //trace { message = { foo = bar }, sourceFilePath = X:\jsc.svn\examples\merge\Test\TestCallerFileLineAttribute\TestCallerFileLineAttribute\Program.cs, sourceLineNumber = 38, sourceFileLine =  }

            Console.Beep();
            Console.ReadLine();

        }
    }
}
