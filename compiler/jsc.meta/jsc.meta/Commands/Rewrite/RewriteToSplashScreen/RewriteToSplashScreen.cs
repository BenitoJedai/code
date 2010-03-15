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
			Action<string[]> ApplicationMain = InternalApplication.Main;

			this.staging = this.staging.CreateTemp();

			var Application = this.PrimaryAssembly.LoadAssemblyAt(this.staging);
			var Splash = this.Splash.LoadAssemblyAt(this.staging);

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
					Path.GetFileNameWithoutExtension( this.PrimaryAssembly.Name)
				}

			};

			r.PostAssemblyRewrite =
				a =>
				{
					//r.RewriteArguments.Assembly.SetCustomAttribute(
					//    new ObfuscationAttribute { Feature = "script" }.ToCustomAttributeBuilder()(r.RewriteArguments.context)
					//);

					a.Assembly.SetEntryPoint(
						r.RewriteArguments.context.MethodCache[Main.Method],
						this.IsConsole ?  PEFileKinds.ConsoleApplication : PEFileKinds.WindowApplication
					);

					foreach (var item in Application.GetCustomAttributes(false).Select(kk => kk.ToCustomAttributeBuilder()))
					{
						a.Assembly.SetCustomAttribute(item(r.RewriteArguments.context));
					}



					foreach (var item in Application.GetManifestResourceNames())
					{
						var n = item;

						if (n.StartsWith(Application.GetName().Name))
							n = r.product + n.Substring(Application.GetName().Name.Length);

						r.RewriteArguments.Module.DefineManifestResource(
							n,
							Application.GetManifestResourceStream(item), ResourceAttributes.Public
						);

					}

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

					if (SourceType == typeof(InternalApplication))
					{
						r.ExternalContext.TypeCache[SourceType] = r.RewriteArguments.context.TypeCache[Application.EntryPoint.DeclaringType];
					}
				};

			r.ExternalContext.MethodCache.Resolve +=
				SourceMethod =>
				{
					if (SourceMethod == ShowDialogSplash.Method)
					{
						r.ExternalContext.MethodCache[SourceMethod] = r.RewriteArguments.context.MethodCache[ImplementationForShowDialogSplash];
					}

					if (SourceMethod == ApplicationMain.Method)
					{
						r.ExternalContext.MethodCache[SourceMethod] = r.RewriteArguments.context.MethodCache[Application.EntryPoint];
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
