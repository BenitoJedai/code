using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib;

[assembly: Script(IsScriptLibrary = true)]
[assembly: ScriptTypeFilter(ScriptCoreLib.ScriptType.CSharp2)]

namespace CILToVisualCSharp
{
    [Script]
    public class Class1
    {
        public void Method1(string e)
        {
            Console.WriteLine("Class1.Method1: " + e);
        }
    }
}
