using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Input;

namespace AvalonWindowlessWindowDrawer
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();
        public AvalonWindowlessWindow.ApplicationCanvas c;
        public ApplicationCanvas()
        {
            c = new AvalonWindowlessWindow.ApplicationCanvas();

            c.Selection.GlassArea.Orphanize();
            c.Selection.Orphanize();



            c.AttachTo(this);
            c.MoveTo(8, 8);

            this.SizeChanged += (s, e) => c.SizeTo(this.Width - 16.0, this.Height - 16.0);

            r.Fill = Brushes.Red;
            r.Opacity = 0;

            r.AttachTo(this);
            r.MoveTo(8, 8);

            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            Rectangle h = null;
            AnimatedOpacity<Rectangle> hOpacity = null;


            Action<Action<double, double>> GetPosition = null;
            var Windows = new List<AvalonWindowlessWindow.ApplicationCanvas.WindowInfo>();

            r.MouseLeftButtonDown +=
               (s, e) =>
               {
                   h = new Rectangle
                   {
                       Fill = Brushes.Black,
                   };
                   hOpacity = h.ToAnimatedOpacity();
                   hOpacity.Opacity = 0.3;

                   var p = e.GetPosition(r);

                   GetPosition = y => y(p.X, p.Y);


                   h.AttachTo(c).MoveTo(p).SizeTo(0, 0);

                   c.Selection.Orphanize();

                   c.Selection.WindowLocation.Left = p.X;
                   c.Selection.WindowLocation.Top = p.Y;
                   c.Selection.WindowLocation.Width = 0;
                   c.Selection.WindowLocation.Height = 0;
                   c.Selection.WindowLocationChanged();
                   c.Selection.Attach();

                   
                   Windows.WithEach(k => k.GlassOpacity.Opacity = 0);
               };

            Action<MouseEventArgs, Action<bool, double, double, double, double>> GetSnapLocation =
                (e, SetLocation) =>
                {
                    var p = e.GetPosition(r);

                    var x = 0.0;
                    var y = 0.0;

                    GetPosition((_x, _y) => { x = _x; y = _y; });

                    var cx = p.X - x;
                    var cy = p.Y - y;

                    if (cx < 0)
                    {
                        x += cx;
                        cx = -cx;
                    }

                    if (cy < 0)
                    {
                        y += cy;
                        cy = -cy;
                    }

                    var Snap = 16;
                    var SnapMode = false;

                    Enumerable.FirstOrDefault(
                        from k in Windows
                        let dx0 = Math.Abs(k.WindowLocation.Left - x)
                        where dx0 < Snap
                        orderby dx0
                        select k
                    ).With(
                     ax =>
                     {
                         SnapMode = true;
                         cx += x - ax.WindowLocation.Left;
                         x = ax.WindowLocation.Left;
                     }
                   );


                    Enumerable.FirstOrDefault(
                         from k in Windows
                         let dx0 = Math.Abs(k.WindowLocation.Top - y)
                         where dx0 < Snap
                         orderby dx0
                         select k
                     ).With(
                      ax =>
                      {
                          SnapMode = true;
                          cy += y - ax.WindowLocation.Top;
                          y = ax.WindowLocation.Top;
                      }
                    );


                    SetLocation(SnapMode, x, y, cx, cy);
                };

            r.MouseMove +=
               (s, e) =>
               {
                   if (GetPosition != null)
                   {



                       GetSnapLocation(e,
                           (SnapMode, x, y, cx, cy) =>
                            {
                                if (SnapMode)
                                    hOpacity.Opacity = 0.9;
                                else
                                    hOpacity.Opacity = 0.3;

                                c.Selection.WindowLocation.Left = x;
                                c.Selection.WindowLocation.Top = y;
                                c.Selection.WindowLocation.Width = cx;
                                c.Selection.WindowLocation.Height = cy;
                                c.Selection.WindowLocationChanged();

                                h.MoveTo(x, y).SizeTo(cx, cy);
                            }
                       );
                   }
               };


            r.MouseLeftButtonUp +=
                 (s, e) =>
                 {
                     if (GetPosition != null)
                     {
                         GetSnapLocation(e,
                            (SnapMode, x, y, cx, cy) =>
                            {
                                Windows.WithEach(k => k.GlassOpacity.Opacity = 1);

                                h.Orphanize();
                                c.Selection.Orphanize();

                                if (cx > 32)
                                    if (cy > 32)
                                        c.CreateWindow(
                                            new AvalonWindowlessWindow.ApplicationCanvas.Position
                                            {
                                                Left = x,
                                                Top = y,
                                                Width = cx,
                                                Height = cy
                                            },

                                           Windows.Add
                                         );


                            }
                        );


                     }

                     GetPosition = null;


                 };
        }

    }
}
