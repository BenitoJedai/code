using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace Mahjong.Desktop
{
	class ServerWindow : Canvas
	{
		public ServerWindow(Action SpawnClient)
		{
			Width = 300;
			Height = 200;

			var b = new Button { Content = "Spawn client", Margin = new Thickness(16), Padding = new Thickness(4) }.AttachTo(this);

			b.Click += delegate { SpawnClient(); };
		}
	}

	class Program
	{
		[STAThread]
		static public void Main(string[] args)
		{
			// we will start an inmemory server and launch 3 clients

			new ServerWindow(
				delegate
				{
					Console.WriteLine("new client");
				}
			).ToWindow().ShowDialog();
		}
	}
}
