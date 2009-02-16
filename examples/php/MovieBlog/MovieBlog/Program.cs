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
using MovieBlog.Client.Avalon;

namespace MovieBlog
{
	class Program
	{

		[STAThread]
		static public void Main(string[] args)
		{

			Server.Application.Application_Entrypoint();

			//var w = new AvalonCanvas().ToWindow();

			//w.ShowDialog();
		}
	}
}
