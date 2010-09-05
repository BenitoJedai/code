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

namespace ScriptCoreLib.Ultra.Components.Volatile.LogoAnimation
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var c = new JSCSolutionsNETCarouselCanvas();



            //c.Container.Effect = new DropShadowEffect();
            //c.Container.BitmapEffect = new DropShadowBitmapEffect();

            var t = new System.Windows.Controls.TextBox
            {
                IsReadOnly = true,
                Background = Brushes.Transparent,
                BorderThickness = new System.Windows.Thickness(0),
                Foreground = Brushes.Blue,
                BitmapEffect = new System.Windows.Media.Effects.OuterGlowBitmapEffect() { GlowColor = Colors.White, GlowSize = 8 },
                //Effect = new DropShadowEffect(),
                Text = "jsc-solutions.net",
                //TextDecorations = TextDecorations.Underline,
                FontFamily = new FontFamily("Verdana"),
                FontSize = 16,
                TextAlignment = System.Windows.TextAlignment.Left
            }
            .AttachTo(c)
            .MoveTo(
            ImageCarouselCanvas.DefaultWidth / 2 + 48,
            ImageCarouselCanvas.DefaultHeight / 2 - 96).SizeTo(ImageCarouselCanvas.DefaultWidth, 96);
            //.MoveTo(0, ImageCarouselCanvas.DefaultHeight - 96).SizeTo(ImageCarouselCanvas.DefaultWidth, 96);

            var cc = new Canvas();


            // http://cloudstore.blogspot.com/2008/05/creating-custom-window-style.html
            var wcam = new Window();
            wcam.Background = Brushes.Transparent;
            wcam.WindowStyle = WindowStyle.None;
            wcam.ResizeMode = ResizeMode.NoResize;
            wcam.SizeTo(200, 200);
            wcam.AllowsTransparency = true;
            wcam.Opacity = 0.5;
            wcam.ShowInTaskbar = false;
            wcam.Cursor = Cursors.Hand;
            wcam.Focusable = false;

            var w = cc.ToWindow();

            w.SizeToContent = SizeToContent.Manual;
            w.SizeTo(400, 400);
            w.ToTransparentWindow();


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

            c.MoveContainerTo(-100, -100).AttachContainerTo(winfoc);

            c.CloseOnClick = false;
            c.AtClose += w.Close;

            #region ActivateInput
            Action ActivateInput =
                delegate
                {
                    w.MakeInteractive(true);

                    w.Background = SystemColors.ActiveCaptionBrush;
                    //wcam.Background = SystemColors.InactiveCaptionBrush;



                };
            #endregion

            #region DeactivateInput
            Action DeactivateInput =
                delegate
                {
                    w.MakeInteractive(false);

                    w.Background = Brushes.Transparent;
                    //wcam.Background = Brushes.Transparent;

                    t.Text = "jsc-solutions.net";
                };
            #endregion



            var NextInputMode = new[] { ActivateInput, DeactivateInput }.ToCyclicAction(a => a());

            var NextInputModeEnabled = false;
            var NextInputModeKeyDownEnabled = false;

            Action<Key> NextInputModeKeyDown = delegate { };

            #region KeyDown
            InterceptKeys.KeyDown +=
                key =>
                {
                    if (key == Key.LeftShift)
                    {
                        wcam.Background = new SolidColorBrush(Color.FromArgb(10, 0, 0, 0));
                        wcam.MakeInteractive(true);


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
                    if (key == Key.LeftShift)
                    {
                        wcam.MakeInteractive(false);
                        wcam.Background = Brushes.Transparent;

                        //DeactivateInput();
                        if (NextInputModeEnabled)
                            NextInputMode();
                    }
                    else
                    {

                    }

                    NextInputModeEnabled = false;
                };

            var s = 8;

            var ThumbnailSize = 0.4;

            Action UpdateChildren =
                delegate
                {
                    if (w.ActualWidth == 0)
                        return;

                    var qw = w.ActualWidth - s * 2;
                    var qh = w.ActualHeight - s * 2;

                    winfo.MoveTo(w.Left, w.Top).SizeTo(w.ActualWidth, w.ActualHeight);

                    wcam.MoveTo(w.Left + s, w.Top + (w.ActualHeight - qh * ThumbnailSize - s));
                    wcam.SizeTo(
                        qw * ThumbnailSize,
                        qh * ThumbnailSize
                    );
                };

            w.LocationChanged +=
                delegate
                {
                    UpdateChildren();
                };

            w.SizeChanged +=
                delegate
                {
                    UpdateChildren();
                };



            wcam.SourceInitialized +=
                delegate
                {
                    {
                        wcam.MakeInteractive(false);

                        UpdateChildren();
                    }

                    Func<IEnumerable<Internal.Window>> GetWindows =
                        delegate
                        {
                            var windows = new List<Internal.Window>();
                            Internal.EnumWindows(
                                (IntPtr hwnd, int lParam) =>
                                {
                                    if (new WindowInteropHelper(wcam).Handle != hwnd && (Internal.GetWindowLongA(hwnd, Internal.GWL_STYLE) & Internal.TARGETWINDOW) == Internal.TARGETWINDOW)
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

                    var thumb = IntPtr.Zero;
                    var ResetThumbnailSkip = 0;

                    Func<Internal.Window> GetCurrentThumbnail = () => GetWindows().AsCyclicEnumerable().Skip(ResetThumbnailSkip).First();


                    Action ResetThumbnail =
                        delegate
                        {
                            GetCurrentThumbnail().With(
                                shadow =>
                                {
                                    t.Text = shadow.Title;

                                    if (thumb != IntPtr.Zero)
                                        Internal.DwmUnregisterThumbnail(thumb);

                                    int i = Internal.DwmRegisterThumbnail(
                                        new WindowInteropHelper(wcam).Handle, shadow.Handle, out thumb);

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
                                                props.opacity = (byte)0xFF;
                                                props.rcDestination = new Internal.Rect(0, 0, (int)wcam.ActualWidth, (int)wcam.ActualHeight);
                                                props.fSourceClientAreaOnly = true;

                                                if (size.x < wcam.ActualWidth)
                                                {
                                                    props.rcDestination.Right = props.rcDestination.Left + size.x;
                                                }

                                                if (size.y < wcam.ActualHeight)
                                                {
                                                    props.rcDestination.Bottom = props.rcDestination.Top + size.y;
                                                }

                                                Internal.DwmUpdateThumbnailProperties(thumb, ref props);
                                            }
                                        };

                                    UpdateThumbnail();

                                    wcam.SizeChanged += delegate { UpdateThumbnail(); };
                                }
                            );
                        };


                    ResetThumbnail();

                    NextInputModeKeyDown +=
                        key =>
                        {
                            if (key == Key.Down)
                            {
                                NextInputModeKeyDownEnabled = true;
                                ResetThumbnailSkip = GetWindows().TakeWhile(k => k.Handle != Internal.GetForegroundWindow()).Count();
                                ResetThumbnail();
                            }
                            if (key == Key.Right)
                            {
                                NextInputModeKeyDownEnabled = true;
                                ResetThumbnailSkip++;
                                ResetThumbnail();
                            }
                            if (key == Key.Left)
                            {
                                NextInputModeKeyDownEnabled = true;

                                if (ResetThumbnailSkip > 0)
                                {
                                    ResetThumbnailSkip--;
                                    ResetThumbnail();
                                }
                            }

                        };

                    wcam.Activated +=
                        delegate
                        {
                            NextInputModeEnabled = false;
                            if (ThumbnailSize == 1)
                                ThumbnailSize = 0.4;
                            else
                                ThumbnailSize = 1;
                            UpdateChildren();

                            winfo.Hide();
                            winfo.Show();
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

                                t.Text = p.ToString();

                                handeled = true;


                                if (p.x < s)
                                    if (p.y < s)
                                        return (IntPtr)HitTestValues.HTTOPLEFT; // HTCAPTION

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


                                return (IntPtr)HitTestValues.HTCAPTION; // HTCAPTION

                            }
                            return IntPtr.Zero;
                        }

                   );
                };

            InterceptKeys.InternalMain(
                delegate
                {
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


        internal class NativeMethods
        {




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

            public static void InternalMain(Action body)
            {
                _hookID = SetHook(_proc);
                try
                {
                    body();
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
