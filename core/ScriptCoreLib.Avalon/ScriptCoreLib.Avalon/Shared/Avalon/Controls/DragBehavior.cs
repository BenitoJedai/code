using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace ScriptCoreLib.Shared.Avalon.Controls
{
	[Script]
	public class DragBehavior
	{
		public event Action DragStart;
		public event Action DragStop;

		public Func<double, double> SnapX = e => e;
		public Func<double, double> SnapY = e => e;

		public DragBehavior(UIElement DraggableArea, UIElement DraggableElement, UIElement DragContainer)
		{
			var drag = new { X = 0.0, Y = 0.0 }.ToDefault();

			DraggableArea.MouseLeftButtonDown +=
				(sender, args) =>
				{
					if (DragStart != null)
						DragStart();

					var p = args.GetPosition(DraggableElement);
					drag = new { p.X, p.Y };
				};

			DragContainer.MouseMove +=
				(sender, args) =>
				{
					if (drag == null)
						return;

					var p = args.GetPosition(DragContainer);
					var q = new { X = SnapX(p.X - drag.X), Y = SnapY(p.Y - drag.Y) };




					DraggableElement.MoveTo(q.X, q.Y);
				};

			DragContainer.MouseLeftButtonUp +=
				(sender, args) =>
				{
					if (drag == null)
						return;

					if (DragStop != null)
						DragStop();

					drag = null;
				};

		}
	}
}
