using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReduceMethodAccessInReference
{
    public class Class1 : foo.Class1
    {
        // Error	1	'TestReduceMethodAccessInReference.Class1.goo()': cannot change access modifiers when overriding 'public' inherited member 'foo.Class1.goo()'	X:\jsc.svn\examples\rewrite\TestReduceMethodAccessInReference\TestReduceMethodAccessInReference\Class1.cs	11	33	TestReduceMethodAccessInReference
        //protected override void goo()

        // Error	1	'TestReduceMethodAccessInReference.Class1.goo()': cannot change access modifiers when overriding 'protected' inherited member 'foo.Class1.goo()'	X:\jsc.svn\examples\rewrite\TestReduceMethodAccessInReference\TestReduceMethodAccessInReference\Class1.cs	14	30	TestReduceMethodAccessInReference

        protected override void goo()
        {
            base.goo();
        }


        protected override void goo2()
        {
            base.goo2();
        }
    }
}
