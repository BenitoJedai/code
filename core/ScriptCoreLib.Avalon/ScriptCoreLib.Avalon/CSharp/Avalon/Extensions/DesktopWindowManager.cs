using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Controls;
using System.Diagnostics;

namespace ScriptCoreLib.CSharp.Avalon.Extensions
{
	public static class DesktopWindowManager
	{

		struct MARGINS
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

		[DllImport("dwmapi.dll", PreserveSig = false)]
		static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		static extern bool DwmIsCompositionEnabled();

		[DllImport("kernel32")]
		static extern IntPtr LoadLibrary(string lpFileName);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool FreeLibrary(IntPtr hModule);

		public static bool ExtendGlassFrame(this Window window)
		{
			return window.ExtendGlassFrame(new Thickness(-1));
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
					x.ExtendGlassFrame();
				};


		}

		public static void ExplicitWithGlass(this Window x)
		{

			x.SourceInitialized +=
				delegate
				{
					x.ExtendGlassFrame();
				};


		}
	}
}
