using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Forms;

namespace ScriptCoreLib.Desktop.Forms.Extensions
{
    public static class DesktopFormsExtensions
    {
        public class WindowInfo<T> where T : Control
        {
            public T Content;

            public Form Window;

            public IEnumerable<WindowInfo<T>> Others;

            public Func<WindowInfo<T>> CreateWindow;
        }

        public delegate void LaunchOptions();


        public static void Launch<T>(Func<T> Create, Action<WindowInfo<T>> Launching = null) where T : Control
        {
            var CreateWindow = default(Func<WindowInfo<T>>);
            var Windows = new List<WindowInfo<T>>();

            CreateWindow =
                delegate
                {
                    var wi = new WindowInfo<T>();

                    Windows.Add(wi);

                    wi.Others = Windows.Except(new[] { wi });

                    wi.CreateWindow =
                        delegate
                        {
                            var ci = CreateWindow();

                            ci.Window.Owner = wi.Window;

                            if (Launching != null)
                                Launching(ci);

                            ci.Window.Show();

                            return ci;
                        };

                    //var ActualSize = new Canvas();

                    var c = Create();
                    //var c = Create().AttachTo(ActualSize);
                    //wi.Content = c;

                    //ActualSize.SizeChanged +=
                    //    delegate
                    //    {
                    //        c.SizeTo(ActualSize.ActualWidth, ActualSize.ActualHeight);
                    //    };

                    var w = new Form();
                    var f = w;

                    wi.Window = w;

                    f.ClientSize = new System.Drawing.Size(c.Width, c.Height);

                    f.ClientSizeChanged +=
                        delegate
                        {
                            c.Width = f.ClientSize.Width;
                            c.Height = f.ClientSize.Height;
                        };

                    f.Controls.Add(c);

                    w.Width = c.Width;
                    w.Height = c.Height;

                    w.Text = c.GetType().Name;

                    w.Text += " | F2 - Spawn, F11 - Fullscreen";

                    var ExitFullscreen = default(Action);

                    Action ToFullscreen =
                        () =>
                        {
                            if (ExitFullscreen != null)
                                return;

                            var x = new { w.TopMost, w.WindowState, w.FormBorderStyle };


                            ExitFullscreen =
                                () =>
                                {
                                    ExitFullscreen = null;

                                    w.FormBorderStyle = x.FormBorderStyle;
                                    w.WindowState = x.WindowState;
                                    w.TopMost = x.TopMost;


                                };

                            w.TopMost = true;
                            w.FormBorderStyle = FormBorderStyle.None;
                            w.WindowState = FormWindowState.Normal;
                            w.WindowState = FormWindowState.Maximized;
                        };


                    w.LostFocus +=
                        delegate
                        {
                            if (ExitFullscreen != null)
                            {
                                ExitFullscreen();

                                return;
                            }
                        };


                    w.KeyPreview = true;
                    f.KeyUp +=
                        (s, e) =>
                        {
                            if (e.KeyData == Keys.Escape)
                            {
                                if (ExitFullscreen != null)
                                {
                                    ExitFullscreen();

                                    //e.Handled = true;
                                    return;
                                }
                            }

                            if (e.KeyData == Keys.F2)
                            {
                                wi.CreateWindow();

                                //e.Handled = true;
                                return;
                            }

                            if (e.KeyData == Keys.F11)
                            {
                                if (ExitFullscreen != null)
                                {
                                    ExitFullscreen();

                                    //e.Handled = true;
                                    return;
                                }


                                ToFullscreen();

                                //e.Handled = true;
                                return;
                            }
                        };


                    return wi;
                };

            var t = new Thread(
                delegate()
                {
                    global::System.Windows.Forms.Application.EnableVisualStyles();


                    var wi = CreateWindow();

                    if (Launching != null)
                        Launching(wi);

                    Application.Run(wi.Window);
                    //wi.Window.ShowDialog();
                }
            )
            {
                ApartmentState = ApartmentState.STA
            };

            t.Start();
            t.Join();

        }
    }

}
