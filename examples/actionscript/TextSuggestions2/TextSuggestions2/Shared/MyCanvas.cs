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
using ScriptCoreLib.Shared.Avalon.TextSuggestions;

namespace TextSuggestions2.Shared
{
	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 480;

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




			// http://www.useit.com/alertbox/intranet_design.html
			var TopTen =
				@"
     Bank of America, US
     Bankinter S.A., Spain
     Barnes & Noble, US
     British Airways, UK
     Campbell Soup Company, US
     Coldwell Banker Real Estate Corporation, US
     IKEA North America Service, LLC, US
     Ministry of Transport, New Zealand
     New South Wales Department of Primary Industries, Australia
     SAP AG, Germany 
				";

			var data = new TextBox
			{
				AcceptsReturn = true,
				Text = TopTen,
				Width = 400,
				Height = 200
			}.MoveTo(8, 8).AttachTo(this);

			var t_unfocus = new TextBox
			{
				// watermark text
				Text = "powered by jsc",
				Width = 400,
				Height = 24,
				Foreground = Brushes.Gray,
				IsReadOnly = true
			}.MoveTo(8, 16 + 200).AttachTo(this);

			
			var t = new TextBox
			{
				Background = Brushes.Transparent,
				Text = "us",
				Width = 400,
				Height = 24,
				TextAlignment = TextAlignment.Right
			}.MoveTo(8, 16 + 200).AttachTo(this);



			var s = new TextSuggestionsControl(t, 7, t_unfocus);

			#region update suggestions
			s.Suggestions = data.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			data.TextChanged +=
				delegate
				{
					s.Suggestions = data.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
				};
			#endregion

		}
	}
}
