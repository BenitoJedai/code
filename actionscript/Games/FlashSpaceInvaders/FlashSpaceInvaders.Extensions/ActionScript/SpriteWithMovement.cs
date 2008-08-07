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



		public SpriteWithMovement TweenMoveTo(double x, double y)
		{
			MoveToTarget.Value = new Point { x = x, y = y };


			return this;
		}


		public SpriteWithMovement TweenMoveToArc(double arc, double length)
		{
			MoveToTarget.Value = MoveToTarget.Value.MoveToArc(arc, length);


			return this;
		}
		public Func<Point, Point> Clip;
		public Rectangle ClipRectangle;

		public double MaxStep = 0;

		public readonly BooleanProperty CloseEnough = new BooleanProperty { Value = true };

		public double StepMultiplier = 1;

		public SpriteWithMovement()
		{
			var t = (1000 / 30).AtInterval(
				delegate
				{
					

					var c = this.ToPoint();

					var x = this.MoveToTarget.Value - c;

					if (x.length < 2)
					{
						if (!CloseEnough.Value)
						{
							CloseEnough.Value = true;

							if (PositionChanged != null)
								PositionChanged();
						}
					}
					else
					{
						CloseEnough.Value = false;

						var step = x.length;

						if (x.length < 4)
						{
							step /= 2;

						}
						else
						{
							step /= 4;

						}
						step *= StepMultiplier;


						if (MaxStep > 0)
							if (MaxStep < step)
								step = MaxStep;

						this.MoveToArc(x.GetRotation(), step);

						if (Clip != null)
						{
							var p = Clip(this.ToPoint());

							this.x = p.x;
							this.y = p.y;
						}

						if (ClipRectangle != null)
						{
							this.x = this.x.Min(this.ClipRectangle.right).Max(this.ClipRectangle.left);
							this.y = this.y.Min(this.ClipRectangle.bottom).Max(this.ClipRectangle.top);
						}

						if (PositionChanged != null)
							PositionChanged();
					}

			
				}
			);


			this.removedFromStage +=
				delegate
				{
					t.stop();
				};

			this.addedToStage +=
				delegate
				{
					t.start();
				};
		}

		public event Action PositionChanged;


		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}

}
