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
using jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.JavaScript.Extensions;

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

			Dictionary<string, TypeVariationsTuple> NamedElements,

			Dictionary<string, TypeVariationsTuple> RemotingNamedElements,

			ImplementConcept ImplementConcept,

			string[] Concepts,

			Dictionary<string, Type> IPageLookup
		)
		{
			new DefineRemotingPages
			{
				DefaultNamespace = DefaultNamespace,
				a = a,
				content = content,
				BodyElement = BodyElement,
				PageName = PageName,
				VariationName = VariationName,
				RemotingNamedElements = RemotingNamedElements
			}.Invoke();

			var FullName = new
			{
				VariationPage = DefaultNamespace + ".HTML.Pages." + VariationName + "." + PageName,

				// who is going to use the interface?
				// plugins/webserver?
				// should we enumerate concepts available?
				// shold we promote string properties and onclick events?
				IPage = DefaultNamespace + ".HTML.Pages.I" + PageName,

				Page = DefaultNamespace + ".HTML.Pages." + PageName,
				PageBase = DefaultNamespace + ".HTML.Pages." + PageName + "Base",

				PageVariations = DefaultNamespace + ".HTML.Pages." + PageName + "+Variations",

				PageSource = DefaultNamespace + ".HTML.Pages." + VariationName + "." + PageName + "+Source",

				// XElement implementation?
				XPageSource = DefaultNamespace + ".XPages." + VariationName + ".X" + PageName

				// As XML asset?
			};

			Console.WriteLine(FullName.VariationPage);

			var PageInterfaces = ImplementConcept.GetInterfaces(BodyElement, ElementTypes).ToArray();

			#region IPage
			var IPage = IPageLookup.GetValue(
				FullName.IPage,
				delegate
				{

					// create only once, as we do implement variations...
					var p = a.Module.DefineInterface(
						FullName.IPage,
					   PageInterfaces.Concat(new[] { typeof(IUltraComponent) })
					);

					var StaticElementProperties =
						from CurrentElement in BodyElement.XPathSelectElements(".//*[@id]")
						let id = CurrentElement.Attribute("id").Value
						let e_Type = ElementTypes.ContainsKey(CurrentElement.Name.LocalName) ? ElementTypes[CurrentElement.Name.LocalName] : typeof(IHTMLElement)
						select new { CurrentElement, id, e_Type };

					foreach (var k in StaticElementProperties)
					{
						var e = p.DefineProperty(k.id, PropertyAttributes.None, k.e_Type, null);

						e.SetGetMethod(p.DefineMethod("get_" + k.id, MethodAttributes.Abstract | MethodAttributes.Virtual | MethodAttributes.Public, k.e_Type, null));
						e.SetSetMethod(p.DefineMethod("set_" + k.id, MethodAttributes.Abstract | MethodAttributes.Virtual | MethodAttributes.Public, null, new[] { k.e_Type }));
					}

					p.CreateType();
					return p;
				}
			);
			#endregion

			#region Page
			var PageBase = IPageLookup.GetValue(
				FullName.PageBase,
				delegate
				{

					// create only once, as we do implement variations...
					var p = a.Module.DefineType(
						FullName.PageBase,
						TypeAttributes.NotPublic | TypeAttributes.Abstract,
						typeof(UltraComponent),
						new[] { IPage }
					);

					p.CreateType();

					return p;
				}
			);

			var Page = IPageLookup.GetValue(
				FullName.Page,
				delegate
				{

					// create only once, as we do implement variations...
					var p = a.Module.DefineType(
						FullName.Page,
						TypeAttributes.Public | TypeAttributes.Abstract,
						PageBase
					);

					var StaticElementProperties =
						from CurrentElement in BodyElement.XPathSelectElements(".//*[@id]")
						let id = CurrentElement.Attribute("id").Value
						let e_Type = ElementTypes.ContainsKey(CurrentElement.Name.LocalName) ? ElementTypes[CurrentElement.Name.LocalName] : typeof(IHTMLElement)
						select new { CurrentElement, id, e_Type };



					var FromDocument = p.DefineNestedType("FromDocument", TypeAttributes.NestedPublic, PageBase);



					p.DefineDefaultConstructor(MethodAttributes.Public);


					#region PageSource
					var PageSource = p.DefineStaticType(FullName.PageSource.SkipUntilLastIfAny("+"));

					#region Page_HTML
					{
						var Page_HTML = PageSource.DefineProperty("Text", PropertyAttributes.None, typeof(string), null);

						var Page_HTML_get = PageSource.DefineMethod("get_Text", MethodAttributes.Static | MethodAttributes.Public, typeof(string), null);

						{
							var il = Page_HTML_get.GetILGenerator();

							var MyBodyElement = new XElement(BodyElement);

							var ElementsWithSource = new[] 
					{
						MyBodyElement.XPathSelectElements(".//img[@src]"),
						MyBodyElement.XPathSelectElements(".//audio[@src]")
					}.SelectMany(k => k).ToArray();

							foreach (var ElementWithSource in ElementsWithSource)
							{
								ElementWithSource.Attribute("src").Value = NamedElements[ElementWithSource.Attribute("src").Value].Source;
							}

							// http://stackoverflow.com/questions/3793/best-way-to-get-innerxml-of-an-xelement
							var body_innerXML = MyBodyElement.Nodes().Aggregate("", (b, node) => b += node.ToString());

							il.Emit(OpCodes.Ldstr, body_innerXML);
							il.Emit(OpCodes.Ret);
						}

						Page_HTML.SetGetMethod(Page_HTML_get);
					}
					#endregion

					PageSource.CreateType();
					#endregion

					p.CreateType();

					FromDocument.DefineDefaultConstructor(MethodAttributes.Public);

					foreach (var k in StaticElementProperties)
					{
						var e = FromDocument.DefineProperty(k.id, PropertyAttributes.None, k.e_Type, null);

						var get_e = FromDocument.DefineMethod("get_" + k.id, MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.Final | MethodAttributes.Public, k.e_Type, null);

						#region get
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
									x.il.Emit(OpCodes.Ldstr, k.id);
								};

							il_a[OpCodes.Ret] =
								x =>
								{
									x.il.Emit(OpCodes.Castclass, k.e_Type);
									x.il.Emit(OpCodes.Ret);
								};

							get_e_template.Method.EmitTo(il, il_a);


						}
						#endregion

						e.SetGetMethod(get_e);

						var set_e = FromDocument.DefineMethod("set_" + k.id, MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.Final | MethodAttributes.Public, null, new[] { k.e_Type });

						#region set
						Action<IHTMLElement, IHTMLElement> set_e_template =
							(c, value) =>
							{
								c.ReplaceWith(value);
							};

						{
							var il = set_e.GetILGenerator();

							var il_a = new ILTranslationExtensions.EmitToArguments();

							il_a[OpCodes.Ldarg_0] =
								x =>
								{
									x.il.Emit(OpCodes.Ldarg_0);
									x.il.Emit(OpCodes.Call, get_e);

								};

							il_a[OpCodes.Ldstr] =
								x =>
								{
									x.il.Emit(OpCodes.Ldstr, k.id);
								};

						

							set_e_template.Method.EmitTo(il, il_a);


						}
						#endregion

						e.SetSetMethod(set_e);
					}


					// The invoked member is not supported in a dynamic module.
					FromDocument.CreateType();
					return p;
				}
			);
			#endregion






			var VariationPage = a.Module.DefineType(
				FullName.VariationPage,
				TypeAttributes.Public,
				PageBase,

				// whom shall we implement?

				null
			);



			// we need to use unified HTML DOM...
			if (!this.IsGeneric)
			{
				// http://www.exampledepot.com/egs/org.w3c.dom/xpath_GetElemByAttr.html

				var Images_value =
					BodyElement.XPathSelectElements("//img").Concat(
					BodyElement.XPathSelectElements("//IMG")
				).ToArray();

				var Anchors_value =
					BodyElement.XPathSelectElements("//a").Concat(
					BodyElement.XPathSelectElements("//A")
				).ToArray();

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


				DefinePageConstructor(BodyElement, VariationPage, new[] { Images_lookup, Anchors_lookup },
					NamedElements,
					ElementTypes
				);

				// and html5 videos and sounds!
				DefineInstanceImages(a, VariationPage, Images_lookup);
				DefineInstanceLinks(a, VariationPage, Anchors_lookup);

			}

			//Static.CreateType();
			VariationPage.CreateType();
		}


	}
}
