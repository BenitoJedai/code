using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System_IO_StringReader.Shared;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace System_IO_StringReader
{
	class Program
	{
	

		[STAThread]
		static public void Main(string[] args)
		{
			new System_IO_StringReaderCanvas().ToWindow().ShowDialog();
		}
	}
}
