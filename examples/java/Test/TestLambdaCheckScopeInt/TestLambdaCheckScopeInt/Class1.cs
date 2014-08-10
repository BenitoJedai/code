using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]
namespace TestLambdaCheckScopeInt
{
    public class Class1 : ScriptCoreLibJava.IAssemblyReferenceToken
    {
        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRLambdaCheckScopeInt\TestJVMCLRLambdaCheckScopeInt\Program.cs
        // didnt we already fix it for js as

        public Class1()
        {
            var flag1 = false;
            var flag2 = false;
            var flag3 = false;
            var flag4 = false;

            //    if (!((((flag1 | flag2) | flag3) | flag4) == 0))
            //   if ((((flag1 | flag2) | flag3) | flag4))
            if (flag1 | flag2 | flag3 | flag4)
            {
                Console.Write("1");
            }
            else
            {
                Console.Write("2");

            }

            var WithoutLinefeedsCounter = 0;
            var WithoutLinefeedsDirty = false;

            Func<IDisposable> WithoutLinefeeds =
             delegate
            {

                // if (this.WithoutLinefeedsCounter == null)
                if (WithoutLinefeedsCounter == 0)
                    WithoutLinefeedsDirty = false;

                return null;
            };


        }
    }
}
