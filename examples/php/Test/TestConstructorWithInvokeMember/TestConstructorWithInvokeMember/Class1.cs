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
        public Class1(int a, int b, int c)
        {

        }

        public Class1 Invoke(int e, int f)
        {
            return this;
        }

        public static Class1 Of(int a, int b, int c, int e, int f)
        {
            return new Class1(a, b, c).Invoke(e, f);
        }
    }
}
