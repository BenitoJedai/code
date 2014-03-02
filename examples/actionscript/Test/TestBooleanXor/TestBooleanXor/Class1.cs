using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestBooleanXor
{
    public class Class1
    {
        public static void foo(bool x, bool y)
        {
            // X:\jsc.svn\examples\javascript\Avalon\AvalonBrowserLogos\AvalonBrowserLogos\ApplicationCanvas.cs

            //Error	1	Cannot implicitly convert type 'bool' to 'int'	X:\jsc.svn\examples\actionscript\Test\TestBooleanXor\TestBooleanXor\Class1.cs	14	21	TestBooleanXor
            var z = x ^ y;



            //var flag0:Boolean;

            //flag0 = (x ^ y);
        }
    }
}
