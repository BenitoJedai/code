using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Diagnostics;

namespace DelegateToExtensionMethod.Shared
{
	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public MyCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;



			var g =
				Colors.Blue.ToGradient(Colors.Red, DefaultHeight / 4).Select(
					(c, i) =>
						new Rectangle
						{
							Fill = new SolidColorBrush(c),
							Width = DefaultWidth,
							Height = 4,
						}.MoveTo(0, i * 4).AttachTo(this)
				).ToArray();



			var t = new TextBox
			{
				 Text = "this text will be removed if you can take a delegate from extension method"
			}.MoveTo(4, 4).AttachTo(this);

			//Debugger.Break();

			//DoIt(global::ScriptCoreLib.Shared.Avalon.Extensions.AvalonSharedExtensions.Orphanize, t);
			DoIt(t.Orphanize);
		}

		void DoIt(Func<object> e)
		{
			e();
		}


		void DoIt(Func<TextBox, object> e, TextBox t)
		{
			e(t);
		}
	}
}
