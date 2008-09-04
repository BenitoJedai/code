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
using ScriptCoreLib.Shared.Lambda;

namespace EmbeddedResourcesBrowser.Shared
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


			var t = new TextBox
			{
				AcceptsReturn = true,
				Text = "",
				BorderThickness = new Thickness(0),
				Foreground = Brushes.Black,
				Background = Brushes.White,
				IsReadOnly = true,
				Width = 400,
				Height = 300
			}.MoveTo(32, 10).AttachTo(this);

			Assets.Default.FileNames.ForEach(
				(v, index, SignalNext) =>
				{
					t.AppendTextLine(v);

					v.ToStringAsset(
						value =>
						{
							t.AppendTextLine(index + ": " + value);


							500.AtDelay(SignalNext);
						}
					);
				}
			);

			//var c = new FutureStream();


			//// ready for first call
			//c.Signal();

			//foreach (var _v in Assets.Default.FileNames)
			//{
			//    var v = _v;

			//    c.Continue(
			//        SignalNext =>
			//        {

			//            t.AppendTextLine(v);

			//            v.ToStringAsset(
			//                value =>
			//                {
			//                    t.AppendTextLine(value);


			//                    SignalNext();
			//                }
			//            );
			//        }
			//    );
			//}


		}
	}
}
