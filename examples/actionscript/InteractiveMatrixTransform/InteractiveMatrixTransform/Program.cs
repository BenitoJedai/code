using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InteractiveMatrixTransform.Shared;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace InteractiveMatrixTransform
{
	class Program
	{

		[STAThread]
		static public void Main(string[] args)
		{
			var w = new OrcasAvalonApplicationCanvas().ToWindow();


			w.ShowDialog();
		}


	}
}
