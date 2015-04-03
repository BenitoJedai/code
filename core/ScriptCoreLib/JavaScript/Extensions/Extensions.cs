using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using System.Xml.Linq;
using System.Linq;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace ScriptCoreLib.JavaScript.Extensions
{
	[Script]
	public static class Extensions
	{
		public static string toDataURL(this IHTMLImage img)
		{
			var context = new CanvasRenderingContext2D();

			context.canvas.width = img.width;
			context.canvas.height = img.height;

			context.drawImage(img, 0, 0, img.width, img.height);

			var dataURL = context.canvas.toDataURL();

			return dataURL;
		}

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
		//[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)
		//, ObsoleteAttribute("", false)
		//]
		//public static T Dispose<T>(this T e)
		//    where T : INode
		//{
		//    return e.Orphanize();
		//}



		public static void Clear(this INodeConvertible<IHTMLElement> ee)
		{
			var e = ee.AsNode();

			var p = e.firstChild;

			while (p != null)
			{
				e.removeChild(p);
				p = e.firstChild;
			}
		}

		public static void ReplaceWith(this INodeConvertible<IHTMLElement> ee, INodeConvertible<IHTMLElement> evalue)
		{
			var e = ee.AsNode();
			var value = evalue.AsNode();

			// http://msdn.microsoft.com/en-us/library/system.xml.linq.xnode.replacewith.aspx

			if (e.parentNode == null)
				return;

			// tested by
			// X:\jsc.svn\examples\javascript\appengine\RemainingMillisExperiment\RemainingMillisExperiment\Application.cs
			// X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\Application.cs


			var old = e.attributes.Select(x => new { x.name, x.value }).ToArray();

			var old_id = ((IHTMLElement)e).id;

			e.parentNode.replaceChild(value, e);


			// merge attributes
			foreach (var item in old)
			{
				if (!value.hasAttribute(item.name))
					value.setAttribute(item.name, item.value);
			}

			if (!string.IsNullOrEmpty(old_id))
			{
				//((IHTMLElement)value).id = old_id;

				// we just swapped out id's. make the old element forget its id
				e.id = "";
			}
		}

		// jsc eXperience
		public static IEnumerable<IHTMLElement> Orphanize(this IHTMLElement.HTMLElementEnum className)
		{
			return className.AsEnumerable().Orphanize();
		}

		public static IEnumerable<T> Orphanize<T>(this IEnumerable<T> z)
			where T : INodeConvertible<IHTMLElement>
		{
			// Error	2	The type 'System.Collections.Generic.IEnumerable<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement>' cannot be used as type parameter 'T' in the generic type or method 'ScriptCoreLib.JavaScript.Extensions.Extensions.Orphanize<T>(T)'. There is no implicit reference conversion from 'System.Collections.Generic.IEnumerable<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement>' to 'ScriptCoreLib.JavaScript.Extensions.INodeConvertible<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement>'.	X:\jsc.svn\examples\javascript\Test\RemoveByQuerySelectorAll\RemoveByQuerySelectorAll\Application.cs	53	22	RemoveByQuerySelectorAll


			// tested by
			// X:\jsc.svn\examples\javascript\Test\RemoveByQuerySelectorAll\RemoveByQuerySelectorAll\Application.cs


			foreach (var item in z)
			{
				item.Orphanize();
			}

			return z;
		}

		public static T Orphanize<T>(this T e)
			where T : INodeConvertible<IHTMLElement>
		{
			if (e == null)
				return e;

			var ee = e.AsNode();
			var n = ee.parentNode;
			if (n != null)
				n.removeChild(ee);

			return e;
		}

		/// <summary>
		/// attaches this element to the current document body
		/// </summary>
		//public static T AttachToDocument<T>(this T e)
		//    where T : INode
		//{
		//    return e.AttachTo(Native.Document.body);
		//}



		#region AttachToDocument
		public static IEnumerable<T> AttachToDocument<T>(this IEnumerable<T> e)
	   where T : INodeConvertible<IHTMLElement>
		{
			if (e != null)
			{
				foreach (var item in e)
				{
					item.AttachToDocument();
				}
			}

			return e;
		}


		// RewriteToAssembly error: System.MissingMethodException: Method not found: '!!0 ScriptCoreLib.JavaScript.Extensions.Extensions.AttachToDocument(!!0)'.
		public static T AttachToDocument<T>(this T e)
			where T : INodeConvertible<IHTMLElement>
		{
			if (e == null)
				return e;

			if (Native.document.body == null)
			{
				// can happen.
				// X:\jsc.svn\examples\javascript\test\TestHopFromIFrame\TestHopFromIFrame\Application.cs
				Native.document.documentElement.appendChild(e.AsNode());
				return e;
			}

			Native.document.body.appendChild(e.AsNode());
			return e;
		}

		public static IHTMLElement AttachToDocument(this XElement e)
		{
			// tested by
			// X:\jsc.svn\examples\javascript\appengine\StopwatchTimetravelExperiment\StopwatchTimetravelExperiment\Application.cs

			return e.AsHTMLElement().AttachToDocument();
		}

		#endregion


		#region AttachTo

		public static XAttribute AttachTo(this XAttribute e, IHTMLElement c)
		{
			// tested by

			c.AsXElement().Add(e);

			return e;
		}

		public static IEnumerable<T> AttachTo<T>(this IEnumerable<T> e, IHTMLElement c)
			where T : INodeConvertible<IHTMLElement>
		{
			if (e != null)
			{
				foreach (var item in e)
				{
					item.AttachTo(c);
				}
			}

			return e;
		}

		// 

		public static T AttachTo<T>(this T e, ISVGElementBase c)
			where T : INodeConvertible<IHTMLElement>
		{
			// what about shadow DOM?

			if (e != null)
			{
				var n = e.AsNode();

				if (c.localName == "foreignObject")
				{
					//xmlns='http://www.w3.org/1999/xhtml'
					// X:\jsc.svn\examples\javascript\svg\SVGCSSContent\SVGCSSContent\Application.cs
					n.setAttributeNS("http://www.w3.org/2000/xmlns/", "xmlns", "http://www.w3.org/1999/xhtml");
				}

				c.appendChild(n);
			}

			return e;
		}





		public static T AttachTo<T>(this T e, IDocumentFragment c)
			 where T : INodeConvertible<IHTMLElement>
		{
			// X:\jsc.svn\examples\javascript\Avalon\Test\TestShadowTextBox\TestShadowTextBox\ApplicationCanvas.cs
			// X:\jsc.svn\core\ScriptCoreLib.Avalon\ScriptCoreLib.Avalon\JavaScript\BCLImplementation\System\Windows\Controls\TextBox.cs

			if (e != null)
				c.appendChild(e.AsNode());

			return e;
		}


		public static T AttachTo<T>(this T e, IHTMLElement c)
			where T : INodeConvertible<IHTMLElement>
		{
			if (e != null)
				c.appendChild(e.AsNode());

			return e;
		}
		#endregion

		#region Obsolete
		[Obsolete]
		public static T Deserialize<T>(this IXMLDocument e, object[] k)
					where T : class, new()
		{
			if (k == null)
				throw new Exception("Deserialize: k is null");

			return new IXMLSerializer<T>(k).Deserialize(e);
		}

		[Obsolete]
		public static void Spawn(this Type alias)
		{
			ScriptCoreLib.JavaScript.Native.Spawn(alias.Name, i => Activator.CreateInstance(alias));
		}

		[Obsolete]
		public static void SpawnEntrypointWithBrandning(this Type alias)
		{
			if (Native.window == null)
				return;

			Native.window.onload +=
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

		[Obsolete]
		public static void SpawnTo(this Type alias, Action<IHTMLElement> h)
		{
			// note: this method is used by jsc.meta

			ScriptCoreLib.JavaScript.Native.Spawn(alias.Name, i => h(i));
		}

		[Obsolete]
		public static void SpawnTo<T>(this Type alias, object[] KnownTypes, Action<T> h)
			 where T : class, new()
		{
			SpawnTo<T>(alias, KnownTypes, (t, i) => h(t));
		}

		[Obsolete]
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
		#endregion



	}
}
