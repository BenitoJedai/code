//using System.Linq;

using ScriptCoreLib;


using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using global::System.Collections.Generic;
using global::System.Linq;
using global::ScriptCoreLib.Shared.Lambda;


namespace NatureBoy.js
{
    using div = IHTMLDiv;
    using img = IHTMLImage;
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Drawing;
    using System;

    [Script]
    public class DudeBase
    {
        public img Shadow;
        public img SelectionImage;
        public img HotImage;

        public Point TargetLocation;

        public Point CurrentLocation
        {
            get
            {
                return new Point(
                           System.Convert.ToInt32(this.X),
                           System.Convert.ToInt32(this.Y)
                       );
            }
        }
        public double X { get; protected set; }
        public double Y { get; protected set; }


        public DudeBase()
        {
            Shadow = new img("assets/NatureBoy/alpha/2.png");
            Shadow.style.position = IStyle.PositionEnum.absolute;

            SelectionImage = new img("assets/NatureBoy/alpha/green-ring.png");
            SelectionImage.style.position = IStyle.PositionEnum.absolute;

            HotImage = new img("assets/NatureBoy/alpha/yellow-ring-50.png");
            HotImage.style.position = IStyle.PositionEnum.absolute;


            Control.style.position = IStyle.PositionEnum.absolute;
            Control.style.overflow = IStyle.OverflowEnum.hidden;

            this.IsSelected = false;

            Control.appendChild(Shadow, SelectionImage, HotImage);

            Control.onmouseover +=
                delegate
                {
                    this.IsHot = true;
                };

            Control.onmouseout +=
                delegate
                {
                    this.IsHot = false;
                };

            this.HotImage.style.display = IStyle.DisplayEnum.none;
        }


        private bool _IsHot;

        public bool IsHot
        {
            get { return _IsHot; }
            set
            {
                _IsHot = value;
                if (value)
                    this.HotImage.style.display = IStyle.DisplayEnum.block;
                else
                    this.HotImage.style.display = IStyle.DisplayEnum.none;
            }
        }

        private bool _IsSelected;

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                if (value)
                    this.SelectionImage.style.display = IStyle.DisplayEnum.block;
                else
                    this.SelectionImage.style.display = IStyle.DisplayEnum.none;
            }
        }

        public readonly div Control = new div();
    }

    [Script]
    public class FrameInfo
    {
        public img Image;

        public double Weight;

        public int OffsetX;
        public int OffsetY;
    }

    [Script]
    public class Dude2 : DudeBase
    {

        public FrameInfo[] Frames { get; set; }

        
        public img CurrentFrameImage;
        public img CurrentFrameBufferImage;

        double _Direction;

        public double Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                _Direction = value;

                this.UpdateFrameImage(CurrentFrame);
            }
        }


        public FrameInfo CurrentFrame
        {
            get
            {
                var value = this.Direction;

                if (value == 0)
                {
                    return this.Frames[0];
                }


                var z = this.Frames[0];

                var p = value / (Math.PI * 2);

                foreach (var v in this.Frames)
                {
                    if (p < v.Weight)
                    {
                        //Console.WriteLine(v.Image.src + " - " + p + " - " + v.Weight);

                        z = v;

                        p = 1;
                    }
                    else
                    {
                        p -= v.Weight;
                    }
                }

                return z;
            }
        }

        public void LookAt(Point point)
        {
            this.TargetLocation = point;

            var a = this.TargetLocation.GetAngle(this.X, this.Y);

            //Console.WriteLine("a = " + a);

            this.Direction = a;
        }

        public void LookAtMouse(IHTMLElement e)
        {
            e.onmousemove +=
                delegate(IEvent ev)
                {
                    try
                    {
                        this.LookAt(ev.CursorPosition);
                    }
                    catch
                    {

                    }
                };
        }


        #region 
        [Script]
        public class ZoomInfo
        {
            public event Action Changed;

            public Func<double, double> DynamicZoomFunc;

            public double DynamicZoom { get; set; }

            double _StaticZoom;
            public double StaticZoom
            {
                get { return _StaticZoom; }
                set
                {
                    _StaticZoom = value; 
                    
                    if (this.Changed != null)
                        this.Changed();
                }
            }

            public double Value
            {
                get
                {
                    return DynamicZoom * StaticZoom;
                }
            }

            public ZoomInfo()
            {
                this.DynamicZoom = 1;
                this.StaticZoom = 1;
            }
        }

        public readonly ZoomInfo Zoom = new ZoomInfo();

        #endregion

        public Dude2()
        {
            this.CurrentFrameImage = new img();
            this.CurrentFrameBufferImage = new img();

            this.Control.style.overflow = IStyle.OverflowEnum.visible;

            this.CurrentFrameImage.style.display = IStyle.DisplayEnum.none;
            this.CurrentFrameBufferImage.style.display = IStyle.DisplayEnum.none;

            this.Control.appendChild(this.CurrentFrameImage, this.CurrentFrameBufferImage);
            
            /*
            this.CurrentFrameImage.style.border = "1px solid blue";
            this.CurrentFrameBufferImage.style.border = "1px solid blue";
            this.Control.style.border = "1px solid yellow";
            this.Shadow.style.border = "1px solid red";
            */
            this.Zoom.Changed +=
                delegate
                {
                    UpdateFrameImage(this.CurrentFrame);
                    // UpdateSize();
                    TeleportTo(this.X, this.Y);

                };
        }


        public void UpdateFrameImage(FrameInfo frame)
        {
            
            var e = frame.Image;

            var ix = Convert.ToInt32(e.width  * this.Zoom.Value);
            var iy = Convert.ToInt32(e.height  * this.Zoom.Value);
 
            var dx = Convert.ToInt32((Width - e.width - frame.OffsetX) * this.Zoom.Value);
            var dy = Convert.ToInt32((Height - e.height - frame.OffsetY) * this.Zoom.Value);

            var a64 = (this.Zoom.Value * 64).ToInt32();
            var a32 = a64 / 2;
            var a16 = a32 / 2;

            this.CurrentFrameBufferImage.src = e.src;
            this.CurrentFrameBufferImage.style.SetLocation(
                dx / 2, 
                dy - a16,
                ix, 
                iy
                );

            this.CurrentFrameImage.style.display = IStyle.DisplayEnum.none;
            this.CurrentFrameBufferImage.style.display = IStyle.DisplayEnum.block;

            var f = this.CurrentFrameImage;

            this.CurrentFrameImage = this.CurrentFrameBufferImage;
            this.CurrentFrameBufferImage = f;

        }

        public int Width;
        public int Height;

        public int ZoomedWidth { get { return Convert.ToInt32( Width * this.Zoom.Value); }}
        public int ZoomedHeight { get { return Convert.ToInt32( Height * this.Zoom.Value); }}

        public void SetSize(int x, int y)
        {
            this.Width = x;
            this.Height = y;

            //Console.WriteLine("size: " + zx + ", " + zy );

            UpdateSize();

        }

        private void UpdateSize()
        {
            var zx = ZoomedWidth;
            var zy = ZoomedHeight;

            var a64 = (this.Zoom.Value * 64).ToInt32();
            var a32 = a64 / 2;

            this.Shadow.style.SetLocation((zx - a64) / 2, zy - a32, a64, a32);
            this.HotImage.style.SetLocation((zx - a64) / 2, zy - a32, a64, a32);

            this.Control.style.SetSize(zx, zy);
        }

        public void TeleportTo(double x, double y)
        {
            var f = this.Zoom.DynamicZoomFunc;
 
            if (f != null)
                this.Zoom.DynamicZoom = f(y);

            this.X = x;
            this.Y = y;

            var a64 = (this.Zoom.Value * 64).ToInt32();
            var a32 = a64 / 2;

            this.Control.style.SetLocation(
                System.Convert.ToInt32(x - ZoomedWidth / 2),
                System.Convert.ToInt32(y - ZoomedHeight + a32 / 2)//,
                );

            this.UpdateSize();

            this.Control.style.zIndex = System.Convert.ToInt32(y);
        }
    }

    [Script]
    public class Dude : DudeBase
    {
        public const int TileWidth = 64;
        public const int TileHeight = 128;





        public img Idle;
        public img Walk1;
        public img Walk2;

        static Dude()
        {
            IStyleSheet.Default.AddRule(".idle img.idle").style.display = IStyle.DisplayEnum.block;
            IStyleSheet.Default.AddRule(".idle img.walk1").style.display = IStyle.DisplayEnum.none;
            IStyleSheet.Default.AddRule(".idle img.walk2").style.display = IStyle.DisplayEnum.none;

            IStyleSheet.Default.AddRule(".walk1 img.walk1").style.display = IStyle.DisplayEnum.block;
            IStyleSheet.Default.AddRule(".walk1 img.walk2").style.display = IStyle.DisplayEnum.none;
            IStyleSheet.Default.AddRule(".walk1 img.idle").style.display = IStyle.DisplayEnum.none;

            IStyleSheet.Default.AddRule(".walk2 img.walk2").style.display = IStyle.DisplayEnum.block;
            IStyleSheet.Default.AddRule(".walk2 img.walk1").style.display = IStyle.DisplayEnum.none;
            IStyleSheet.Default.AddRule(".walk2 img.idle").style.display = IStyle.DisplayEnum.none;
        }

        public Dude()
        {



            Idle = new img("assets/NatureBoy/dude1/1.png") { className = "idle" };
            Idle.style.position = IStyle.PositionEnum.absolute;


            Walk1 = new img("assets/NatureBoy/dude1/3.png") { className = "walk1" };
            Walk1.style.position = IStyle.PositionEnum.absolute;

            Walk2 = new img("assets/NatureBoy/dude1/4.png") { className = "walk2" };
            Walk2.style.position = IStyle.PositionEnum.absolute;

            SetClipping(2, 1);
            IsWalking = false;
            Zoom = 1;


            Control.appendChild(Idle, Walk1, Walk2);

            this.Walk +=
                delegate
                {
                    if (this.TargetLocation == null)
                        return;

                    var z = this.TargetLocation.GetRange(this.CurrentLocation);

                    if (z / this.CurrentSpeed < 1)
                    {
                        // we are there
                        this.IsWalking = false;

                        if (this.DoneWalking != null)
                            this.DoneWalking(this);

                        return;
                    }

                    this.TeleportToArc(this.CurrentSpeed, this.Rotation);
                };




        }

        private double _Zoom;

        public double Zoom
        {
            get { return _Zoom; }
            set
            {
                _Zoom = value;

                Control.style.SetSize(Width, Height);

                Shadow.style.top = (Height - ShadowHeight) + "px";
                Shadow.style.height = ShadowHeight + "px";
                Shadow.style.width = Width + "px";
                Shadow.style.clip = "rect(0px, 0px, " + ShadowHeight + "px, " + Width + "px)";

                SelectionImage.style.top = (Height - ShadowHeight) + "px";
                SelectionImage.style.height = ShadowHeight + "px";
                SelectionImage.style.width = Width + "px";
                SelectionImage.style.clip = "rect(0px, 0px, " + ShadowHeight + "px, " + Width + "px)";

                HotImage.style.top = (Height - ShadowHeight) + "px";
                HotImage.style.height = ShadowHeight + "px";
                HotImage.style.width = Width + "px";
                HotImage.style.clip = "rect(0px, 0px, " + ShadowHeight + "px, " + Width + "px)";


                var w = System.Convert.ToInt32(512 * Zoom);
                var h = System.Convert.ToInt32(256 * Zoom);

                Idle.style.SetSize(w, h);
                Walk1.style.SetSize(w, h);
                Walk2.style.SetSize(w, h);

                SetRotation16(_Rotation16);
            }
        }

        public int ShadowHeight
        {
            get
            {
                return System.Convert.ToInt32(32 * Zoom);
            }
        }

        public int Height
        {
            get
            {
                return System.Convert.ToInt32(TileHeight * Zoom);
            }
        }

        public int Width
        {
            get
            {
                return System.Convert.ToInt32(TileWidth * Zoom);
            }
        }

        public double _Rotation;

        public double Rotation
        {
            set
            {
                _Rotation = value;

                var z = System.Convert.ToInt32(16 * value / (System.Math.PI * 2) - (0.7 / 16));


                this.Rotation16 = z + 12;
            }
            get
            {
                return _Rotation;
            }
        }
        private int _Rotation16;

        public int Rotation16
        {
            get { return _Rotation16; }
            set { _Rotation16 = value; SetRotation16(value); }
        }


        void SetRotation16(int i)
        {
            var x = i % 16;

            if (x < 0)
                x = 16 + x;

            if (x < 8)
                SetClipping(x, 0);
            else
                SetClipping(x - 8, 1);
        }

        void SetClipping(int x, int y)
        {
            var tw = System.Convert.ToInt32(64 * Zoom);
            var th = System.Convert.ToInt32(128 * Zoom);

            var _x = x * tw;
            var _y = y * th;
            var _w = (x + 1) * tw;
            var _h = (y + 1) * th;

            Idle.style.SetLocation(-_x, -_y);
            Walk1.style.SetLocation(-_x, -_y);
            Walk2.style.SetLocation(-_x, -_y);

            // top, right, bottom, left
            var clip = "rect(" + _y + "px, " + _x + "px, " + _h + "px, " + _w + "px)"; ;

            Idle.style.clip = clip;
            Walk1.style.clip = clip;
            Walk2.style.clip = clip;
        }

        public double RawSpeed = 7;

        public double CurrentSpeed
        {
            get
            {
                return this.RawSpeed * this.Zoom;
            }
        }



        public event Action<Dude> DoneWalking;
        public event Action<Dude> Walk;

        public Point TargetLocation;

        Timer WalkTimer;

        private bool _IsWalking;

        public bool IsWalking
        {
            get { return _IsWalking; }
            set
            {
                _IsWalking = value;

                if (value)
                {

                    if (WalkTimer == null)
                    {
                        WalkTimer = new Timer(
                            t =>
                            {

                                if ((t.Counter % 2) == 0)
                                {
                                    this.Control.className = "walk1";
                                    //Walk1.style.display = IStyle.DisplayEnum.none;
                                    //Walk2.style.display = IStyle.DisplayEnum.block;
                                }
                                else
                                {
                                    this.Control.className = "walk2";
                                    //Walk1.style.display = IStyle.DisplayEnum.block;
                                    //Walk2.style.display = IStyle.DisplayEnum.none;
                                }


                                if (Walk != null)
                                    Walk(this);

                            }
                        );
                    }

                    this.Control.className = "walk1";

                    //Idle.style.display = IStyle.DisplayEnum.none;
                    //Walk1.style.display = IStyle.DisplayEnum.block;
                    //Walk2.style.display = IStyle.DisplayEnum.none;

                    WalkTimer.StartInterval(100);


                }
                else
                {
                    if (WalkTimer != null)
                    {
                        WalkTimer.Stop();
                    }

                    this.Control.className = "idle";

                    //Idle.style.display = IStyle.DisplayEnum.block;
                    //Walk1.style.display = IStyle.DisplayEnum.none;
                    //Walk2.style.display = IStyle.DisplayEnum.none;
                }
            }
        }

        public Point CurrentLocation
        {
            get
            {
                return new Point(
                           System.Convert.ToInt32(this.X),
                           System.Convert.ToInt32(this.Y)
                       );
            }
        }
        public double X { get; private set; }
        public double Y { get; private set; }

        public Func<double, double> ZoomFunc;

        public void TeleportToArc(double z, double a)
        {
            this.TeleportTo(
                this.X + System.Math.Cos(a) * z,
                this.Y + System.Math.Sin(a) * z
                );
        }

        public event Action<Dude> AfterTeleport;

        public void TeleportTo(double x, double y)
        {
            if (this.ZoomFunc != null)
                this.Zoom = this.ZoomFunc(y);

            this.X = x;
            this.Y = y;

            this.Control.style.SetLocation(
                System.Convert.ToInt32(x - Width / 2),
                System.Convert.ToInt32(y - Height + ShadowHeight / 2),

                Width, Height);

            this.Control.style.zIndex = System.Convert.ToInt32(y);

            if (AfterTeleport != null)
                AfterTeleport(this);
        }

        public void WalkTo(Point point)
        {
            LookAt(point);
            this.IsWalking = true;
        }

        public void LookAt(Point point)
        {
            this.TargetLocation = point;
            this.Rotation = this.TargetLocation.GetAngle(this.X, this.Y);
        }





    }

}