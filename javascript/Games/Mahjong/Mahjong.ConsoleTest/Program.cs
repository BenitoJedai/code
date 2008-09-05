using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Mahjong.Code;
using System.Windows.Media;
using ScriptCoreLib.CSharp.Extensions;
using System.IO;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using Mahjong.Shared;
using Mahjong.ActionScript;

namespace Mahjong.ConsoleTest
{
	class Program
	{
		public static Window ToWindow(Canvas e)
		{
			return new Window
			{
				Background = Brushes.Black,
				SizeToContent = SizeToContent.WidthAndHeight,
				Content = e
			};
		}


		[STAThread]
		static void Main(string[] args)
		{
			//var lay = "assets/Mahjong.Layouts/test.lay".ToManifestResourceStream();
			//var lay = "assets/Mahjong.Layouts/Abstract Building.lay".ToManifestResourceStream();


			Assets.Default.FileNames.Where(k => k.EndsWith(".lay")).Random().ToStringAsset(
				lay => RenderLayout(new Layout(lay))
			);


			Assets.Default.FileNames.Where(k => k.EndsWith(".lay")).Random().ToStringAsset(
				lay => RenderLayout(new Layout(lay))
			);

			ToWindow(new MyCanvas()).ShowDialog();

		}

		private static void RenderLayout(Layout n)
		{
			var c = new[]
					{
						ConsoleColor.Black,
						ConsoleColor.Green,
						ConsoleColor.Yellow,
						ConsoleColor.Red,
						ConsoleColor.Cyan,
						ConsoleColor.Blue,
					};

			Console.WriteLine(n.Version);
			Console.WriteLine(n.Comment);

			var u = Console.BackgroundColor;
			for (int y = 0; y < Layout.DefaultCountY; y++)
			{
				for (int x = 0; x < Layout.DefaultCountX; x++)
				{
					var i = 0;

					for (int z = 0; z < n.CountZ; z++)
					{
						if (n[x, y, z])
							i = z + 1;
					}

					Console.BackgroundColor = c[i];
					Console.Write(" ");


				}
				Console.WriteLine();
			}
			Console.BackgroundColor = u;
		}
	}
}
