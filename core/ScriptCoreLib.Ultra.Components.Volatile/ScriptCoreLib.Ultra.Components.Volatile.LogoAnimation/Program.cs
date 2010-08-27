using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Threading;
using System.Windows.Media;
using ScriptCoreLib.Avalon;
using System.Windows.Media.Effects;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

            new System.Windows.Controls.TextBox
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
            w.Background = Brushes.Transparent;
            w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            w.Topmost = true;
            w.ShowInTaskbar = false;
            w.Focusable = false;

            InterceptKeys.KeyDown +=
                key =>
                {
                    IntPtr hwnd = new WindowInteropHelper(w).Handle;
                    if (key == Key.LeftCtrl)
                    {
                        // Change the extended window style to include WS_EX_TRANSPARENT
                        WindowExStyles extendedStyle = (WindowExStyles)NativeMethods.GetWindowLong(hwnd, DesktopWindowManager.GWL_EXSTYLE);

                        extendedStyle &= ~(WindowExStyles.WS_EX_TRANSPARENT | WindowExStyles.WS_EX_NOACTIVATE);

                        NativeMethods.SetWindowLong(hwnd, DesktopWindowManager.GWL_EXSTYLE, extendedStyle);
                        w.Opacity = 0.5;
                    }
                };

            InterceptKeys.KeyUp +=
                key =>
                {
                    if (key == Key.LeftCtrl)
                    {
                        IntPtr hwnd = new WindowInteropHelper(w).Handle;
                        w.Opacity = 1;

                        // Change the extended window style to include WS_EX_TRANSPARENT
                        WindowExStyles extendedStyle = (WindowExStyles)NativeMethods.GetWindowLong(hwnd, DesktopWindowManager.GWL_EXSTYLE);

                        extendedStyle |= WindowExStyles.WS_EX_TRANSPARENT | WindowExStyles.WS_EX_NOACTIVATE;

                        NativeMethods.SetWindowLong(hwnd, DesktopWindowManager.GWL_EXSTYLE, extendedStyle);
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


                                handeled = true;
                                return (IntPtr)2; // HTCAPTION

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
