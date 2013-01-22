﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://www.koders.com/default.aspx?s=%22%5BSWF%28backgroundColor%22&btn=&la=ActionScript&li=*
    [Script(IsNative = true)]
    public sealed class SWFAttribute : Attribute
    {
        const uint White = 0xffffff;
        const uint AdobeFlashDefault = 0xcccccc;

        [Hex]
        public uint backgroundColor = White;

        //public uint backgroundColor = 0xcccccc;

        public int frameRate = 60;

        public int width = ScriptApplicationEntryPointAttribute.DefaultWidth;
        public int height = ScriptApplicationEntryPointAttribute.DefaultHeight;

        public SWFAttribute()
        {

        }


        public SWFAttribute(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
