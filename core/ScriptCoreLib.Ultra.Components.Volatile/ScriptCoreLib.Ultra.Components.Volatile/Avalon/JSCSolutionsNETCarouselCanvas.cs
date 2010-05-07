using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Components.Avalon.Images;
using ScriptCoreLib.Ultra.Components.Volatile.Avalon.Images;

namespace ScriptCoreLib.Avalon
{
	public class JSCSolutionsNETCarouselCanvas : ImageCarouselCanvas
	{
		class JSCSolutionsNETArguments : Arguments
		{
			public JSCSolutionsNETArguments()
			{
				this.AddImages =
					Add =>
					{
						Add(new Apple_Safari());
						Add(new Google_Chrome());
						Add(new Internet_Explorer_7_Logo());
						Add(new Firefox_3_5_logo());
						Add(new Opera());
					};

				this.ImageDefaultWidth = JSCSolutionsNETImage.ImageDefaultWidth;
				this.ImageDefaultHeight = JSCSolutionsNETImage.ImageDefaultHeight;

				this.CreateCenterImage =
					() => new JSCSolutionsNETImage();

			}
		}
		public JSCSolutionsNETCarouselCanvas()
			: base(new JSCSolutionsNETArguments())
		{

		}
	}
}
