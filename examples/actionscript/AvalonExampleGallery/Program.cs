using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AvalonExampleGallery.Shared;
using ScriptCoreLib.CSharp.Avalon.Extensions;

namespace AvalonExampleGallery
{
	class Program
	{
	

		[STAThread]
		static public void Main(string[] args)
		{
			new AvalonExampleGalleryCanvas().ToWindow().ShowDialog();
		}
	}
}
