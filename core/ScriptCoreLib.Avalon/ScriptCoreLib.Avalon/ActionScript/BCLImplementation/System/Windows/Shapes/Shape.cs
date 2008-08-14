using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Shapes
{
	[Script(Implements = typeof(global::System.Windows.Shapes.Shape))]
	internal abstract class __Shape : __FrameworkElement
	{
		public Sprite InternalSprite;

		public __Shape()
		{
			InternalSprite = new Sprite();
		}

		public override InteractiveObject InternalGetDisplayObject()
		{
			return InternalSprite;
		}

		public Brush Fill { get; set; }
		public Brush Stroke { get; set; }

	}
}
