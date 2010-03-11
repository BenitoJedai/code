using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using jsc.meta.Commands.Rewrite.RewriteToSplashScreen.Templates;
using System.Reflection;
using System.Reflection.Emit;

namespace jsc.meta.Commands.Rewrite.RewriteToSplashScreen
{
	public partial class RewriteToSplashScreen : CommandBase
	{
		public override void Invoke()
		{
			// rewrite two assemblies and inject splash

			Action<Action> ShowDialogSplash = InternalSplashScreen.ShowDialogSplash;
			Action<string[]> Main = InternalSplashScreenApplication.Main;

			var Splash = Assembly.LoadFile(this.Splash.FullName);

			var ImplementationForShowDialogSplash = Enumerable.First(
				from i in new ILBlock(Splash.EntryPoint).Instructrions
				where i.OpCode == OpCodes.Call
				let m = i.TargetMethod
				where m.GetSignatureTypes().SequenceEqual(ShowDialogSplash.Method.GetSignatureTypes())
				select m
			);


			Console.WriteLine(ImplementationForShowDialogSplash.DeclaringType.FullName);


			var r = new RewriteToAssembly
			{
				Output = this.PrimaryAssembly,

				merge = new RewriteToAssembly.MergeInstruction[]
				{
					"ScriptCoreLib.Avalon",
					"ScriptCoreLib.Query",
					"ScriptCoreLibA",
				}
			};

			r.PostAssemblyRewrite =
					a =>
					{
						a.Assembly.SetEntryPoint(r.RewriteArguments.context.MethodCache[Main.Method], PEFileKinds.ConsoleApplication);
					};

			var CurrentScriptResources = new jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument.ScriptResources();

			r.AtILOverride +=
				(context, x) =>
				{
					// how slow will it be if we run this method for each instruction? :)

					x.BeforeInstruction +=
						e =>
						{
							if (e.i.OpCode == OpCodes.Ldstr)
							{
								// if it is a websource we need to copy it.

								CurrentScriptResources.Cache[e.i.OwnerMethod.DeclaringType.Assembly].AddWhenResource(
									r.RewriteArguments.ScriptResourceWriter,
									e.i.TargetLiteral
								);
							}
						};
				};

			r.ExternalContext.TypeCache.Resolve +=
				SourceType =>
				{
					if (SourceType == typeof(InternalSplashScreen))
					{
						r.ExternalContext.TypeCache[SourceType] = r.RewriteArguments.context.TypeCache[ImplementationForShowDialogSplash.DeclaringType];
					}
				};

			r.ExternalContext.MethodCache.Resolve +=
				SourceMethod =>
				{
					if (SourceMethod == ShowDialogSplash.Method)
					{
						r.ExternalContext.MethodCache[SourceMethod] = r.RewriteArguments.context.MethodCache[ImplementationForShowDialogSplash];
					}
				};

			r.PrimaryTypes = new[] { typeof(InternalSplashScreenApplication) };

			r.Invoke();
		}
	}

	namespace Templates
	{
		internal static class InternalApplication
		{
			public static void Main(string[] args)
			{
			}
		}

		internal static class InternalSplashScreen
		{
			public static void ShowDialogSplash(Action e)
			{
			}
		}

		internal static class InternalSplashScreenApplication
		{
			public static void Main(string[] args)
			{
				InternalSplashScreen.ShowDialogSplash(
					delegate
					{
						InternalApplication.Main(args);
					}
				);
			}
		}
	}
}
