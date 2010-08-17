using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Input;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]
namespace TestImplementsViaAQN
{
    [Script]
    public class Class1
    {
        public TouchEventArgs Field1;
        public List<TouchEventArgs> Field2;
    }


    interface AssemblyReferenceToken : TestImplementsViaAQN.BCL.AssemblyReferenceToken
    {

    }
}
