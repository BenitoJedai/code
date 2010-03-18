using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;
using ScriptCoreLib.JavaScript.Remoting.Extensions;
using UltraLibraryWithAssets1.HTML.Images.FromBase64.Remoting;
using UltraLibraryWithAssets1.HTML.Pages.FromBase64.Remoting;

namespace UltraLibraryWithAssets1
{
	public class Class1 : Assets
	{
		public Class1(PHTMLDocument doc_, PHTMLElementAction Enhance)
			: base(doc_)
		{
			// be careful with closures!
			// csc will try to pass the closure to the base ctor!.

			var doc = doc_;

			// when not ready all calls shall be delayed and be implicitly done at WhenReady
			WhenReady(
				delegate
				{
					
					this.WorldSpan.onmouseover +=
						e =>
						{
							// can we have a "look ahead" Interface .style generated?

							this.WorldSpan.setAttribute("style", "color:red;");
							this.jsc.Container.get_style(style => style.border = "4px solid red");
						};

					this.WorldSpan.onmouseout +=
						e =>
						{
							// can we have a "look ahead" Interface .style generated?

							this.WorldSpan.setAttribute("style", "");
							this.jsc.Container.get_style(style => style.border = "1px solid red");
						};

					this.jsc.Container.get_style(style => style.border = "1px solid red");

					Enhance(this.jsc.Container);

					this.jsc.Container.onclick +=
						delegate
						{
							var n = new jsc(doc);

							n.WhenReady(
								delegate
								{
									n.Container.setAttribute("style", "cursor: pointer;");
									n.Container.setAttribute("title", "click to remove");

									n.Container.onclick +=
										delegate
										{
											n.Container.Orphanize();
										};

									n.AttachTo(this.Zone);
								}
							);
						};

					this.OK.onclick +=
						e =>
						{
							this.OK.setAttribute("style", "color: blue;");

							this.OK.get_style(
								style => style.color = "yellow"
							);

							this.OK.innerText = "thanks!";
						};

				}
			);
		}
	}
}
