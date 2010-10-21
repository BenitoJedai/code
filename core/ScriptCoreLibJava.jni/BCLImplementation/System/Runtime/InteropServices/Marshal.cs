using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using jni;

namespace ScriptCoreLibJava.BCLImplementation.System.Runtime.InteropServices
{
    [Script(Implements = typeof(global::System.Runtime.InteropServices.Marshal))]
    internal class __Marshal
    {
        public static IntPtr AllocHGlobal(int cb)
        {
            var p = new CMalloc(cb);

            return (IntPtr)p;
        }

        public static void FreeHGlobal(IntPtr hglobal)
        {
            var p = (CPtr)hglobal;
            var m = (CMalloc)p;

            m.free();
        }

        public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
        {
            var p = (CPtr)destination;
            var buf = (sbyte[])(object)source;

            p.copyIn(startIndex, buf, 0, length);
        }

       public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
        {
            var p = (CPtr)source;
            var buf = (sbyte[])(object)destination;

            p.copyOut(0, buf, startIndex, length);
        }
    }
}
