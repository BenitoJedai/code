using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace FlashResources.AvalonTest
{
	public class DefaultPage
	{
	
	

		[STAThread]
		public static void Main(string[] args)
		{
			new FlashResources.ActionScript.MyCanvas().ToWindow().ShowDialog();
		}
	}
}
