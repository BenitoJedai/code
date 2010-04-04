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
		private static void DefinePageConstructor(
			XElement CurrentElement,
			TypeBuilder Page,
			Dictionary<XElement, MethodBuilder>[] lookup,
			Dictionary<string, TypeVariationsTuple> NamedElements,
			Dictionary<string, Type> ElementTypes
		)
		{
			// what happens in design mode in .net ? :)

			var ctor = Page.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);

			var DefaultElementType =
				// ScriptCoreLib ElementType Lookup...
				(ElementTypes.ContainsKey(CurrentElement.Name.LocalName) ? ElementTypes[CurrentElement.Name.LocalName] : typeof(IHTMLElement));

			var ElementType = DefaultElementType;

			if (ElementType == typeof(IHTMLBody))
				ElementType = typeof(IHTMLDiv);

			var PageCreate = Page.DefineMethod("Create", MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard,
				Page, null);

			{
				var il = PageCreate.GetILGenerator();

				il.Emit(OpCodes.Newobj, ctor);
				il.Emit(OpCodes.Ret);
			}


			var FieldContainer = Page.DefineField("_Container", ElementType, FieldAttributes.Private | FieldAttributes.InitOnly);

			{
				var ElementProperty = Page.DefineProperty("Container", PropertyAttributes.None, ElementType, null);

				var get_ElementField = Page.DefineMethod("get_Container", MethodAttributes.Public, CallingConventions.Standard, ElementType, null);

				var get_ElementField_il = get_ElementField.GetILGenerator();

				get_ElementField_il.Emit(OpCodes.Ldarg_0);
				get_ElementField_il.Emit(OpCodes.Ldfld, FieldContainer);
				get_ElementField_il.Emit(OpCodes.Ret);

				ElementProperty.SetGetMethod(get_ElementField);
			}

			{
				var GetContainer = Page.DefineMethod("GetContainer", MethodAttributes.Public | MethodAttributes.Virtual, typeof(IHTMLDiv), null);


				var get_ElementField_il = GetContainer.GetILGenerator();

				get_ElementField_il.Emit(OpCodes.Ldarg_0);
				get_ElementField_il.Emit(OpCodes.Ldfld, FieldContainer);
				get_ElementField_il.Emit(OpCodes.Ret);

			}


			var Counter = new Counter();

			{
				var il = ctor.GetILGenerator();

				DefinePageElement(
					CurrentElement,
					Page,
					Counter,
					il,
					OpCodes.Ldnull,
					lookup,
					NamedElements,
					ElementTypes
				);

				#region this.Container = loc0
				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldloc_0);
				il.Emit(OpCodes.Stfld, FieldContainer);
				#endregion

				il.Emit(OpCodes.Ret);
			}
		}



	}
}
