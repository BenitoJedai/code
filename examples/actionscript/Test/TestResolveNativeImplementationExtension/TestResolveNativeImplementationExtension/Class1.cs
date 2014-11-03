using ScriptCoreLib.ActionScript.flash.display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestResolveNativeImplementationExtension
{
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141103

    public class Class1
    {
        public static void invoke(InteractiveObject x)
        {
            //  double0 = x.alpha;
            var a = x.async;


        }
    }
}
