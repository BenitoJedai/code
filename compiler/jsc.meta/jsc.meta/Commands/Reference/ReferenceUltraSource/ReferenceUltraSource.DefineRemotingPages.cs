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
			public Dictionary<string, Type> NamedElements;

			public void Invoke()
			{
				var PageFullName = DefaultNamespace + ".HTML.Pages." + VariationName + ".Remoting." + PageName;

				Console.WriteLine(PageFullName);

				var __createElement = new Func<PHTMLDocument, Action<string, PHTMLElementAction>>(doc => doc.createElement).ToReferencedMethod();
				var __createTextNode = new Func<PHTMLDocument, Action<string, PTextNodeAction>>(doc => doc.createTextNode).ToReferencedMethod();
				var __setAttribute = new Func<PHTMLElement, Action<string, string>>(doc => doc.setAttribute).ToReferencedMethod();
				var __appendChild = new Func<PNode, Action<PNode>>(doc => doc.appendChild).ToReferencedMethod();
				var __InternalMarkReady = new Func<PUltraComponent, Action>(doc => doc.InternalMarkReady).ToReferencedMethod();
				var __get_Container = new Func<PUltraComponent, PHTMLElement>(doc => doc.Container).ToReferencedMethod();
				var __get_Document = new Func<PUltraComponent, PHTMLDocument>(doc => doc.Document).ToReferencedMethod();

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




				Action<FieldBuilder> Define_get_Document =
					__Field =>
					{
						var get_ = Page.DefineMethod(__get_Document.Name,
							MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.Public,
							typeof(PHTMLDocument),
							null
						);

						var il = get_.GetILGenerator();

						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldfld, __Field);
						il.Emit(OpCodes.Ret);
					};

				var Page_InternalDocument = Page.DefineField("InternalDocument", typeof(PHTMLDocument), FieldAttributes.InitOnly);

				Define_get_Document(Page_InternalDocument);

				#region special
				{
					var il = Page_ctor.GetILGenerator();

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldarg_1);
					il.Emit(OpCodes.Stfld, Page_InternalDocument);
				}
				#endregion

		
				var CurrentIndex = -1;

				Func<FieldBuilder, MethodBuilder> DefineInitializeTextNode =
					(__ParentField) =>
					{
						CurrentIndex++;

						var InternalInitialize = Page.DefineMethod("InternalInitialize" + CurrentIndex, MethodAttributes.Private, CallingConventions.HasThis, typeof(void),
							new[] { typeof(PTextNode) }
						);

						var il = InternalInitialize.GetILGenerator();

						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldfld, __ParentField);

						il.Emit(OpCodes.Ldarg_1);

						il.Emit(OpCodes.Call, __appendChild);

						il.Emit(OpCodes.Ret);

						return InternalInitialize;
					};
	
				var DefineInitializeElement = default(Action<XElement, ILGenerator, FieldBuilder, Action<FieldBuilder, ILGenerator>>);

				DefineInitializeElement =
					(CurrentElement, xil, __ParentField, done) =>
					{
						CurrentIndex++;

						var __Field = Page.DefineField("InternalElement" + CurrentIndex, typeof(PHTMLElement), FieldAttributes.InitOnly);


						var InternalInitialize = Page.DefineMethod("InternalInitialize" + CurrentIndex, MethodAttributes.Private, CallingConventions.HasThis, typeof(void),
							new[] { typeof(PHTMLElement) }
						);



						{
							var il = xil;


							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldfld, Page_InternalDocument);
							il.Emit(OpCodes.Ldstr, CurrentElement.Name.LocalName);
							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldftn, InternalInitialize);
							il.Emit(OpCodes.Newobj, typeof(PHTMLElementAction).GetConstructors().Single());
							il.Emit(OpCodes.Call, __createElement);
						}


						{
							var il = InternalInitialize.GetILGenerator();

							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldarg_1);
							il.Emit(OpCodes.Stfld, __Field);

							if (__ParentField != null)
							{
								il.Emit(OpCodes.Ldarg_0);
								il.Emit(OpCodes.Ldfld, __ParentField);

								il.Emit(OpCodes.Ldarg_0);
								il.Emit(OpCodes.Ldfld, __Field);

								il.Emit(OpCodes.Call, __appendChild);
							}

							foreach (var item in CurrentElement.Attributes())
							{
								if (item.Name.LocalName == "id")
									continue;

								il.Emit(OpCodes.Ldarg_1);
								il.Emit(OpCodes.Ldstr, item.Name.LocalName);
								il.Emit(OpCodes.Ldstr, item.Value);
								il.Emit(OpCodes.Call, __setAttribute);

							}

							foreach (var item in CurrentElement.Nodes())
							{
								if (item is XText)
								{
									var InitializeTextNode = DefineInitializeTextNode(__Field);

									il.Emit(OpCodes.Ldarg_0);
									il.Emit(OpCodes.Ldfld, Page_InternalDocument);
									il.Emit(OpCodes.Ldstr, ((XText)item).Value);

									il.Emit(OpCodes.Ldarg_0);
									il.Emit(OpCodes.Ldftn, InitializeTextNode);
									il.Emit(OpCodes.Newobj, typeof(PTextNodeAction).GetConstructors().Single());

									il.Emit(OpCodes.Call, __createTextNode);
								}

								if (item is XElement)
								{

									DefineInitializeElement(item as XElement, il, __Field, null);
								}
							}

							if (done != null)
								done(__Field, il);

							il.Emit(OpCodes.Ret);

						}

					};

				Action<FieldBuilder> Define_get_Container =
					__Field =>
					{
						var get_ = Page.DefineMethod(__get_Container.Name,
							MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.Public,
							typeof(PHTMLElement),
							null
						);

						var il = get_.GetILGenerator();

						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldfld, __Field);
						il.Emit(OpCodes.Ret);
					};




				DefineInitializeElement(this.BodyElement, Page_ctor.GetILGenerator(), null,
					(__Field, il) =>
					{
						Define_get_Container(__Field);

						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Call, __InternalMarkReady);
					}
				);

				Page_ctor.GetILGenerator().Emit(OpCodes.Ret);


				Page.CreateType();
			}


		}


	}
}
