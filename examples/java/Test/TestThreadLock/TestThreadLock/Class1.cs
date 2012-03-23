using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.Java)]


namespace TestThreadLock
{
    [Script]
    public class Class1
    {
        static void Foo(object e)
        {
            // http://www.danielmoth.com/Blog/NET-4-MonitorEnter-Replaced-By-MonitorEnter.aspx

            lock (e)
            {
                S1();
            }
        }

        static void S1()
        {
        }
    }
}
