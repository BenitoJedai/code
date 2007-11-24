using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace gameclient.source.js.Controls
{
    using shared;
    using System.Collections.Generic;

    [Script]
    class __Type1
    {
        public double x;
        public double y;
    }

    [Script]
    public class MySession : ClientToServerBase, Message.IClient
    {
        #region IClient Members

        public void CreateExplosionByServer(int x, int y, string text)
        {

        }


        // implement interface member as an event
        public event EventHandler<Message._IClient_DisplayNotification> OnIClient_DisplayNotification;

        public void IClient_DisplayNotification(string text, int color)
        {
            var p = new Message._IClient_DisplayNotification { text = text, color = color };

            Helper.Invoke(OnIClient_DisplayNotification, p);
        }

        public void ForceReload()
        {

        }

        #endregion

        // implement interface member as an event
        public event EventHandler<Message._IClient_DrawRectangle> OnIClient_DrawRectangle;

        public void IClient_DrawRectangle(RectangleInfo rect, int color)
        {
            var p = new Message._IClient_DrawRectangle { rect = rect, color = color };

            Helper.Invoke(OnIClient_DrawRectangle, p);
        }
    }

    [Script]
    public class TimerEvent
    {
        public long Interval = 50;

        /// <summary>
        /// -1 means forever
        /// </summary>
        public long TimeToLive = -1;

        public TimerEvent()
        {

        }

        public static TimerEvent DelayOnce(long Interval)
        {
            return new TimerEvent
                   {
                       Interval = Interval,
                       TimeToLive = 1
                   };
        }
    }

    [Script]
    public class TimerEventInfo
    {
        public long Trigger;

        public TimerEvent Settings;

        public EventHandler<TimerEvent> Handler;
    }


    [Script]
    public class MasterTimer
    {
        readonly Timer timer = new Timer();

        public long CurrentTicks
        {
            get
            {
                return IDate.Now.getTime();
            }
        }

        public MasterTimer(int interval)
        {
            this.timer.StartInterval(interval);
            this.timer.Tick +=
                delegate(Timer timer)
                {
                    var n = CurrentTicks;
                    var a = events.ToArray();

                    foreach (TimerEventInfo v in a)
                    {
                        if (v.Trigger <= n)
                        {
                            if (v.Settings.TimeToLive > 0)
                            {
                                v.Settings.TimeToLive--;
                            }

                            Helper.Invoke(v.Handler, v.Settings);

                            if (v.Settings.TimeToLive == 0)
                            {
                                events.Remove(v);
                            }

                            v.Trigger = n + v.Settings.Interval;
                        }
                    }
                };
        }

        readonly List<TimerEventInfo> events = new List<TimerEventInfo>();


        public EventHandler<TimerEvent> this[TimerEvent e]
        {
            set
            {
                var z = new TimerEventInfo
                        {
                            Trigger = CurrentTicks + e.Interval,
                            Handler = value,
                            Settings = e
                        };

                this.events.Add(z);
            }
        }
    }



    /// <summary>
    /// a building, footman, tree, helicopter
    /// </summary>
    [Script]
    class ArenaUnit
    {
        [Script]
        public class HealthInfo
        {
            public int Max;
            public int Current;
        }

        public readonly HealthInfo Health = new HealthInfo();

        public readonly IHTMLDiv Control = new IHTMLDiv();

        public ArenaUnit()
        {
            Control.style.backgroundColor = Color.Yellow;

        }

        public double Direction;
        public double HullDirection;

        public ArenaInfo Owner;

        public Color Color;

        #region selection stuff
        /// <summary>
        /// trees are not selectable, or are they?
        /// </summary>
        public bool IsSelectable;

        private bool _IsSelected;

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;

                if (_IsSelected == true)
                {
                    this.Control.style.border = "1px dotted white";
                }
                else
                {
                    this.Control.style.border = "0";
                }
            }
        }

        #endregion


        public string Title;

        public Point CurrentLocation = Point.Zero;

        public void SetLocation(Point p)
        {
            this.CurrentLocation = p;
        }

        public Point CurrentSize = Point.Zero;

        public void SetSize(Point p)
        {
            this.CurrentSize = p;
        }

        public void UpdateControlLocation()
        {
            this.Control.style.SetLocation(this.Bounds);
        }

        public Rectangle Bounds
        {
            get
            {

                return Rectangle.Of(
                        this.CurrentLocation.X - this.CurrentSize.X / 2,
                        this.CurrentLocation.Y - this.CurrentSize.Y / 2,
                        this.CurrentSize.X,
                        this.CurrentSize.Y
                    );
            }
        }
    }

    [Script]
    class ArenaInfo
    {
        public ArenaControl Arena;
        public ArenaMinimapControl Minimap;

        public readonly List<ArenaUnit> Units = new List<ArenaUnit>();

        public void DrawToMinimap()
        {
            foreach (ArenaUnit v in Units.ToArray())
            {
                Minimap.DrawRectangleToCanvas(
                    v.Bounds * Minimap.Zoom.Value
                    ,
                    v.Color
                );
            }
        }

        public ArenaUnit SingleOrDefault(Point p)
        {
            ArenaUnit u = null;

            foreach (ArenaUnit v in this.Units.ToArray())
            {
                if (v.Bounds.Contains(p))
                {
                    u = v;
                    break;
                }

            }

            return u;
        }

        public void SelectUnits(Point p)
        {
            foreach (ArenaUnit v in this.Units.ToArray())
            {
                if (v.IsSelectable)
                {
                    v.IsSelected = v.Bounds.Contains(p);
                }

            }

        }

        public void SelectUnits(Rectangle p)
        {
            foreach (ArenaUnit v in this.Units.ToArray())
            {
                if (v.IsSelectable)
                {
                    v.IsSelected = p.Contains(v.CurrentLocation);
                }

            }

        }
    }

    [Script, ScriptApplicationEntryPoint]
    public class Program
    {
        //public const string Alias = "fx.Program";

        public readonly MySession Session;



        public readonly MasterTimer SessionTimer = new MasterTimer(50);

        // on firefox:
        // menu/options/advanced/general/browsing/use autoscrolling = false
        // autoscrolling: false
        static Program()
        {
            typeof(Program).SpawnTo(i => new Program(i));
        }

        public Program(IHTMLElement placeholder)
        {
            // http://www.howtocreate.co.uk/tutorials/javascript/browserwindow
            // http://www.quirksmode.org/js/doctypes.html
            // http://www.evolt.org/article/document_body_doctype_switching_and_more/17/30655/index.html

            // we do not want to see those scrollbars
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;


            Console.EnableActiveXConsole();


            Session = new MySession();

            this.Session.IServer_EnterLobby(
                delegate(string e)
                {
                    var TheWorld = new MyGameWorld();

                    #region supporting user chat

                    var ChatBox = new LayeredTextBox();

                    Native.Document.onkeypress +=
                        delegate(IEvent ev)
                        {
                            if (ev.KeyCode == 'q') Console.Log("Q");
                            if (ev.KeyCode == 'w') Console.Log("W");
                            if (ev.KeyCode == 'e') Console.Log("E");
                            if (ev.KeyCode == 'r')
                            {
                                var random_spawn_position = new __Type1
                                                            {
                                                                x = new System.Random().NextDouble() * 600,
                                                                y = new System.Random().NextDouble() * 400,
                                                            };

                                Console.Log("random_spawn_position: " + random_spawn_position);

                                // Lets Spawn Something into the world
                            }

                            //Console.Log("onkeypress: " + new { KeyCode = ev.KeyCode });

                            if (!ChatBox.IsVisible)
                            {
                                if (ev.KeyCode == 't')
                                {
                                    ev.PreventDefault();

                                    Timer.DoAsync(
                                        delegate
                                        {
                                            ChatBox.ShowAndFocus();
                                        }
                                    );
                                }
                            }
                        };

                    #endregion

                    this.Session.ClientName = e;


                    var a = new ArenaControl();
                    var m = new ArenaMinimapControl();

                    a.Control.AttachToDocument();

                    a.Layers.Canvas.style.backgroundColor = Color.FromRGB(0, 0x80, 0);

                    // set the map to be somewhere left
                    a.SetLocation(Rectangle.Of(32, 32, 640, 480));

    
                    // set tha map canvas size to be something big
                    a.SetCanvasSize(new Point(8000, 8000));

                    #region DrawTextWithTimeout
                    EventHandler<string, Color> DrawTextWithTimeout =
                                   delegate(string text, Color color)
                                   {
                                       var z = new IHTMLDiv(new ITextNode(text));

                                       z.style.color = color;
                                       z.style.backgroundColor = Color.Black;

                                       a.Layers.Info.appendChild(z);

                                       this.SessionTimer[TimerEvent.DelayOnce(9000)] =
                                           delegate
                                           {
                                               z.Dispose();
                                           };
                                   }; 
                    #endregion

                    this.SessionTimer[TimerEvent.DelayOnce(1000)] =
                       delegate
                       {
                           a.DrawTextToInfo("just some data", new Point(46, 246), Color.Black);
                           a.DrawTextToInfo("just some data", new Point(45, 245), Color.Yellow);

                           DrawTextWithTimeout("hello world", Color.Red);
                       };



                    var data = new List<Pair<Rectangle, Color>>();

                    #region minimap


                    m.Zoom.Validate += delegate
                    {
                        if (a.CurrentCanvasSize.X > a.CurrentCanvasSize.Y)
                        {
                            var w = m.CurrentLocation.Width / a.CurrentCanvasSize.X;

                            if (m.Zoom.Value < w)
                                m.Zoom.Value = w;
                        }
                        else
                        {
                            var h = m.CurrentLocation.Height / a.CurrentCanvasSize.Y;

                            if (m.Zoom.Value < h)
                                m.Zoom.Value = h;
                        }
                    };


                    m.Zoom.Changed += delegate
                    {
                        m.Layers.Canvas.removeChildren();

                        m.SetCanvasSize(a.CurrentCanvasSize * m.Zoom.Value);
                        m.SetSelectionLocation(a.CanvasView * m.Zoom.Value);
                        m.MakeSelectionVisible();


                        var data_array = data.ToArray();

                        foreach (var v in data_array)
                        {
                            var r = v.A;
                            var c = v.B;

                            m.DrawRectangleToCanvas(r * m.Zoom.Value, c);
                        }
                    };

                    m.Control.AttachToDocument();

                    m.SetLocation(Rectangle.Of(690, 50, 200, 200));


                    #endregion


                    EventHandler<Rectangle, Color> DrawRectangleLocal =
                        delegate(Rectangle r, Color c)
                        {
                            var p = new Pair<Rectangle, Color>(r, c);

                            data.Add(p);

                            a.DrawRectangleToCanvas(r, c);
                            m.DrawRectangleToCanvas(r * m.Zoom.Value, c);
                        };

                    EventHandler<Rectangle, Color> DrawRectangle =
                        delegate(Rectangle r, Color c)
                        {
                            DrawRectangleLocal(r, c);

                            this.Session.IServer_DrawRectangle(r, c);
                        };


                    this.Session.OnIClient_DrawRectangle += delegate(Message._IClient_DrawRectangle p)
                    {
                        var r = new Rectangle
                                {
                                    Left = p.rect.Left,
                                    Top = p.rect.Top,
                                    Width = p.rect.Width,
                                    Height = p.rect.Height,
                                };

                        DrawRectangleLocal(r, p.color);
                    };


                    a.SelectionClick += delegate(Point p, IEvent ev)
                    {
                        Console.Log("SelectionClick_1");

                        if (ev.ctrlKey)
                        {
                            DrawRectangle(p.WithMargin(a.SelectionMinimumSize * 2), Color.Green);
                        }

                    };


                    a.ApplySelection += delegate(Rectangle r, IEvent ev)
                    {
                        if (ev.ctrlKey)
                        {
                            DrawRectangle(r, RandomColor);
                        }
                    };

                    a.CanvasViewChanged += delegate(Rectangle p)
                    {
                        m.SetSelectionLocation(p * m.Zoom.Value);
                        m.MakeSelectionVisible();
                    };

                    a.SetCanvasPosition(Point.Zero);

                    m.SelectionCenterChanged += delegate(Point p)
                    {
                        a.SetCanvasViewCenter(p / m.Zoom.Value);
                    };

                    m.Zoom.SetValue(0);

                    //chatbox.style.SetLocation(32, 480 - 32, 650, 22);
                    //chatbox.style.height = "1em";
                    //chatbox.attachToDocument();



                    ChatBox.SetLocation(Rectangle.Of(0, 440, 640, 22));
                    a.Layers.Info.appendChild(ChatBox.Control);

                    ChatBox.Layers.Canvas.style.backgroundColor = Color.Yellow;
                    ChatBox.Layers.Canvas.style.Opacity = 0.8;

                    ChatBox.Layers.Text.style.color = Color.White;

                    ChatBox.Hide();



                    ChatBox.Send += delegate(string text)
                    {
                        DrawTextWithTimeout(text, Color.White);

                        this.Session.IServer_TalkToOthers(text);
                    };



                    this.Session.OnIClient_DisplayNotification += x => DrawTextWithTimeout(x.text, x.color);


                    // put some elements on the canvas
                    //DrawRectangleLocal(Rectangle.Of(48, 48, 128, 64), Color.Green);
                    //DrawRectangleLocal(Rectangle.Of(48, 128, 128, 64), Color.Gray);
                    //DrawRectangleLocal(Rectangle.Of(400, 300, 128, 64), Color.Black);
                    //DrawRectangleLocal(Rectangle.Of(400, 500, 128, 64), 0xff5566);
                    //DrawRectangleLocal(Rectangle.Of(700, 600, 128, 64), 0x3f5466);


                    #region game units

                    var ai = new ArenaInfo();

                    ai.Arena = a;
                    ai.Minimap = m;

                    {
                        var mcy = new ArenaUnit();

                        mcy.Color = Color.Red;
                        mcy.Owner = ai;
                        mcy.Title = "My Construction Yard Unit";


                        mcy.SetLocation(new Point(64, 64));
                        mcy.SetSize(new Point(72, 72));

                        // put the unit on the canvas
                        ai.Arena.Layers.Canvas.appendChild(mcy.Control);

                        // update its location
                        mcy.UpdateControlLocation();

                        // remeber that we have such a unit
                        ai.Units.Add(mcy);
                    }



                    {

                        var mcy = new ArenaUnit();

                        mcy.Health.Max = 2000;
                        mcy.Health.Current = 1500;


                        mcy.Color = Color.Red;
                        mcy.Owner = ai;
                        mcy.Title = "My Construction Yard Unit 3";

                        mcy.IsSelectable = true;


                        mcy.SetLocation(new Point(250, 200));
                        var mfx = fx.Settings.ConstructionYard;

                        mcy.SetSize(mfx.Size);

                        mfx.ShowFrame(mcy.Control, 12);


                        // put the unit on the canvas
                        ai.Arena.Layers.Canvas.appendChild(mcy.Control);

                        // update its location
                        mcy.UpdateControlLocation();

                        // remeber that we have such a unit
                        ai.Units.Add(mcy);
                    }


                    {

                        var mcy = new ArenaUnit();

                        mcy.Health.Max = 2000;
                        mcy.Health.Current = 1500;


                        mcy.Color = Color.Red;
                        mcy.Owner = ai;
                        mcy.Title = "My Construction Yard Unit 3";

                        mcy.IsSelectable = true;


                        mcy.SetLocation(new Point(250, 300));
                        var mfx = fx.Settings.ConstructionYard;

                        mcy.SetSize(mfx.Size);

                        mfx.ShowFrame(mcy.Control, 12);


                        // put the unit on the canvas
                        ai.Arena.Layers.Canvas.appendChild(mcy.Control);

                        // update its location
                        mcy.UpdateControlLocation();

                        // remeber that we have such a unit
                        ai.Units.Add(mcy);
                    }

                    m.Zoom.Changed +=
                        delegate
                        {
                            // redraw all units to the minimap
                            ai.DrawToMinimap();
                        };

                    #endregion

                    a.SelectionClick +=
                        delegate(Point p, IEvent ev)
                        {
                            ai.SelectUnits(p);
                        };

                    a.ApplySelection +=
                        delegate(Rectangle r, IEvent ev)
                        {
                            ai.SelectUnits(r);
                        };

                    ai.DrawToMinimap();
                }
            );
        }

        private static void Test1(ArenaControl a)
        {
            a.SelectionClick += delegate(Point p, IEvent ev)
              {
                  Console.WriteLine("Test1");
              };
        }

        private static void Test2(ArenaControl a)
        {
            a.SelectionClick += delegate(Point p, IEvent ev)
              {
                  Console.WriteLine("Test2");
              };
        }

        static Color RandomColor
        {
            get
            {
                return (Color) System.Math.Floor(new System.Random().NextDouble() * 0xFFFFFF);
            }
        }
    }
}
