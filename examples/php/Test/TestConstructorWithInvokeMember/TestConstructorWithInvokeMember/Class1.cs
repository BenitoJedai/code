using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.PHP)]
namespace TestConstructorWithInvokeMember
{
    [Script]
    public class Class1
    {
        public Class1(int a, int b, int c, int d = 0)
        {

        }

        public Class1 Invoke(int e, int f, int g = 9)
        {
            return this;
        }

        public static Class1 Of(int a, int b, int e, int f)
        {
            return new Class1(a, b, 7).Invoke(e, f);
        }
    }
}
