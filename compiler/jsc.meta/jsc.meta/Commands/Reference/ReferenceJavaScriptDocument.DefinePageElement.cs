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
using jsc.meta.Tools;
using jsc.Script;
using Microsoft.CSharp;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using jsc.meta.Library.Templates.JavaScript;

namespace jsc.meta.Commands.Reference
{

	partial class ReferenceJavaScriptDocument
	{
		private static void DefinePageElement(
			XElement body,
			TypeBuilder Page,
			Counter Counter,
			ILGenerator il,
			OpCode parent,
			Dictionary<XElement, FieldBuilder>[] lookup,
			Func<string, Type> SourceToNamedElement

		)
		{
			Action Implementation1 =
				delegate
				{
					var c = TemplateHolder.Initialize(null);
				};

			{
				var il_a = new ILTranslationExtensions.EmitToArguments();

				il_a[OpCodes.Call] = x =>
				{
					var Initialize = Page.DefineMethod(
						"Initialize_" + Counter.Value++,
						MethodAttributes.Family | MethodAttributes.Static,
						typeof(IHTMLElement),
						new[] { Page, typeof(IHTMLElement) }
					);

					DefinePageElement(
						body, 
						Initialize.GetILGenerator(), 
						Page, 
						Counter, 
						lookup,
						SourceToNamedElement
					);

					x.il.Emit(OpCodes.Ldarg_0);
					x.il.Emit(parent);
					x.il.Emit(OpCodes.Call, Initialize);
				};

				il_a[OpCodes.Ret] = x => { };
				il_a[OpCodes.Ldnull] = x => { };

				Implementation1.Method.EmitTo(il, il_a);
			}
		}


		private static void DefinePageElement(
			XElement CurrentElement,
			ILGenerator il,
			TypeBuilder Page,
			Counter Counter,
			Dictionary<XElement, FieldBuilder>[] lookup,

			Func<string, Type> SourceToNamedElement
			//Dictionary<string, TypeVariations> TypeVariations

			)
		{
			var DefaultElementType =
				// ScriptCoreLib ElementType Lookup...
				(ElementTypes.ContainsKey(CurrentElement.Name.LocalName) ? ElementTypes[CurrentElement.Name.LocalName] : typeof(IHTMLElement));

			var ElementType = DefaultElementType;

			if (ElementType == typeof(IHTMLImage))
			{
				CurrentElement.Attribute("src").Apply(
					src =>
					{
						//var v = TypeVariations[src.Value];

						//// if the image is not on the web
						//// shall we default to FromAssets or FromBase64 ?
						//ElementType = v.FromWeb ?? v.FromAssets;

						ElementType = SourceToNamedElement(src.Value);
					}
				);
			}


			Action Continuation1 =
				delegate
				{
					// http://www.456bereastreet.com/archive/200412/the_alt_and_title_attributes/

					var Element__id = CurrentElement.Attribute("id") ?? CurrentElement.Attribute("alt");
					var ElementHasId = Element__id != null;
					var ElementInLookup = lookup.Any(k => k.Keys.Contains(CurrentElement));

					if (ElementHasId || ElementInLookup)
					{


						var ElementField = Page.DefineField("_" + (Element__id == null ? "" + Counter.Value++ : Element__id.Value), ElementType, FieldAttributes.Private);

						foreach (var k in
							from k0 in lookup
							where k0.ContainsKey(CurrentElement)
							select k0
							)
						{
							k[CurrentElement] = ElementField;
						}

						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldloc_0);
						il.Emit(OpCodes.Castclass, ElementType);
						il.Emit(OpCodes.Stfld, ElementField);

						if (ElementHasId)
						{
							var ElementPropertyName = CompilerBase.GetSafeLiteral(
									Element__id.Value, null
								);

							var ElementProperty = Page.DefineProperty(
								ElementPropertyName, PropertyAttributes.None, ElementType, null);

							var get_ElementField = Page.DefineMethod("get_" + ElementPropertyName, MethodAttributes.Public, CallingConventions.Standard, ElementType, null);

							var get_ElementField_il = get_ElementField.GetILGenerator();

							get_ElementField_il.Emit(OpCodes.Ldarg_0);
							get_ElementField_il.Emit(OpCodes.Ldfld, ElementField);
							get_ElementField_il.Emit(OpCodes.Ret);

							ElementProperty.SetGetMethod(get_ElementField);
						}
					}


					#region c.setAttribute("name", "value");
					Action<IHTMLElement> Implementation3 =
						c =>
						{
							// seems to work for .style too in browsers :)
							c.setAttribute("name", "value");
						};




					foreach (var item in CurrentElement.Attributes())
					{
						if (item.Name.LocalName == "id")
							continue;

						if (DefaultElementType == typeof(IHTMLImage) && item.Name == "src")
							continue;

						var il_a = new ILTranslationExtensions.EmitToArguments();

						il_a[OpCodes.Ret] = delegate { };
						il_a[OpCodes.Ldarg_0] = x => x.il.Emit(OpCodes.Ldloc_0);
						il_a[OpCodes.Ldstr] = x => x.il.Emit(OpCodes.Ldstr,
							x.i.TargetLiteral == "name" ? item.Name.LocalName : item.Value
						);

						Implementation3.Method.EmitTo(il, il_a);

					}
					#endregion

					#region c.appendChild
					Action<IHTMLElement> Implementation4 =
						c =>
						{
							c.appendChild(new ITextNode("e"));
						};

					foreach (var item in CurrentElement.Nodes())
					{
						if (item is XText)
						{
							var il_a = new ILTranslationExtensions.EmitToArguments();

							il_a[OpCodes.Ret] = delegate { };
							il_a[OpCodes.Ldarg_0] = x => x.il.Emit(OpCodes.Ldloc_0);
							il_a[OpCodes.Ldstr] = x => x.il.Emit(OpCodes.Ldstr, ((XText)item).Value);

							Implementation4.Method.EmitTo(il, il_a);
						}

						if (item is XElement)
						{
							DefinePageElement(
								(XElement)item, 
								Page, 
								Counter, 
								il, 
								OpCodes.Ldloc_0, 
								lookup,
								SourceToNamedElement
							);
						}
					}
					#endregion
				};

			{
				#region Implementation1
				Func<object, IHTMLElement, IHTMLElement> Implementation1 =
					(__this, parent) =>
					{
						var c = new IHTMLElement("" /* body.Name.LocalName */);

						//c.setAttribute("title", "hi");
						TemplateHolder.Implementation();



						if (parent != null)
							parent.appendChild(c);

						return c;
					};

				Func<object, IHTMLElement, IHTMLElement> Implementation2 =
						(__this, parent) =>
						{
							var c = new NamedImage();

							//c.setAttribute("title", "hi");
							TemplateHolder.Implementation();



							if (parent != null)
								parent.appendChild(c);

							return c;
						};

				{
					var il_a = new ILTranslationExtensions.EmitToArguments
					{
						TranslateTargetType =
							SourceType =>
							{
								if (ElementType != DefaultElementType)
									if (SourceType == typeof(NamedImage))
										return ElementType;

								return SourceType;
							},

						TranslateTargetConstructor =
							SourceConstructor =>
							{
								if (ElementType != DefaultElementType)
									if (SourceConstructor.DeclaringType == typeof(NamedImage))
										return ElementType.GetConstructor(new Type[0]);

								return SourceConstructor;
							}
					};

					il_a[OpCodes.Call] = x =>
					{
						Action Implementation = TemplateHolder.Implementation;
						if (x.i.TargetMethod == Implementation.Method)
						{
							Continuation1();

							return;
						}

						il.Emit(OpCodes.Call, x.i.TargetMethod);
					};

					il_a[OpCodes.Ldarg_0] = x => x.il.Emit(OpCodes.Ldarg_1);
					il_a[OpCodes.Ldstr] = x => x.il.Emit(OpCodes.Ldstr, CurrentElement.Name.LocalName);

					if (DefaultElementType == typeof(IHTMLImage))
						Implementation2.Method.EmitTo(il, il_a);
					else
						Implementation1.Method.EmitTo(il, il_a);
				}
				#endregion
			}
		}



	}
}
