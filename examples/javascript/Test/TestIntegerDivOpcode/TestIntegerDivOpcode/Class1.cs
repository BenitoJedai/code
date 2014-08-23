using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]
namespace TestIntegerDivOpcode
{
    public class Class1
    {
        static void foo()
        {
            // X:\jsc.svn\examples\javascript\Test\TestInt32Div\TestInt32Div\Class1.cs
            // X:\jsc.svn\examples\javascript\test\TestIntegerDiv\TestIntegerDiv\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestFromBase64String\TestFromBase64String\Application.cs

            {

                var x = 32;
                var y = 17;

                //     d = (b / c);
                var z = x / y;
                var zz = z;
            }

            {

                var x = 32u;
                var y = 17u;

                //     d = (b / c);
                var z = x / y;
            }

            {

                var x = 32.0;
                var y = 17.0;

                //     d = (b / c);
                var z = x / y;
                var zz = z;
            }
        }
    }
}
