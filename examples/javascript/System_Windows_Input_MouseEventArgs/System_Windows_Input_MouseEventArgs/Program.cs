using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System_Windows_Input_MouseEventArgs.Shared;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace System_Windows_Input_MouseEventArgs
{
	class Program
	{


		[STAThread]
		static public void Main(string[] args)
		{
			new MouseEventArgsCanvas().ToWindow().ShowDialog();
		}
	}
}
