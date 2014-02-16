using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestByRefThis
{

    public
        class
        //struct
        Class1
    {
        public void invoke()
        {
            // Error	1	Cannot pass '<this>' as a ref or out argument because it is read-only	X:\jsc.svn\examples\java\async\Test\TestByRefThis\TestByRefThis\Class1.cs	13	24	TestByRefThis

            var that = this;
            invoke(ref that);
        }

        public static void invoke(ref Class1 x)
        {

        }
    }
}
