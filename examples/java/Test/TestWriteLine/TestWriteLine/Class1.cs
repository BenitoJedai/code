using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestWriteLine
{
    public class Class1 : ScriptCoreLibJava.IAssemblyReferenceToken
    {

        public Class1()
        {
            //   __Console.WriteLine("? WriteLine_06000995");
            Console.WriteLine("? WriteLine_06000995");
        }
    }
}
