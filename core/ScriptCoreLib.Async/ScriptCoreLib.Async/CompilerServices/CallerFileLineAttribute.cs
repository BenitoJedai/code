using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//  System.Runtime.CompilerServices
namespace ScriptCoreLib.CompilerServices
{
    // this type is to be used by the compiler during a merge phase

    [AttributeUsage(AttributeTargets.Parameter)]
    public class CallerFileLineAttribute : Attribute
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201502/20150220
        // first step towards understanding multidevice code patching

        // https://msdn.microsoft.com/en-us/library/hh534540.aspx
        //[CallerFilePath]
        public string sourceFilePath = "";

        //[CallerLineNumber]
        public int sourceLineNumber = 0;

        //[CallerFileLine]
        // discovered dynamically by compiler?
        public string sourceFileLine = "";

        // X:\jsc.svn\market\synergy\THREE\THREE\Extensions\THREEExtensions.cs
        // X:\jsc.svn\examples\merge\Test\TestCallerFileLineAttribute\TestCallerFileLineAttribute\Program.cs
    }
}
