using Serializable = System.SerializableAttribute;

namespace ScriptCoreLib.Shared.Drawing
{
    [Script, Serializable]
    public class RectangleInfo
    {
        public int Left;
        public int Top;

        public int Width;
        public int Height;
    }

    [Script]
    public class Rectangle : RectangleInfo
    {
       

        public bool Contains(Point p)
        {
            if (p.X < this.Left)
                return false;

            if (p.Y < this.Top)
                return false;

            if (p.X > this.Right)
                return false;

            if (p.Y > this.Bottom)
                return false;

            return true;
        }

        public static Rectangle operator /(Rectangle r, int i)
        {
            return Rectangle.Of(r.Left / i, r.Top / i, r.Width / i, r.Height / i);

        }

        public static Rectangle operator *(Rectangle r, double i)
        {
            return Rectangle.Of(
                (int)(r.Left * i), 
                (int)(r.Top * i), 
                (int)(r.Width * i),
                (int)(r.Height * i)
            );

        }

        public Point Location
        {
            get
            {
                return new Point(Left, Top);
            }
        }

        public static implicit operator Point(Rectangle r)
        {
            return new Point(r.Left, r.Top);
        }

        public Size Size
        {
            get
            {
                return Size.Of(Width, Height);
            }
        }

        public int Right
        {
            get { return Left + Width; }
            set { Width = value - Left; }
        }

        public int Bottom
        {
            get { return Top + Height; }
            set { Height = value - Top; }
        }

        public bool IntersectsWith(Rectangle rect)
        {
            bool a = (rect.Left < (this.Right));
            bool b = (this.Left < (rect.Right));
            bool c = (rect.Top < (this.Bottom));
            bool d = (this.Top < (rect.Bottom));

            return (a && b && c && d);
        }

        public static Rectangle Of(int left, int top, int width, int height)
        {
            Rectangle r = new Rectangle();

            r.Left = left;
            r.Top = top;
            r.Width = width;
            r.Height = height;

            return r;
        }

        public override string ToString()
        {
            return "[" + Left + ", " + Top + ", " + Width + ", " + Height + "]";
        }

        public static Rectangle Of(Point Location, Size TextSize)
        {
            return Of(Location.X, Location.Y, TextSize.Width, TextSize.Height);
        }

        public void Offset(Point point)
        {
            this.Left += point.X;
            this.Top += point.Y;

        }
    }

    [Script]
    public class Size
    {
        public int Width;
        public int Height;

        public static Size Of(int w, int h)
        {
            Size n = new Size();

            n.Width = w;
            n.Height = h;

            return n;
        }
    }

    [Script, Serializable]
    public class Point<T>
    {
        public T X;
        public T Y;

        
    }

    [Script, Serializable]
    public class Point : Point<int>
    {
        public Rectangle WithMargin(int i)
        {
            return Rectangle.Of(X - i, Y - i, i * 2, i * 2);
        }

        public static Point operator *(Point p, double v)
        {
            return new Point((int)(p.X * v), (int)(p.Y * v));
        }

        public static Point operator /(Point p, double v)
        {
            return new Point((int)(p.X / v), (int)(p.Y / v));
        }

        public Point Min(Point p)
        {
            var c = new Point(this.X, this.Y);

            if (c.X > p.X) c.X = p.X;
            if (c.Y > p.Y) c.Y = p.Y;

            return c;
        }

        public Point Max(Point p)
        {
            var c = new Point(this.X, this.Y);

            if (c.X < p.X) c.X = p.X;
            if (c.Y < p.Y) c.Y = p.Y;

            return c;
        }

        public static Point Zero
        {
            get { return new Point(0, 0); }
        }


        // jsc:php can only have 1 ctor at the moment... so we have broken it with this change
        public Point()
        {
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public override string ToString()
        {
            return "[" + X + ", " + Y + "]";
        }

        public string AsPosition()
        {
            return X + " " + Y;

        }

        public void Offset(Point point)
        {
            X += point.X;
            Y += point.Y;
        }

        public void CopyTo(Point pos)
        {
            pos.X = X;
            pos.Y = Y;
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }


        public static Point operator /(Point a, int b)
        {
            return new Point(a.X / b, a.Y / b);
        }

        public static Point operator *(Point a, int b)
        {
            return new Point(a.X * b, a.Y * b);
        }


  


        public static Point Of(Point p)
        {
            if (p == null)
                return new Point(0, 0);


            return new Point(p.X, p.Y);
        }

        public static void SpawnHelper(Predicate<Point> p)
        {
            p.Target = Point.Of(p.Target);
        }


        public int Z
        {
            get
            {
                return X * X + Y * Y;
            }
        }

        public int CompareRange(Point Position, int e)
        {
            var x = (X - Position.X);
            var y = (Y - Position.Y);

            var z = x * x + y * y;
            var c = e * e;

            if (z == c)
                return 0;

            if (z < c)
                return -1;

            return 1;
        }
    }


}
