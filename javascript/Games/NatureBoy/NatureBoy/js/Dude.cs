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

    [Script]
    public class Dude
    {
        public const int TileWidth = 64;
        public const int TileHeight = 128;


        public readonly div Control = new div();

        public img Shadow;
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


            Control.style.position = IStyle.PositionEnum.absolute;
            Control.style.overflow = IStyle.OverflowEnum.hidden;

            Shadow = new img("assets/NatureBoy/alpha/2.png");
            Shadow.style.position = IStyle.PositionEnum.absolute;


            Idle = new img("assets/NatureBoy/dude1/1.png") { className = "idle" };
            Idle.style.position = IStyle.PositionEnum.absolute;


            Walk1 = new img("assets/NatureBoy/dude1/3.png") { className = "walk1" };
            Walk1.style.position = IStyle.PositionEnum.absolute;

            Walk2 = new img("assets/NatureBoy/dude1/4.png") { className = "walk2" };
            Walk2.style.position = IStyle.PositionEnum.absolute;

            SetClipping(2, 1);
            IsWalking = false;
            Zoom = 1;

            Control.appendChild(Shadow, Idle, Walk1, Walk2);
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

                var w = System.Convert.ToInt32(512 * Zoom);
                var h = System.Convert.ToInt32(256 * Zoom);

                Idle.style.SetSize(w, h);
                Walk1.style.SetSize(w, h);
                Walk2.style.SetSize(w, h);

                SetRotation(_Rotation);
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

        private int _Rotation;

        public int Rotation
        {
            get { return _Rotation; }
            set { _Rotation = value; SetRotation(value); }
        }


        void SetRotation(int i)
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

        public int X { get; private set; }
        public int Y { get; private set; }

        public Func<int, double> ZoomFunc;

        public void TeleportTo(int x, int y)
        {
            if (this.ZoomFunc != null)
                this.Zoom = this.ZoomFunc(y);

            this.X = x;
            this.Y = y;

            this.Control.style.SetLocation( x - Width / 2, y - Height + ShadowHeight / 2, Width, Height);
        }
    }

}