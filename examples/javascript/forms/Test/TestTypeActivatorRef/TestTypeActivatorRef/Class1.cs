using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestTypeActivatorRef
{
    public class Class1
    {
        // X:\jsc.svn\examples\javascript\test\TestTypeActivator\TestTypeActivator\Program.cs
        public Class1()
        {

        }

        static Class1 Create()
        {
            return new Class1();
        }
    }
}
