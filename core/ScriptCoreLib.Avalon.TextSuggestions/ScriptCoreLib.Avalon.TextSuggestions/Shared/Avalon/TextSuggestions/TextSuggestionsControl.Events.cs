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
	partial class TextSuggestionsControl
	{

		public event Action Enter;
		public event Action Exit;
	}
}
