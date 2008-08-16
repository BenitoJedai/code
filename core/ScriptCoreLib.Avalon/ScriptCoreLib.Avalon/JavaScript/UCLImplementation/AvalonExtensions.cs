using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;

namespace ScriptCoreLib.JavaScript.UCLImplementation
{
	[Script(Implements = typeof(global::ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions))]
	internal static class __AvalonExtensions
	{
		public static ImageSource ToSource(this string e)
		{
			// the c# version must do some internal work to figure
			// out the right stream name
			// in actionscript we are using [Embed]

			return new __ImageSource { InternalManifestResourceAlias = e };
		}
	}
}
