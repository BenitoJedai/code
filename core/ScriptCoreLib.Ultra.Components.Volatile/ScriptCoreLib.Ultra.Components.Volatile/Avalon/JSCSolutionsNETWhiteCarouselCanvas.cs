using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Components.Avalon.Images;
using ScriptCoreLib.Ultra.Components.Volatile.Avalon.Images;

namespace ScriptCoreLib.Avalon
{
	public class JSCSolutionsNETWhiteCarouselCanvas : ImageCarouselCanvas
	{
		class JSCSolutionsNETWhiteArguments : Arguments
		{
            public JSCSolutionsNETWhiteArguments()
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

				this.ImageDefaultWidth = JSCSolutionsNETWhiteImage.ImageDefaultWidth;
                this.ImageDefaultHeight = JSCSolutionsNETWhiteImage.ImageDefaultHeight;

				this.CreateCenterImage =
                    () => new JSCSolutionsNETWhiteImage();

			}
		}
        public JSCSolutionsNETWhiteCarouselCanvas()
            : base(new JSCSolutionsNETWhiteArguments())
		{

		}
	}
}
