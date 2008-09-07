using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ScriptCoreLib.Shared.Avalon.TextSuggestions
{
	[Script]
	public class TextSuggestionsControl
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

		public TextSuggestionsControl(TextBox Input)
		{
			this.Input = Input;
		}
	}
}
