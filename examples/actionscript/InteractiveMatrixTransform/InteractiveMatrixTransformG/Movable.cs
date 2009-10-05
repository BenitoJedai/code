using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Controls;
using System.Windows.Media;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Input;

namespace InteractiveMatrixTransformG
{
	public class Movable
	{
		public Canvas Container { get; set; }

		const int Size = 8;
		public Brush Color
		{
			set
			{
				Container.Background = value;
			}
		}

		double InternalX;
		double InternalY;

		public double X
		{
			get
			{
				return InternalX;
			}
		}

		public double Y
		{
			get
			{
				return InternalY;
			}
		}

		public event Action Changed;

		Panel InternalContext;
		public Panel Context
		{
			get
			{
				return InternalContext;
			}
			set
			{
				if (InternalContext != null)
					throw new InvalidOperationException();

				InternalContext = value;
				Container.Orphanize().AttachTo(value);

				var m = false;

				this.Container.MouseLeftButtonDown +=
					delegate
					{
						m = true;
					};

				this.Context.MouseMove +=
					(sender, args) =>
					{
						if (m)
						{
							var p = args.GetPosition(this.Context);

							var x = p.X;
							var y = p.Y;

							MoveTo(x, y);
						}
					};

				this.Context.MouseLeftButtonUp +=
					delegate
					{
						m = false;
					};
			}
		}

		public void MoveTo(double x, double y)
		{
			InternalX = x;
			InternalY = y;

			this.Container.MoveTo(InternalX - Size, InternalY - Size);

			if (Changed != null)
				Changed();
		}

		public Movable()
		{
			this.Container = new Canvas
			{
				Width = Size * 2,
				Height = Size * 2,
				Cursor = Cursors.Hand,
				Background = Brushes.Red
			};


		}
	}
}
