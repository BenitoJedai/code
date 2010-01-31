using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AvalonExampleGallery.Shared;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using AvalonExampleGallery;

[assembly: ScriptCoreLib.Script(
	IsScriptLibrary = true,
	NonScriptTypes = new[] { typeof(Program) }
	),
ScriptCoreLib.ScriptTypeFilter(ScriptCoreLib.ScriptType.ActionScript),
ScriptCoreLib.ScriptTypeFilter(ScriptCoreLib.ScriptType.JavaScript),

]

namespace AvalonExampleGallery
{
	public class Program
	{


		[STAThread]
		static public void Main(string[] args)
		{
			new AvalonExampleGalleryCanvas().ToWindow().ShowDialog();
		}
	}
}
