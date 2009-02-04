using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Controls;
using System.ComponentModel;
using ScriptCoreLib.Shared.Lambda;

namespace ScriptCoreLib.Shared.Avalon.Extensions
{
	[Script]
	public interface ISupportsContainer
	{
		Canvas Container { get; }
	}

	[Script]
	public interface ISupportsLayout<T>
		where T : UIElement
	{
		T Value { get; }
		double Zoom { get; }


	}

	[Script]
	public static class SupportsContainerExtensions
	{
		// javascript DOM will not reflect the latest position
		// within the same callstack

		public static void Show(this ISupportsContainer e)
		{
			e.Show(true);
		}

		public static void Hide(this ISupportsContainer e)
		{
			e.Show(false);
		}

		public static void Show(this ISupportsContainer e, bool value)
		{
			e.Container.Show(value);
		}


		public static BindingList<T> AttachTo<T, K>(this BindingList<T> e, Func<T, K> selector, IAddChild c)
			where K : ISupportsContainer
		{
			e.ForEachNewOrExistingItem(k => selector(k).AttachContainerTo(c));
			e.ForEachItemDeleted(k => selector(k).OrphanizeContainer());

			return e;
		}

		public static BindingList<T> AttachTo<T>(this BindingList<T> e,  Func<IAddChild> GetContainer)
			where T : ISupportsContainer
		{
			e.ForEachNewOrExistingItem(
				k =>
				{
					var c = GetContainer();

					if (c != null)
						k.AttachContainerTo(c);
				}
			);
			e.ForEachItemDeleted(
				k =>
				{
					var c = GetContainer();

					if (c != null)
						k.OrphanizeContainer();
				}
			);

			return e;
		}


		public static BindingList<T> AttachTo<T>(this BindingList<T> e, IAddChild c)
			where T : ISupportsContainer
		{
			e.ForEachNewOrExistingItem(k => k.AttachContainerTo(c));
			e.ForEachItemDeleted(k => k.OrphanizeContainer());

			return e;
		}

		public static BindingList<T> AttachToFrameworkElement<T>(this BindingList<T> e, IAddChild c)
		where T : FrameworkElement
		{
			e.ForEachNewOrExistingItem(k => k.AttachTo(c));
			e.ForEachItemDeleted(k => k.Orphanize());

			return e;
		}

		public static T BringContainerToFront<T>(this T e)
			where T : ISupportsContainer
		{
			if (e == null)
				return e;

			e.Container.BringToFront();

			return e;
		}

		public static Panel GetParentPanel<T>(this T e)
			where T : FrameworkElement
		{
			var p = e.Parent;

			if (p == null)
				return null;

			var Panel = p as Panel;

			if (Panel == null)
				throw new NotImplementedException("Parent should have been a Panel");

			return Panel;
		}

		public static T BringToFront<T>(this T e)
			where T : FrameworkElement
		{
			if (e == null)
				return e;


			var Panel = e.GetParentPanel();

			if (Panel == null)
				return e;

			Panel.Children.Remove(e);
			Panel.Children.Add(e);

			return e;
		}

		[Script]
		internal class SupportsLayout<T> : ISupportsLayout<T>
		where T : UIElement
		{
			public T Value { get; set; }
			public double Zoom { get; set; }

			public SupportsLayout(T Value, double Zoom)
			{
				this.Value = Value;
				this.Zoom = Zoom;
			}
		}

		public static ISupportsLayout<T> WithZoom<T>(this T e, double Zoom)
		where T : UIElement
		{
			return new SupportsLayout<T>(e, Zoom);
		}

		public static T MoveTo<T>(this T e, double x, double y)
					where T : UIElement
		{
			Canvas.SetLeft(e, x);
			Canvas.SetTop(e, y);

			return e;
		}

		public static ISupportsLayout<T> MoveTo<T>(this ISupportsLayout<T> e, double x, double y)
				where T : UIElement
		{
			Canvas.SetLeft(e.Value, x * e.Zoom);
			Canvas.SetTop(e.Value, y * e.Zoom);

			return e;
		}

		public static ISupportsLayout<T> MoveTo<T>(this ISupportsLayout<T> e, int x, int y)
			where T : UIElement
		{
			Canvas.SetLeft(e.Value, x * e.Zoom);
			Canvas.SetTop(e.Value, y * e.Zoom);

			return e;
		}


		public static T MoveTo<T>(this T e, int x, int y)
			where T : UIElement
		{
			Canvas.SetLeft(e, x);
			Canvas.SetTop(e, y);

			return e;
		}

		public static T SizeTo<T>(this T e, int w, int h)
			where T : FrameworkElement
		{
			e.Width = w;
			e.Height = h;


			return e;
		}


		public static T SizeTo<T>(this T e, double w, double h)
			where T : FrameworkElement
		{
			e.Width = w;
			e.Height = h;


			return e;
		}

		public static ISupportsLayout<T> SizeTo<T>(this ISupportsLayout<T> e, double w, double h)
			where T : FrameworkElement
		{
			e.Value.Width = e.Zoom * w;
			e.Value.Height = e.Zoom * h;


			return e;
		}

		public static ISupportsLayout<T> SizeTo<T>(this ISupportsLayout<T> e, int w, int h)
		where T : FrameworkElement
		{
			e.Value.Width = e.Zoom * w;
			e.Value.Height = e.Zoom * h;


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

		public static T[] AttachContainerTo<T>(this T[] e, ISupportsContainer c)
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

		public static T[] Orphanize<T>(this T[] e)
			where T : FrameworkElement
		{
			foreach (var v in e)
			{
				v.Orphanize();
			}

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
