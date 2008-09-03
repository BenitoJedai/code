using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media.Animation;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.ImageSource))]
	internal class __ImageSource : __Animatable
	{
		public static implicit operator __ImageSource(ImageSource e)
		{
			return (__ImageSource)(object)e;
		}

		public static implicit operator ImageSource(__ImageSource e)
		{
			return (ImageSource)(object)e;
		}

		public string InternalManifestResourceAlias;


		public Future<IHTMLImage> InternalBitmap;
	
	}
}
