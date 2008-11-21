using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using WebApplication.Client.Avalon;

namespace WebApplication
{
	class Program
	{

		[STAThread]
		static public void Main(string[] args)
		{
			

			var w = new AvalonCanvas().ToWindow();

			w.ShowDialog();
		}
	}
}
