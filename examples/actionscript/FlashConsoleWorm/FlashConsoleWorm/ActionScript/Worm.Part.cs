using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashConsoleWorm.ActionScript
{
    partial class Worm
    {
        [Script]
        public class Part
        {
            public Point Location;

            public Func<Point, Point> Wrapper;

            internal protected readonly Shape Control = new Shape();

            public Part()
            {
                Control.graphics.beginFill(FlashConsoleWorm.ColorGreen);
                Control.graphics.drawRect(0, 0, 1, 1);
            }

            public Part AttachTo(DisplayObjectContainer canvas)
            {
                MoveToLocation();

                Control.AttachTo(canvas);

                return this;
            }

            public void MoveToLocation()
            {
                Location = Wrapper(Location);

                Control.x = Location.x;
                Control.y = Location.y;
            }

            public void Dispose()
            {
                this.Control.Orphanize();
            }
        }
    }
}
