using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;

//using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SpaceInvaders.source.js.Controls
{
    using fbool = Func<bool>;

    [Script]
    public class SpaceInvaders : SpawnControlBase
    {
        public const string Alias = "SpaceInvaders";

        // http://www.digitalinsane.com/archives/2007/01/21/space_invaders/

        [Script(NoDecoration = true)]
        static void SpawnSpaceInvaders(string resx)
        {
            new SpaceInvaders(null, resx);
        }

        public SpaceInvaders(IHTMLElement placeholder)
            : this(placeholder, "")
        {
        }

        public SpaceInvaders(IHTMLElement placeholder, string resx)
            : base(placeholder)
        {
            Console.WriteLine("resx: " + resx);

            gfx.ImageResources gfx = resx;

            var overlay = new Overlay();

            overlay.BackgroundColor = Color.Black;
            overlay.MaximumOpacity = 1;
            overlay.ControlInBack.style.zIndex = 100000;
            overlay.ControlInFront.style.zIndex = 100001;

            overlay.ControlInBack.onclick +=
                delegate
                {
                    overlay.Visible = false;
                };

            var view = overlay.ControlInFront;

            view.style.textAlign = IStyle.TextAlignEnum.center;
            view.style.SetSize(480, 480);
            view.style.backgroundColor = Color.Green;
            view.style.color = Color.White;
            view.style.fontFamily = IStyle.FontFamilyEnum.Fixedsys;




            //Native.Document.body.appendChild(
            //    new IHTMLElement(IHTMLElement.HTMLElementEnum.center,
            //    view)
            //    );



            //Native.Document.body.style.backgroundColor = Color.Black;
            // Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            System.Func<IHTMLDiv> CreateCanvas =
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

            overlay.Visible = true;

            ScriptCoreLib.JavaScript.Runtime.Timer.DoAsync(overlay.UpdateLocation);

            // now wait while all images are loaded/complete

            ((fbool)(() => !gfx.IsComplete)).Trigger(
            delegate
            {
                // loading images is done now.



                // build the scoreboard
                var MyEnemyDirectory = new EnemyDirectory(gfx);

                var board = new ScoreBoard(gfx);

                board.Control.style.SetLocation(8, 8, 464, 64);

                canvas.appendChild(board.Control);

                board.Lives = 2;
                board.Score = 450;

                // now we can see lives and score.
                // ie does not issue keypress for control keys.
                // scriptcorelib should filter firefox events...

                // lets show main menu

                var mmenu = new MainMenu(MyEnemyDirectory, gfx);
                var gameovermenu = new GameOverMenu();

                menu.appendChild(mmenu.Control, gameovermenu.Control);

                gameovermenu.Visible = false;
                gameovermenu.Control.style.SetLocation(0, 100, 468, 468 - 100);

                mmenu.Control.style.SetLocation(0, 64, 468, 468 - 64);
                mmenu.Visible = true;

                var Enemy_Ammo = new AmmoInfo
                                 {
                                     Color = Color.White,
                                     Speed = 8
                                 };

                var Player = (IHTMLImage)gfx.biggun.Clone();
                var Player_Ammo = new AmmoInfo
                                  {
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

                AmmoInfo[] KnownAmmo = new[] { Player_Ammo, Enemy_Ammo };

                var KnownConcrete = new List<Concrete>();
                var ConcreteTop = 432;

                KnownConcrete.AddRange(Concrete.BuildAt(new Point(62 + 120 * 0, ConcreteTop)));
                KnownConcrete.AddRange(Concrete.BuildAt(new Point(62 + 120 * 1, ConcreteTop)));
                KnownConcrete.AddRange(Concrete.BuildAt(new Point(62 + 120 * 2, ConcreteTop)));
                KnownConcrete.AddRange(Concrete.BuildAt(new Point(62 + 120 * 3, ConcreteTop)));

                foreach (Concrete v in KnownConcrete.ToArray())
                {
                    canvas.appendChild(v.Control);
                }


                var UFO = new EnemyUnit(MyEnemyDirectory.UFO);
                var UFO_Direction = 1;

                UFO.Visible = false;

                canvas.appendChild(UFO.Control);


                var EnemyTop = 128;
                var EnemySpacing = 32;
                var EnemyCount = 9;

                var KnownEnemies = new List<EnemyUnit>();

                KnownEnemies.AddRange(EnemyUnit.Build(MyEnemyDirectory.A, 20, EnemyTop + 0 * EnemySpacing, EnemyCount, EnemySpacing));
                KnownEnemies.AddRange(EnemyUnit.Build(MyEnemyDirectory.B, 20, EnemyTop + 1 * EnemySpacing, EnemyCount, EnemySpacing));
                KnownEnemies.AddRange(EnemyUnit.Build(MyEnemyDirectory.B, 20, EnemyTop + 2 * EnemySpacing, EnemyCount, EnemySpacing));
                KnownEnemies.AddRange(EnemyUnit.Build(MyEnemyDirectory.C, 20, EnemyTop + 3 * EnemySpacing, EnemyCount, EnemySpacing));
                KnownEnemies.AddRange(EnemyUnit.Build(MyEnemyDirectory.C, 20, EnemyTop + 4 * EnemySpacing, EnemyCount, EnemySpacing));

                foreach (EnemyUnit v in KnownEnemies.ToArray())
                {
                    canvas.appendChild(v.Control);
                }

                var HitDamage = 40;

                var GameTimer = new ScriptCoreLib.JavaScript.Runtime.Timer();

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


                var MyRandom = new System.Random();


                #region EnemyAction
                Action EnemyAction =
                    delegate
                    {
                        #region create ufo

                        if (!UFO.Visible)
                        {
                            if (MyRandom.NextDouble() < 0.1)
                            {
                                Console.WriteLine("UFO!");

                                if (MyRandom.NextDouble() > 0.5)
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

                        var ev = Enumerable.Where(KnownEnemies.ToArray(), i => i.Visible);

                        if (!Enemy_Ammo.Visible)
                        {
                            var ei = (int)System.Math.Round(MyRandom.NextDouble() * Enumerable.Count(ev));

                            EnemyUnit et = Enumerable.ElementAt(ev, ei);

                            if (et == null)
                                System.Console.WriteLine("element at " + ei + " not found");
                            else
                            {
                                int ey = Enumerable.Max(
                                    from i in ev where i.X == et.X select i.Y
                                    //    Enumerable.Select(Enumerable.Where(ev, i => i.X == et.X), i => i.Y)
                                );

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
                            int ex_max = Enumerable.Max(Enumerable.Select(ev, i => i.X));

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
                            int ex_min = Enumerable.Min(Enumerable.Select(ev, i => i.X));

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



                        var AliveEnemies = Enumerable.Where(KnownEnemies.ToArray(), i => i.Visible);
                        var AliveCount = Enumerable.Count(AliveEnemies);

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
                        // the animated gifs would stop after escape key
                        ev.PreventDefault();

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

            public MainMenu(EnemyDirectory MyEnemyDirectory, gfx.ImageResources gfx)
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
                        gfx.cenemy.Clone(),
                            GetText("&nbsp;SPACE&nbsp;", Color.White, "48px"),
                        gfx.cenemy.Clone()
                    )
                );

                Control.appendChild(
                  new IHTMLDiv(
                      gfx.aenemy.Clone(),
                          GetText("&nbsp;INVADERS&nbsp;", Color.Green, "48px"),
                      gfx.aenemy.Clone()
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
                              e.Image.Clone(),
                                  GetText2("&nbsp;- " + e.Points + " points", Color.White)
                          )
                        );
                    };

                DrawEnemyInfo(MyEnemyDirectory.A);
                DrawBreak();
                DrawEnemyInfo(MyEnemyDirectory.B);
                DrawBreak();
                DrawEnemyInfo(MyEnemyDirectory.C);
                DrawBreak();
                DrawEnemyInfo(MyEnemyDirectory.UFO);
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
            public readonly EnemyInfo A, B, C, UFO;


            public EnemyDirectory(gfx.ImageResources gfx)
            {
                Func<gfx.ImageResources.Item, int, EnemyInfo> ctor =
                    (Image, Points) => new EnemyInfo { Image = Image, Points = Points };

                this.A = ctor(gfx.aenemy, 4);
                this.B = ctor(gfx.benemy, 2);
                this.C = ctor(gfx.cenemy, 1);
                this.UFO = ctor(gfx.ufo, 10);


            }

        }

        [Script]
        public class EnemyInfo
        {
            public gfx.ImageResources.Item Image;

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
                Control = (IHTMLImage)Info.Image.Clone();

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

            readonly IHTMLImage Life1;
            readonly IHTMLImage Life2;
            readonly IHTMLImage Life3;



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


            public ScoreBoard(gfx.ImageResources gfx)
            {
                Life1 = gfx.biggun.Clone();
                Life2 = gfx.biggun.Clone();
                Life3 = gfx.biggun.Clone();

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
