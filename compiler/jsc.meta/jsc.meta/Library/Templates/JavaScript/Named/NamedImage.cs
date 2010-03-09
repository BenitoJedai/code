﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib;
namespace jsc.meta.Library.Templates.JavaScript
{
	[Script(InternalConstructor = true)]
	public class NamedImage : global::ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage
	{
		internal const string IHTMLImage_src = "IHTMLImage.src";
		
		public NamedImage()
		{

		}

		public static NamedImage InternalConstructor()
		{
			var i = new IHTMLImage { src = IHTMLImage_src };

			i.style.SetSize(NamedImageInformation.GetImageDefaultWidth(), NamedImageInformation.GetImageDefaultHeight());


			return (NamedImage)i;
		}

		public static int ImageDefaultWidth
		{
			get
			{
				return NamedImageInformation.GetImageDefaultWidth();
			}
		}

		public static int ImageDefaultHeight
		{
			get
			{
				return NamedImageInformation.GetImageDefaultHeight();
			}
		}


		public static int ImageFileSize
		{
			get
			{
				return NamedImageInformation.GetImageFileSize();
			}
		}
	}


}
