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
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Ultra.Library.Extensions;
using jsc.meta.Library.Templates.JavaScript.Named;

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
			Dictionary<string, TypeVariationsTuple> NamedElements,
			Dictionary<string, Type> ElementTypes
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
						"Initialize_" + Counter.Value++ + "_" + body.Name.LocalName,
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
						NamedElements,
						ElementTypes
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

			Dictionary<string, TypeVariationsTuple> NamedElements,
			Dictionary<string, Type> ElementTypes

			)
		{
			var DefaultElementType =
				// ScriptCoreLib ElementType Lookup...
				(ElementTypes.ContainsKey(CurrentElement.Name.LocalName) ? ElementTypes[CurrentElement.Name.LocalName] : typeof(IHTMLElement));

			var ElementType = DefaultElementType;

			CurrentElement.Attribute("src").Apply(
				src =>
				{
					//var v = TypeVariations[src.Value];

					//// if the image is not on the web
					//// shall we default to FromAssets or FromBase64 ?
					//ElementType = v.FromWeb ?? v.FromAssets;






					var src_value = src.Value;
					if (src_value.StartsWith("//"))
						src_value = "http:" + src_value;

					if (NamedElements.ContainsKey(src_value))
					{
						ElementType = NamedElements[src_value].Type;
					}
				}
			);


			Action Continuation1 =
				delegate
				{
					// http://www.456bereastreet.com/archive/200412/the_alt_and_title_attributes/

					var Element__id = CurrentElement.Attribute("id");

					if (Element__id == null && CurrentElement.Attribute("alt") != null && !string.IsNullOrEmpty(CurrentElement.Attribute("alt").Value))
						Element__id = CurrentElement.Attribute("alt");

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
									 Element__id.Value.TakeUntilLastIfAny("."), null
								);

							var ElementProperty = Page.DefineProperty(
								ElementPropertyName, PropertyAttributes.None, DefaultElementType, null);


							{
								var get_MethodName = "get_" + ElementPropertyName;
								var get_MethodAttributes = MethodAttributes.Public | MethodAttributes.HideBySig;

								if (Page.GetInterfaces().SelectMany(k => k.GetMethods()).Where(k => k.Name == get_MethodName).Any())
									get_MethodAttributes |= MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.Final;


								var get_ElementField = Page.DefineMethod(
									get_MethodName,
									get_MethodAttributes, CallingConventions.Standard, DefaultElementType, null);

								var get_ElementField_il = get_ElementField.GetILGenerator();

								get_ElementField_il.Emit(OpCodes.Ldarg_0);
								get_ElementField_il.Emit(OpCodes.Ldfld, ElementField);
								get_ElementField_il.Emit(OpCodes.Ret);

								ElementProperty.SetGetMethod(get_ElementField);
							}

							{
								var set_MethodName = "set_" + ElementPropertyName;
								var set_MethodAttributes = MethodAttributes.Public | MethodAttributes.HideBySig;

								if (Page.GetInterfaces().SelectMany(k => k.GetMethods()).Where(k => k.Name == set_MethodName).Any())
									set_MethodAttributes |= MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.Final;

								var set_ElementField = Page.DefineMethod(
									set_MethodName,
									set_MethodAttributes, CallingConventions.Standard, null, new[] { DefaultElementType });

								var set_ElementField_il = set_ElementField.GetILGenerator();

								set_ElementField_il.Emit(OpCodes.Ldarg_0);
								set_ElementField_il.Emit(OpCodes.Ldfld, ElementField);

								set_ElementField_il.Emit(OpCodes.Ldarg_1);

								set_ElementField_il.Emit(OpCodes.Call,
									((Action<INode, INode>)INodeExtensions.ReplaceWith).Method);

								set_ElementField_il.Emit(OpCodes.Ldarg_0);
								set_ElementField_il.Emit(OpCodes.Ldarg_1);
								set_ElementField_il.Emit(OpCodes.Stfld, ElementField);

								set_ElementField_il.Emit(OpCodes.Ret);

								ElementProperty.SetSetMethod(set_ElementField);
							}


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

						if (DefaultElementType != ElementType && item.Name == "src")
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
								NamedElements,
								ElementTypes
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
								if (SourceType == typeof(NamedImage))
									return ElementType;

								return SourceType;
							},

						TranslateTargetConstructor =
							SourceConstructor =>
							{
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

					if (DefaultElementType != typeof(IHTMLElement))
						Implementation2.Method.EmitTo(il, il_a);
					else
						Implementation1.Method.EmitTo(il, il_a);
				}
				#endregion
			}
		}



	}
}
