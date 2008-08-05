using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared.Lambda;

using FlashSpaceInvaders.ActionScript.Extensions;
using System.Collections;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class SpriteWithMovement : Sprite, IEnumerable
	{
		

		public void Add(DisplayObject e)
		{
			e.AttachTo(this);
		}


		public readonly Property<Point> MoveToTarget = new Property<Point> { Value = new Point() };

		public SpriteWithMovement MoveTo(double x, double y)
		{
			MoveToTarget.Value = new Point { x = x, y = y };


			return this;
		}

		public Rectangle ClipRectangle;

		public double MaxStep = 0;

		public SpriteWithMovement()
		{
			(1000 / 30).AtInterval(
				t =>
				{
					var c = this.ToPoint();

					var x = this.MoveToTarget.Value - c;

					if (x.length < 2)
					{
					}
					else
					{
						var step = x.length;

						if (x.length < 4)
						{
							step /= 2;

						}
						else
						{
							step /= 4;

						}

						if (MaxStep > 0)
							if (MaxStep < step)
								step = MaxStep;

						this.MoveToArc(x.GetRotation(), step);

						if (ClipRectangle != null)
						{
							this.x = this.x.Min(this.ClipRectangle.right).Max(this.ClipRectangle.left);
							this.y = this.y.Min(this.ClipRectangle.bottom).Max(this.ClipRectangle.top);
						}
					}
				}
			);
		}



		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}

}
