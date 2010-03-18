using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;
using ScriptCoreLib.JavaScript.Remoting;

namespace jsc.meta.Library.Templates.JavaScript.Named.Remoting
{
	public class NamedImage : PUltraComponent
	{
		// FromWeb/FromAssets/FromBase64
		internal const string DefaultSource = "DefaultSource";


		public NamedImage(PHTMLDocument doc)
		{
			this.InternalDocument = doc;

			doc.createElement("img",
				img =>
				{
					this.InternalElement = img;

					this.InternalElement.setAttribute("src", DefaultSource);

					this.InternalElement.get_style(
						style =>
						{
							style.width = ImageDefaultWidth + "px";
							style.height = ImageDefaultHeight + "px";

							base.InternalMarkReady();
						}
					);
				}
			);
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
