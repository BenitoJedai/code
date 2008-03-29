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
        public int backgroundColor = 0xcccccc;

        public int frameRate = 30;
        public int width = 320;
        public int height = 240;
    }
}
