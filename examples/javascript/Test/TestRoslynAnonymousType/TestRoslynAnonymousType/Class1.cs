using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestRoslynAnonymousType
{
    public class Class1 : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        // X:\jsc.svn\examples\javascript\Test\TestPrimaryConstructors\CustomerPrimaryConstructorsWebApplication\Application.cs


        //  typeof(System.Text.StringBuilder)

        static void Main()
        {
            var i = 13;

            //          // <>f__AnonymousType0`1.ToString
            //          type$_4FBT1vrWSD_aExrsILOc25Q.toString /* <>f__AnonymousType0`1.ToString */ = function()
            //{
            //              throw 'Not implemented, ToString';
            //          };
            //          _4FBT1vrWSD_aExrsILOc25Q.prototype.toString /* System.Object.ToString */ = _4FBT1vrWSD_aExrsILOc25Q.prototype.toString /* <>f__AnonymousType0`1.ToString */;

            var u = new { i };

            var i13 = u.i;

            //          // <>f__AnonymousType0`1.ToString
            //          type$_4FBT1vrWSD_aExrsILOc25Q.toString /* <>f__AnonymousType0`1.ToString */ = function()
            //{
            //              var a = [this], b;

            //              new Array(1)[0] = a[0].i;
            //              b = RR0ABtNdQz66ZYUODttTfw('{{ i = {0} }}', new Array(1));
            //              return b;
            //          };

            // roslyn has changed the ToString IL!

        }
    }
}
