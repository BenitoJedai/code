using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;

namespace FlashConsoleWorm.ActionScript
{
    [Script]
    class Apple
    {
        public Func<Point> GetRandomLocation;

        public Func<Point, Point> Wrapper;

        public Point Location;

        readonly Shape Control = new Shape();

        public Apple MoveToRandomLocation()
        {
            Location = GetRandomLocation();
            MoveToLocation();
            return this;
        }

        public Apple()
        {
            Control.graphics.beginFill(FlashConsoleWorm.ColorRed);
            Control.graphics.drawRect(0, 0, 1, 1);
        }

        public void MoveToLocation()
        {
            Location = Wrapper(Location);

            Control.x = Location.x;
            Control.y = Location.y;

        }

        public Apple AttachTo(DisplayObjectContainer canvas)
        {
            MoveToLocation();
            Control.AttachTo(canvas);

            return this;
        }

    }
}
