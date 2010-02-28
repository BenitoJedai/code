﻿using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.Extensions
{
	[Script]
	public static class Extensions
	{
		/// <summary>
		/// shows element and sets opacity to 1
		/// </summary>
		public static T Show<T>(this T e)
			where T : IHTMLElement
		{
			e.style.display = IStyle.DisplayEnum.empty;
			e.style.Opacity = 1;

			return e;
		}



		public static T Show<T>(this T e, bool bVisible)
			where T : IHTMLElement
		{
			if (bVisible)
				return e.Show();
			else
				return e.Hide();
		}

		public static T Hide<T>(this T e)
			where T : IHTMLElement
		{
			e.style.display = IStyle.DisplayEnum.none;

			return e;
		}


		public static bool ToggleVisible<T>(this T e)
			where T : IHTMLElement
		{
			IStyle.DisplayEnum v = IStyle.DisplayEnum.empty;

			if (e.style.display == v)
			{
				e.Hide();

				return false;
			}
			else
			{
				e.Show();

				return true;
			}
		}

		/// <summary>
		/// detaches the node from dom; should be renamed to Orphanize
		/// </summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public static T Dispose<T>(this T e)
			where T : INode
		{
			return e.Orphanize();
		}

		public static T Orphanize<T>(this T e)
				where T : INode
		{
			if (e == null)
				throw new NullReferenceException();

			INode n = e.parentNode;

			if (n != null)
				n.removeChild(e);

			return e;
		}

		/// <summary>
		/// attaches this element to the current document body
		/// </summary>
		public static T AttachToDocument<T>(this T e)
			where T : INode
		{
			return e.AttachTo(Native.Document.body);
		}

		public static T AttachTo<T>(this T e, INode c)
			where T : INode
		{
			c.appendChild(e);

			return e;
		}

		public static T Deserialize<T>(this IXMLDocument e, object[] k)
					where T : class, new()
		{
			if (k == null)
				throw new Exception("Deserialize: k is null");

			return new IXMLSerializer<T>(k).Deserialize(e);
		}

		public static void Spawn(this Type alias)
		{
			ScriptCoreLib.JavaScript.Native.Spawn(alias.Name, i => Activator.CreateInstance(alias));
		}

		public static void SpawnEntrypointWithBrandning(this Type alias)
		{
			if (Native.Window == null)
				return;

			Native.Window.onload +=
				delegate
				{
					IHTMLImage i = "assets/ScriptCoreLib/jsc.png";

					i.style.position = IStyle.PositionEnum.absolute;
					i.style.right = "1em";
					i.style.bottom = "1em";
					i.AttachToDocument();
				};

			ScriptCoreLib.JavaScript.Native.Spawn(alias.Name, i => Activator.CreateInstance(alias));
		}

		public static void SpawnTo(this Type alias, Action<IHTMLElement> h)
		{
			// note: this method is used by jsc.meta

			ScriptCoreLib.JavaScript.Native.Spawn(alias.Name, i => h(i));
		}

		public static void SpawnTo<T>(this Type alias, object[] KnownTypes, Action<T> h)
			 where T : class, new()
		{
			SpawnTo<T>(alias, KnownTypes, (t, i) => h(t));
		}

		public static void SpawnTo<T>(this Type alias, object[] KnownTypes, Action<T, IHTMLElement> h)
			where T : class, new()
		{
			if (KnownTypes == null)
				throw new Exception("GetKnownTypes is null");

			ScriptCoreLib.JavaScript.Native.Spawn(alias.Name,
				i =>
				{
					if (i.nodeName == "SCRIPT")
					{
						var tag = (IHTMLScript)i;
						var text = i.text;

						if (tag.type == "text/xml")
						{
							var doc = IXMLDocument.Parse(text);

							h(doc.Deserialize<T>(KnownTypes), i);
						}
						else if (tag.type == "text/json")
						{
							// reflection info will be lost here?

							h((T)(object)Expando.FromJSON(text), i);
						}
					}
				}
			);
		}


	}
}
