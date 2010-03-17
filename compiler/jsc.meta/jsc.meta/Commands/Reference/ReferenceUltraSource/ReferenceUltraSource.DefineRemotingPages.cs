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
				var __createTextNode = new Func<PHTMLDocument, Action<string>>(doc => doc.createTextNode).ToReferencedMethod();

				var Page = a.Module.DefineType(
					PageFullName,
					TypeAttributes.Public,
					typeof(PUltraComponent)
				);

				var Page_InternalDocument = Page.DefineField("InternalDocument", typeof(PHTMLDocument), FieldAttributes.InitOnly);
				var Page_InternalContainer = Page.DefineField("InternalContainer", typeof(PHTMLElement), FieldAttributes.InitOnly);

				var Page_ctor = Page.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis,
					new[] 
					{
						typeof(PHTMLDocument)
					}
				);

				{
					var il = Page_ctor.GetILGenerator();

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldarg_1);
					il.Emit(OpCodes.Stfld, Page_InternalDocument);
				}

				var Continuation = Page.DefineMethod("Continuation1", MethodAttributes.Private, CallingConventions.HasThis, typeof(void),
					new[] { typeof(PHTMLElement) }
				);

				{
					var il = Page_ctor.GetILGenerator();


					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldfld, Page_InternalDocument);
					il.Emit(OpCodes.Ldstr, this.BodyElement.Name.LocalName);
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldftn, Continuation);
					il.Emit(OpCodes.Newobj, typeof(PHTMLElementAction).GetConstructors().Single());
					il.Emit(OpCodes.Call, __createElement);
					il.Emit(OpCodes.Ret);
				}

				{
					var il = Continuation.GetILGenerator();

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldarg_1);
					il.Emit(OpCodes.Stfld, Page_InternalContainer);
				}

				{
					var il = Continuation.GetILGenerator();

					foreach (var item in BodyElement.Nodes())
					{
						if (item is XText)
						{
							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldfld, Page_InternalDocument);
							il.Emit(OpCodes.Ldstr, ((XText)item).Value);
							il.Emit(OpCodes.Call, __createTextNode);
						}

						if (item is XNode)
						{
							// recursion!
						}
					}

					il.Emit(OpCodes.Ret);

				}



				Page.CreateType();
			}


		}


	}
}
