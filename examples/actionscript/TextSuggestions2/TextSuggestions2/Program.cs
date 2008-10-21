using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TextSuggestions2.Shared;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace TextSuggestions2
{
	class Program
	{
	
		[STAThread]
		static public void Main(string[] args)
		{
			new MyCanvas().ToWindow().ShowDialog();
		}
	}
}
