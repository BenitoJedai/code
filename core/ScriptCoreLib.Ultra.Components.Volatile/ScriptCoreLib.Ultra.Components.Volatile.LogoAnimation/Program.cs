using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Components.Volatile.LogoAnimation.Library;
using System.Windows.Shapes;
using System.Windows.Media.Effects;

namespace ScriptCoreLib.Ultra.Components.Volatile.LogoAnimation
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var c = new JSCSolutionsNETCarouselCanvas
                {
                    CloseOnClick = false
                };

            c.HideSattelites();


            //c.Container.Effect = new DropShadowEffect();
            //c.Container.BitmapEffect = new DropShadowBitmapEffect();

            //.MoveTo(0, ImageCarouselCanvas.DefaultHeight - 96).SizeTo(ImageCarouselCanvas.DefaultWidth, 96);

            var cc = new Canvas();


            // http://cloudstore.blogspot.com/2008/05/creating-custom-window-style.html
            var wcam = new Window();
            wcam.Background = Brushes.Transparent;
            wcam.WindowStyle = WindowStyle.None;
            wcam.ResizeMode = ResizeMode.NoResize;
            wcam.SizeTo(200, 200);
            wcam.AllowsTransparency = true;
            //wcam.Opacity = 0.5;
            wcam.ShowInTaskbar = false;
            wcam.Cursor = Cursors.Hand;
            wcam.Focusable = false;
            wcam.Topmost = true;

            var w = cc.ToWindow();


            w.SizeToContent = SizeToContent.Manual;
            w.SizeTo(400, 400);
            //w.ToTransparentWindow();


            // http://blog.joachim.at/?p=39
            // http://blogs.msdn.com/changov/archive/2009/01/19/webbrowser-control-on-transparent-wpf-window.aspx
            // http://blogs.interknowlogy.com/johnbowen/archive/2007/06/20/20458.aspx
            w.AllowsTransparency = true;
            w.WindowStyle = System.Windows.WindowStyle.None;
            w.Focusable = false;

            //w.Background = new SolidColorBrush(Color.FromArgb(0x20, 0, 0, 0));
            w.Background = Brushes.Transparent;
            w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            w.Topmost = true;
            //w.ShowInTaskbar = false;

            var winfoc = new Canvas();



            var winfo = winfoc.ToWindow();

            winfo.AllowsTransparency = true;
            winfo.ShowInTaskbar = false;
            winfo.WindowStyle = WindowStyle.None;
            winfo.Background = Brushes.Transparent;
            winfo.ResizeMode = ResizeMode.NoResize;
            
            winfo.SizeToContent = SizeToContent.Manual;
            winfo.Topmost = true;
            // http://www.squidoo.com/youtubehd









            var NextInputModeEnabled = false;
            var NextInputModeKeyDownEnabled = false;

            Action<Key> NextInputModeKeyDown = delegate { };

            var CommandKeysEnabled = false;

            var TopicText = new System.Windows.Controls.TextBox
            {
                //IsReadOnly = true,
                Background = Brushes.Transparent,
                BorderThickness = new System.Windows.Thickness(0),
                Foreground = Brushes.White,
                Effect = new DropShadowEffect(),
                Text = "JSC C# Foo Bar",
                //TextDecorations = TextDecorations.Underline,
                FontFamily = new FontFamily("Verdana"),
                FontSize = 24,
                TextAlignment = System.Windows.TextAlignment.Right
            };

            #region KeyDown
            InterceptKeys.KeyDown +=
                key =>
                {
                    if (key == Key.LeftShift)
                    {
                        //w.Background = new SolidColorBrush(Color.FromArgb(2, 0, 0, 0));
                        //w.MakeInteractive(true);
                        NextInputModeEnabled = true;
                    }
                    else
                    {

                        if (NextInputModeEnabled || NextInputModeKeyDownEnabled)
                        {

                            NextInputModeKeyDownEnabled = false;
                            NextInputModeKeyDown(key);

                        }

                        NextInputModeEnabled = false;
                    }
                };
            #endregion

            InterceptKeys.KeyUp +=
                key =>
                {
                    if (key == Key.CapsLock)
                    {
                        CommandKeysEnabled = !CommandKeysEnabled;
                        TopicText.IsReadOnly = CommandKeysEnabled;
                        TopicText.Select(0, 0);
                    }

                    if (key == Key.LeftShift)
                    {
                        NextInputModeKeyDownEnabled = false;
                    }
                    else
                    {

                    }

                    NextInputModeEnabled = false;
                };

            var s = 7;

            var ThumbnailSize = 0.4;
            var CaptionBackgroundHeight = 24;

            Action UpdateChildren =
                delegate
                {
                    if (w.ActualWidth == 0)
                        return;

                    var ss = s;
                    var ss2 = 0;

               
                    Console.WriteLine(
                        new { w.Left, w.Top });

                    winfo.MoveTo(w.Left, w.Top).SizeTo(w.ActualWidth, w.ActualHeight);

                    if (ThumbnailSize == 1)
                    {
                        wcam.Background = Brushes.Black;
                        ss = 0;

                        var qw = w.ActualWidth - ss * 2;
                        var qh = w.ActualHeight - ss * 2;

                        // no status bars or menues please :)

                        wcam.MoveTo(
                            w.Left + ss + ss2,
                            w.Top + (w.ActualHeight - qh * ThumbnailSize - ss) - ss2
                        ).SizeTo(
                            qw * ThumbnailSize,
                            qh * ThumbnailSize
                        );

                    }
                    else
                    {
                        wcam.Background = Brushes.Transparent;

                        //if (w.WindowState == WindowState.Maximized)
                        //{
                        //    ss2 = s;
                        //}

                        var qw = w.ActualWidth - ss * 2;
                        var qh = w.ActualHeight - ss * 2;



                        wcam.MoveTo(w.Left + ss + ss2, w.Top + (w.ActualHeight - qh * ThumbnailSize - ss) - ss2).SizeTo(qw * ThumbnailSize, qh * ThumbnailSize);

                    }


                };

            w.LocationChanged +=
                delegate
                {
                    UpdateChildren();
                };



            var Borders = Enumerable.Range(1, s * 2).Reverse().Select(
                Width =>
                new
                {
                    Width = Width * 2,
                    Left = new Rectangle { Fill = Brushes.Black, Opacity = 0.06 }.MoveTo(0, 0).AttachTo(winfoc),
                    Right = new Rectangle { Fill = Brushes.Black, Opacity = 0.06 }.MoveTo(0, 0).AttachTo(winfoc),
                    Bottom = new Rectangle { Fill = Brushes.Black, Opacity = 0.03 }.MoveTo(0, 0).AttachTo(winfoc),
                    Top = new Rectangle { Fill = Brushes.Black, Opacity = 0.11 }.MoveTo(0, 0).AttachTo(winfoc)
                }
            ).ToArray();

            var CaptionBackgroundOverlay = new Rectangle
            {
                Fill = Brushes.Black,
                Opacity = 0.02,
            }.AttachTo(cc);

            var CaptionSysMenuOverlay = new Rectangle
            {
                Fill = Brushes.Black,
                Opacity = 0.02,
            }.AttachTo(cc).SizeTo(CaptionBackgroundHeight * 4, CaptionBackgroundHeight);

            var ExtraBorderTop = new Rectangle
            {
                Fill = Brushes.Black,
                Opacity = 0.0,
            }.AttachTo(winfoc);

            var ExtraBorderBottom = new Rectangle
            {
                Fill = Brushes.Black,
                Opacity = 0.0,
            }.AttachTo(winfoc);

            var CaptionClose = new TextBox
            {
                Foreground = Brushes.Red,
                FontFamily = new FontFamily("Webdings"),
                Text = "r",
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                TextAlignment = System.Windows.TextAlignment.Center,
                Opacity = 0.5
            }.AttachTo(winfoc);


            var CaptionText = new System.Windows.Controls.TextBox
            {
                IsReadOnly = true,
                Background = Brushes.Transparent,
                BorderThickness = new System.Windows.Thickness(0),
                Foreground = Brushes.White,
                Effect = new System.Windows.Media.Effects.DropShadowEffect(),
                Text = "jsc-solutions.net",
                //TextDecorations = TextDecorations.Underline,
                FontFamily = new FontFamily("Verdana"),
                FontSize = 16,
                TextAlignment = System.Windows.TextAlignment.Right
            }
            .AttachTo(winfoc);

            TopicText.AttachTo(winfoc);

            Action<string> SetCaption =
                text =>
                {
                    if (string.IsNullOrEmpty(text))
                        CaptionText.Text = "jsc-solutions.net";
                    else
                        CaptionText.Text = text + " | jsc-solutions.net";

                };

            //var ink = new InkCanvas
            //{
            //    Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
            //}.AttachTo(cc).MoveTo(0, CaptionBackgroundHeight);

            //ink.DefaultDrawingAttributes.IgnorePressure = false;
            //ink.DefaultDrawingAttributes.Color = Colors.Yellow;

            c.AttachContainerTo(winfoc);

            var ExtraBorderSize = 0.10;


            var Intro = new PromotionBrandIntro.ApplicationCanvas().AttachTo(winfoc);
            Intro.Opacity = 0;

            Action SizeChanged =
                delegate
                {
                    Intro.SizeTo(w.ActualWidth, w.ActualHeight);
                    //ink.SizeTo(w.ActualWidth, w.ActualHeight - CaptionBackgroundHeight);

                    var CaptionWidth = 200;

                    CaptionBackgroundOverlay.MoveTo(w.ActualWidth - CaptionWidth, 0).SizeTo(CaptionWidth, CaptionBackgroundHeight);


                    ExtraBorderTop.MoveTo(0, 0).SizeTo(w.ActualWidth, w.ActualHeight * ExtraBorderSize);
                    ExtraBorderBottom.MoveTo(0, w.ActualHeight * (1 - ExtraBorderSize)).SizeTo(w.ActualWidth, w.ActualHeight * ExtraBorderSize);

                    TopicText.MoveTo(
                        0,
                        w.ActualHeight - 48
                    ).SizeTo(w.ActualWidth - 48, 48);

                    // .NET 4 ?

                    //if (w.WindowState == WindowState.Maximized)
                    //{
                    //    if (c != null)
                    //        c.MoveContainerTo(-200 + 42 + s, -200 + 38 + s);
                    //    Borders.WithEach(k => k.Left.MoveTo(s, 0).SizeTo(k.Width, w.ActualHeight));
                    //    Borders.WithEach(k => k.Right.MoveTo(w.ActualWidth - k.Width - s + 2, 0).SizeTo(k.Width, w.ActualHeight));
                    //    Borders.WithEach(k => k.Bottom.MoveTo(0, w.ActualHeight - k.Width - s).SizeTo(w.ActualWidth, k.Width));
                    //    Borders.WithEach(k => k.Top.MoveTo(0, s).SizeTo(w.ActualWidth, k.Width));
                    //    CaptionText.MoveTo(0, 2 + s).SizeTo(w.ActualWidth - CaptionBackgroundHeight, 32);
                    //    CaptionClose.MoveTo(w.ActualWidth - CaptionBackgroundHeight, s + s).SizeTo(CaptionBackgroundHeight - s, CaptionBackgroundHeight - s);

                    //}
                    //else
                    //{
                        if (c != null)
                            c.MoveContainerTo(-200 + 42, -200 + 38);
                        Borders.WithEach(k => k.Left.MoveTo(0, 0).SizeTo(k.Width, w.ActualHeight));
                        Borders.WithEach(k => k.Right.MoveTo(w.ActualWidth - k.Width, 0).SizeTo(k.Width, w.ActualHeight));
                        Borders.WithEach(k => k.Bottom.MoveTo(0, w.ActualHeight - k.Width).SizeTo(w.ActualWidth, k.Width));
                        Borders.WithEach(k => k.Top.MoveTo(0, 0).SizeTo(w.ActualWidth, k.Width));
                        CaptionText.MoveTo(0, 2).SizeTo(w.ActualWidth - CaptionBackgroundHeight, 32);
                        CaptionClose.MoveTo(w.ActualWidth - CaptionBackgroundHeight, s).SizeTo(CaptionBackgroundHeight - s, CaptionBackgroundHeight - s);
                    //}

                    UpdateChildren();
                };


            //Action StylusOutOfRange = delegate { };

            //w.StylusInRange +=
            //    delegate
            //    {
            //        ink.Background = new SolidColorBrush(Color.FromArgb(0x10, 0, 0, 0));
            //        StylusOutOfRange = delegate { };

            //        SetCaption("drawing");
            //    };

            ////ink.StylusInRange +=
            ////    delegate
            ////    {
            ////        StylusOutOfRange = delegate { };
            ////    };

            //w.StylusOutOfRange +=
            //    delegate
            //    {
            //        StylusOutOfRange = delegate
            //        {
            //            SetCaption("");
            //            ink.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //        };

            //        10000.AtDelay(
            //            delegate
            //            {
            //                StylusOutOfRange();
            //            }
            //        );

            //    };

            w.SizeChanged +=
                delegate
                {
                    SizeChanged();
                };

            w.StateChanged +=
                delegate
                {
                    if (w.WindowState == WindowState.Maximized)
                        w.WindowState = WindowState.Normal;

                    SizeChanged();
                };



            #region GetWindows
            Func<IEnumerable<Internal.Window>> GetWindows =
                delegate
                {
                    var windows = new List<Internal.Window>();
                    Internal.EnumWindows(
                        (IntPtr hwnd, int lParam) =>
                        {
                            if (new WindowInteropHelper(wcam).Handle != hwnd
                                && (Internal.GetWindowLongA(hwnd, Internal.GWL_STYLE) & Internal.TARGETWINDOW) == Internal.TARGETWINDOW
                                )
                            {
                                StringBuilder sb = new StringBuilder(100);
                                Internal.GetWindowText(hwnd, sb, sb.Capacity);

                                windows.Add(
                                    new Internal.Window
                                    {
                                        Handle = hwnd,
                                        Title = sb.ToString()
                                    }
                                );
                            }

                            return true; //continue enumeration
                        }
                        , 0);

                    return windows.OrderBy(k => k.Title);
                };
            #endregion

            var ResetThumbnailSkip = 0;

            Func<Internal.Window> GetCurrentThumbnail = () => GetWindows().AsCyclicEnumerable().Skip(ResetThumbnailSkip).First();

            Intro.AnimationCompleted +=
                delegate
                {
                    if (c == null)
                    {
                        c = new JSCSolutionsNETCarouselCanvas
                        {
                            CloseOnClick = false
                        }.AttachContainerTo(winfoc);
                        c.HideSattelites();

                        CaptionClose.Show();
                        SizeChanged();
                    }
                };

            wcam.SourceInitialized +=
                delegate
                {
                    {
                        wcam.MakeInteractive(false);

                        UpdateChildren();
                    }



                    var thumb = IntPtr.Zero;
                    ResetThumbnailSkip = GetWindows().Where(
                        k =>
                            k.Title.Contains("Chrome")
                            || k.Title.Contains("Studio")
                            || k.Title.Contains("Minefield")

                            ).TakeWhile(k => k.Handle != Internal.GetForegroundWindow()).Count();


                    #region ResetThumbnail
                    Action ResetThumbnail =
                        delegate
                        {
                            GetCurrentThumbnail().With(
                                shadow =>
                                {
                                    //t.Text = shadow.Title;

                                    if (thumb != IntPtr.Zero)
                                        Internal.DwmUnregisterThumbnail(thumb);

                                    int i = Internal.DwmRegisterThumbnail(
                                        new WindowInteropHelper(wcam).Handle, shadow.Handle, out thumb);

                                    #region UpdateThumbnail
                                    Action UpdateThumbnail =
                                        delegate
                                        {
                                            if (thumb != IntPtr.Zero)
                                            {
                                                Internal.PSIZE size;
                                                Internal.DwmQueryThumbnailSourceSize(thumb, out size);

                                                Internal.DWM_THUMBNAIL_PROPERTIES props = new Internal.DWM_THUMBNAIL_PROPERTIES();

                                                props.fVisible = true;
                                                props.dwFlags = Internal.DWM_TNP_VISIBLE | Internal.DWM_TNP_RECTDESTINATION | Internal.DWM_TNP_OPACITY | Internal.DWM_TNP_SOURCECLIENTAREAONLY;
                                                props.opacity = (byte)((byte)(0x7F) + (byte)((0x80) * (ThumbnailSize)));
                                                props.rcDestination = new Internal.Rect(0, 0, (int)wcam.ActualWidth, (int)wcam.ActualHeight);
                                                props.fSourceClientAreaOnly = true;

                                                if (size.x < wcam.ActualWidth)
                                                {
                                                    props.rcDestination.Right = props.rcDestination.Left + size.x;

                                                    props.rcDestination.Left += ((int)wcam.ActualWidth - size.x) / 2;
                                                    props.rcDestination.Right += ((int)wcam.ActualWidth - size.x) / 2;
                                                }

                                                if (size.y < wcam.ActualHeight)
                                                {

                                                    props.rcDestination.Bottom = props.rcDestination.Top + size.y;

                                                    props.rcDestination.Top += ((int)wcam.ActualHeight - size.y) / 2;
                                                    props.rcDestination.Bottom += ((int)wcam.ActualHeight - size.y) / 2;

                                                }



                                                Internal.DwmUpdateThumbnailProperties(thumb, ref props);
                                            }
                                        };
                                    #endregion


                                    (1000 / 15).AtInterval(UpdateThumbnail);

                                    wcam.SizeChanged += delegate { UpdateThumbnail(); };
                                }
                            );
                        };
                    #endregion



                    ResetThumbnail();

                    NextInputModeKeyDown +=
                        key =>
                        {
                            if (key == Key.RightShift)
                            {
                                NextInputModeKeyDownEnabled = true;

                                if (c != null)
                                {
                                    CaptionClose.Hide();
                                    c.AtClose +=
                                        delegate
                                        {
                                            c.OrphanizeContainer();
                                        };
                                    c.Close();
                                    c = null;
                                }

                                Intro.Background = Brushes.Transparent;
                                Intro.Overlay.Opacity = 1;
                                Intro.FadeIn(
                                    delegate
                                    {


                                        2000.AtDelay(Intro.PrepareAnimation());
                                    }
                                );
                            }

                            if (!CommandKeysEnabled)
                            {
                                if (key == Key.F2)
                                {
                                    w.Activate();

                                    TopicText.Focusable = true;
                                    TopicText.Focus();
                                    TopicText.SelectAll();
                                }
                                return;
                            }

                            if (key == Key.Right)
                            {
                                if (w.IsActive)
                                {
                                    GetCurrentThumbnail().Activate();
                                }
                                else
                                {
                                    NextInputModeKeyDownEnabled = true;
                                    ResetThumbnailSkip = GetWindows().TakeWhile(k => k.Handle != Internal.GetForegroundWindow()).Count();
                                    ResetThumbnail();
                                }
                            }

                            if (key == Key.Up)
                            {
                                NextInputModeKeyDownEnabled = true;

                                if (ThumbnailSize < 0.3)
                                    ThumbnailSize = 0.3;
                                else if (ThumbnailSize < 0.5)
                                    ThumbnailSize = 0.5;
                                else if (ThumbnailSize < 1)
                                    ThumbnailSize = 1;
                                else
                                {
                                    if (c != null)
                                    {
                                        CaptionClose.Hide();
                                        c.AtClose +=
                                            delegate
                                            {
                                                c.OrphanizeContainer();
                                            };
                                        c.Close();
                                        c = null;
                                    }

                                }


                                UpdateChildren();

                            }
                            if (key == Key.Down)
                            {
                                NextInputModeKeyDownEnabled = true;
                                if (c == null)
                                {
                                    c = new JSCSolutionsNETCarouselCanvas
                                    {
                                        CloseOnClick = false
                                    }.AttachContainerTo(winfoc);
                                    CaptionClose.Show();
                                }
                                else if (ThumbnailSize > 0.5)
                                    ThumbnailSize = 0.5;
                                else if (ThumbnailSize > 0.3)
                                    ThumbnailSize = 0.3;
                                else if (ThumbnailSize == 0.3)
                                    ThumbnailSize = 0;

                                SizeChanged();
                            }

                          

                            if (key == Key.Left)
                            {
                                if (w.IsActive)
                                {
                                    NextInputModeKeyDownEnabled = true;
                                    if (ExtraBorderTop.Opacity == 1)
                                    {
                                        ExtraBorderTop.Opacity = 0;
                                        ExtraBorderBottom.Opacity = 0;
                                    }
                                    else
                                    {
                                        ExtraBorderTop.Opacity = 1;
                                        ExtraBorderBottom.Opacity = 1;
                                    }
                                }
                            }

                        };


                };

            winfo.SourceInitialized +=
                delegate
                {
                    winfo.MakeInteractive(false);
                };

            w.SourceInitialized +=
                delegate
                {
                    wcam.Owner = w;
                    winfo.Owner = w;
                    wcam.Show();
                    winfo.Show();

                    HwndSource hwndSource = (HwndSource)HwndSource.FromVisual(w);
                    hwndSource.AddHook(
                        (IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handeled) =>
                        {
                            if (msg == 0x0084) // WM_NCHITTEST
                            {
                                //http://agsmith.wordpress.com/2008/09/16/hit-testing-in-wpf/

                                var p = new
                                {
                                    cx = w.ActualWidth - (Internal.LOWORD(lParam) - w.Left),
                                    cy = w.ActualHeight - (Internal.HIWORD(lParam) - w.Top),
                                    x = Internal.LOWORD(lParam) - w.Left,
                                    y = Internal.HIWORD(lParam) - w.Top
                                };


                                //t.Text = p.ToString();

                                handeled = true;


                                if (p.x < s)
                                    if (p.y < s)
                                        return (IntPtr)HitTestValues.HTTOPLEFT; // HTCAPTION

                                if (p.x < CaptionBackgroundHeight)
                                    if (p.y < CaptionBackgroundHeight)
                                        return (IntPtr)HitTestValues.HTSYSMENU; // HTCAPTION


                                if (p.cx < CaptionBackgroundHeight)
                                    if (p.y < CaptionBackgroundHeight)
                                        return (IntPtr)HitTestValues.HTCLOSE; // HTCAPTION


                                if (p.cx < s)
                                    if (p.cy < s)
                                        return (IntPtr)HitTestValues.HTBOTTOMRIGHT; // HTCAPTION

                                if (p.cx < s)
                                    if (p.y < s)
                                        return (IntPtr)HitTestValues.HTTOPRIGHT; // HTCAPTION

                                if (p.x < s)
                                    if (p.cy < s)
                                        return (IntPtr)HitTestValues.HTBOTTOMLEFT; // HTCAPTION

                                if (p.x < s)
                                    return (IntPtr)HitTestValues.HTLEFT; // HTCAPTION

                                if (p.y < s)
                                    return (IntPtr)HitTestValues.HTTOP; // HTCAPTION


                                if (p.cx < s)
                                    return (IntPtr)HitTestValues.HTRIGHT; // HTCAPTION

                                if (p.cy < s)
                                    return (IntPtr)HitTestValues.HTBOTTOM; // HTCAPTION

                                if (p.y < CaptionBackgroundHeight)
                                    return (IntPtr)HitTestValues.HTCAPTION; // HTCAPTION

                                return (IntPtr)HitTestValues.HTTRANSPARENT; // HTCAPTION

                            }
                            return IntPtr.Zero;
                        }

                   );
                };

            w.SizeTo(1280, 768);
            SizeChanged();


            InterceptKeys.InternalMain(
                Rehook =>
                {
                    w.Deactivated +=
                        delegate
                        {
                            TopicText.Focusable = false;
                        };
                    w.Activated +=
                        delegate
                        {


                            //TopicText.Focus();
                            Rehook();
                        };

                    w.ShowDialog();
                }
            );

        }

        enum HitTestValues
        {
            HTERROR = -2,
            HTTRANSPARENT = -1,
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTSYSMENU = 3,
            HTGROWBOX = 4,
            HTMENU = 5,
            HTHSCROLL = 6,
            HTVSCROLL = 7,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTBORDER = 18,
            HTOBJECT = 19,
            HTCLOSE = 20,
            HTHELP = 21
        }





        //http://blogs.msdn.com/b/toub/archive/2006/05/03/589423.aspx
        class InterceptKeys
        {
            private const int WH_KEYBOARD_LL = 13;
            private const int WM_KEYDOWN = 0x0100;
            private const int WM_KEYUP = 0x0101;
            private static LowLevelKeyboardProc _proc = HookCallback;
            private static IntPtr _hookID = IntPtr.Zero;

            public static event Action<Key> KeyUp;
            public static event Action<Key> KeyDown;


            public static void InternalMain(Action<Action> body)
            {
                _hookID = SetHook(_proc);
                try
                {
                    body(
                        delegate
                        {
                            UnhookWindowsHookEx(_hookID);
                            _hookID = SetHook(_proc);
                        }
                    );
                }
                finally
                {
                    UnhookWindowsHookEx(_hookID);
                }
            }

            private static IntPtr SetHook(LowLevelKeyboardProc proc)
            {
                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                        GetModuleHandle(curModule.ModuleName), 0);
                }
            }

            private delegate IntPtr LowLevelKeyboardProc(
                int nCode, IntPtr wParam, IntPtr lParam);

            static Dictionary<Key, bool> KeyDownEnabled = new Dictionary<Key, bool>();

            private static IntPtr HookCallback(
                int nCode, IntPtr wParam, IntPtr lParam)
            {
                if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    var key = (KeyInterop.KeyFromVirtualKey(vkCode));

                    if (!KeyDownEnabled.ContainsKey(key) || KeyDownEnabled[key])
                        if (KeyDown != null)
                            KeyDown(key);

                    KeyDownEnabled[key] = false;
                }

                if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    var key = (KeyInterop.KeyFromVirtualKey(vkCode));
                    KeyDownEnabled[key] = true;
                    if (KeyUp != null)
                        KeyUp(key);
                }

                return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern IntPtr SetWindowsHookEx(int idHook,
                LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool UnhookWindowsHookEx(IntPtr hhk);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
                IntPtr wParam, IntPtr lParam);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern IntPtr GetModuleHandle(string lpModuleName);
        }
    }
}
