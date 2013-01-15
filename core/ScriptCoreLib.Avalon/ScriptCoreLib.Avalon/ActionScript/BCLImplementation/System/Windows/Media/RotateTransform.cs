using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.RotateTransform))]
	internal class __RotateTransform : __Transform
	{
        public double Angle { get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }

        public __RotateTransform(double Angle) : this(Angle, 0, 0)
        {
        }

        public __RotateTransform(double Angle, double CenterX, double CenterY)
        {
            this.Angle = Angle;
            this.CenterX = CenterX;
            this.CenterY = CenterY;
        }
	}
}
