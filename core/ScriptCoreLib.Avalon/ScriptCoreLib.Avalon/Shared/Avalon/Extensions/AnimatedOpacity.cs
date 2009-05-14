using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Tween;
using System.Windows;

namespace ScriptCoreLib.Shared.Avalon.Extensions
{
	[Script]
	public abstract class AnimatedOpacity
	{
		public abstract double Opacity { get; set; }
		public abstract void SetOpacity(double value, Action done);
	}

	[Script]
	public class AnimatedOpacity<T> : AnimatedOpacity
		where T : UIElement
	{
		public readonly T Element;

		public AnimatedOpacity(T Element)
		{
			this.Element = Element;

			this.InternalSetOpacity = NumericEmitter.OfDouble(
				(x, y) =>
				{
					this.Element.Show(x > 0);

					this.Element.Opacity = x;

					if (x == this.InternalOpacity)
						if (OpacityChanged != null)
							OpacityChanged();

				}
			);
		}

		Action<double, double> InternalSetOpacity;

		double InternalOpacity;

		public event Action OpacityChanged;
		public override double Opacity
		{
			get
			{
				return this.InternalOpacity;
			}
			set
			{
				this.InternalOpacity = value;
				this.InternalSetOpacity(value, 0);
			}
		}

		public override void SetOpacity(double value, Action done)
		{
			Action handler = null;

			handler = delegate
			{
				this.OpacityChanged -= handler;
				done();
			};

			this.OpacityChanged += handler;
			this.Opacity = value;
		}
	}

}
