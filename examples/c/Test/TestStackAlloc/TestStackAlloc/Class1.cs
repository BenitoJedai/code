using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestStackAlloc
{
    [Script(IsNative = true)]
    public unsafe struct sockaddr_in // : sockaddr
    {
        public short sin_family;
    }

    public unsafe struct sockaddr_in0 // : sockaddr
    {
        public short sin_family;
    }


    public class Class1
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201502/20150214

        unsafe static void Invoke()
        {
            //sockaddr_in* sockaddr_in0;
            //TestStackAlloc_sockaddr_in0 sockaddr_in01;

            // Create the local endpoint
            {
                sockaddr_in localEndPoint;
                sockaddr_in0 localEndPoint0;

                localEndPoint.sin_family = 7;
                localEndPoint0.sin_family = 7;
            }

            {
                // http://en.wikipedia.org/wiki/C_dynamic_memory_allocation
                // Opcode not implemented: localloc at TestStackAlloc.Class1.Invoke
                // https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.localloc(v=vs.110).aspx

                // Allocate space from the local heap.
                var localEndPoint = stackalloc sockaddr_in[1];
                var localEndPoint0 = stackalloc sockaddr_in0[1];

                localEndPoint->sin_family = 7;
                localEndPoint0->sin_family = 7;
            }
        }
    }
}
