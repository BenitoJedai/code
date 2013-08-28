using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestNewByteArrayViaScriptCoreLib
{
    [Script]
    public class Class1 : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        public Class1()
        {
            var bytes = new byte[0x10];

        }
    }
}
