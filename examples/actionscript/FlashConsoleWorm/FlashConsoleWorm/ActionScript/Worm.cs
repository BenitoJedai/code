using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashConsoleWorm.ActionScript
{
    [Script]
    public partial class Worm
    {
		public static Point VectorLeft = new Point(-1, 0);
		public static Point VectorUp = new Point(0, -1);
		public static Point VectorRight = new Point(1, 0);
		public static Point VectorDown = new Point(0, 1);

        public bool IsAlive = true;

        public Point Location;

        public Func<Point, Point> Wrapper;

        Point _Vector;

        public Point Vector
        {
            get
            {
                return _Vector;
            }
            set
            {
                if (_Vector != null)
                    if (new Point { x = -_Vector.x, y = -_Vector.y }.IsEqual(value))
                        return;

                _Vector = value;

				if (VectorChanged != null)
					VectorChanged();
            }
        }
		public event Action VectorChanged;

        public readonly List<Part> Parts = new List<Part>();

        public DisplayObjectContainer Canvas { get; set; }

        public Worm()
        {
            Vector = new Point { x = 1, y = 0 };
        }

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
                new Part(Color) { Location = x, Wrapper = Wrapper }.AttachTo(Canvas)
            );

            Location = x;

            return this;
        }

		public uint Color = FlashConsoleWorm.ColorGreen;

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
