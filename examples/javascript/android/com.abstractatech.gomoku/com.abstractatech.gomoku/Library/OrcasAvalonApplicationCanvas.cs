using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using System.ComponentModel;

namespace AvalonGomuko.Shared
{
    public class OrcasAvalonApplicationCanvas : Canvas
    {
        const int Intersections = 16;

        public const int DefaultWidth = 32 * Intersections + 2;
        public const int DefaultHeight = 32 * Intersections + 2;

        public OrcasAvalonApplicationCanvas()
        {
            Width = DefaultWidth;
            Height = DefaultHeight;
            //Background = 0xffc0c0c0.ToSolidColorBrush();
            Background = Brushes.Black;

            var InfoOverlay = new Canvas
            {
                Width = DefaultWidth,
                Height = DefaultHeight,
            };


            var InfoOverlayShadow = new Rectangle
            {
                Width = DefaultWidth,
                Height = DefaultHeight,
                Fill = Brushes.Black
            }.AttachTo(InfoOverlay);

            var InfoOverlayShadowOpacity = InfoOverlay.ToAnimatedOpacity();
            InfoOverlayShadowOpacity.Opacity = 1;

            var InfoOverlayText = new TextBox
            {
                AcceptsReturn = true,
                IsReadOnly = true,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                Width = DefaultWidth,
                Height = 180,
                TextAlignment = TextAlignment.Center,
                FontSize = 30,
                Foreground = Brushes.White,
                Text = "The winner is\n the first player\n to get an unbroken row\n of five stones",
                FontFamily = new FontFamily("Verdana")

            }.MoveTo(0, (DefaultHeight - 180) / 2).AttachTo(InfoOverlay);



            var TouchOverlay = new Canvas
            {
                Width = DefaultWidth,
                Height = DefaultHeight,
                Background = Brushes.Yellow,
                Opacity = 0,
            };



            var Tiles = new Tile[Intersections * Intersections];

            #region WhereUnderAttack
            Func<int, int, IEnumerable<Tile>> WhereUnderAttack =
                (length, value) =>
                    Tiles.Where(
                        k =>
                        {
                            if (k.Value != 0)
                                return false;


                            return k.IsUnderAttack(value, length);
                        }
                    );
            #endregion

            #region AI
            Action AI =
                delegate
                {
                    // defensive rule:

                    var AttackWith4 = WhereUnderAttack(4, -1).FirstOrDefault();
                    if (AttackWith4 != null)
                    {
                        Console.WriteLine("AttackWith4");
                        AttackWith4.Value = -1;
                        return;
                    }

                    var AttackedBy4 = WhereUnderAttack(4, 1).FirstOrDefault();
                    if (AttackedBy4 != null)
                    {
                        Console.WriteLine("AttackedBy4");
                        AttackedBy4.Value = -1;
                        return;
                    }

                    var AttackWith3 = WhereUnderAttack(3, -1).FirstOrDefault();
                    if (AttackWith3 != null)
                    {
                        Console.WriteLine("AttackWith3");
                        AttackWith3.Value = -1;
                        return;
                    }

                    var AttackedBy3 = WhereUnderAttack(3, 1).FirstOrDefault();
                    if (AttackedBy3 != null)
                    {
                        Console.WriteLine("AttackedBy3");
                        AttackedBy3.Value = -1;
                        return;
                    }

                    var AttackWith2 = WhereUnderAttack(2, -1).FirstOrDefault();
                    if (AttackWith2 != null)
                    {
                        Console.WriteLine("AttackWith2");
                        AttackWith2.Value = -1;
                        return;
                    }

                    var AttackedBy2 = WhereUnderAttack(2, 1).FirstOrDefault();
                    if (AttackedBy2 != null)
                    {
                        Console.WriteLine("AttackedBy2");
                        AttackedBy2.Value = -1;
                        return;
                    }

                    var AttackedBy1 = WhereUnderAttack(1, 1).FirstOrDefault();
                    if (AttackedBy1 != null)
                    {
                        Console.WriteLine("AttackedBy1");
                        AttackedBy1.Value = -1;
                        return;
                    }



                    Console.WriteLine("Random");
                    Tiles.Where(k => k.Value == 0).Random().Value = -1;
                };
            #endregion

            var ResetOverlay = new Rectangle
            {
                Fill = Brushes.Yellow,
                Width = DefaultWidth,
                Height = DefaultHeight,
                Cursor = Cursors.Hand,
            };

            //9000.AtDelay(
            //    delegate
            //    {
            //        ResetOverlay.Orphanize();
            //        InfoOverlayShadowOpacity.Opacity = 0;
            //    }
            //);

            ResetOverlay.MouseLeftButtonUp +=
                delegate
                {
                    Tiles.ForEach(k => k.Value = 0);
                    ResetOverlay.Orphanize();
                    InfoOverlayShadowOpacity.Opacity = 0;
                };

            Action<Tile> Tiles_WithEvents =
                t =>
                {
                    #region add 2d awareness
                    t.ByOffset =
                        (ox, oy) =>
                        {
                            var x = t.X + ox;
                            var y = t.Y + oy;

                            if (x < 0)
                                return null;
                            if (y < 0)
                                return null;
                            if (x >= Intersections)
                                return null;
                            if (y >= Intersections)
                                return null;
                            return Tiles[x + y * Intersections];
                        };
                    #endregion

                    t.TouchOverlay.MouseLeftButtonUp +=
                        delegate
                        {
                            if (t.Value != 0)
                                return;

                            t.Value = 1;

                            if (AtClick != null)
                                AtClick();

                            // did we win?
                            if (Tiles.Any(
                                k =>
                                {
                                    if (k.Value != 1)
                                        return false;

                                    return k.IsUnderAttack(1, 4);
                                }
                            ))
                            {
                                InfoOverlayShadowOpacity.Opacity = 0.8;
                                InfoOverlayText.Text = "You won!\n\n:)";
                                ResetOverlay.AttachTo(TouchOverlay);


                                if (AtWin != null)
                                    AtWin();

                                return;
                            }

                            t.TouchOverlay.Hide();
                            100.AtDelay(
                                delegate
                                {
                                    AI();
                                    t.TouchOverlay.Show();


                                    if (Tiles.Any(
                                        k =>
                                        {
                                            if (k.Value != -1)
                                                return false;

                                            return k.IsUnderAttack(-1, 4);
                                        }
                                    ))
                                    {
                                        InfoOverlayShadowOpacity.Opacity = 0.8;
                                        InfoOverlayText.Text = "You lost!\n\n:(";
                                        ResetOverlay.AttachTo(TouchOverlay);


                                        if (AtLoss != null)
                                            AtLoss();

                                        return;
                                    }
                                }
                            );
                        };


                };

            var TileContainer = new Canvas().AttachTo(this);

            #region build board
            for (int x = 0; x < Intersections; x++)
                for (int y = 0; y < Intersections; y++)
                {

                    var t = new Tile(x, y);



                    t.Container.AttachTo(TileContainer);
                    t.TouchOverlay.AttachTo(TouchOverlay);

                    Tiles[x + y * Intersections] = t;
                    Tiles_WithEvents(t);
                }
            #endregion

            #region add logo
            var img = new com.abstractatech.gomoku.Avalon.Images.jsc
            {
            }.MoveTo(DefaultWidth - 96, DefaultHeight - 96).AttachTo(TileContainer);
            #endregion

            ResetOverlay.AttachTo(TouchOverlay);
            InfoOverlay.AttachTo(this);
            TouchOverlay.AttachTo(this);

            this.SizeChanged +=
                delegate
                {
                    TileContainer.MoveTo(
                        (this.Width - DefaultWidth) /2,
                        (this.Height - DefaultHeight) /2
                        );
                    TouchOverlay.MoveTo(
                     (this.Width - DefaultWidth) / 2,
                     (this.Height - DefaultHeight) / 2
                     );

                    ResetOverlay.SizeTo(this.Width, this.Height);
                    InfoOverlay.SizeTo(this.Width, this.Height);
                    InfoOverlayShadow.SizeTo(this.Width, this.Height);
                    //TouchOverlay.SizeTo(this.Width, this.Height);

                    InfoOverlayText.MoveTo(0, (this.Height - 180) / 2);
                    InfoOverlayText.SizeTo(this.Width, 180);


                };
        }

        public event Action AtClick;
        public event Action AtWin;
        public event Action AtLoss;

        public class Tile : ISupportsContainer
        {


            public Func<int, int, Tile> ByOffset;

            public class VectorInt32
            {
                public int X;
                public int Y;

                public static VectorInt32 operator -(VectorInt32 e)
                {
                    return new VectorInt32
                    {
                        X = -e.X,
                        Y = -e.Y
                    };
                }

                public static VectorInt32 operator *(VectorInt32 e, int i)
                {
                    return new VectorInt32
                    {
                        X = e.X * i,
                        Y = e.Y * i
                    };
                }
            }

            public VectorInt32[] Directions
            {
                get
                {
                    return new[]
					{
							new VectorInt32 { X = -1, Y = -1},
							new VectorInt32 { X = 0, Y = -1},
							new VectorInt32 { X =1, Y = -1},
							new VectorInt32 { X =1, Y = 0},
							new VectorInt32 { X =1, Y = 1},
							new VectorInt32 { X =0, Y = 1},
							new VectorInt32 { X =-1, Y = 1},
							new VectorInt32 { X =-1, Y = 0},
					};
                }
            }

            public IEnumerable<Tile> Neighbours
            {
                get
                {
                    return new[] {
							ByOffset(-1, -1),
							ByOffset(0, -1),
							ByOffset(1, -1),
							ByOffset(1, 0),
							ByOffset(1, 1),
							ByOffset(0, 1),
							ByOffset(-1, 1),
							ByOffset(-1, 0),
						}.Where(k => k != null);
                }
            }


            public Canvas Container { get; set; }

            public Rectangle TouchOverlay { get; set; }

            public readonly int X;
            public readonly int Y;

            public Tile(int x, int y)
            {
                this.X = x;
                this.Y = y;

                Func<int, Image> s =
                    j =>
                    {
                        if (j == -1)
                            return new com.abstractatech.gomoku.Avalon.Images.s_1
                            {
                                Width = 32,
                                Height = 32
                            };

                        if (j == 1)
                            return new com.abstractatech.gomoku.Avalon.Images.s1
                            {
                                Width = 32,
                                Height = 32
                            };

                        return new com.abstractatech.gomoku.Avalon.Images.s0
                        {
                            Width = 32,
                            Height = 32
                        };

                    };


                this.Container =
                    new Canvas
                    {
                        Width = 32,
                        Height = 32,
                    }.MoveTo(32 * x, 32 * y);

                this.TouchOverlay = new Rectangle
                {
                    Fill = Brushes.Yellow,
                    Opacity = 0,
                    Width = 32,
                    Height = 32,
                    Cursor = Cursors.Hand
                }.MoveTo(32 * x, 32 * y);

                var value = 0;
                var img = s(value).AttachTo(Container);

                this.InternalGetValue = () => value;
                this.InternalSetValue =
                    n =>
                    {
                        value = n;
                        img.Orphanize();
                        img = s(n).AttachTo(Container);

                        if (n == 0)
                            this.TouchOverlay.Cursor = Cursors.Hand;
                        else
                            this.TouchOverlay.Cursor = Cursors.Arrow;
                    };
            }

            readonly Action<int> InternalSetValue;
            readonly Func<int> InternalGetValue;

            public int Value
            {
                get
                {
                    return InternalGetValue();
                }
                set
                {
                    InternalSetValue(value);
                }
            }


            public bool IsUnderAttack(int value, int length)
            {
                return this.Directions.Any(d => this.IsUnderAttack(d, value, length));
            }

            public bool IsUnderAttack(VectorInt32 d, int value, int length)
            {
                var a = 0;

                for (int i = 1; i <= length + 1; i++)
                {
                    var o = d * i;
                    var t = ByOffset(o.X, o.Y);

                    if (t != null)
                    {
                        if (t.Value == value)
                            a++;
                        else
                        {
                            //if (t.Value == -value)
                            //    a--;

                            break;
                        }
                    }
                }

                if (a > 0)
                {
                    for (int i = 1; i < length; i++)
                    {
                        var o = d * -i;
                        var t = ByOffset(o.X, o.Y);

                        if (t != null)
                        {
                            if (t.Value == value)
                                a++;
                            else
                            {
                                //if (t.Value == -value)
                                //    a--;

                                break;
                            }
                        }
                    }
                }

                return a >= length;
            }

            public override string ToString()
            {
                return "" + Value;
            }
        }
    }
}
