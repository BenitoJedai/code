using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Controls;

namespace ScriptCoreLib.Shared.Avalon.Extensions
{
	[Script]
	public interface ISupportsContainer
	{
		Canvas Container { get; }
	}

	[Script]
	public static class SupportsContainerExtensions
	{
		// javascript DOM will not reflect the latest position
		// within the same callstack



		public static T MoveTo<T>(this T e, double x, double y)
					where T : UIElement
		{
			Canvas.SetLeft(e, x);
			Canvas.SetTop(e, y);

			return e;
		}


		public static T MoveTo<T>(this T e, int x, int y)
			where T : UIElement
		{
			Canvas.SetLeft(e, x);
			Canvas.SetTop(e, y);

			return e;
		}


		public static T MoveContainerTo<T>(this T e, int x, int y)
			where T : ISupportsContainer
		{
			var c = e.Container;

			Canvas.SetLeft(c, x);
			Canvas.SetTop(c, y);

			return e;
		}

		public static void AttachTo<T>(this T[] e, IAddChild c)
			where T : UIElement
		{


			foreach (var k in e)
			{
				k.AttachTo(c);
			}

		}

		public static T AttachTo<T>(this T e, IAddChild c)
			where T : UIElement
		{
			if (e == null)
				return e;

			UIElement x = e;

			c.AddChild(x);

			return e;
		}

		public static T AttachContainerTo<T>(this T e, IAddChild c)
			where T : ISupportsContainer
		{
			if (e == null)
				return e;

			e.Container.AttachTo(c);

			return e;
		}

		public static T[] AttachContainerTo<T>(this T[]  e, ISupportsContainer c)
			where T : ISupportsContainer
		{
			if (e == null)
				return e;

			foreach (var v in e)
			{
				v.Container.AttachTo(c.Container);
			}

			return e;
		}

		public static T[] AttachContainerTo<T>(this T[] e, IAddChild c)
			where T : ISupportsContainer
		{
			if (e == null)
				return e;

			foreach (var v in e)
			{
				v.Container.AttachTo(c);
			}

			return e;
		}

		public static T AttachContainerTo<T>(this T e, ISupportsContainer c)
			where T : ISupportsContainer
		{
			if (e == null)
				return e;

			e.Container.AttachTo(c.Container);

			return e;
		}

		public static T AttachTo<T>(this T e, ISupportsContainer c)
			where T : UIElement
		{
			if (e == null)
				return e;

			e.AttachTo(c.Container);

			return e;
		}

		public static T Orphanize<T>(this T e)
			where T : FrameworkElement
		{
			if (e == null)
				return default(T);

			var p = e.Parent;

			if (p == null)
				return e;

			var Panel = p as Panel;

			if (Panel == null)
				throw new NotImplementedException("Parent should have been a Panel");

			Panel.Children.Remove(e);

			return e;
		}

		public static T OrphanizeContainer<T>(this T e)
			where T : ISupportsContainer
		{
			e.Container.Orphanize();

			return e;
		}

		public static IEnumerable<T> Orphanize<T>(this IEnumerable<T> e)
				where T : FrameworkElement
		{
			foreach (var v in e)
			{
				v.Orphanize();
			}

			return e;
		}
	}
}
