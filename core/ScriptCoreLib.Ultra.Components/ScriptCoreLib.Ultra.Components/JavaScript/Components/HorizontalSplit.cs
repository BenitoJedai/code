using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.Ultra.Components.HTML.Images.SpriteSheet.FromAssets;

namespace ScriptCoreLib.JavaScript.Components
{
	public class HorizontalSplit : HorizontalSplitBase
	{
		// are we using the correct namespace?
		// we should be inheriting Component

		public class HorizontalSplitArguments : HorizontalSplitBase.Arguments
		{
			public HorizontalSplitArguments()
			{
				Split = new HorizontalSplitPage();
				SplitArea = new HorizontalSplitAreaPage();
				SplitImage = new DragAreaImage();
				SplitImageWidth = DragAreaImage.ImageDefaultWidth;
				SplitImageHeight = DragAreaImage.ImageDefaultHeight;
			}

		}

		public HorizontalSplit()
			: base(new HorizontalSplitArguments())
		{

		}

	}
}
