using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestGenericParameterForChar
{
    public class Class1
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref0;

        public void PING_InvokeAsync(string host, Action<string> y)
        {
            if (host.ToCharArray().Any(k => !(char.IsNumber(k) || char.IsLetter(k) || k == '.')))
            {
            }
        }
    }
}
