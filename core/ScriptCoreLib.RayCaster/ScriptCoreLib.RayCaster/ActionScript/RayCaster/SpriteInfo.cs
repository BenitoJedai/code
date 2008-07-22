using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.RayCaster
{
    [Script]
    public class SpriteInfo
    {
        public Point Position = new Point();

        public Texture64[] Frames;

        public double Direction = 0;
    }
}
