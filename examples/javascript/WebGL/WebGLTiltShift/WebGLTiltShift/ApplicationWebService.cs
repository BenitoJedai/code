using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace WebGLTiltShift
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // jsc java does not yet like roslyn 2015

        //0001 02000178 ScriptCoreLib::ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProvider
        //script: error JSC1000: Java : Opcode not implemented: stind.i1 at ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood.CreatePaddedBuffer
        //internal compiler error at method
        // assembly: C:\util\jsc\bin\ScriptCoreLib.dll at
        // type: ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood, ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null
        // method: CreatePaddedBuffer
        // Java : Opcode not implemented: stind.i1 at ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood.CreatePaddedBuffer


        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

    }
}
