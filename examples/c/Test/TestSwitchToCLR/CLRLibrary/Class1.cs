using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLRLibrary
{
    public class Class1
    {
        // .export [1] as 'Export2'

            // dllExport?

        public static void Export2() => CLRLibraryCSharp.Class1.Export2();
        public static long Export4(long u) => CLRLibraryCSharp.Class1.Export4(u);
    }
}
