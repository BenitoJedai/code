using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Threading;
using System.Windows.Media;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.Shared.Avalon;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Media.Effects;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ScriptCoreLib.Shared.Lambda;

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

            var w = c.ToWindow();

            w.ToTransparentWindow();
            c.CloseOnClick = false;
            c.AtClose += w.Close;

            // http://blog.joachim.at/?p=39
            // http://blogs.msdn.com/changov/archive/2009/01/19/webbrowser-control-on-transparent-wpf-window.aspx
            // http://blogs.interknowlogy.com/johnbowen/archive/2007/06/20/20458.aspx
            w.AllowsTransparency = true;
            w.WindowStyle = System.Windows.WindowStyle.None;
            w.Background = new SolidColorBrush(Color.FromArgb(0x7F, 0, 0, 0));
            //w.Background = Brushes.Transparent;
            w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            w.Topmost = true;
            //w.ShowInTaskbar = false;
            w.Focusable = false;

            Action ActivateInput =
                delegate
                {
                    IntPtr hwnd = new WindowInteropHelper(w).Handle;

                    // Change the extended window style to include WS_EX_TRANSPARENT
                    WindowExStyles extendedStyle = (WindowExStyles)NativeMethods.GetWindowLong(hwnd, DesktopWindowManager.GWL_EXSTYLE);

                    extendedStyle &= ~(WindowExStyles.WS_EX_TRANSPARENT | WindowExStyles.WS_EX_NOACTIVATE);

                    NativeMethods.SetWindowLong(hwnd, DesktopWindowManager.GWL_EXSTYLE, extendedStyle);
                    //w.Opacity = 0.5;


                    w.Background = SystemColors.ActiveCaptionBrush;
                    
                };

            Action DeactivateInput =
                delegate
                {
                    IntPtr hwnd = new WindowInteropHelper(w).Handle;
                    //w.Opacity = 1;

                    // Change the extended window style to include WS_EX_TRANSPARENT
                    WindowExStyles extendedStyle = (WindowExStyles)NativeMethods.GetWindowLong(hwnd, DesktopWindowManager.GWL_EXSTYLE);

                    extendedStyle |= WindowExStyles.WS_EX_TRANSPARENT | WindowExStyles.WS_EX_NOACTIVATE;

                    NativeMethods.SetWindowLong(hwnd, DesktopWindowManager.GWL_EXSTYLE, extendedStyle);

                    w.Background = Brushes.Transparent;
                };

            var NextInputMode = new[] { ActivateInput, DeactivateInput }.ToCyclicAction(a => a());

            InterceptKeys.KeyDown +=
                key =>
                {
                    if (key == Key.LeftCtrl)
                    {
                        //ActivateInput();
                    }
                };

            InterceptKeys.KeyUp +=
                key =>
                {
                    if (key == Key.LeftCtrl)
                    {
                        //DeactivateInput();
                        NextInputMode();
                    }
                };



            w.SourceInitialized +=
                delegate
                {
                    HwndSource hwndSource = (HwndSource)HwndSource.FromVisual(w);
                    hwndSource.AddHook(

                        (IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handeled) =>
                        {
                            if (msg == 0x0084) // WM_NCHITTEST
                            {
                                //http://agsmith.wordpress.com/2008/09/16/hit-testing-in-wpf/

                                var p = new
                                {
                                    cx = w.ActualWidth - (NativeMethods.LOWORD(lParam) - w.Left),
                                    cy = w.ActualHeight - (NativeMethods.HIWORD(lParam) - w.Top),
                                    x = NativeMethods.LOWORD(lParam) - w.Left,
                                    y = NativeMethods.HIWORD(lParam) - w.Top
                                };

                                t.Text = p.ToString();

                                handeled = true;

                                var s = 8;

                                if (p.x < s)
                                    if (p.y < s)
                                        return (IntPtr)HitTestValues.HTTOPLEFT; // HTCAPTION

                                if (p.cx < s)
                                    if (p.cy < s)
                                        return (IntPtr)HitTestValues.HTBOTTOMRIGHT; // HTCAPTION

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
        

            [DllImport("user32.dll")]
            public static extern int GetWindowLong(IntPtr hwnd, int index);

            [DllImport("user32.dll")]
            public static extern int SetWindowLong(IntPtr hwnd, int index, WindowExStyles newStyle);


            internal const int WM_NCHITTEST = 0x0084,
                             HTBOTTOM = 15,
                             HTBOTTOMRIGHT = 17;
            internal static int HIWORD(int n)
            {
                return (n >> 16) & 0xffff;
            }

            internal static int HIWORD(IntPtr n)
            {
                return HIWORD(unchecked((int)(long)n));
            }

            internal static int LOWORD(int n)
            {
                return n & 0xffff;
            }

            internal static int LOWORD(IntPtr n)
            {
                return LOWORD(unchecked((int)(long)n));
            }

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

            private static IntPtr HookCallback(
                int nCode, IntPtr wParam, IntPtr lParam)
            {
                if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    if (KeyDown != null)
                        KeyDown((KeyInterop.KeyFromVirtualKey(vkCode)));
                }

                if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    if (KeyUp != null)
                        KeyUp((KeyInterop.KeyFromVirtualKey(vkCode)));
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
