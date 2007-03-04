using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Query;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace SpaceInvaders.source.js.Controls
{
    [Script]
    public class SpaceInvaders : SpawnControlBase
    {
        public const string Alias = "SpaceInvaders";

        // http://www.digitalinsane.com/archives/2007/01/21/space_invaders/

        public SpaceInvaders(IHTMLElement e)
            : base(e)
        {

            var view = new IHTMLDiv();

            view.style.SetSize(480, 480);
            view.style.backgroundColor = Color.Green;
            view.style.color = Color.White;
            view.style.fontFamily = IStyle.FontFamilyEnum.Fixedsys;


            Native.Document.body.appendChild(
                new IHTMLElement(IHTMLElement.HTMLElementEnum.center,
                view)
                );

            Native.Document.body.style.backgroundColor = Color.Black;
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            Func<IHTMLDiv> CreateCanvas =
                delegate
                {
                    var c = new IHTMLDiv();

                    c.style.overflow = IStyle.OverflowEnum.hidden;
                    c.style.SetLocation(1, 1, 478, 478);

                    return c;
                };

            view.style.position = IStyle.PositionEnum.relative;

            var canvas = CreateCanvas();
            var menu = CreateCanvas();

            canvas.style.backgroundColor = Color.Black;


            view.appendChild(canvas, menu);

            var msg_loading = new IHTMLDiv("loading...");

            msg_loading.style.color = Color.Green;

            menu.appendChild(msg_loading);

            // at this point we want our images


            // now wait while all images are loaded/complete
            Timer.While(
                delegate
                {
                    return Sequence.Count(
                        Sequence.Where(
                            gfx.ImageResources.Default.Images, i => !i.complete
                        )
                ) > 0;
                },
                delegate
                {
                    // loading images is done now.



                    // build the scoreboard

                    var board = new ScoreBoard();

                    board.Control.style.SetLocation(8, 8, 464, 64);

                    canvas.appendChild(board.Control);

                    board.Lives = 2;
                    board.Score = 450;

                    // now we can see lives and score.
                    // ie does not issue keypress for control keys.
                    // scriptcorelib should filter firefox events...

                    // lets show main menu

                    var mmenu = new MainMenu();
                    var gameovermenu = new GameOverMenu();

                    menu.appendChild(mmenu.Control, gameovermenu.Control);

                    gameovermenu.Visible = false;
                    gameovermenu.Control.style.SetLocation(0, 100, 468, 468 - 100);

                    mmenu.Control.style.SetLocation(0, 64, 468, 468 - 64);
                    mmenu.Visible = true;

                    var Enemy_Ammo = new AmmoInfo {
                        Color = Color.White,
                        Speed = 8
                    };

                    var Player = (IHTMLImage)gfx.ImageResources.Default.biggun.cloneNode(false);
                    var Player_Ammo = new AmmoInfo {
                        Color = Color.Green,
                        Speed = -8
                    };

                    var Map_Top = 64;
                    var Map_Left = 20;
                    var Map_Right = 450;
                    var Map_Bottom = 470;

                    var Map_Rect = new Rectangle();

                    Map_Rect.Top = Map_Top;
                    Map_Rect.Left = Map_Left;
                    Map_Rect.Right = Map_Right;
                    Map_Rect.Bottom = Map_Bottom;

                    var Player_Y = 460;
                    var Player_X = 200;

                    var Player_X_step = 8;

                    Action<int> UpdatePlayer =
                        delegate(int v)
                        {
                            Player_X += v;

                            if (Player_X < Map_Left)
                                Player_X = Map_Left;

                            if (Player_X > Map_Right)
                                Player_X = Map_Right;


                            Player.SetCenteredLocation(Player_X, Player_Y);
                            Player.style.position = IStyle.PositionEnum.absolute;
                        };

                    Player.Hide();

                    canvas.appendChild(Player, Player_Ammo.Control, Enemy_Ammo.Control);

                    AmmoInfo[] KnownAmmo = new [] { Player_Ammo, Enemy_Ammo };

                    var KnownConcrete = new List<Concrete>();
                    var ConcreteTop = 432;

                    KnownConcrete.Add(Concrete.BuildAt(new Point(62 + 120 * 0, ConcreteTop)));
                    KnownConcrete.Add(Concrete.BuildAt(new Point(62 + 120 * 1, ConcreteTop)));
                    KnownConcrete.Add(Concrete.BuildAt(new Point(62 + 120 * 2, ConcreteTop)));
                    KnownConcrete.Add(Concrete.BuildAt(new Point(62 + 120 * 3, ConcreteTop)));

                    foreach (Concrete v in KnownConcrete.ToArray())
                    {
                        canvas.appendChild(v.Control);
                    }

                    var UFO = new EnemyUnit(EnemyDirectory.Default.UFO);
                    var UFO_Direction = 1;

                    UFO.Visible = false;

                    canvas.appendChild(UFO.Control);


                    var EnemyTop = 128;
                    var EnemySpacing = 32;
                    var EnemyCount = 9;

                    var KnownEnemies = new List<EnemyUnit>();

                    KnownEnemies.Add(EnemyUnit.Build(EnemyDirectory.Default.A, 20, EnemyTop + 0 * EnemySpacing, EnemyCount, EnemySpacing));
                    KnownEnemies.Add(EnemyUnit.Build(EnemyDirectory.Default.B, 20, EnemyTop + 1 * EnemySpacing, EnemyCount, EnemySpacing));
                    KnownEnemies.Add(EnemyUnit.Build(EnemyDirectory.Default.B, 20, EnemyTop + 2 * EnemySpacing, EnemyCount, EnemySpacing));
                    KnownEnemies.Add(EnemyUnit.Build(EnemyDirectory.Default.C, 20, EnemyTop + 3 * EnemySpacing, EnemyCount, EnemySpacing));
                    KnownEnemies.Add(EnemyUnit.Build(EnemyDirectory.Default.C, 20, EnemyTop + 4 * EnemySpacing, EnemyCount, EnemySpacing));

                    foreach (EnemyUnit v in KnownEnemies.ToArray())
                    {
                        canvas.appendChild(v.Control);
                    }

                    var HitDamage = 40;

                    Timer GameTimer = new Timer();

                    int GangDirection = 1;

                    Action<string> EndGame =
                        delegate
                        {
                            gameovermenu.Visible = true;

                            GameTimer.Stop();
                        };

                    #region DoAmmoDamage
                    Func<AmmoInfo, bool> DoAmmoDamage =
                        delegate(AmmoInfo a)
                        {
                            bool hit = false;

                            #region did we hit ufo?
                            if (UFO.Visible)
                            {
                                if (UFO.Bounds.Contains(a.Location))
                                {
                                    board.Score += UFO.Info.Points;

                                    UFO.Visible = false;

                                    hit = true;
                                }
                            }
                            #endregion

                            #region did we hit player 
                            if (Player.Bounds.Contains(a.Location))
                            {
                                board.Lives--;

                                hit = true;

                                if (board.Lives < 1)
                                {
                                    EndGame("Ship destroied");

                                }
                            }
                            #endregion


                            foreach (Concrete v in KnownConcrete.ToArray())
                            {
                                if (v.Visible)
                                {
                                    if (v.Bounds.Contains(a.Location))
                                    {
                                        v.Health -= HitDamage;

                                        if (v.Health > 0)
                                        {
                                            hit = true;
                                        }
                                        else
                                        {
                                            v.Visible = false;
                                        }
                                    }
                                }
                            }

                            foreach (EnemyUnit v in KnownEnemies.ToArray())
                            {
                                if (v.Visible)
                                {
                                    if (v.Bounds.Contains(a.Location))
                                    {
                                        v.Visible = false;

                                        hit = true;

                                        board.Score += v.Info.Points;
                                    }
                                }
                            }



                            return hit;
                        };
                    #endregion


         

                    #region EnemyAction
                    Action EnemyAction =
                        delegate
                        {
                            #region create ufo 
                            
                            if (!UFO.Visible)
                            {
                                if (Native.Math.random() < 0.1)
                                {
                                    Console.WriteLine("UFO!");

                                    if (Native.Math.random() > 0.5)
                                    {
                                        UFO_Direction = 1;
                                        UFO.MoveTo(0, EnemyTop - UFO.Control.height * 2);
                                    }
                                    else
                                    {
                                        UFO_Direction = -1;
                                        UFO.MoveTo(478, EnemyTop - UFO.Control.height * 2);
                                    }

                                    UFO.Visible = true;
                                }
                            }
                            #endregion

                            var ev = Sequence.Where(KnownEnemies.ToArray(), i => i.Visible);

                            if (!Enemy_Ammo.Visible)
                            {
                                var ei = Native.Math.round(Native.Math.random() * Sequence.Count(ev));

                                EnemyUnit et = Sequence.ElementAt(ev, ei);

                                if (et == null)
                                    Console.WriteLine("element at " + ei + " not found");
                                else
                                {
                                    int ey = Sequence.Max(Sequence.Select(Sequence.Where(ev, i => i.X == et.X), i => i.Y));

                                    Enemy_Ammo.MoveTo(et.X, ey + 20);
                                    Enemy_Ammo.Visible = true;
                                }
                            }


                            #region MoveAll
                            Action<Point> MoveAll =
                                delegate(Point to)
                                {
                                    var ConcreteReached = false;

                                    foreach (EnemyUnit v in ev)
                                    {
                                        var vy = v.Y + to.Y;

                                        if (vy > ConcreteTop)
                                        {
                                            ConcreteReached = true;
                                        }

                                        v.MoveTo(v.X + to.X, vy);
                                    }

                                    if (ConcreteReached)
                                    {
                                        EndGame("The walls have been breached.");
                                    }
                                };
                            #endregion

                            Action MoveAllDown =
                                delegate
                                {
                                    MoveAll(new Point(0, 8));
                                };
                            

                            #region move the gang
                            if (GangDirection > 0)
                            {
                                int ex_max = Sequence.Max(Sequence.Select(ev, i => i.X));

                                // gang goes right

                                if (ex_max >= Map_Rect.Right)
                                {
                                    GangDirection = -1;
                                    MoveAllDown();
                                }
                                else
                                {
                                    MoveAll(new Point(4, 0));
                                }
                            }
                            else
                            {
                                int ex_min = Sequence.Min(Sequence.Select(ev, i => i.X));

                                // gang goes left

                                if (ex_min <= Map_Rect.Left)
                                {
                                    GangDirection = 1;
                                    MoveAllDown();
                                }
                                else
                                {
                                    MoveAll(new Point(-4, 0));
                                }
                            }
                            #endregion

                        };
                    #endregion

                    bool GamePaused = false;




                    GameTimer.Tick +=
                        delegate
                        {

                            #region only blink while paused
                            if (GamePaused)
                            {
                                if (GameTimer.Counter % 15 == 0)
                                {
                                    Player.ToggleVisible();
                                }

                                return;
                            }
                            #endregion



                            Player.Show();

                            #region move ufo

                            if (UFO.Visible)
                            {
                                if (UFO_Direction > 0)
                                {
                                    UFO.MoveTo(UFO.X + 4, UFO.Y);

                                    if (UFO.X > 478 + UFO.Control.width)
                                    {
                                        UFO.Visible = false;
                                    }
                                }
                                else
                                {
                                    UFO.MoveTo(UFO.X - 4, UFO.Y);

                                    if (UFO.X < -UFO.Control.width)
                                    {
                                        UFO.Visible = false;
                                    }
                                }
                            }
                            #endregion


                            #region do ammo stuff
                            foreach (AmmoInfo v in KnownAmmo)
                            {
                                if (v.Visible)
                                {
                                    var y = v.Y + v.Speed;

                                    if (Map_Rect.Contains(new Point(v.X, y)))
                                    {
                                        // did we hit?
                                        if (DoAmmoDamage(v))
                                        {
                                            v.Visible = false;
                                        }
                                        else
                                        {
                                            v.MoveTo(v.X, y);
                                        }
                                    }
                                    else
                                    {
                                        v.Visible = false;
                                    }
                                }
                            }
                            #endregion



                            var AliveEnemies = Sequence.Where(KnownEnemies.ToArray(), i => i.Visible);
                            var AliveCount = Sequence.Count(AliveEnemies);

                            if (AliveCount == 0)
                            {
                                EndGame("Aliens destoried");

                                return;
                            }

                            if (GameTimer.Counter % (AliveCount / 2) == 0)
                            {
                                EnemyAction();
                            }

                        };

                    Native.Document.onkeydown += delegate(IEvent ev)
                    {
                        if (mmenu.Visible)
                        {
                            if (ev.IsReturn)
                            {
                                mmenu.Visible = false;

                                Player_X = 220;
                                board.Score = 0;
                                board.Lives = 3;

                                Player.Show();

                                foreach (Concrete v in KnownConcrete.ToArray())
                                {
                                    v.Health = 255;
                                    v.Visible = true;
                                }


                                foreach (EnemyUnit v in KnownEnemies.ToArray())
                                {
                                    v.ResetPosition();
                                    v.Visible = true;
                                }

                                EnemyAction();

                                GameTimer.StartInterval(50);

                                UpdatePlayer(0);


                            }

                            return;
                        }
                        else
                        {
                            if (ev.IsEscape)
                            {
                                GameTimer.Stop();

                                Player.Hide();

                                mmenu.Visible = true;

                                foreach (AmmoInfo v in KnownAmmo)
                                {
                                    v.Visible = false;
                                }

                                foreach (Concrete v in KnownConcrete.ToArray())
                                {
                                    v.Visible = false;
                                }

                                foreach (EnemyUnit v in KnownEnemies.ToArray())
                                {
                                    v.Visible = false;
                                }

                                UFO.Visible = false;

                                gameovermenu.Visible = false;

                                // the animated gifs would stop after escape key
                                ev.PreventDefault();

                                GamePaused = false;
                            }
                        }

                        int key_p = 80;


                        if (ev.KeyCode == key_p)
                        {
                            GamePaused = !GamePaused;
                        }

                        // player shouldn't really move while game is paused
                        // its cheating:)
                        if (GamePaused)
                            return;

                        int key_right = 39;
                        int key_left = 37;
                        int key_space = 32;

                        if (ev.KeyCode == key_left)
                        {
                            UpdatePlayer(-Player_X_step);
                        }
                        else if (ev.KeyCode == key_right)
                        {
                            UpdatePlayer(Player_X_step);
                        }
                        else if (ev.KeyCode == key_space)
                        {
                            if (!Player_Ammo.Visible)
                            {
                                Player_Ammo.MoveTo(Player_X, Player_Y - 20);


                                Player_Ammo.Visible = true;

                            }
                        }
                        else
                        {
                            Console.WriteLine("key: " + ev.KeyCode);
                        }
                    };

                    msg_loading.Dispose();
                }
                , 50);


        }

        [Script]
        public class Concrete
        {
            public readonly IHTMLDiv Control = new IHTMLDiv();

            public const int Size = 16;

            private int _Health;

            public int Health
            {
                get { return _Health; }
                set
                {
                    _Health = value;
                    if (value > 0xFF)
                    {
                        Control.style.backgroundColor = Color.FromRGB(0, 0xff, 0);
                        return;
                    }

                    if (value < 0)
                    {
                        Control.style.backgroundColor = Color.FromRGB(0, 0, 0);
                        return;
                    }

                    Control.style.backgroundColor = Color.FromRGB(0, value, 0);

                }
            }


            private bool _Visible;

            public bool Visible
            {
                get { return _Visible; }
                set
                {
                    _Visible = value;
                    Control.Show(value);
                }
            }

            public Rectangle Bounds
            {
                get
                {
                    return Rectangle.Of(X, Y, Size, Size);
                }
            }

            public Concrete()
            {
                Control.style.position = IStyle.PositionEnum.absolute;
                Control.style.overflow = IStyle.OverflowEnum.hidden;
                Control.style.SetSize(Size, Size);

                Visible = false;

                Health = 255;
            }

            public int X;
            public int Y;

            public void MoveTo(int x, int y)
            {
                this.X = x;
                this.Y = y;

                Control.SetCenteredLocation(x, y);
            }

            public static Concrete[] BuildAt(Point point)
            {
                var a = new List<Concrete>();

                EventHandler<int, int> Add =
                    delegate(int x, int y)
                    {
                        var c = new Concrete();

                        c.MoveTo(x, y);

                        a.Add(c);
                    };

                Add(point.X - Size * 2, point.Y);
                Add(point.X - Size * 2, point.Y - Size);
                Add(point.X - Size * 1, point.Y - Size);
                Add(point.X, point.Y - Size);
                Add(point.X + Size * 1, point.Y - Size);
                Add(point.X + Size * 1, point.Y);

                return a.ToArray();
            }
        }

        [Script]
        public class AmmoInfo
        {
            public int Speed;

            public readonly IHTMLDiv Control = new IHTMLDiv();

            private Color _Color;

            public Color Color
            {
                get { return _Color; }
                set
                {
                    _Color = value;
                    Control.style.backgroundColor = value;
                }
            }




            public AmmoInfo()
            {
                Control.style.overflow = IStyle.OverflowEnum.hidden;
                Control.style.position = IStyle.PositionEnum.absolute;

                Control.style.SetSize(1, 10);
                Control.Hide();

            }


            private bool _Visible;

            public bool Visible
            {
                get { return _Visible; }
                set
                {
                    _Visible = value;
                    Control.Show(value);
                }
            }

            public int X;
            public int Y;

            public Point Location
            {
                get
                {
                    return new Point(X, Y);
                }
            }

            public void MoveTo(int x, int y)
            {
                this.X = x;
                this.Y = y;

                Control.SetCenteredLocation(x, y);
            }
        }

        [Script]
        public class GameOverMenu
        {
            public readonly IHTMLDiv Control = new IHTMLDiv();

            private bool _Visible;

            public bool Visible
            {
                get { return _Visible; }
                set
                {
                    _Visible = value;

                    Control.Show(value);
                }
            }

            public GameOverMenu()
            {
                Func<string, Color, IHTMLSpan> GetText2 =
                           delegate(string text, Color color)
                           {
                               var s = new IHTMLSpan(text);
                               s.style.color = color;
                               return s;
                           };

                Func<string, Color, string, IHTMLSpan> GetText =
                    delegate(string text, Color color, string size)
                    {
                        var s = GetText2(text, color);

                        s.style.fontSize = size;

                        return s;
                    };

                Control.appendChild(GetText("GAME OVER", Color.Green, "44px"));
            }
        }

        [Script]
        public class MainMenu
        {
            public readonly IHTMLDiv Control = new IHTMLDiv();

            private bool _Visible;

            public bool Visible
            {
                get { return _Visible; }
                set
                {
                    _Visible = value;

                    Control.Show(value);
                }
            }

            public MainMenu()
            {
                Func<string, Color, IHTMLSpan> GetText2 =
                           delegate(string text, Color color)
                           {
                               var s = new IHTMLSpan(text);
                               s.style.color = color;
                               return s;
                           };

                Func<string, Color, string, IHTMLSpan> GetText =
                    delegate(string text, Color color, string size)
                    {
                        var s = GetText2(text, color);

                        s.style.fontSize = size;

                        return s;
                    };


                Control.appendChild(
                    new IHTMLDiv(
                        gfx.ImageResources.Default.cenemy.cloneNode(false),
                            GetText("&nbsp;SPACE&nbsp;", Color.White, "48px"),
                        gfx.ImageResources.Default.cenemy.cloneNode(false)
                    )
                );

                Control.appendChild(
                  new IHTMLDiv(
                      gfx.ImageResources.Default.aenemy.cloneNode(false),
                          GetText("&nbsp;INVADERS&nbsp;", Color.Green, "48px"),
                      gfx.ImageResources.Default.aenemy.cloneNode(false)
                  )
                );

                Action DrawBreak =
                    delegate
                    {
                        Control.appendChild(new IHTMLBreak());
                    };


                DrawBreak();

                Control.appendChild(
                    GetText2("Press&nbsp;", Color.White),
                    GetText2("enter", Color.Green),
                    GetText2("&nbsp;to start game", Color.White)
                );

                DrawBreak();
                DrawBreak();
                Action<EnemyInfo> DrawEnemyInfo =
                    delegate(EnemyInfo e)
                    {
                        Control.appendChild(
                          new IHTMLDiv(
                              e.Image.cloneNode(false),
                                  GetText2("&nbsp;- " + e.Points + " points", Color.White)
                          )
                        );
                    };

                DrawEnemyInfo(EnemyDirectory.Default.A);
                DrawBreak();
                DrawEnemyInfo(EnemyDirectory.Default.B);
                DrawBreak();
                DrawEnemyInfo(EnemyDirectory.Default.C);
                DrawBreak();
                DrawEnemyInfo(EnemyDirectory.Default.UFO);
                DrawBreak();
                DrawBreak();


                Control.appendChild(
                    new IHTMLDiv(
                    GetText2("Left/Right arrow", Color.Green),
                    GetText2(" - move, ", Color.White),
                    GetText2("SPACE", Color.Green),
                    GetText2(" - fire", Color.White)
                    )
                );

                Control.appendChild(
                    new IHTMLDiv(
                             GetText2("Escape", Color.Green),
                             GetText2(" - quit, ", Color.White),
                             GetText2("'p'", Color.Green),
                             GetText2(" - pause", Color.White)
                             )
                         );

                DrawBreak();
                DrawBreak();


                Control.appendChild(
                    new IHTMLDiv(
                        new IHTMLAnchor("http://zproxy.wordpress.com/2007/03/03/jsc-space-invaders/", "post a comment")
                    ),
                    new IHTMLDiv(
                        new IHTMLAnchor("http://jsc.sourceforge.net", "powered by jsc")
                    )
                );
            }
        }

        [Script]
        public class EnemyDirectory
        {
            public readonly EnemyInfo A = new EnemyInfo {
                Image = gfx.ImageResources.Default.aenemy,
                Points = 4
            };

            public readonly EnemyInfo B = new EnemyInfo {
                Image = gfx.ImageResources.Default.benemy,
                Points = 2
            };

            public readonly EnemyInfo C = new EnemyInfo {
                Image = gfx.ImageResources.Default.cenemy,
                Points = 1
            };

            public readonly EnemyInfo UFO = new EnemyInfo {
                Image = gfx.ImageResources.Default.ufo,
                Points = 10
            };

            static private EnemyDirectory _Default;

            static public EnemyDirectory Default
            {
                get
                {
                    if (_Default == null)
                        _Default = new EnemyDirectory();

                    return _Default;
                }
            }

        }

        [Script]
        public class EnemyInfo
        {
            public IHTMLImage Image;

            public int Points;
        }

        [Script]
        public class EnemyUnit
        {
            public readonly IHTMLImage Control;
            public readonly EnemyInfo Info;

            public Rectangle Bounds
            {
                get
                {
                    var w = Control.width;
                    var h = Control.height;

                    return Rectangle.Of(
                        this.X - w / 2,
                        this.Y - h / 2,
                        w,
                        h);

                }
            }

            public Action ResetPosition;

            public EnemyUnit(EnemyInfo i)
            {
                Info = i;
                Control = (IHTMLImage)Info.Image.cloneNode(false);

                Visible = false;
            }

            private bool _Visible;

            public bool Visible
            {
                get { return _Visible; }
                set
                {
                    _Visible = value;
                    Control.Show(value);
                }
            }

            public int X;
            public int Y;

            public void MoveTo(int x, int y)
            {
                this.X = x;
                this.Y = y;

                Control.SetCenteredLocation(x, y);
            }

            public static EnemyUnit[] Build(EnemyInfo info, int x, int y, int c, int s)
            {
                var a = new EnemyUnit[c];

                for (int i = 0; i < c; i++)
                {
                    var ke = new EnemyUnit(info);

                    var kx = x + s * i;
                    var ky = y;

                    ke.ResetPosition =
                        delegate
                        {
                            ke.MoveTo(kx, ky);
                        };

                    a[i] = ke;

                }

                return a;
            }
        }

        [Script]
        public class ScoreBoard
        {
            public readonly IHTMLDiv Control = new IHTMLDiv();

            readonly IHTMLSpan score_label = new IHTMLSpan("Score:");
            readonly IHTMLSpan score_value = new IHTMLSpan("0");


            readonly IHTMLSpan lives_label = new IHTMLSpan("Lives:");
            readonly IHTMLSpan lives_value = new IHTMLSpan();

            readonly IHTMLImage Life1 = (IHTMLImage)gfx.ImageResources.Default.biggun.cloneNode(false);
            readonly IHTMLImage Life2 = (IHTMLImage)gfx.ImageResources.Default.biggun.cloneNode(false);
            readonly IHTMLImage Life3 = (IHTMLImage)gfx.ImageResources.Default.biggun.cloneNode(false);

            private int _Score;

            public int Score
            {
                get { return _Score; }
                set
                {
                    _Score = value;
                    score_value.innerHTML = "" + value;
                }
            }

            private int _Lives;

            public int Lives
            {
                get { return _Lives; }
                set
                {
                    _Lives = value;

                    if (value > 2)
                        Life3.style.visibility = IStyle.VisibilityEnum.visible;
                    else
                        Life3.style.visibility = IStyle.VisibilityEnum.hidden;

                    if (value > 1)
                        Life2.style.visibility = IStyle.VisibilityEnum.visible;
                    else
                        Life2.style.visibility = IStyle.VisibilityEnum.hidden;

                    if (value > 0)
                        Life1.style.visibility = IStyle.VisibilityEnum.visible;
                    else
                        Life1.style.visibility = IStyle.VisibilityEnum.hidden;
                }
            }

            public ScoreBoard()
            {
                score_label.style.color = Color.White;
                lives_label.style.color = Color.White;

                score_value.style.color = Color.Green;

                var left = new IHTMLDiv(score_label, score_value);
                left.style.Float = IStyle.FloatEnum.left;

                lives_value.appendChild(
                    Life1,
                    Life2,
                    Life3
                );

                score_value.style.paddingLeft = "1em";
                Life1.style.paddingLeft = "1em";
                Life2.style.paddingLeft = "1em";
                Life3.style.paddingLeft = "1em";

                var right = new IHTMLDiv(lives_label, lives_value);
                right.style.Float = IStyle.FloatEnum.right;

                Control.style.fontSize = "22px";
                Control.appendChild(left, right);

            }
        }

    }


}
