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
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.ActionScript.flash.external;
using ScriptCoreLib.ActionScript.Extensions;
using jsc.meta.Library.Templates;

namespace jsc.meta.Commands.Rewrite
{
	partial class RewriteToJavaScriptDocument
	{
		public const string ExternalInterfacePrefix = "__ExternalInterfacePrefix_";

		private void WriteInitialization_JavaInternalElement(
			ILGenerator il,
			Type proxy,
			Type context,
			ScriptApplicationEntryPointAttribute entry,
			FieldBuilder __InternalElement,
			MethodInfo __SetElementLoaded,
			MethodInfo __AfterElementLoaded
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

					o.onload += null;
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
					il.Emit(OpCodes.Ret);
				};

			il_a[OpCodes.Ldnull] =
				e =>
				{
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldftn, __SetElementLoaded);
					il.Emit(OpCodes.Newobj, typeof(Action).GetConstructors().Single());

				};

			Implementation1.Method.EmitTo(il, il_a);
		}

		private void WriteInitialization_ActionScriptInternalElement(
			ILGenerator il,
			Type proxy,
			Type context,
			ScriptApplicationEntryPointAttribute entry,
			FieldInfo __InternalElement,
			MethodInfo __SetElementLoaded,
			MethodInfo __AfterElementLoaded
			)
		{
			const string src = @"assets/Ultra1.UltraApplication/UltraSprite.swf";
			const string type = "application/x-shockwave-flash";
			const int width = 4001;
			const int height = 4002;
			// http://kb2.adobe.com/cps/164/tn_16494.html
			// http://stackoverflow.com/questions/2154931/how-to-call-dynamically-created-flash-external-interface-in-ie-from-javascript

			Action Implementation1 =
				delegate
				{
					var o = new IHTMLEmbed();
					//var id = "__embed_" + new Random().Next();

					// good luck getting it to work with ID :)
					// there probably is a way to do it!

					//o = (IHTMLEmbed)new IFunction("e", "/*@cc_on return this.createElement(e); @*/ return null;").apply(Native.Document,
					//    "<embed id='accc' name='accc' width='500' height='400'  src='assets/Ultra2.UltraApplication/UltraSprite.swf' allowfullscreen='true' allownetworking='all' allowscriptaccess='always'  />"
					//);


					//if (o == null)
					//{


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
					//o.id = id;
					//o.name = id;



					o.setAttribute("allowFullScreen", "true");
					o.setAttribute("allowNetworking", "all");
					o.setAttribute("allowScriptAccess", "always");

					// we need Ldc_I4
					o.width = 4001;
					o.height = 4002;

					o.src = src;

					o.onload += null;
					//}
				};

			var il_a = new ILTranslationExtensions.EmitToArguments();

			//il_a.TranslateTargetType = t => t == typeof(Implementation1) ? a.Type : t;
			//il_a.TranslateTargetMethod = m => m == Implementation4.Method ? __cctor_1 : m;

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
					il.Emit(OpCodes.Ret);
				};


			il_a[OpCodes.Ldnull] =
				e =>
				{
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldftn, __SetElementLoaded);
					il.Emit(OpCodes.Newobj, typeof(Action).GetConstructors().Single());

				};

			Implementation1.Method.EmitTo(il, il_a);

		}


		private void WriteInitialization_JavaExternalInterface(
			RewriteToAssembly r,
			RewriteToAssembly.PostTypeRewriteArguments a,
			Type TargetType
			)
		{
			foreach (var kk in TargetType.GetMethods(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.Instance))
			{
				if (kk.IsVirtual)
					continue;

				// http://olondono.blogspot.com/2008/02/creating-code-at-runtime-part-2.html

				var IsEventMethod = kk.IsEventMethod();

				if (IsEventMethod)
				{
					// we need some additional code
					var Invoke = kk.GetParameterTypes().Single().GetMethod("Invoke");

					#region add_
					if (kk.Name.StartsWith("add_"))
					{
						WriteInitialization_JavaExternalInterface_add(r, kk, a.Type, Invoke);


					}
					#endregion


					#region remove_
					if (kk.Name.StartsWith("remove_"))
					{
						var LocalMethod = a.Type.DefineMethod(ExternalInterfacePrefix + kk.Name, MethodAttributes.Public, CallingConventions.Standard, typeof(void), new[] { typeof(string) });

						var LocalMethod_il = LocalMethod.GetILGenerator();

						LocalMethod_il.EmitCode(() => { throw new NotSupportedException(); });

					}
					#endregion

				}
			}
		}


		private void WriteInitialization_ActionScriptExternalInterface(
			RewriteToAssembly r,
			ILTranslationExtensions.EmitToArguments.ILRewriteContext c,
			TypeBuilder DeclaringType,
			Type TargetType)
		{
			var __InitializeExternalInterface = DeclaringType.DefineMethod("__InitializeExternalInterface", MethodAttributes.Private, CallingConventions.Standard, typeof(void), null);

			c.il.Emit(OpCodes.Ldarg_0);
			c.il.Emit(OpCodes.Call, __InitializeExternalInterface);

			var il = __InitializeExternalInterface.GetILGenerator();

			il.Emit(OpCodes.Call, r.RewriteArguments.context.MethodCache[((Action)InternalActionScriptToJavascriptBridge.ExternalInterface_isActive).Method]);

			Action Implementation1 =
				delegate
				{
					ExternalInterface.addCallback("isActive", new Action(Console.WriteLine).ToFunction());
				};

			foreach (var kk in TargetType.GetMethods(
				BindingFlags.DeclaredOnly |
				BindingFlags.Public | BindingFlags.Instance))
			{

				// http://olondono.blogspot.com/2008/02/creating-code-at-runtime-part-2.html

				var IsEventMethod = kk.IsEventMethod();


				#region __Delegate
				var __Delegate = DeclaringType.DefineNestedType("__Delegate_" + kk.Name, TypeAttributes.NestedFamily | TypeAttributes.AutoClass | TypeAttributes.Sealed, typeof(MulticastDelegate));

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
					CallingConventions.Standard, kk.ReturnType,
					Enumerable.ToArray(
						from ParameterType in kk.GetParameterTypes()
						// add and remove events
						// must first save the delegate in global object like window
						select typeof(Delegate).IsAssignableFrom(ParameterType) ? typeof(string) : r.RewriteArguments.context.TypeCache[ParameterType]
					)
				);


				__Delegate_Invoke.SetImplementationFlags(mia);
				kk.GetParameters().CopyTo(__Delegate_Invoke);

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

				var ExposedMethod = r.RewriteArguments.context.MethodCache[kk];

				if (IsEventMethod)
				{
					// we need some additional code
					var Invoke = kk.GetParameterTypes().Single().GetMethod("Invoke");

					#region add_
					if (kk.Name.StartsWith("add_"))
					{
						// we need a closure!

						ExposedMethod = WriteInitialization_ActionScriptExternalInterface_add(r, DeclaringType, kk, ExposedMethod, Invoke);
					}
					#endregion


					#region remove_
					if (kk.Name.StartsWith("remove_"))
					{
						var LocalMethod = DeclaringType.DefineMethod(kk.Name, MethodAttributes.Public, CallingConventions.Standard, typeof(void), new[] { typeof(string) });

						var LocalMethod_il = LocalMethod.GetILGenerator();

						LocalMethod_il.EmitCode(() => { throw new NotSupportedException(); });

						ExposedMethod = LocalMethod;
					}
					#endregion

				}

				if (ExposedMethod != null)
				{
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
							e.il.Emit(OpCodes.Ldstr, kk.Name);
						};

					il_a[OpCodes.Ret] =
						e =>
						{

						};

					Implementation1.Method.EmitTo(il, il_a);
					#endregion

				}
			}

			il.Emit(OpCodes.Ret);
		}


		private static void WriteInitialization_JavaExternalInterface_add(
			RewriteToAssembly r,
			MethodInfo kk,

			TypeBuilder DeclaringType,
			MethodInfo Invoke

			)
		{
			Action<Func<string>, object> Implementation2 =
						(current, value) =>
						{
							current += value.ToString;
						};

			var Closure = DeclaringType.DefineNestedType("<>" + kk.Name, TypeAttributes.NestedPrivate | TypeAttributes.Sealed);
			var Closure_ctor = Closure.DefineDefaultConstructor(MethodAttributes.Public);
			var Closure_this = Closure.DefineField("<>this", DeclaringType, FieldAttributes.Public);
			var Closure_value = Closure.DefineField("<>value", typeof(string), FieldAttributes.Public);

			var Closure_Invoke = Closure.DefineMethod(
				"Invoke",
				MethodAttributes.Public,
				CallingConventions.Standard,
				Invoke.ReturnType,
				Invoke.GetParameterTypes()
			);

			var Closure_Invoke_il = Closure_Invoke.GetILGenerator();

			Func<Applet, string, object[], object> InternalJavaToJavascriptBridge_Invoke = InternalJavaToJavascriptBridge.Invoke;


			var args = Closure_Invoke_il.DeclareLocal(typeof(object[]));

			Closure_Invoke_il.Emit(OpCodes.Ldc_I4, Invoke.GetParameters().Length);
			Closure_Invoke_il.Emit(OpCodes.Newarr, typeof(object));
			Closure_Invoke_il.Emit(OpCodes.Stloc, (short)args.LocalIndex);

			for (short i = 0; i < Invoke.GetParameters().Length; i++)
			{
				Closure_Invoke_il.Emit(OpCodes.Ldloc, (short)args.LocalIndex);
				Closure_Invoke_il.Emit(OpCodes.Ldc_I4, (int)i);
				Closure_Invoke_il.Emit(OpCodes.Ldarg, (short)(i + 1));
				Closure_Invoke_il.Emit(OpCodes.Stelem_Ref);
			}


			Closure_Invoke_il.Emit(OpCodes.Ldarg_0);
			Closure_Invoke_il.Emit(OpCodes.Ldfld, Closure_this);

			Closure_Invoke_il.Emit(OpCodes.Ldarg_0);
			Closure_Invoke_il.Emit(OpCodes.Ldfld, Closure_value);

			Closure_Invoke_il.Emit(OpCodes.Ldloc, (short)args.LocalIndex);



			// this is the first time we mention this function
			// it will now be copied to our new assembly
			Closure_Invoke_il.Emit(OpCodes.Call, r.RewriteArguments.context.MethodCache[InternalJavaToJavascriptBridge_Invoke.Method]);

			if (Invoke.ReturnType == typeof(void))
				Closure_Invoke_il.Emit(OpCodes.Pop);


			Closure_Invoke_il.Emit(OpCodes.Ret);


			Closure.CreateType();

			var LocalMethod = DeclaringType.DefineMethod(ExternalInterfacePrefix + kk.Name, MethodAttributes.Public, CallingConventions.Standard, typeof(void), new[] { typeof(string) });

			var il = LocalMethod.GetILGenerator();

			il.DeclareLocal(Closure);

			il.Emit(OpCodes.Newobj, Closure_ctor);
			il.Emit(OpCodes.Stloc_0);

			il.Emit(OpCodes.Ldloc_0);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Stfld, Closure_this);

			il.Emit(OpCodes.Ldloc_0);
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Stfld, Closure_value);

			il.Emit(OpCodes.Ldarg_0);

			il.Emit(OpCodes.Ldloc_0);
			il.Emit(OpCodes.Ldftn, Closure_Invoke);

			il.Emit(OpCodes.Newobj, r.RewriteArguments.context.ConstructorCache[kk.GetParameterTypes().Single().GetConstructors().Single()]);

			il.Emit(OpCodes.Call, r.RewriteArguments.context.MethodCache[kk]);




			il.Emit(OpCodes.Ret);
		}

		private static MethodInfo WriteInitialization_ActionScriptExternalInterface_add(
			RewriteToAssembly r,

			TypeBuilder DeclaringType,
			MethodInfo kk,
			MethodInfo ExposedMethod,
			MethodInfo Invoke
			)
		{
			Action<Func<string>, object> Implementation2 =
				(current, value) =>
				{
					current += value.ToString;
				};

			var Closure = DeclaringType.DefineNestedType("<>" + kk.Name, TypeAttributes.NestedPrivate | TypeAttributes.Sealed);
			var Closure_ctor = Closure.DefineDefaultConstructor(MethodAttributes.Public);
			//var Closure_this = Closure.DefineField("<>this", DeclaringType, FieldAttributes.Public);
			var Closure_value = Closure.DefineField("<>value", typeof(string), FieldAttributes.Public);

			var Closure_Invoke = Closure.DefineMethod(
				"Invoke",
				MethodAttributes.Public,
				CallingConventions.Standard,
				Invoke.ReturnType,
				Invoke.GetParameterTypes()
			);

			var Closure_Invoke_il = Closure_Invoke.GetILGenerator();

			Func<string, object> _0 = ExternalInterface.call;
			Func<string, object, object> _1 = ExternalInterface.call;
			Func<string, object, object, object> _2 = ExternalInterface.call;
			Func<string, object, object, object, object> _3 = ExternalInterface.call;

			// we are going to crash if there are more than 3 params :)

			var Dispatcher = new Delegate[] { _0, _1, _2, _3 }[Invoke.GetParameterTypes().Length].Method;

			Closure_Invoke_il.Emit(OpCodes.Ldarg_0);
			Closure_Invoke_il.Emit(OpCodes.Ldfld, Closure_value);

			for (short i = 0; i < Invoke.GetParameters().Length; i++)
			{
				Closure_Invoke_il.Emit(OpCodes.Ldarg, (short)(i + 1));
			}

			Closure_Invoke_il.Emit(OpCodes.Call, Dispatcher);

			if (Invoke.ReturnType == typeof(void))
				Closure_Invoke_il.Emit(OpCodes.Pop);


			Closure_Invoke_il.Emit(OpCodes.Ret);


			Closure.CreateType();

			var LocalMethod = DeclaringType.DefineMethod(kk.Name, MethodAttributes.Public, CallingConventions.Standard, typeof(void), new[] { typeof(string) });

			var il = LocalMethod.GetILGenerator();

			il.DeclareLocal(Closure);

			il.Emit(OpCodes.Newobj, Closure_ctor);
			il.Emit(OpCodes.Stloc_0);

			//LocalMethod_il.Emit(OpCodes.Ldloc_0);
			//LocalMethod_il.Emit(OpCodes.Ldarg_0);
			//LocalMethod_il.Emit(OpCodes.Stfld, Closure_this);

			il.Emit(OpCodes.Ldloc_0);
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Stfld, Closure_value);

			il.Emit(OpCodes.Ldarg_0);

			il.Emit(OpCodes.Ldloc_0);
			il.Emit(OpCodes.Ldftn, Closure_Invoke);

			il.Emit(OpCodes.Newobj, r.RewriteArguments.context.ConstructorCache[kk.GetParameterTypes().Single().GetConstructors().Single()]);

			il.Emit(OpCodes.Call, r.RewriteArguments.context.MethodCache[kk]);




			il.Emit(OpCodes.Ret);



			ExposedMethod = LocalMethod;
			return ExposedMethod;
		}
	}
}
