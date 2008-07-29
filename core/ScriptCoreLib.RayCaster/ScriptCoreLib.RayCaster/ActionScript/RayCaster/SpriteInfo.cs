using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.RayCaster
{
    [Script]
    public class SpriteInfo : IVector
    {
		public SpriteInfo()
		{
			Position = new Point();
		}


		public Point Position { get; set; }

        public Texture64[] Frames;

        public double Direction { get; set; }

		public double Range;
    }
}
