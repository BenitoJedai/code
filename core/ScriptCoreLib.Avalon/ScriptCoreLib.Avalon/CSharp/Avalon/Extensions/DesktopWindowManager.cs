using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Controls;
using System.Diagnostics;

namespace ScriptCoreLib.CSharp.Avalon.Extensions
{
	public static class DesktopWindowManager
	{
        // http://msdn.microsoft.com/en-us/magazine/cc163435.aspx

        #region types
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
		{
			public MARGINS(Thickness t)
			{
				Left = (int)t.Left;
				Right = (int)t.Right;
				Top = (int)t.Top;
				Bottom = (int)t.Bottom;
			}
			public int Left;
			public int Right;
			public int Top;
			public int Bottom;
		}

        [StructLayout(LayoutKind.Sequential)]
        public class DWM_THUMBNAIL_PROPERTIES
        {
            public uint dwFlags;
            public RECT rcDestination;
            public RECT rcSource;
            public byte opacity;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fVisible;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fSourceClientAreaOnly;
            public const uint DWM_TNP_RECTDESTINATION = 0x00000001;
            public const uint DWM_TNP_RECTSOURCE = 0x00000002;
            public const uint DWM_TNP_OPACITY = 0x00000004;
            public const uint DWM_TNP_VISIBLE = 0x00000008;
            public const uint DWM_TNP_SOURCECLIENTAREAONLY = 0x00000010;
        }

        //[StructLayout(LayoutKind.Sequential)]
        //public class MARGINS
        //{
        //    public int cxLeftWidth, cxRightWidth,
        //               cyTopHeight, cyBottomHeight;

        //    public MARGINS(int left, int top, int right, int bottom)
        //    {
        //        cxLeftWidth = left; cyTopHeight = top;
        //        cxRightWidth = right; cyBottomHeight = bottom;
        //    }
        //}

        [StructLayout(LayoutKind.Sequential)]
        public class DWM_BLURBEHIND
        {
            public uint dwFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fEnable;
            public IntPtr hRegionBlur;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fTransitionOnMaximized;

            public const uint DWM_BB_ENABLE = 0x00000001;
            public const uint DWM_BB_BLURREGION = 0x00000002;
            public const uint DWM_BB_TRANSITIONONMAXIMIZED = 0x00000004;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, right, bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left; this.top = top;
                this.right = right; this.bottom = bottom;
            }
        }
        #endregion

        #region methods

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableBlurBehindWindow(
            IntPtr hWnd, DWM_BLURBEHIND pBlurBehind);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(
            IntPtr hWnd, MARGINS pMargins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableComposition(bool bEnable);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmGetColorizationColor(
            out int pcrColorization,
            [MarshalAs(UnmanagedType.Bool)]out bool pfOpaqueBlend);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern IntPtr DwmRegisterThumbnail(
            IntPtr dest, IntPtr source);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmUnregisterThumbnail(IntPtr hThumbnail);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmUpdateThumbnailProperties(
            IntPtr hThumbnail, DWM_THUMBNAIL_PROPERTIES props);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmQueryThumbnailSourceSize(
            IntPtr hThumbnail, out Size size);

        [DllImport("dwmapi.dll", PreserveSig = false)]
		static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);



		[DllImport("kernel32")]
		static extern IntPtr LoadLibrary(string lpFileName);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool FreeLibrary(IntPtr hModule);

        #endregion

        public static bool ExtendGlassFrame(this Window window)
		{
			return ExtendGlassFrame(window, new Thickness(-1));
		}

		public static void ExtendGlassFrame(this Window window, Control e)
		{
			e.Background = System.Windows.Media.Brushes.White;
			ExtendGlassFrame(window, e.Margin);
		}

		public static bool ExtendGlassFrame(this Window window, Thickness margin)
		{
			// http://www.experts-exchange.com/Programming/Languages/Visual_Basic/Q_22916254.html

			var dwmapi = LoadLibrary("dwmapi.dll");

			if (dwmapi == IntPtr.Zero)
			{
				return false;
			}
			else
			{
				FreeLibrary(dwmapi);
				return InternalExtendGlassFrame(window, ref margin);
			}
		}

		private static bool InternalExtendGlassFrame(Window window, ref Thickness margin)
		{
#if NO_DWM
			return false;
#endif
			if (!DwmIsCompositionEnabled())
				return false;

			IntPtr hwnd = new WindowInteropHelper(window).Handle;
			if (hwnd == IntPtr.Zero)
				throw new InvalidOperationException("The Window must be shown before extending glass.");

			// Set the background to transparent from both the WPF and Win32 perspectives
			//SolidColorBrush background = new SolidColorBrush(Colors.Red);
			//background.Opacity = 0.5;
			window.Background = System.Windows.Media.Brushes.Transparent;
			HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent;

			MARGINS margins = new MARGINS(margin);
			DwmExtendFrameIntoClientArea(hwnd, ref margins);
			return true;
		}

		[Conditional("transparent")]
		public static void WithGlass(this Window x)
		{

			x.SourceInitialized +=
				delegate
				{
					ExtendGlassFrame(x);
				};


		}

		public static void ExplicitWithGlass(this Window x)
		{

			x.SourceInitialized +=
				delegate
				{
					ExtendGlassFrame(x);
				};


		}


		public const int WS_EX_TRANSPARENT = 0x00000020;
		public const int GWL_EXSTYLE = (-20);

		[DllImport("user32.dll")]
		public static extern int GetWindowLong(IntPtr hwnd, int index);

		[DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr hwnd, int index, WindowExStyles newStyle);



		public static void ToTransparentWindow(this Window x)
		{

			x.SourceInitialized +=
				delegate
				{
					// Get this window's handle
					IntPtr hwnd = new WindowInteropHelper(x).Handle;

					// Change the extended window style to include WS_EX_TRANSPARENT
					WindowExStyles extendedStyle = (WindowExStyles)GetWindowLong(hwnd, GWL_EXSTYLE);

					SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WindowExStyles.WS_EX_TRANSPARENT | WindowExStyles.WS_EX_NOACTIVATE);
				};


		}

	}
}
