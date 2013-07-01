using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIntPtrParameter
{
    public class Class1
    {
        // Error	1	A value of type '<null>' cannot be used as a default parameter because there are no standard conversions to type 'System.IntPtr'	X:\jsc.svn\examples\rewrite\TestIntPtrParameter\TestIntPtrParameter\Class1.cs	11	39	TestIntPtrParameter
        // Error	1	Default parameter value for 'e' must be a compile-time constant	X:\jsc.svn\examples\rewrite\TestIntPtrParameter\TestIntPtrParameter\Class1.cs	13	43	TestIntPtrParameter


        public static void foo(IntPtr e = IntPtr.Zero)
        {

        }
    }
}
