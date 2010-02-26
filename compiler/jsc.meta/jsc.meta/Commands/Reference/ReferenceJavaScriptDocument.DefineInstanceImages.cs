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

		private void DefineInstanceImages(RewriteToAssembly.AssemblyRewriteArguments a, TypeBuilder Page, Dictionary<XElement, FieldBuilder> lookup)
		{
			#region References
			var References = Page.DefineProperty("Images", PropertyAttributes.None, typeof(IHTMLImage[]), null);

			var References_get = Page.DefineMethod("get_Images", MethodAttributes.Public | MethodAttributes.Virtual, typeof(IHTMLImage[]), null);

			References.SetGetMethod(References_get);

			{
				var il = References_get.GetILGenerator();

				Func<IHTMLImage[]> Implementation1 = () => new IHTMLImage[] { };

				var il_a = new ILTranslationExtensions.EmitToArguments();

				var Images = lookup.Where(k => k.Value != null).Select((k, index) => new { k, index }).ToArray();


				il_a[OpCodes.Ldc_I4_0] =
					x =>
					{
						il.Emit(OpCodes.Ldc_I4, Images.Length);
					};

				il_a[OpCodes.Stloc_0] =
					x =>
					{
						il.Emit(OpCodes.Stloc_0);

						foreach (var item in Images)
						{
							il.Emit(OpCodes.Ldloc_0);
							il.Emit(OpCodes.Ldc_I4, item.index);
							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldfld, item.k.Value);
							il.Emit(OpCodes.Stelem_Ref);


							/*
							L_0007: ldloc.1 
							L_0008: ldc.i4.0 
							L_0009: ldstr ""
							L_000e: stelem.ref 
							 */
						}
					};



				Implementation1.Method.EmitTo(il, il_a);
			}
			#endregion

		}



	}
}
