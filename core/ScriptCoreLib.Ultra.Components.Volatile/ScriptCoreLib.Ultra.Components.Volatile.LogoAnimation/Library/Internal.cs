using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace ScriptCoreLib.Ultra.Components.Volatile.LogoAnimation.Library
{
    internal static class Internal
    {
        /// <summary>The GetForegroundWindow function returns a handle to the foreground window.</summary>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

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

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        // http://marcin.floryan.pl/blog/2010/08/wpf-drop-shadow-with-windows-dwm-api

        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        #region Constants

        internal static readonly int GWL_STYLE = -16;

        internal static readonly uint DWM_TNP_VISIBLE = 0x8;
        internal static readonly uint DWM_TNP_OPACITY = 0x4;
        internal static readonly uint DWM_TNP_RECTDESTINATION = 0x1;
        public const uint DWM_TNP_SOURCECLIENTAREAONLY = 0x00000010;

        static readonly ulong WS_VISIBLE = 0x10000000L;
        static readonly ulong WS_BORDER = 0x00800000L;
        internal static readonly ulong TARGETWINDOW = WS_BORDER | WS_VISIBLE;

        #endregion

        #region DWM functions

        [DllImport("dwmapi.dll")]
        internal static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

        [DllImport("dwmapi.dll")]
        internal static extern int DwmUnregisterThumbnail(IntPtr thumb);

        [DllImport("dwmapi.dll")]
        internal static extern int DwmQueryThumbnailSourceSize(IntPtr thumb, out PSIZE size);

        [DllImport("dwmapi.dll")]
        internal static extern int DwmUpdateThumbnailProperties(IntPtr hThumb, ref DWM_THUMBNAIL_PROPERTIES props);

        #endregion

        #region Win32 helper functions

        [DllImport("user32.dll")]
        internal static extern ulong GetWindowLongA(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        internal static extern int EnumWindows(EnumWindowsCallback lpEnumFunc, int lParam);
        internal delegate bool EnumWindowsCallback(IntPtr hwnd, int lParam);

        [DllImport("user32.dll")]
        public static extern void GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        #endregion

        internal class Window
        {
            public string Title;
            public IntPtr Handle;

            public override string ToString()
            {
                return Title;
            }

            [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
            public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);



            [DllImport("User32.dll", EntryPoint = "ShowWindowAsync")]
            private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
            private const int WS_SHOWNORMAL = 1;
            private const int SW_SHOWMAXIMIZED = 3;

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

            private struct WINDOWPLACEMENT
            {
                public int length;
                public int flags;
                public int showCmd;
                public System.Drawing.Point ptMinPosition;
                public System.Drawing.Point ptMaxPosition;
                public System.Drawing.Rectangle rcNormalPosition;
            }


            public void Activate()
            {

                //SystemParametersInfo((uint)0x2001, 0, 0, 0x0002 | 0x0001);
                ShowWindowAsync(Handle, WS_SHOWNORMAL);
                SetForegroundWindow(Handle);

                //SetForegroundWindow(Handle);
                //SystemParametersInfo((uint)0x2001, 200000, 200000, 0x0002 | 0x0001);
            }

            [DllImport("user32.dll")]
            static extern bool SetForegroundWindow(IntPtr hWnd);

        }

        #region Interop structs

        [StructLayout(LayoutKind.Sequential)]
        internal struct DWM_THUMBNAIL_PROPERTIES
        {
            public uint dwFlags;
            public Rect rcDestination;
            public Rect rcSource;
            public byte opacity;
            public bool fVisible;
            public bool fSourceClientAreaOnly;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Rect
        {
            internal Rect(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PSIZE
        {
            public int x;
            public int y;
        }

        #endregion
    }
}
