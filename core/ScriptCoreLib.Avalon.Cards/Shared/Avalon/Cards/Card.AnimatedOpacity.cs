using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Tween;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	partial class Card 
	{
		
		internal Action<int, int> AnimatedOpacityEmitter;

		public double AnimatedOpacity
		{
			get
			{
				return this.Container.Opacity;
			}
			set
			{
				if (AnimatedOpacityEmitter == null)
				{
					AnimatedOpacityEmitter = NumericEmitter.Of(
						(v, r) =>
						{
							this.Container.Opacity = v * 0.01;
						}
					);


				}

				AnimatedOpacityEmitter(Convert.ToInt32(value * 100), 0);
			}
		}

	}
}
