using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNestedAbstractMethod
{
    public class GenerateMethod
    {
        public class AbstractGenerateMethodService<A, B, C, D>
        {
            abstract class MethodInfo
            {
                //Error	1	'TestNestedAbstractMethod.GenerateMethod.AbstractGenerateMethodService<A,B,C,D>.MethodInfo.DetermineReturnType(object)': virtual or abstract members cannot be private	X:\jsc.svn\examples\rewrite\TestNestedAbstractMethod\TestNestedAbstractMethod\Class1.cs	15	33	TestNestedAbstractMethod
                protected abstract object DetermineReturnType(object e);
            }


            class MethodSymbolInfo : MethodInfo
            {

                protected override object DetermineReturnType(object e)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
