using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.UserControl))]
	internal class __UserControl : __ContentControl
	{
		public readonly IHTMLDiv InternalSprite = new IHTMLDiv();

		public override IHTMLElement InternalGetDisplayObject()
		{
			return InternalSprite;
		}


		public __UserControl()
		{
			InternalSprite.style.width = "600px";
			InternalSprite.style.height = "400px";

			InternalSprite.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;


			InternalSprite.style.left = "0px";
			InternalSprite.style.top = "0px";



		}

		object InternalContent;

		protected override object InternalGetContent()
		{
			return InternalContent;
		}


		protected override void InternalSetContent(object e)
		{
			// removing "this." will fault MS csc
			this.InternalContent = e;

			var InternalContentUIElement = e as __UIElement;
			var InternalContent = InternalContentUIElement.InternalGetDisplayObjectDirect();

			InternalSprite.appendChild(InternalContent);

			InternalContent.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;

		}
	}
}
