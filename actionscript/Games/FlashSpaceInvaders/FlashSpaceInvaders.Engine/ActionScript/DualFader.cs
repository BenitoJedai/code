using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;

using FlashSpaceInvaders.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class DualFader : Property<DisplayObject>
	{
		public DualFader()
		{
			var AbortFade = default(Action);

			this.ValueChanging +=
				(_Current, value) =>
				{
					if (_Current == value)
						return;

					if (AbortFade != null)
						AbortFade();

					if (_Current != null)
					{
						value.alpha = 1;
						value.AttachTo(_Current.parent);

						AbortFade = _Current.FadeOutAndOrphanize();
					}
				};
		}




	}
}
