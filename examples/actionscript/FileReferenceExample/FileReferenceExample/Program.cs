using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FileReferenceExample.Shared;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace FileReferenceExample
{
	class Program
	{

		[STAThread]
		static public void Main(string[] args)
		{
			new OrcasAvalonApplicationCanvas().ToWindow().ShowDialog();
		}
	}
}
