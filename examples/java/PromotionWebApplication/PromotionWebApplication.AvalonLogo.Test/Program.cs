using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows;
using PromotionWebApplication.AvalonLogo.Desktop;
using System.Threading;

namespace PromotionWebApplication.AvalonLogo.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			AvalonLogoForDesktop.ShowDialogSplash(
				// primary task executes longer than splash
				() => Thread.Sleep(7000)
			);
		}
	}
}
