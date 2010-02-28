using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using jsc.Languages.IL;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.meta.Library.Templates.JavaScript;
using jsc.meta.Tools;
using jsc.Script;
using Microsoft.CSharp;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace jsc.meta.Commands.Reference
{

	partial class ReferenceJavaScriptDocument
	{
		private void DefinePageType(
			string DefaultNamespace,
			RewriteToAssembly.AssemblyRewriteArguments a,
			string content,
			XElement BodyElement,
			string PageName,

			string VariationName,
			Dictionary<string, Type> NamedElements
		)
		{
			var PageFullName = DefaultNamespace + ".HTML.Pages." + VariationName + "." + PageName;
			Console.WriteLine(PageFullName);

			var Page = a.Module.DefineType(
				PageFullName, 
				TypeAttributes.Public, 
				typeof(UltraComponent)
			);

			//{
			//    var PropertyName = "Tag";
			//    var PropertyType = typeof(object);

			//    Page.DefineAutomaticProperty(PropertyName, PropertyType);
			//}

			var Static = Page.DefineNestedType("Static", TypeAttributes.NestedPublic | TypeAttributes.Abstract | TypeAttributes.Sealed);


			// we should be returning DOM object instead?
			// should use DataNamespace instead?

			#region DocumentHTML
			if (content != null)
			{
				var Page_HTML = Static.DefineProperty("DocumentHTML", PropertyAttributes.None, typeof(string), null);

				var Page_HTML_get = Static.DefineMethod("get_DocumentHTML", MethodAttributes.Static | MethodAttributes.Public, typeof(string), null);

				{
					var il = Page_HTML_get.GetILGenerator();

					il.Emit(OpCodes.Ldstr, content);
					il.Emit(OpCodes.Ret);
				}

				Page_HTML.SetGetMethod(Page_HTML_get);
			}
			#endregion

			#region Page_HTML
			{
				var Page_HTML = Static.DefineProperty("HTML", PropertyAttributes.None, typeof(string), null);

				var Page_HTML_get = Static.DefineMethod("get_HTML", MethodAttributes.Static | MethodAttributes.Public, typeof(string), null);

				{
					var il = Page_HTML_get.GetILGenerator();

					// http://stackoverflow.com/questions/3793/best-way-to-get-innerxml-of-an-xelement
					var body_innerXML = BodyElement.Nodes().Aggregate("", (b, node) => b += node.ToString());

					il.Emit(OpCodes.Ldstr, body_innerXML);
					il.Emit(OpCodes.Ret);
				}

				Page_HTML.SetGetMethod(Page_HTML_get);
			}
			#endregion

			// we need to use unified HTML DOM...
			if (!this.IsGeneric)
			{
				// http://www.exampledepot.com/egs/org.w3c.dom/xpath_GetElemByAttr.html
				var Elements = Static.DefineNestedType("Elements", TypeAttributes.NestedPublic);

				var Images_value = 
					
					BodyElement.XPathSelectElements("//img").Concat(
					BodyElement.XPathSelectElements("//IMG")
					).ToArray();

				var Anchors_value = 
					BodyElement.XPathSelectElements("//a").Concat(
					BodyElement.XPathSelectElements("//A")
				).ToArray();

				DefineStaticImages(a, Static, Images_value);



				foreach (var CurrentElement in BodyElement.XPathSelectElements("/*[@id]"))
				{
					var id = CurrentElement.Attribute("id").Value;

					var e_Type = ElementTypes.ContainsKey(CurrentElement.Name.LocalName) ? ElementTypes[CurrentElement.Name.LocalName] : typeof(IHTMLElement);

					var e = Elements.DefineProperty(id, PropertyAttributes.None, e_Type, null);
					var get_e = Elements.DefineMethod("get_" + id, MethodAttributes.Static | MethodAttributes.Public, e_Type, null);

					Func<IHTMLElement> get_e_template =
						delegate
						{
							return Native.Document.getElementById("id");
						};

					{
						var il = get_e.GetILGenerator();

						var il_a = new ILTranslationExtensions.EmitToArguments();

						il_a[OpCodes.Ldstr] =
							x =>
							{
								x.il.Emit(OpCodes.Ldstr, id);
							};

						il_a[OpCodes.Ret] =
							x =>
							{
								x.il.Emit(OpCodes.Castclass, e_Type);
								x.il.Emit(OpCodes.Ret);
							};

						get_e_template.Method.EmitTo(il, il_a);


					}

					e.SetGetMethod(get_e);
				}

				Elements.CreateType();

				// not all elements should have a field
				// it would be pure overkill

				var Images_lookup = new Dictionary<XElement, FieldBuilder>();

				foreach (var i in Images_value)
				{
					Images_lookup[i] = null;
				}

				var Anchors_lookup = new Dictionary<XElement, FieldBuilder>();

				foreach (var i in Anchors_value)
				{
					Anchors_lookup[i] = null;
				}


				DefinePageConstructor(BodyElement, Page, new[] { Images_lookup, Anchors_lookup }, 
					NamedElements,
					ElementTypes
				);

				// and html5 videos and sounds!
				DefineInstanceImages(a, Page, Images_lookup);
				DefineInstanceLinks(a, Page, Anchors_lookup);

			}

			Static.CreateType();
			Page.CreateType();
		}


	}
}
