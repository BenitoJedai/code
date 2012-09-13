using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.InteropServices
{
    [Script(Implements = typeof(global::System.Runtime.InteropServices.Marshal))]
    internal class __Marshal
    {
        public static void WriteInt32(IntPtr ptr, int ofs, int val)
        {

        }
    }
}
