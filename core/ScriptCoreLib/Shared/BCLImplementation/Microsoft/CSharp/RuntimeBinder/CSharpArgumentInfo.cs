using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.Microsoft.CSharp
{
    // http://referencesource.microsoft.com/#Microsoft.CSharp/Microsoft/CSharp/RuntimeBinder/CSharpArgumentInfo.cs

    [Script(Implements = typeof(global::Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo))]
    internal class __CSharpArgumentInfo
    {
        public CSharpArgumentInfoFlags flags;
        public string name;

        public static __CSharpArgumentInfo Create(CSharpArgumentInfoFlags flags, string name)
        {
            return new __CSharpArgumentInfo { flags = flags, name = name };
        }

        public override string ToString()
        {
            return "CSharpArgumentInfo " + new { name };
        }
    }
}
