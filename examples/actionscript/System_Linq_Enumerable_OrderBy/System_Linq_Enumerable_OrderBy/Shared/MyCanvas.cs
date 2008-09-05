using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace System_Linq_Enumerable_OrderBy.Shared
{
	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		void Assert(bool v, string message)
		{
			if (v)
				return;

		}

		public MyCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			#region Gradient
			for (int i = 0; i < DefaultHeight; i += 4)
			{
				new Rectangle
				{
					Fill = ((uint)(0xff00007F + Convert.ToInt32(128 * i / DefaultHeight))).ToSolidColorBrush(),
					Width = DefaultWidth,
					Height = 4,
				}.MoveTo(0, i).AttachTo(this);
			}
			#endregion


			var _string = new[] { "z", "b", "c", "a" };
			//var _int = new[] { 35, 10, 20, 30, 5 };
			//var _double = new[] { 0.35, 0.1, 0.2, 0.3, 0.05 };

			// jsc:javascript  InitializeArray

			Assert(_string.OrderBy(k => k).First() == "a", "string");
			Assert(_string.OrderByDescending(k => k).First() == "z", "string");
		}
	}
}
