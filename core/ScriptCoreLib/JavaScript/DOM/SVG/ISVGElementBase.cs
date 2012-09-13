using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.SVG
{
	[Script(InternalConstructor = true)]
	public class ISVGElementBase : IHTMLElement
	{
		[Script]
		public class Settings
		{
			// http://thomas.tanreisoftware.com/?p=79

			public static readonly string[] MimeTypes =
				new[] { "image/svg+xml", "image/svg-xml", "image/svg" };

			public static string MimeType { get { return MimeTypes[0]; } }

			//static bool _IsSupported;

			public static bool IsSupported
			{
				get
				{
					// http://code.google.com/android/reference/org/w3c/dom/DOMImplementation.html#hasFeature(java.lang.String,%20java.lang.String)

					// document.implementation.hasFeature("org.w3c.dom.svg", "1.0")
					//http://www.w3.org/TR/SVG11/feature#Structure
					// fore firefox 2 and earlier
					if (Native.Document.implementation.hasFeature("org.w3c.dom.svg", "1.0"))
					{
						return true;
					}

					if (Native.Document.implementation.hasFeature("http://www.w3.org/TR/SVG11/feature#Structure", "1.1"))
					{
						return true;
					}

					return false;

					// http://dojotoolkit.org/forum/dojo-0-4-x-legacy/dojo-0-4-x-support/charting-dont-working-under-firefox-3
					// http://www.nabble.com/SVG-detection-in-Firefox-3-and-Safari-3-td17378932.html
					// http://www.w3.org/TR/SVG11/feature#Image%27,%271.1%27


					//javascript:alert(document.implementation.hasFeature("http://www.w3.org/TR/SVG11/feature#Structure", "1.1"))

					/*
                    
				var p = Native.Window.navigator.mimeTypes;

				for (int i = 0; i < p.length; i++)
				{
					if (MimeTypes.Contains(p[i].type))
					{
						_IsSupported = true;

						break;
					}

					Console.WriteLine("mime: " + p[i].type + " : " + p[i].description);
				}

				return _IsSupported;*/

				}
			}
		}

		public static readonly string NS = "http://www.w3.org/2000/svg";

		public string type;
		public ISVGElementBase ownerSVGElement;
		public ISVGElementBase viewportSVGElement;

		protected ISVGElementBase()
		{
		}

		protected static ISVGElementBase InternalConstructor()
		{
			throw new Exception();
		}

		public ISVGElementBase(string tag)
		{
		}

		internal static ISVGElementBase InternalConstructor(string tag)
		{

			return (ISVGElementBase)(object) //null;
				Native.Document.createElementNS(NS, tag);




		}

	}

}
