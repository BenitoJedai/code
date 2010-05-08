using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using System.Xml.XPath;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.meta.Library.Templates.JavaScript;
using jsc.Script;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Windows.Controls;
using jsc.meta.Library.Templates.Avalon;
using jsc.meta.Library.Templates;
using ScriptCoreLib.JavaScript.Remoting;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;
using ScriptCoreLib.Extensions;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	partial class ReferenceUltraSource
	{
		public class DefineRemotingPages
		{
			public string DefaultNamespace;
			public RewriteToAssembly.AssemblyRewriteArguments a;
			public string content;
			public XElement BodyElement;
			public string PageName;

			public string VariationName;
			public Dictionary<string, TypeVariationsTuple> RemotingNamedElements;

			public void Invoke()
			{
				var PageFullName = DefaultNamespace + ".HTML.Pages." + VariationName + ".Remoting." + PageName;

				Console.WriteLine(PageFullName);

				var __createElement = new Func<PHTMLDocument, Action<string, PHTMLElementAction>>(doc => doc.createElement).ToReferencedMethod();
				var __createTextNode = new Func<PHTMLDocument, Action<string, PTextNodeAction>>(doc => doc.createTextNode).ToReferencedMethod();
				var __setAttribute = new Func<PHTMLElement, Action<string, string>>(doc => doc.setAttribute).ToReferencedMethod();
				var __appendChild = new Func<PNode, Action<PNode>>(doc => doc.appendChild).ToReferencedMethod();
				var __InternalMarkReady = new Func<PUltraComponent, Action>(doc => doc.InternalMarkReady).ToReferencedMethod();
				var __WhenReady = new Func<PUltraComponent, Action<Action>>(doc => doc.WhenReady).ToReferencedMethod();
				var __get_Container = new Func<PUltraComponent, PHTMLElement>(doc => doc.Container).ToReferencedMethod();
				var __get_Document = new Func<PUltraComponent, PHTMLDocument>(doc => doc.Document).ToReferencedMethod();
				var __InternalDocument = new Func<PUltraComponent, PHTMLDocument>(doc => doc.InternalDocument).ToReferencedField();
				var __InternalElement = new Func<PUltraComponent, PHTMLElement>(doc => doc.InternalElement).ToReferencedField();


				var Page = a.Module.DefineType(
					PageFullName,
					TypeAttributes.Public,
					typeof(PUltraComponent)
				);


				var Page_ctor = Page.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis,
					new[] 
					{
						typeof(PHTMLDocument)
					}
				);





				var Page_InternalProgress = Page.DefineMethod("InternalProgress", MethodAttributes.Private);



				#region special
				{
					var il = Page_ctor.GetILGenerator();

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldarg_1);
					il.Emit(OpCodes.Stfld, __InternalDocument);
				}
				#endregion


				var CurrentIndex = -1;

				#region DefineInitializeTextNode
				Func<Action<ILGenerator>, MethodBuilder> DefineInitializeTextNode =
					(EmitParentField) =>
					{
						CurrentIndex++;

						var InternalInitialize = Page.DefineMethod("InternalInitialize" + CurrentIndex, MethodAttributes.Private, CallingConventions.HasThis, typeof(void),
							new[] { typeof(PTextNode) }
						);

						var il = InternalInitialize.GetILGenerator();

						EmitParentField(il);

						il.Emit(OpCodes.Ldarg_1);

						il.Emit(OpCodes.Call, __appendChild);


						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Call, Page_InternalProgress);

						il.Emit(OpCodes.Ret);

						return InternalInitialize;
					};
				#endregion

				#region DefineProperty
				Action<string, FieldBuilder> DefineProperty =
					(PropertyName, Field) =>
					{
						var ElementPropertyName = CompilerBase.GetSafeLiteral(
								 PropertyName.TakeUntilLastIfAny("."), null
							);

						var ElementProperty = Page.DefineProperty(
							ElementPropertyName, PropertyAttributes.None,
							Field.FieldType
							, null);

						{
							var get_ElementField = Page.DefineMethod("get_" + ElementPropertyName, MethodAttributes.Public, CallingConventions.Standard,
								Field.FieldType
								, null);

							var get_ElementField_il = get_ElementField.GetILGenerator();

							get_ElementField_il.Emit(OpCodes.Ldarg_0);
							get_ElementField_il.Emit(OpCodes.Ldfld, Field);
							get_ElementField_il.Emit(OpCodes.Ret);

							ElementProperty.SetGetMethod(get_ElementField);
						}
					};
				#endregion

				#region DefineFieldAndOptionallyProperty
				Func<XElement, Type, FieldInfo> DefineFieldAndOptionallyProperty =
					(CurrentElement, __FieldType) =>
					{
						var __Field = Page.DefineField("InternalElement" + CurrentIndex, __FieldType, FieldAttributes.Private);

						// so whats the name of this element?
						var __id = CurrentElement.Attribute("id");

						if (__id == null && CurrentElement.Attribute("alt") != null && !string.IsNullOrEmpty(CurrentElement.Attribute("alt").Value))
							__id = CurrentElement.Attribute("alt");

						if (__id != null)
							DefineProperty(__id.Value, __Field);

						return __Field;
					};
				#endregion


				#region DefineInitializeNamedElement
				Action<XElement, Type, ILGenerator, Action<ILGenerator>, Action<Action<ILGenerator>, ILGenerator>> DefineInitializeNamedElement =
					(CurrentElement, NamedComponent, xil, EmitParentField, done) =>
					{
						var __Field = DefineFieldAndOptionallyProperty(CurrentElement, NamedComponent);

						var InternalInitialize = Page.DefineMethod("InternalInitialize" + CurrentIndex, MethodAttributes.Private, CallingConventions.HasThis, typeof(void),
							new Type[0] { }
						);

						{
							var il = xil;


							il.Emit(OpCodes.Ldarg_0);

							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldfld, __InternalDocument);
							il.Emit(OpCodes.Newobj, NamedComponent.GetConstructors().Single());

							il.Emit(OpCodes.Stfld, __Field);


							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldfld, __Field);

							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldftn, InternalInitialize);
							il.Emit(OpCodes.Newobj, typeof(Action).GetConstructors().Single());

							il.Emit(OpCodes.Call, __WhenReady);

						}

						Action<ILGenerator> EmitLoadElement =
							il =>
							{
								//il.Emit(OpCodes.Nop);
								il.Emit(OpCodes.Ldarg_0);
								il.Emit(OpCodes.Ldfld, __Field);
								il.Emit(OpCodes.Ldfld, __InternalElement);
								//il.Emit(OpCodes.Nop);
							};

						{
							var il = InternalInitialize.GetILGenerator();



							if (EmitParentField != null)
							{
								EmitParentField(il);

								EmitLoadElement(il);

								il.Emit(OpCodes.Callvirt, __appendChild);
							}

							if (done != null)
								done(EmitLoadElement, il);

							il.Emit(OpCodes.Ret);
						}
					};
				#endregion


				#region DefineInitializeElement
				var DefineInitializeElement = default(Action<XElement, ILGenerator, Action<ILGenerator>, Action<Action<ILGenerator>, ILGenerator>>);


				DefineInitializeElement =
					(CurrentElement, xil, EmitParentField, ElementStored) =>
					{
						CurrentIndex++;

						// img, audio, video 
						var NamedComponent = default(Type);

						var __src = CurrentElement.Attribute("src");
						if (__src != null && RemotingNamedElements.ContainsKey(__src.Value))
							NamedComponent = RemotingNamedElements[__src.Value].Type;

						Action<Action<ILGenerator>, ILGenerator> BeforeDone =
							(EmitField, il) =>
							{
								#region Attributes
								foreach (var item in CurrentElement.Attributes())
								{
									if (item.Name.LocalName == "id")
										continue;

									if (item.Name.LocalName == "src")
										if (NamedComponent != null)
											continue;

									EmitField(il);

									il.Emit(OpCodes.Ldstr, item.Name.LocalName);
									il.Emit(OpCodes.Ldstr, item.Value);
									il.Emit(OpCodes.Call, __setAttribute);

								}
								#endregion

								#region Nodes
								foreach (var item in CurrentElement.Nodes())
								{
									if (item is XText)
									{
										var InitializeTextNode = DefineInitializeTextNode(EmitField);

										il.Emit(OpCodes.Ldarg_0);
										il.Emit(OpCodes.Ldfld, __InternalDocument);
										il.Emit(OpCodes.Ldstr, ((XText)item).Value);

										il.Emit(OpCodes.Ldarg_0);
										il.Emit(OpCodes.Ldftn, InitializeTextNode);
										il.Emit(OpCodes.Newobj, typeof(PTextNodeAction).GetConstructors().Single());

										il.Emit(OpCodes.Call, __createTextNode);
									}

									if (item is XElement)
									{

										DefineInitializeElement(item as XElement, il, EmitField, null);
									}
								}
								#endregion


								il.Emit(OpCodes.Ldarg_0);
								il.Emit(OpCodes.Call, Page_InternalProgress);
							};

						if (NamedComponent != null)
						{
							DefineInitializeNamedElement(CurrentElement, NamedComponent, xil, EmitParentField, BeforeDone);
							return;
						}

						var __Field = DefineFieldAndOptionallyProperty(CurrentElement, typeof(PHTMLElement));


						var InternalInitialize = Page.DefineMethod("InternalInitialize" + CurrentIndex + "_" + CurrentElement.Name.LocalName, MethodAttributes.Private, CallingConventions.HasThis, typeof(void),
							new[] { typeof(PHTMLElement) }
						);


						#region  __createElement
						{
							var il = xil;


							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldfld, __InternalDocument);
							il.Emit(OpCodes.Ldstr, CurrentElement.Name.LocalName);
							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldftn, InternalInitialize);
							il.Emit(OpCodes.Newobj, typeof(PHTMLElementAction).GetConstructors().Single());
							il.Emit(OpCodes.Callvirt, __createElement);
						}
						#endregion

						Action<ILGenerator> EmitLoadElement =
							il =>
							{
								//il.Emit(OpCodes.Nop);
								il.Emit(OpCodes.Ldarg_0);
								il.Emit(OpCodes.Ldfld, __Field);
								//il.Emit(OpCodes.Nop);
							};

						#region InternalInitialize
						{
							var il = InternalInitialize.GetILGenerator();

							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldarg_1);
							il.Emit(OpCodes.Stfld, __Field);

							if (EmitParentField != null)
							{
								EmitParentField(il);

								EmitLoadElement(il);

								il.Emit(OpCodes.Callvirt, __appendChild);
							}

							if (ElementStored != null)
								ElementStored(EmitLoadElement, il);

							BeforeDone(EmitLoadElement, il);

							il.Emit(OpCodes.Ret);

						}
						#endregion

					};
				#endregion



				DefineInitializeElement(this.BodyElement, Page_ctor.GetILGenerator(), null,
					(EmitField, il) =>
					{
						il.Emit(OpCodes.Ldarg_0);

						EmitField(il);

						il.Emit(OpCodes.Stfld, __InternalElement);
					}
				);

				Page_ctor.GetILGenerator().Emit(OpCodes.Ret);

				var Page_InternalProgressState = Page.DefineField("InternalProgressState", typeof(int), FieldAttributes.Private);

				#region Page_InternalProgress
				{
					var il = Page_InternalProgress.GetILGenerator();

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldfld, Page_InternalProgressState);
					il.Emit(OpCodes.Ldc_I4_1);
					il.Emit(OpCodes.Add);
					il.Emit(OpCodes.Stfld, Page_InternalProgressState);

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldfld, Page_InternalProgressState);
					il.Emit(OpCodes.Ldc_I4, CurrentIndex);

					var __if  = il.DeclareLocal(typeof(bool));

					il.Emit(OpCodes.Ceq);
					il.Emit(OpCodes.Stloc_S, (byte)__if.LocalIndex);

					var skip = il.DefineLabel();

					il.Emit(OpCodes.Ldloc_S, (byte)__if.LocalIndex);
					
					il.Emit(OpCodes.Brfalse, skip);


					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Call, __InternalMarkReady);

					il.MarkLabel(skip);

					il.Emit(OpCodes.Ret);
				}
				#endregion

				Page.CreateType();
			}


		}


	}
}
