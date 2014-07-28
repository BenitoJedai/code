﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.InteropServices
{
    // http://referencesource.microsoft.com/#mscorlib/system/runtime/interopservices/marshal.cs

    [Script(Implements = typeof(global::System.Runtime.InteropServices.Marshal))]
    internal class __Marshal
    {
        public static void WriteInt32(IntPtr ptr, int offset, int value)
        {
            // tested by ?

            var p = (__IntPtr)(object)ptr;

            if (p.PointerToUInt8 != null)
            {
                var _buffer = p.PointerToUInt8;

                // System.Drawing.Imaging.PixelFormat.Format32bppArgb

                _buffer[(uint)offset + 2] = (byte)((value >> 8 * 0) & 0xff);
                _buffer[(uint)offset + 1] = (byte)((value >> 8 * 1) & 0xff);
                _buffer[(uint)offset + 0] = (byte)((value >> 8 * 2) & 0xff);
                _buffer[(uint)offset + 3] = (byte)((value >> 8 * 3) & 0xff);

            }
        }
    }
}
