using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly:Obfuscation(Feature ="script")]
namespace Test453Event
{
    class Program : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/20150101/event

        //public event Action InstanceEvent1;
        public static event Action StaticEvent2;

//02000002 Test453Event.Program
//script: error JSC1000: unsupported flow detected, try to simplify.
// Assembly X:\jsc.svn\examples\javascript\Test\Test453Event\Test453Event\bin\Debug\Test453Event.exe
// DeclaringType Test453Event.Program, Test453Event, Version= 1.0.0.0, Culture= neutral, PublicKeyToken= null
// OwnerMethod add_InstanceEvent1
// Offset 0026
// . Try ommiting the return, break or continue instruction.

        static void Main(string[] args)
        {
        }
    }
}
