using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.Extensions
{
	[Script]
	public static class IEventExtensions
	{
		public static double GetOffsetX(this IEvent e, IHTMLElement relativeTo)
		{
			__MouseEventArgs m = e;

			var p = m.GetPosition(relativeTo);

			return p.X;
		}

		public static double GetOffsetY(this IEvent e, IHTMLElement relativeTo)
		{
			__MouseEventArgs m = e;

			var p = m.GetPosition(relativeTo);

			return p.Y;
		}


		[Script]
		public class Point
		{
			public double X;
			public double Y;
		}

		// to be used from .Avalon too
		[Script]
		public class __MouseEventArgs
		{
			public double Internal_OffsetX;
			public double Internal_OffsetY;
			public IHTMLElement Internal_Element;

			public Point GetPosition(IHTMLElement relativeTo)
			{
				var e = relativeTo;

				// case 1
				if (e == Internal_Element)
					return new Point { X = Internal_OffsetX, Y = Internal_OffsetY };

				// case 2
				if (e.parentNode == Internal_Element.parentNode)
					return new Point
					{
						X = Internal_OffsetX + Internal_Element.offsetLeft + e.offsetLeft,
						Y = Internal_OffsetY + Internal_Element.offsetTop + e.offsetTop
					};

				// case 3
				return InternalGetPosition(e);
			}

			[Script]
			class GetPositionData
			{
				public IHTMLElement Element;

				public int X;
				public int Y;

				public static List<GetPositionData> Of(IHTMLElement e)
				{
					var a = new List<GetPositionData>();

					var x = 0;
					var y = 0;

					while (ShouldVisitParent(e))
					{
						x += e.offsetLeft;
						y += e.offsetTop;

						a.Add(
							new GetPositionData
							{
								Element = e,
								X = x,
								Y = y
							}
						);

						e = (IHTMLElement)e.parentNode;
					}

					return a;
				}

				private static bool ShouldVisitParent(IHTMLElement e)
				{
					if (e.parentNode == null)
						return false;

					return e.parentNode != Native.Document;
				}
			}

			private Point InternalGetPosition(IHTMLElement relativeTo)
			{
				var a = GetPositionData.Of(relativeTo);
				var b = GetPositionData.Of(Internal_Element);

				// top elements might be the same so we remove them
				var loop = true;

				while (loop)
				{
					loop = false;

					if (a.Count > 0)
						if (b.Count > 0)
							if (a[a.Count - 1].Element == b[b.Count - 1].Element)
							{
								a.RemoveAt(a.Count - 1);
								b.RemoveAt(b.Count - 1);

								loop = true;
							}
				}

				var x = 0;
				var y = 0;

				if (a.Count > 0)
				{
					var a_ = a[a.Count - 1];

					x += a_.X;
					y += a_.Y;
				}

				if (b.Count > 0)
				{
					var b_ = b[b.Count - 1];

					x += b_.X;
					y += b_.Y;
				}

				return new Point
				{
					X = Internal_OffsetX + x,
					Y = Internal_OffsetY + y
				};
			}



			public static implicit operator __MouseEventArgs(IEvent e)
			{
				return new __MouseEventArgs
				{
					Internal_OffsetX = e.OffsetX,
					Internal_OffsetY = e.OffsetY,
					Internal_Element = (IHTMLElement)e.Element
				};
			}
		}

	}
}
