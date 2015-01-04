using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test453ArrayInit
{
    public class Class1
    {
        // X:\jsc.svn\examples\javascript\Test\Test454AnonymousToStringFake\Test454AnonymousToStringFake\Class1.cs
       
        static void Invoke()
        {

            ///* let 0002 = */new Array(2);
            ///* ldloc 0002 */new Array(2)[0] = 1;
            ///* ldloc 0002 */new Array(2)[1] = 2;
            //b = /* 0002 */new Array(2);

            var bytes = new[] { 1, 2, 3 };

            ///* let 0002 = */
            //                [1, 2, 3];

            //b = /* 0002 */[1, 2, 3];

        }
    }
}
