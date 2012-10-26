using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestScriptCoreLibReflectionOptions
{
    public interface TestScriptCoreLibReflectionOptions_IAssemblyReferenceToken : 
        ScriptCoreLibJava.IAssemblyReferenceToken
    { 

    }

    public class Class1
    {
        public static void foo()
        {
            ScriptCoreLib.Reflection.Options.ParameterDispatcherExtensions.AtTrace = null;
        }
    }
}
