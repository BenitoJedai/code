﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NavigationButtons.Code;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace NavigationButtons
{
	class Program
	{
	
		[STAThread]
		static public void Main(string[] args)
		{
			new MyCanvas().ToWindow().ShowDialog();
		}
	}
}
