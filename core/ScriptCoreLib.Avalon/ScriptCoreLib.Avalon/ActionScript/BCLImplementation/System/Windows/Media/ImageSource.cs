﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Animation;
using System.Windows.Media;
using ScriptCoreLib.ActionScript.Extensions;
using System.IO;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Media.Animation;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media
{
    // http://referencesource.microsoft.com/#PresentationCore/src/Core/CSharp/System/Windows/Media/ImageSource.cs

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

		public Stream InternalStreamAlias;


		public Future<Bitmap> InternalBitmap;

	
	}
}
