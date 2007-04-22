using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.PHP.System
{
    [Script(Implements = typeof(byte))]
    class ByteImpl
    {
        public static ByteImpl Parse(string e)
        {
            return (ByteImpl)((object)Native.API.intval(e));
        }
    }
}
