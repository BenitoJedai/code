using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestNewMethod
{
    [ScriptAttribute.ExplicitInterface]
    public interface I3
    {
        void Initialize();
    }

    //[ScriptAttribute.ExplicitInterface]


    public class Class3
    {
        public virtual void Dispose(bool disposing)
        {
        }
    }

    class Pair<A, B>
    {

    }

    namespace bar
    {
    }

    namespace foo
    {
        public interface I3
        {
            void Dispose(bool disposing);
        }

        public class Class2 : Class3
        {
            public object components;


            public Class2()
            {
                Initialize();
            }

            public void Initialize()
            {
                components = null;
            }

            public override void Dispose(bool disposing)
            {
                // Note: This jsc project does not support unmanaged resources.
                base.Dispose(disposing);
            }

        }
    }

    public class Class2 : foo.Class2, I3, foo.I3
    {
        public new object components;

        public Class2()
        {
            Initialize();
        }

         Pair<I3, foo.I3> foo(I3 y, foo.I3 x)
        {
            throw null;
        }

        //private void Initialize()
        public new void Initialize()
        {
            components = null;
        }

        public override void Dispose(bool disposing)
        {
            // Note: This jsc project does not support unmanaged resources.
            base.Dispose(disposing);
        }
    }
}
