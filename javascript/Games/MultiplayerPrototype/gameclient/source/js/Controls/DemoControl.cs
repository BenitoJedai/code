using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace gameclient.source.js.Controls
{
    using shared;

    [Script]
    class UnitCache
    {
        public int Width;
        public int Height;

        public int Length
        {
            get
            {
                return Cache.Length;
            }
        }
        public IHTMLImage this[int i]
        {
            get
            {
                return Cache[i];
            }
        }

        IHTMLImage[] Cache;

        public void LoadCache(string fx, int min, int max)
        {
            this.Cache = new IHTMLImage[max - min + 1];

            for (int i = min; i < max; i++)
            {
                this.Cache[i - min] = new IHTMLImage("fx/" + fx + "/" + i + ".png");
            }
        }

        public static UnitCache Of(string fx, int min, int max, int w, int h, EventHandler<UnitCache> done)
        {
            var u = new UnitCache();

            u.LoadCache(fx, min, max);
            u.Width = w;
            u.Height = h;

            u.WhenCacheLoaded(done);

            return u;
        }

        public void WhenCacheLoaded(EventHandler<UnitCache> p)
        {
            if (p == null)
                return;

            Timer.Interval(
                delegate(Timer t)
                {
                    bool b = true;
                    for (int i = 0; i < this.Cache.Length; i++)
                    {
                        if (!this.Cache[i].complete)
                        {
                            b = false;
                            break;
                        }
                    }

                    if (b)
                    {
                        t.Stop();
                        p(this);
                    }
                }
            , 50);

        }
    }

    [Script]
    class Unit
    {
        public UnitCache Cache;

        public bool OnlyOnce;
        public bool ReverseAnimation;

        public readonly IHTMLDiv Control = new IHTMLDiv();

        public Unit()
        {

        }

        public static Unit Of(UnitCache c, int x, int y, Timer t, int skip)
        {
            var u = new Unit();

            u.Cache = c;
            u.Moveto(x, y);

            int s = 0;

            t.Tick += delegate
            {
                if (s % skip == 0)
                {
                    s = 0;
                    u.Tick(t);
                }

                s++;
            };

            return u;
        }

        public void Moveto(int x, int y)
        {
            this.Moveto(x, y, Cache.Width, Cache.Height);
        }

        public void Moveto(int x, int y, int w, int h)
        {
            Control.AttachToDocument();
            Control.style.SetSize(w, h);
            Control.SetCenteredLocation(x, y);
        }



        int Index = 0;


        public void Tick(Timer t)
        {
            if (Index < 0)
            {
                if (Index == -1)
                {
                    this.Control.Dispose();
                    Index--;
                }

                return;
            }

            if (this.ReverseAnimation)
                this.Cache[this.Cache.Length - 1 - Index].ToBackground(this.Control);
            else
                this.Cache[Index].ToBackground(this.Control);

            Index++;

            if (Index > this.Cache.Length - 1)
            {
                if (OnlyOnce)
                {
                    Index = -1;
                }
                else
                {
                    if (this.WhenDone != null)
                    {
                        this.WhenDone();
                    }

                    Index = 0;
                }
            }
        }



        public EventHandler WhenDone;

    }



    [Script, ScriptApplicationEntryPoint]
    public class DemoControl //: SpawnControlBase
    {
        //public const string Alias = "fx.DemoControl";

        public ClientSession CurrentSession;

        static DemoControl()
        {
            typeof(DemoControl).SpawnTo(i => new DemoControl(i));
        }

        static IHTMLElement ImportStyleSheet(string url)
        {
            Console.WriteLine("importing css at " + url);

            var s = new IHTMLElement(IHTMLElement.HTMLElementEnum.link);

            s.setAttribute("rel", "stylesheet");
            s.setAttribute("type", "text/css");
            s.setAttribute("href", url);

            s.AttachToDocument();

            return s;
        }

        public DemoControl(IHTMLElement e)
        //: base(e)
        {
            //Native.DebugBreak();

            Console.EnableActiveXConsole();
            // Console.WriteLine("!!!");

            ImportStyleSheet("fx/css/cnc.css");


            var loading = new IHTMLDiv("Connecting to server...");

            loading.style.fontSize = "36pt";
            loading.style.backgroundColor = Color.Black;
            loading.style.padding = "2em";

            Native.Document.body.appendChild(loading);

            Native.Document.body.style.color = Color.White;
            Native.Document.body.style.backgroundColor = Color.Black;


            this.CurrentSession = new ClientSession();
            this.CurrentSession.Control = this;

            this.CurrentSession.MethodA("a1", "a2",
                delegate(string text)
                {
                    loading.innerHTML = "Entering lobby...";

                    this.CurrentSession.IServer_EnterLobby(
                        delegate(string str)
                        {
                            this.CurrentSession.ClientName = str;

                            Native.Document.title = str;

                            Console.WriteLine("i am: " + str);

                            loading.innerHTML = "Loading gfx... " + text;

                            new IHTMLImage("fx/gfx/logo/636.jpg").InvokeOnComplete(
                                delegate(IHTMLImage bg)
                                {
                                    var bs = Native.Document.body.style;

                                    bs.backgroundImage = "url(" + bg.src + ")";
                                    bs.backgroundRepeat = "no-repeat";


                                    Setup(
                                        delegate
                                        {

                                            loading.innerHTML = "ready!";
                                            loading.Dispose();








                                        }

                                            );
                                }
                                    );
                        });

                });
        }


        UnitCache building_1 = null;
        UnitCache building_3 = null;
        UnitCache building_4 = null;
        UnitCache explosion_1 = null;

        Timer t = null;

        private void Setup(EventHandler done)
        {
            t = new Timer();

            var text = new IHTMLInput(HTMLInputTypeEnum.text);

            text.className = "TalkToOthers";

            text.AttachToDocument();

            text.onkeypress += delegate(IEvent x)
            {
                if (x.IsReturn)
                {
                    this.CurrentSession.IServer_TalkToOthers(text.value);
                    this.DisplayNotification(text.value, Color.Blue);

                    text.value = "";
                }
            };

            text.style.zIndex = 1000;

            //Native.Document.body.DisableContextMenu();

            //CreateRotatingTank(96 + 48 * 7, 96 + 48 * 1, t, "tree_1", 383, 392);
            //CreateRotatingTank(96 + 48 * 1, 96 + 48 * 2, t, "tank_1", 308, 339);

            int u = 7;
            EventHandler idone = null;

            EventHandler<EventHandler> adone = delegate(EventHandler x)
            {
                if (x != null)
                    idone += x;
                u--;

                if (u == 0)
                {
                    if (idone != null)
                    {
                        idone();
                        idone = null;
                    }

                    //done();

                    //t.StartInterval(100);
                }
            };

            done();
            t.StartInterval(100);

            UnitCache.Of("harvester_1", 71, 71 + 31, 48, 48,
                delegate(UnitCache c)
                {
                    adone(delegate
                    {
                        for (int i = 1; i < 10; i++)
                        {
                            Unit.Of(c, 32 * 7, 24 * i, t, 5);
                        }
                    });
                }
            );


            building_1 = UnitCache.Of("building_1", 365, 365 + 17, 72, 72,
                delegate(UnitCache c)
                {

                    adone(delegate
                    {
                        for (int i = 1; i < 5; i++)
                        {
                            var xu = Unit.Of(c, 32 * 14, 72 * i, t, 2);

                            EventHandler To3 = null;
                            EventHandler To4 = null;
                            EventHandler To1r = null;
                            EventHandler To1 = null;

                            To3 = delegate
                            {

                                xu.Cache = building_3;
                                xu.WhenDone = To4;

                            };


                            To1r = delegate
                            {
                                xu.ReverseAnimation = true;
                                xu.Cache = building_1;
                                xu.WhenDone = To1;

                            };

                            To1 = delegate
                           {
                               xu.ReverseAnimation = false;
                               xu.Cache = building_1;
                               xu.WhenDone = To3;

                           };


                            To4 = delegate
                               {
                                   xu.Cache = building_4;
                                   xu.WhenDone = To1r;

                               };

                            xu.WhenDone = To3;
                        }
                    });
                }
            );

            UnitCache.Of("building_2", 1252, 1252 + 15, 48, 48,
                delegate(UnitCache c)
                {
                    adone(delegate
                    {
                        for (int i = 1; i < 10; i++)
                        {
                            Unit.Of(c, 32 * 17, 48 * i, t, 6);

                        }
                    });
                }
            );

            building_4 = UnitCache.Of("building_4", 405, 405 + 17, 72, 72, uc => adone(null));

            building_3 = UnitCache.Of("building_3", 393, 393 + 11, 72, 72, uc => adone(null));

            UnitCache.Of("tank_2", 308, 308 + 31, 24, 24,
               delegate(UnitCache c)
               {
                   adone(delegate
                   {
                       for (int i = 1; i < 10; i++)
                       {
                           Unit.Of(c, 32 * 3, 24 * i, t, 3);
                       }
                   });
               }
            );

            explosion_1 = UnitCache.Of("explosion_1", 990, 1015, 78, 121,
               delegate(UnitCache c)
               {
                   adone(delegate
                   {


                       Native.Document.body.onclick +=
                           delegate(IEvent ev)
                           {
                               int cx = ev.CursorX;
                               int cy = ev.CursorY;

                               UserCreateExplosion(cx, cy);
                           };
                   });
               }
            );

            new IHTMLImage("fx/building/power.png").InvokeOnComplete(
                delegate(IHTMLImage img)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        SpawnAnimation(img, 50 + 48 * i, 200, 48, 72, 0, 23, t);

                    }


                }
            );


            new IHTMLImage("fx/building/cy.png").InvokeOnComplete(
                delegate(IHTMLImage img)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        SpawnAnimation(img, 50 + 72 * i, 300, 72, 72, 0, 34, t);

                    }


                }
            );

            new IHTMLImage("fx/building/barrack.png").InvokeOnComplete(
                    delegate(IHTMLImage img)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            SpawnAnimation(img, 280 + 48 * i, 300, 48, 72, 0, 46, t);

                        }


                    }
                );


            new IHTMLImage("fx/vehicle/veh_cy.png").InvokeOnComplete(
                delegate(IHTMLImage img)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        SpawnAnimation(img, 50 + 48 * i, 400, 48, 48, 0, 31, t);
                    }

                }
            );

            //.InvokeOnComplete(
            //    delegate(IHTMLImage img)
            //    {
            //        this.Explosion = img;

            //        for (int i = 0; i < 3; i++)
            //        {
            //            SpawnAnimation(img, 250 + 48 * i, 200, 78, 121, 0, 25, t);
            //        }

            //    }
            //);
        }

        public IHTMLImage Explosion = new IHTMLImage("fx/explosion/explosion.png");

        void SpawnAnimation(IHTMLImage img, int x, int y, int w, int h, int from, int to, Timer t)
        {
            var div = new IHTMLDiv();
            var animate = true;

            div.style.SetSize(w, h);
            div.style.backgroundRepeat = "no-repeat";
            div.style.backgroundImage = "url(" + img.src + ")";

            div.SetCenteredLocation(x, y);
            Native.Document.body.appendChild(div);

            div.onmouseover += delegate { animate = false; };
            div.onmouseout += delegate { animate = true; };

            var frame = from;

            t.Tick += delegate
            {
                if (!animate)
                    return;

                frame++;

                if (frame > to)
                    frame = from;

                div.style.backgroundPosition = -(frame * w) + "px 0px";
            };
        }

        private void UserCreateExplosion(int cx, int cy)
        {
            DrawExplosion(cx, cy);

            var str = "explosion: " + new Point(cx, cy);

            Console.WriteLine(str);

            CurrentSession.CreateExplosionAt(cx, cy,
                delegate
                {
                    Console.WriteLine("did it get broadcasted?");
                }
            );


        }

        public void DrawExplosion(int cx, int cy)
        {
            SpawnAnimation(this.Explosion, cx, cy, 78, 121, 0, 25, t);
            // Unit.Of(explosion_1, cx, cy, t, 1).OnlyOnce = true;
        }



        // _1483b5833155c53585239c5e871e940c_600000c	2496	93.55%	871.254ms	1041.498ms	0.417ms

        public void DisplayNotification(string text, Color color)
        {
            var x = new IHTMLDiv(new ITextNode(text));

            x.style.color = color;

            x.style.backgroundColor = Color.Black;

            x.style.padding = "1em";
            x.style.zIndex = 1000;

            x.AttachToDocument();

            new Timer(
                delegate
                {
                    x.Dispose();
                }, 15000, 0);


        }
    }


}
