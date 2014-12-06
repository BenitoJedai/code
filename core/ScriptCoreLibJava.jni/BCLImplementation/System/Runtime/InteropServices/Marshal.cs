using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using jni;

namespace ScriptCoreLibJava.BCLImplementation.System.Runtime.InteropServices
{
    // https://github.com/mono/mono/blob/master/mono/metadata/remoting.c
    // https://github.com/mono/mono/blob/master/mono/metadata/marshal.h
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Runtime.InteropServices/MarshalAsAttribute.cs

    [Script(Implements = typeof(global::System.Runtime.InteropServices.Marshal))]
    internal class __Marshal
    {
        // X:\jsc.svn\examples\java\hybrid\JVMCLRLoadLibrary\JVMCLRLoadLibrary\Program.cs

        public static IntPtr StringToHGlobalAnsi(string s)
        {
            if (s == null)
            {
                return (IntPtr)CPtr.NULL;
            }

            var bytes = (sbyte[])(object)Encoding.ASCII.GetBytes(s);
            var p = new CMalloc(bytes.Length + 1);

            p.copyIn(0, bytes, 0, bytes.Length);

            return (IntPtr)p;
        }

        public static string PtrToStringAnsi(IntPtr ptr)
        {
            var p = (CPtr)ptr;

            if (p.IsNull) return null;

            return p.getString();
        }

        public static IntPtr AllocHGlobal(int cb)
        {
            var p = new CMalloc(cb);

            return (IntPtr)p;
        }

        public static void FreeHGlobal(IntPtr hglobal)
        {
            var p = (CPtr)hglobal;

            if (p.IsNull)
                return;

            p.free();
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

        public static IntPtr ReadIntPtr(IntPtr ptr)
        {
            var p = (CPtr)ptr;

            return (IntPtr)p.getCPtr(0);
        }
    }
}
