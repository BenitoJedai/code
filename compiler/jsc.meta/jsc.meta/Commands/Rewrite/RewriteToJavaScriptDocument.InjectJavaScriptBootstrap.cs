using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using java.applet;
using jsc.meta.Library;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using jsc.Languages.IL;
using System.Reflection.Emit;

namespace jsc.meta.Commands.Rewrite
{
	partial class RewriteToJavaScriptDocument
	{
		class Implementation1
		{
			public Implementation1(IHTMLElement e)
			{

			}


			public static void Implementation2()
			{
				typeof(Implementation1).SpawnTo(Implementation4);

				foreach (var item in Native.Document.getElementsByClassName("LoadingAnimation").ToArray())
				{
					item.FadeOut();
				}

			}

			public static void Implementation4(IHTMLElement i)
			{
				new Implementation1(i);
			}
		}

		private void InjectJavaScriptBootstrap(RewriteToAssembly.TypeRewriteArguments a)
		{
			// would be nice to have simpler copy API!:D

			Action Implementation2 = Implementation1.Implementation2;
			Action<IHTMLElement> Implementation4 = Implementation1.Implementation4;

			var __cctor_1 = a.Type.DefineMethod("__cctor_1", Implementation4.Method.Attributes,
				Implementation4.Method.CallingConvention,
				Implementation4.Method.ReturnType,
				Implementation4.Method.GetParameterTypes()
			);

			#region __cctor_1
			{
				var il = __cctor_1.GetILGenerator();

				var il_a = new ILTranslationExtensions.EmitToArguments();

				il_a.TranslateTargetConstructor =
					ctor => ctor.DeclaringType == typeof(Implementation1) ?
						a.context.ConstructorCache[a.SourceType.GetConstructor(ctor.GetParameterTypes())] : ctor;


				Implementation4.Method.EmitTo(il, il_a);
			}
			#endregion

			var cctor = a.Type.DefineTypeInitializer();

			#region cctor
			{
				var il = cctor.GetILGenerator();

				var il_a = new ILTranslationExtensions.EmitToArguments();

				il_a.TranslateTargetType = t => t == typeof(Implementation1) ? a.Type : t;
				il_a.TranslateTargetMethod = m => m == Implementation4.Method ? __cctor_1 : m;


				Implementation2.Method.EmitTo(il, il_a);
			}
			#endregion

		}
	}
}
