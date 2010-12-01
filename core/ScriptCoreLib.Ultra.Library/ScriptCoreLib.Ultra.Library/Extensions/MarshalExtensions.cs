using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ScriptCoreLib.Extensions
{
    public static class MarshalExtensions
    {
        public static IntPtr AllocHGlobalIntPtrMarker(object source)
        {
            if (source == null)
                return IntPtr.Zero;

            return Marshal.AllocHGlobal(1);
        }

        public static void FreeHGlobalIntPtrMarker(object source, IntPtr ptr)
        {
            if (source == null)
                return;

            Marshal.FreeHGlobal(ptr);
        }
    }
}
