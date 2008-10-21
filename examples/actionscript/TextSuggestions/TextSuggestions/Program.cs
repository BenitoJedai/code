using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TextSuggestions.Shared;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace TextSuggestions
{
	class Program
	{
	

		[STAThread]
		static public void Main(string[] args)
		{
			new TextSuggestionsCanvas().ToWindow().ShowDialog();
		}
	}
}
