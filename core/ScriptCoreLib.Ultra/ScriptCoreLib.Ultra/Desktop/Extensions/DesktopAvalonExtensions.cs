﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using System.Threading;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Diagnostics;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Media;

namespace ScriptCoreLib.Desktop.Extensions
{
    public static class DesktopAvalonExtensions
    {
        public class WindowInfo<T> where T : Canvas
        {
            public T Content;

            public Window Window;

            public IEnumerable<WindowInfo<T>> Others;

            public Func<WindowInfo<T>> CreateWindow;
        }


        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121101/20121102
        //public delegate void LaunchOptions();

        public static void Launch<T>(Func<T> Create, Action<WindowInfo<T>> Launching = null) where T : Canvas
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

                    var CreateContent = Create;
                    var ActualSize = new Canvas();
                    var ActualSize2 = new Canvas();

                    var c = CreateContent();

                    if (double.IsNaN(c.Width))
                    {
                      
                    }
                    else
                    {
                        ActualSize.Width = c.Width;
                        ActualSize.Height = c.Height;
                    }

                    c.AttachTo(ActualSize);

                    var w = ActualSize.ToWindow();

                    if (double.IsNaN(c.Width))
                    {
                        w.SizeToContent = SizeToContent.Manual;


                        w.Width = ScriptApplicationEntryPointAttribute.DefaultWidth;
                        w.Height = ScriptApplicationEntryPointAttribute.DefaultHeight;
                    }
                    else
                    {
                        ActualSize.Width = c.Width;
                        ActualSize.Height = c.Height;
                    }


                    ActualSize2.SizeChanged +=
                        delegate
                        {
                            c.SizeTo(ActualSize2.ActualWidth, ActualSize2.ActualHeight);
                        };

                    ActualSize.SizeChanged +=
                        delegate
                        {
                            c.SizeTo(ActualSize.ActualWidth, ActualSize.ActualHeight);

                        };

                    w.SourceInitialized +=
                        delegate
                        {
                            if (w.SizeToContent != SizeToContent.Manual)
                            {
                                w.Content = null;

                                w.SizeToContent = SizeToContent.Manual;
                                c.Orphanize().AttachTo(ActualSize2);
                                ActualSize2.AttachTo(w);
                                return;
                            }

                        };

                    wi.Content = c;
                    wi.Window = w;

                    w.Background = Brushes.White;



                    w.Title += " | F2 - Spawn, F11 - Fullscreen";

                    var ExitFullscreen = default(Action);

                    #region ToFullscreen
                    Action ToFullscreen =
                        () =>
                        {
                            if (ExitFullscreen != null)
                                return;

                            var x = new { w.Topmost, w.WindowState, w.WindowStyle };


                            ExitFullscreen =
                                () =>
                                {
                                    ExitFullscreen = null;

                                    w.WindowStyle = x.WindowStyle;
                                    w.WindowState = x.WindowState;
                                    w.Topmost = x.Topmost;


                                };

                            w.Topmost = true;
                            w.WindowStyle = System.Windows.WindowStyle.None;
                            w.WindowState = System.Windows.WindowState.Normal;
                            w.WindowState = System.Windows.WindowState.Maximized;
                        };
                    #endregion


                    #region Deactivated
                    w.Deactivated +=
                        delegate
                        {
                            if (ExitFullscreen != null)
                            {
                                ExitFullscreen();

                                return;
                            }
                        };
                    #endregion


                    #region PreviewKeyUp
                    w.PreviewKeyUp +=
                        (s, e) =>
                        {
                            if (e.Key == Key.Escape)
                            {
                                if (ExitFullscreen != null)
                                {
                                    ExitFullscreen();

                                    e.Handled = true;
                                    return;
                                }
                            }

                            if (e.Key == Key.F2)
                            {
                                wi.CreateWindow();

                                e.Handled = true;
                                return;
                            }

                            if (e.Key == Key.F11)
                            {
                                if (ExitFullscreen != null)
                                {
                                    ExitFullscreen();

                                    e.Handled = true;
                                    return;
                                }


                                ToFullscreen();

                                e.Handled = true;
                                return;
                            }
                        };
                    #endregion



                    return wi;
                };

            var t = new Thread(
                delegate()
                {
                    var wi = CreateWindow();

                    if (Launching != null)
                        Launching(wi);

                    wi.Window.ShowDialog();
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
