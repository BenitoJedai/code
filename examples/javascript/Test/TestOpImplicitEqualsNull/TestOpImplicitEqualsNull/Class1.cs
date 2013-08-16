using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestOpImplicitEqualsNull
{
    public class Class1
    {
        public Class1(Class1 x)
        {
            var u = x == null;
        }


        //Error	3	The operator 'TestOpImplicitEqualsNull.Class1.operator ==(TestOpImplicitEqualsNull.Class1, TestOpImplicitEqualsNull.Class1)' requires a matching operator '!=' to also be defined	x:\jsc.svn\examples\javascript\Test\TestOpImplicitEqualsNull\TestOpImplicitEqualsNull\Class1.cs	18	28	TestOpImplicitEqualsNull

        public static bool operator ==(Class1 a, Class1 b)
        {
            if (a == null)
                return false;

            return true;
        }

        public static bool operator !=(Class1 a, Class1 b)
        {
            return false;
        }
    }
}
