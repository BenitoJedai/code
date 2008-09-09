using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.TextSuggestions;
using ScriptCoreLib.Shared.Lambda;

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
				Height = 200,
				BorderThickness = new Thickness(0)
			}.MoveTo(8, 8).AttachTo(this);

			var t_unfocus = new TextBox
			{
				// watermark text
				Text = "powered by jsc",
				Width = 400,
				Height = 24,
				Foreground = Brushes.Gray,
				BorderThickness = new Thickness(0),
				IsReadOnly = true
			}.MoveTo(8, 16 + 200).AttachTo(this);

			
			var t = new TextBox
			{
				Background = Brushes.Transparent,
				Text = "us",
				Width = 400,
				Height = 24,
				TextAlignment = TextAlignment.Right,
				BorderThickness = new Thickness(0)
			}.MoveTo(8, 16 + 200).AttachTo(this);

	

			var s = new TextSuggestionsControl(t, 7, t_unfocus, this);

			#region indicate when we are in search mode
			s.Enter +=
				delegate
				{
					t_unfocus.Text = "search";
				};

			s.Exit +=
				delegate
				{
					t_unfocus.Text = "powered by jsc";
				};
			#endregion

			#region update suggestions
			Action UpdateSuggestions = 
				delegate
				{
					s.Suggestions = data.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(k => k.Trim()).WhereNot(k => string.IsNullOrEmpty(k)).ToArray();
				};


			data.TextChanged +=
				delegate
				{
					UpdateSuggestions();
				};

			UpdateSuggestions();
			#endregion

		}
	}
}
