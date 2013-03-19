using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestInheritedDictionary
{
    interface I : ScriptCoreLibJava.IAssemblyReferenceToken { }

    partial class Level
    {
        public class Attribute
        {
        }

        public sealed class AttributeDictonary : Dictionary<string, Action<string>>
        {
            public void Add(Attribute e)
            {
                //KeyValuePair<string, Action<string>> i = e;

                //this.Add(i);
            }
        }

    }

}
