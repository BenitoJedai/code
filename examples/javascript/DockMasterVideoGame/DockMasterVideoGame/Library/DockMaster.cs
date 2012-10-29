
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using System;
using DockMasterVideoGame.HTML.Images.FromAssets;


namespace DockMaster.source.js
{
    public class DockMaster
    {

        internal class Kast
        {
            public IHTMLImage pilt = new kast();

            public void Attach()
            {
                pilt.style.cursor = IStyle.CursorEnum.pointer;

                Native.Document.body.appendChild(pilt);
            }

            public Point Location;

            public void MoveTo(int x, int y)
            {
                Location = new Point(x, y);

                pilt.SetCenteredLocation(x, y);
            }
        }

        internal class Magnet
        {
            public IHTMLImage piltm = new magnet();
            public IHTMLImage piltk = new k6is();

            public IHTMLElement k6is = new IHTMLDiv();

            public Rectangle Bounds = new Rectangle();

            public void Attach(int x, int y)
            {
                k6is.AttachToDocument();
                piltm.AttachToDocument();

                new k6is().ToBackground(k6is.style);
                k6is.style.backgroundRepeat = "repeat-y";

                MoveTo(x, y);


                Native.Document.onkeydown += Native.DisabledEventHandler;
                Native.Document.onkeydown += new Action<IEvent>(Document_onkeypress);
            }

            int x;
            int y;


            void MoveTo(int x, int y)
            {
                this.x = x;
                this.y = y;

                if (this.x > Bounds.Right) this.x = Bounds.Right;
                if (this.x < Bounds.Left) this.x = Bounds.Left;

                if (this.y < Bounds.Top) this.y = Bounds.Top;
                if (this.y > Bounds.Bottom) this.y = Bounds.Bottom;

                k6is.style.SetLocation(x, Bounds.Top, piltk.width, y - Bounds.Top - 15);
                piltm.SetCenteredLocation(x, y);
            }

            void Document_onkeypress(IEvent e)
            {
                e.PreventDefault();

                if (e.KeyCode == 39) // right
                {
                    MoveTo(x + 3, y);
                }

                if (e.KeyCode == 37) // left
                {
                    MoveTo(x - 3, y);

                }


                if (e.KeyCode == 40) // down
                {
                    MoveTo(x, y + 3);

                }

                if (e.KeyCode == 38) // up
                {
                    MoveTo(x, y - 3);

                }

                if (e.KeyCode == 32) // space
                {

                }

                // Native.Document.title = e.KeyCode + "";

            }
        }

        internal class Kraana
        {
            public IHTMLImage piltx = new kraanax();
            public IHTMLImage pilty = new kraanay();
            public IHTMLImage piltkabiin = new kraanakabiin();

            public Magnet magnet = new Magnet();

            public void Attach(int x, int y, int bottom)
            {

                Native.Document.body.appendChild(piltx, pilty, piltkabiin);

                piltx.style.SetLocation(x, y);

                piltkabiin.style.SetLocation(x + 22, y + 90);

                magnet.Bounds.Left = x - 30;
                magnet.Bounds.Right = x + pilty.width - 90;
                magnet.Bounds.Top = y + pilty.height + 40;
                magnet.Bounds.Bottom = bottom;

                magnet.Attach(x + 200, y + 120);

                pilty.style.SetLocation(x - 60, y + 50);
                pilty.style.zIndex = 100;
            }
        }

        internal class Sadam
        {
            Kraana kraana = new Kraana();

            IHTMLImage pilt = new sadam();

            public void Attach(int x, int y, int bottom)
            {
                kraana.Attach(80, y - 401, bottom);


                Native.Document.body.appendChild(pilt);

                pilt.style.SetLocation(x, y);
                pilt.style.left = "-80px";


            }
        }

        internal class Meri
        {

            IHTMLImage pilt = new meri();

            const string srckalar = "assets/DockMaster/kalar.gif";

            IHTMLImage piltkala = new kala();



            Timer t = new Timer();

            IHTMLElement laine = new IHTMLDiv();
            IHTMLElement antilaine = new IHTMLDiv();

            IHTMLElement vesi = new IHTMLDiv();

            public Meri()
            {
                t.Tick += new Action<Timer>(t_Tick);
                t.StartInterval(70);
            }

            void t_Tick(Timer e)
            {
                laine.style.left = ((t.Counter % ViewWidth) - ViewWidth) + "px";
                antilaine.style.left = (-t.Counter % ViewWidth) + "px";

                if (System.Math.Abs(kalax - cursor.X) > 32)
                {
                    if (kalax > cursor.X)
                    {
                        piltkala.src = new kala().src;
                        kalax--;
                    }
                    else
                    {
                        piltkala.src = new kalar().src;
                        kalax++;

                    }

                    piltkala.style.left = kalax + "px";
                }
            }

            public int ViewWidth
            {
                get { return Native.Document.body.clientWidth; }
            }

            int kalax = 100;

            public void Attach(int y)
            {


                Native.Document.onmousemove += new Action<IEvent>(Document_onmousemove);
                Native.Document.body.appendChild(antilaine, piltkala, laine, vesi);

                new meri().ToBackground(laine.style);

                laine.style.backgroundRepeat = "repeat-x";
                laine.style.Opacity = 0.8;

                new meri().ToBackground(antilaine.style);

                antilaine.style.backgroundRepeat = "repeat-x";
                vesi.style.background = "#1c00c4";

                laine.style.SetLocation(-ViewWidth, y - 5, ViewWidth * 2, 70);

                piltkala.style.SetLocation(kalax, y - 10);

                antilaine.style.SetLocation(0, y + 5, ViewWidth * 2, 70);

                try
                {
                    vesi.style.SetLocation(0, y + 70, Native.Document.body.clientWidth, Native.Document.body.clientHeight - (y + 70));
                }
                catch
                {
                }


            }

            Point cursor = new Point(0, 0);

            void Document_onmousemove(IEvent e)
            {

                cursor = e.CursorPosition;
            }
        }

        internal class Laev
        {
            IHTMLImage pilt = new laev();

            Timer t = new Timer();

            public Laev()
            {
                t.Tick += new Action<Timer>(t_Tick);
                t.StartInterval(100);




            }

            void t_Tick(Timer e)
            {
                if (bAnimate)
                {
                    x--;


                    if (x < xto)
                        bAnimate = false;
                    else
                        this.MoveShip();

                }
            }

            private void MoveShip()
            {
                k.MoveTo(x + 40, y + 40);

                pilt.style.SetLocation(x, y);
            }

            bool bAnimate;

            int xfrom;
            int xto;
            int y;
            int x;

            public void Animate(int y, int xfrom, int xto)
            {
                bAnimate = true;
                this.xfrom = xfrom;
                this.xto = xto;
                this.y = y;
                this.x = xfrom;
            }

            public int ViewWidth
            {
                get { return Native.Document.body.clientWidth; }
            }

            Kast k = new Kast();

            public void Attach()
            {

                Native.Document.body.appendChild(pilt);

                pilt.style.SetLocation(ViewWidth, 400);




                k.Attach();

            }
        }


        public DockMaster()
        {
            ImageResources.Default.WaitUntilLoaded(Window_onload);
        }


        void Window_onload()
        {
            new taust().ToBackground(Native.Document.body.style);

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.Document.body.Clear();

            Native.Document.oncontextmenu += Native.DisabledEventHandler;

            Laev x = new Laev();

            x.Animate(400, Native.Document.body.clientWidth, 400);
            x.Attach();


            Sadam s = new Sadam();

            s.Attach(-80, 450, 500);

            Meri m = new Meri();

            m.Attach(500);

            Kast k = new Kast();

            k.MoveTo(150, 435);
            k.Attach();



        }



    }

    public class ImageResources
    {

        public IHTMLImage[] Images
        {
            get
            {
                return new DockMasterVideoGame.HTML.Pages.TexturesImages().Images;

            }
        }

        public void WaitUntilLoaded(Action e)
        {
            new Timer(
                t =>
                {
                    bool r = false;

                    foreach (IHTMLImage v in Images)
                    {
                        if (!v.complete)
                        {
                            System.Console.WriteLine("not loaded: " + v.src);

                            r = true;
                            break;
                        }
                    }


                    if (!r)
                    {
                        t.Stop();

                        e();
                    }

                }, 0, 300);
        }

        static ImageResources _Default;

        public static ImageResources Default
        {
            get
            {
                if (_Default == null)
                    _Default = new ImageResources();

                return _Default;
            }
        }
    }

}

