using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace jsc.meta.Library.Templates.Avalon
{
	public class AvalonNamedImage : Image
	{
		internal const string _src = "_src";

		public AvalonNamedImage()
		{
			var Source = global::ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions.ToSource(_src);

			this.Source = Source;

			this.Width = ImageDefaultWidth;
			this.Height = ImageDefaultHeight;
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
