using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ObfuscateInternalVirtuals.Core;

namespace ObfuscateInternalVirtuals
{
    public class Class1 : Core.Class1
    {
        // promote to module?
        public static object f;

        public override void Bar()
        {
            base.Bar();
        }
    }


    public static class Foo
    {
        public static void InvokeFoo(this Class1 e)
        {
            e.Bar();
        }
    }

    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    public static class Bar
    {
        public static void InvokeBar(this Class1 e)
        {
            e.Bar();
        }
    }
}
