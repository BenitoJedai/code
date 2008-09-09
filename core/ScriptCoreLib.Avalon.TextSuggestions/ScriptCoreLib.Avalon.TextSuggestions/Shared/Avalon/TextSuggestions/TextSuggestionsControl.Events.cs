using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using ScriptCoreLib.Shared.Lambda;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ScriptCoreLib.Shared.Avalon.TextSuggestions
{
	partial class TextSuggestionsControl
	{

		public event Action Enter;
		public event Action Exit;

		public event Action<Rectangle, TextBox> Activate;
		public event Action<Rectangle, TextBox> Deactivate;

		public event Action<string> Select;
	}
}
