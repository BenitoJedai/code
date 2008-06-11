using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://www.koders.com/default.aspx?s=%22%5BSWF%28backgroundColor%22&btn=&la=ActionScript&li=*
    [Script(IsNative = true)]
    public sealed class SWFAttribute : Attribute
    {
        [Hex]
        public uint backgroundColor = 0xcccccc;

        public int frameRate = 30;
        public int width = ScriptApplicationEntryPointAttribute.DefaultWidth;
        public int height = ScriptApplicationEntryPointAttribute.DefaultHeight;
    }
}
