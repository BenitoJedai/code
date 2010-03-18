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

		public NamedImage(PHTMLDocument doc, PHTMLElementAction c)
		{
			this.InternalDocument = doc;

			doc.createElement("img",
				img =>
				{
					this.InternalElement = img;

					this.InternalElement.setAttribute("src", DefaultSource);

					base.InternalMarkReady();

					c(img);
				}
			);
		}


	}
}
