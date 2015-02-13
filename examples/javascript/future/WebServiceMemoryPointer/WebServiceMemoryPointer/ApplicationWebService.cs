using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebServiceMemoryPointer
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public unsafe class ApplicationWebService
    {
        // how is the pointer to be used?
        // read only?
        // up to virtual page?
        // can it be written to from multiple threads?

        //Error CS0306  The type 'byte*' may not be used as a type argument WebServiceMemoryPointer ApplicationWebService.cs	26

        public Task<IntPtr> GetPointerToBuffer()
        //public Task<byte*> GetPointerToBuffer()
        {
            var p = Marshal.AllocHGlobal(2);


            //return (byte*)p.ToPointer();
            return Task.FromResult(p);
        }

        public void WebMethod2(string e, Action<string> y)
        {

        }

    }
}
