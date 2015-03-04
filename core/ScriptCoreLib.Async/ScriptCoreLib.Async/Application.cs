using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Async;
using ScriptCoreLib.Async.Design;
using ScriptCoreLib.Async.HTML.Pages;

namespace ScriptCoreLib.Async
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application
	{
		public readonly ApplicationWebService service = new ApplicationWebService();

		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			@"Hello world".ToDocumentTitle();
			// Send data from JavaScript to the server tier
			service.WebMethod2(
				@"A string from JavaScript.",
				value => value.ToDocumentTitle()
			);
		}


		//414: erase { SourceMethod = Void<pushState> b__0(), offset = 6, x = [0x024b]
		//		stsfld     +0 -1 }
		//..................
		//{ trace = X:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs, TargetMethod = ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton WhenClicked(ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton, System.Func`2[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton, System.Threading.Tasks.Task]), DeclaringType = ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButtonAsyncExtensions, Location =
		// assembly: C:\Users\Arvo\AppData\Local\Temp\zg4au0wp.ack\ScriptCoreLib.Async.exe
		// type: ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButtonAsyncExtensions, ScriptCoreLib.Async, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
		// offset: 0x001b
		//  method:ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton WhenClicked(ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton, System.Func`1[System.Threading.Tasks.Task]), ex = System.ArgumentException: Empty name is not legal.
		//Parameter name: fieldName
		//   at System.Reflection.Emit.FieldBuilder..ctor(TypeBuilder typeBuilder, String fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		//   at System.Reflection.Emit.TypeBuilder.DefineFieldNoLock(String fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		//   at System.Reflection.Emit.TypeBuilder.DefineField(String fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		//   at System.Reflection.Emit.TypeBuilder.DefineField(String fieldName, Type type, FieldAttributes attributes)
		//   at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass725.<InternalInvoke>b__1011(FieldInfo SourceField) in X:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.cs:line 2763
		//   at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 163
		//   at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 113
		//   at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyType(Type SourceType, AssemblyBuilder a, ModuleBuilder m, TypeBuilder OverrideDeclaringType, VirtualDictionary`2 TypeRenameCache, VirtualDictionary`2 NameObfuscation, Func`2 ShouldCopyType, Func`3 FullNameFixup, Action`1 PostTypeRewrite, Action`1 PreTypeRewrite, Action`1 TypeCreated, RewriteToAssembly r, ILTranslationContext context, Action AtCodeTraceCreateType) in X:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.CopyType.cs:line 268
	}
}
