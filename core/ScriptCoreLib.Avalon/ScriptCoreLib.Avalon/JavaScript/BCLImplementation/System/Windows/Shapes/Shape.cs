using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Shapes
{
	[Script(Implements = typeof(global::System.Windows.Shapes.Shape))]
	internal abstract class __Shape : __FrameworkElement
	{
		public IHTMLDiv InternalSprite;

		public __Shape()
		{
			InternalSprite = new IHTMLDiv();
		}

		public override IHTMLElement InternalGetDisplayObject()
		{
			return InternalSprite;
		}

		public Brush Fill { get; set; }
		public Brush Stroke { get; set; }

	}
}
