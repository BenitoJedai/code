﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashConsoleWorm.ActionScript
{
    [Script]
    partial class Worm
    {
        public Point Location;

        public Func<Point, Point> Wrapper;

        public Point Vector = new Point { x = 1, y = 0 };

        public readonly List<Part> Parts = new List<Part>();

        public DisplayObjectContainer Canvas { get; set; }

        public Worm GrowToVector()
        {
            return GrowTo(this.Vector);
        }

        public Point NextLocation
        {
            get
            {
                return Wrapper(this.Location + this.Vector);
            }
        }

        public Worm GrowTo(Point p)
        {
            var x = Wrapper(this.Location + p);

            Parts.Add(
                new Part { Location = x, Wrapper = Wrapper }.AttachTo(Canvas)
            );

            Location = x;

            return this;
        }

        public Worm Grow()
        {
            return GrowTo(new Point { x = 0, y = 0 });
        }

        public void Shrink()
        {
            var p = this.Parts.FirstOrDefault();

            if (p == null)
                return;

            this.Parts.Remove(p);

            p.Dispose();
        }
    }
}
