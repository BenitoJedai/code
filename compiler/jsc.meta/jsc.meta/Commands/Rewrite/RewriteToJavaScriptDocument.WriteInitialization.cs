using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using java.applet;
using jsc.Languages.IL;
using jsc.meta.Library;
using jsc.meta.Library.Templates;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.external;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace jsc.meta.Commands.Rewrite
{
	partial class RewriteToJavaScriptDocument
	{

		private void WriteInitialization_JavaInternalElement(
			ILGenerator il,
			Type proxy,
			Type context,
			ScriptApplicationEntryPointAttribute entry,
			FieldBuilder __InternalElement,
			ExternalInterfaceConsumer Consumer
			)
		{
			const string archive = "assets/Ultra1.UltraApplication/UltraApplet.jar";
			const string code = "Ultra1.UltraApplet";
			const int width = 4001;
			const int height = 4002;

			Action Implementation1 =
				delegate
				{
					var o = new IHTMLApplet();

					// see: http://java.sun.com/j2se/1.4.2/docs/guide/plugin/developer_guide/java_js.html
					// see: http://www.raditha.com/java/javascript.php
					// see: http://java.sun.com/j2se/1.4.2/docs/guide/plugin/developer_guide/using_tags.html

					o.archive = archive;
					o.mayscript = true;
					o.setAttribute("scriptable", "true");
					o.code = code;
					o.width = width;
					o.height = height;

				};

			var il_a = new ILTranslationExtensions.EmitToArguments();

			il_a[OpCodes.Ldc_I4] =
				e =>
				{
					if (e.i.TargetInteger == width)
					{
						il.Emit(OpCodes.Ldc_I4, entry.Width);
						return;
					}

					if (e.i.TargetInteger == height)
					{
						il.Emit(OpCodes.Ldc_I4, entry.Height);
						return;
					}

					e.Default();
				};

			il_a[OpCodes.Ldstr] =
				e =>
				{
					if (e.i.TargetLiteral == archive)
					{
						il.Emit(OpCodes.Ldstr, "assets/" + context.FullName + "/" + proxy.FullName + ".jar");
						return;
					}

					if (e.i.TargetLiteral == code)
					{
						il.Emit(OpCodes.Ldstr, proxy.FullName.Replace("+", "_"));
						return;
					}

					e.Default();
				};
			il_a[OpCodes.Ret] =
				e =>
				{
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldloc_0);
					il.Emit(OpCodes.Stfld, __InternalElement);

					// start probing...
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Call, Consumer.__out_Method_init);


					il.Emit(OpCodes.Ret);
				};

	
			Implementation1.Method.EmitTo(il, il_a);
		}

		private void WriteInitialization_ActionScriptInternalElement(
			ILGenerator il,
			Type proxy,
			Type context,
			ScriptApplicationEntryPointAttribute entry,
			FieldInfo __InternalElement,
			Func<MethodInfo, MethodInfo> MethodCache,

			ExternalInterfaceConsumer Consumer
			)
		{
			// whatever they are doing it may work! :D
			// see: http://code.google.com/p/swfobject/source/browse/trunk/swfobject/src/swfobject.js

			const string src = @"assets/Ultra1.UltraApplication/UltraSprite.swf";
			const string type = "application/x-shockwave-flash";
			const int width = 4001;
			const int height = 4002;
			// http://kb2.adobe.com/cps/164/tn_16494.html
			// http://stackoverflow.com/questions/2154931/how-to-call-dynamically-created-flash-external-interface-in-ie-from-javascript

			Action Implementation1 =
				delegate
				{
					var oo = new object();

					var id = "__embed_" + new Random().Next();

					// good luck getting it to work with ID :)
					// there probably is a way to do it!

					var o = new IHTMLEmbed();


					// http://www.bobbyvandersluis.com/ufo/index.html
					// for IE we might need to consider setting innerHTML
					// as we do already in scriptcorelib for some elements IHTMLInput

					o.type = type;

					// http://perishablepress.com/press/2007/04/17/embed-flash-or-die-trying/
					// http://curtismorley.com/2008/11/01/actionscript-security-error-2060-security-sandbox-violation/
					// http://developer.yahoo.com/ylive/flash_js_api/
					// http://www.extremefx.com.ar/blog/fixing-flash-external-interface-inside-form-on-internet-explorer
					// http://code.google.com/p/swfobject/source/browse/trunk/swfobject/src/swfobject.js


					// do we need id and names? for IE?
					o.id = id;
					o.name = id;



					o.setAttribute("allowFullScreen", "true");
					o.setAttribute("allowNetworking", "all");
					o.setAttribute("allowScriptAccess", "always");

					// we need Ldc_I4
					o.width = 4001;
					o.height = 4002;

					o.src = src;

					__InternalElementProxy.OrphanizeLater(o);

					oo = o;
				};

			var il_a = new ILTranslationExtensions.EmitToArguments();

			//il_a.TranslateTargetType = t => t == typeof(Implementation1) ? a.Type : t;
			il_a.TranslateTargetMethod = MethodCache;

			il_a[OpCodes.Ldc_I4] =
				e =>
				{
					if (e.i.TargetInteger == width)
					{
						il.Emit(OpCodes.Ldc_I4, entry.Width);
						return;
					}

					if (e.i.TargetInteger == height)
					{
						il.Emit(OpCodes.Ldc_I4, entry.Height);
						return;
					}

					e.Default();
				};



			il_a[OpCodes.Ldstr] =
				e =>
				{
					if (e.i.TargetLiteral == src)
					{
						il.Emit(OpCodes.Ldstr, "assets/" + context.FullName + "/" + proxy.FullName + ".swf");
						return;
					}

					e.Default();
				};

			il_a[OpCodes.Ret] =
				e =>
				{
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldloc_0);
					il.Emit(OpCodes.Stfld, __InternalElement);

					// start probing...
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Call, Consumer.__out_Method_init);

					il.Emit(OpCodes.Ret);
				};


			//il_a[OpCodes.Ldarg_0] =
			//    e =>
			//    {
			//        il.Emit(OpCodes.Ldarg_0);
			//        il.Emit(OpCodes.Ldftn, __SetElementLoaded);
			//        il.Emit(OpCodes.Newobj, typeof(Action).GetConstructors().Single());

			//    };

			Implementation1.Method.EmitTo(il, il_a);

		}






		private void WriteInitialization_ActionScriptExternalInterface(
			RewriteToAssembly r,
			ILTranslationExtensions.EmitToArguments.ILRewriteContext c,
			TypeBuilder DeclaringType,
			Type TargetType,
			IEnumerable<MethodBuilderInfo> ExternalCallbackList)
		{
			var __InitializeExternalInterface = DeclaringType.DefineMethod(__out_Method, MethodAttributes.Private, CallingConventions.Standard, typeof(void), null);

			__InitializeExternalInterface.DefineAttribute(
				new ObfuscationAttribute
				{
					Feature = "meta method: external interface is now able to call our methods"
				}
				,
				typeof(ObfuscationAttribute)
			);

			c.il.Emit(OpCodes.Ldarg_0);
			c.il.Emit(OpCodes.Call, __InitializeExternalInterface);

			var il = __InitializeExternalInterface.GetILGenerator();

			Action Implementation1 =
				delegate
				{
					// to be replaced by .out_method
					ExternalInterface.addCallback("isActive", new Action(Console.WriteLine).ToFunction());
				};

			foreach (var kk in ExternalCallbackList)
			{

				// http://olondono.blogspot.com/2008/02/creating-code-at-runtime-part-2.html


				#region __Delegate
				var __Delegate = DeclaringType.DefineNestedType(__in_Delegate + kk.Method.Name, TypeAttributes.NestedFamily | TypeAttributes.AutoClass | TypeAttributes.Sealed, typeof(MulticastDelegate));

				var __Delegate_ctor = __Delegate.DefineConstructor(
					MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
					CallingConventions.Standard,
					new Type[] { typeof(object), typeof(IntPtr) }
				);

				__Delegate_ctor.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);
				// mxmlc will complain that jsc didnt create parameter names... jsc should generate them :)
				__Delegate_ctor.DefineParameter(1, ParameterAttributes.None, "object");
				__Delegate_ctor.DefineParameter(2, ParameterAttributes.None, "method");

				// Method attributes flags
				MethodAttributes maDelegate = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual;
				MethodImplAttributes mia = MethodImplAttributes.Runtime | MethodImplAttributes.Managed;



				var __Delegate_Invoke = __Delegate.DefineMethod(
					"Invoke", maDelegate,
					CallingConventions.Standard, kk.Method.ReturnType, kk.Parameters);


				__Delegate_Invoke.SetImplementationFlags(mia);


				#region BeginInvoke, EndInvoke
				{
					// BeginInvoke for Asynchronous call
					var methodBuilder = __Delegate.DefineMethod("BeginInvoke", maDelegate, typeof(IAsyncResult), new Type[] { typeof(AsyncCallback), typeof(object) });
					methodBuilder.SetImplementationFlags(mia);
				}
				{
					// EndInvoke for Asynchronous call
					var methodBuilder = __Delegate.DefineMethod("EndInvoke", maDelegate, typeof(void), new Type[] { typeof(IAsyncResult) });
					methodBuilder.SetImplementationFlags(mia);
				}
				#endregion

				__Delegate.CreateType();

				#endregion

				var ExposedMethod = kk.Method;

				#region addCallback
				var il_a = new ILTranslationExtensions.EmitToArguments();

				il_a[OpCodes.Newobj] =
					e =>
					{
						e.il.Emit(OpCodes.Newobj, __Delegate_ctor);
					};

				il_a[OpCodes.Ldftn] =
					e =>
					{
						e.il.Emit(OpCodes.Ldftn, ExposedMethod);
					};


				il_a[OpCodes.Ldnull] =
					e =>
					{
						e.il.Emit(OpCodes.Ldarg_0);
					};

				il_a[OpCodes.Ldstr] =
					e =>
					{
						e.il.Emit(OpCodes.Ldstr, ExposedMethod.Name);
					};

				il_a[OpCodes.Ret] =
					e =>
					{

					};

				Implementation1.Method.EmitTo(il, il_a);
				#endregion

			}

			il.Emit(OpCodes.Ret);

		}


	
	}
}
