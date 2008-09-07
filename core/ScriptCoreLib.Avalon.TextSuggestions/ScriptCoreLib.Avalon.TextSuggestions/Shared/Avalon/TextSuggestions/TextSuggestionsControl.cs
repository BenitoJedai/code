using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using ScriptCoreLib.Shared.Lambda;
using System.Windows.Media;

namespace ScriptCoreLib.Shared.Avalon.TextSuggestions
{
	[Script]
	public partial class TextSuggestionsControl
	{
		string[] InternalSuggestions;
		public string[] Suggestions
		{
			get
			{
				return InternalSuggestions;
			}
			set
			{
				InternalSuggestions = value;

				// clear caches
			}
		}

		public readonly TextBox Input;
		public readonly int MaxResults;
		public readonly UIElement Unfocus;

		public Brush InactiveResultForeground = Brushes.Black;
		public Brush InactiveResultBackground = Brushes.White;

		public Brush ActiveResultForeground = Brushes.White;
		public Brush ActiveResultBackground = Brushes.Blue;

		public int Margin = 4;

		public int Spacing = 8;

		public int Delay = 300;

		public TextSuggestionsControl(TextBox Input, int MaxResults, UIElement Unfocus)
		{
			this.Input = Input;
			this.MaxResults = MaxResults;
			this.Unfocus = Unfocus;
		}

		
	}
}
