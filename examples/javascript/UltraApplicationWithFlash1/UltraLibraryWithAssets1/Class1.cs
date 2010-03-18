using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltraLibraryWithAssets1.HTML.Pages.FromAssets.Remoting;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;

namespace UltraLibraryWithAssets1
{
	public class Class1 : Assets
	{
		public Class1(PHTMLDocument doc)
			: base(doc)
		{
			WhenReady(
				delegate
				{
					this.WorldSpan.onmouseover +=
						e =>
						{
							// can we have a "look ahead" Interface .style generated?

							this.WorldSpan.setAttribute("style", "color:red;");
						};

					this.WorldSpan.onmouseout +=
						e =>
						{
							// can we have a "look ahead" Interface .style generated?

							this.WorldSpan.setAttribute("style", "");
						};

					this.OK.onclick +=
						e =>
						{
							this.OK.setAttribute("style", "color: blue;");

							this.OK.innerText = "thanks!";
						};

				}
			);
		}
	}
}
