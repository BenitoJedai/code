using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Web;
using System.IO;
using jsc.meta.Library;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Reflection.Emit;
using jsc.meta.Commands.Rewrite.RewriteToSplashScreen.Templates;

namespace jsc.meta.Commands.Rewrite.RewriteToUltraApplication
{
	partial class RewriteToUltraApplication
	{
		[Obfuscation(Feature = "invalidmerge")]
		public class AsProgram
		{

			public static void Launch(Type PrimaryApplication)
			{
				new AsProgram { PrimaryApplication = PrimaryApplication }.Launch();
			}

			public bool DisableSplash;
			public Type PrimaryApplication;

			public void Launch()
			{
				var SplashTemplate = new FileInfo(@"c:\util\jsc\bin\jsc.splash.exe");
				var WebDevLauncher = default(FileInfo);

				if (!DisableSplash && SplashTemplate.Exists)
				{
					var Splash = Assembly.LoadFile(SplashTemplate.FullName);

					Action<Action> ShowDialogSplash = InternalSplashScreen.ShowDialogSplash;

					var ImplementationForShowDialogSplash = Enumerable.First(
						from i in new ILBlock(Splash.EntryPoint).Instructrions
						where i.OpCode == OpCodes.Call
						let m = i.TargetMethod
						where m.GetSignatureTypes().SequenceEqual(ShowDialogSplash.Method.GetSignatureTypes())
						select m
					);

					ShowDialogSplash = (Action<Action>)Delegate.CreateDelegate(typeof(Action<Action>), ImplementationForShowDialogSplash);


					ShowDialogSplash(
						delegate
						{
							WebDevLauncher = CompileAndLaunch(PrimaryApplication, WebDevLauncher);
						}
					);
				}
				else
				{
					WebDevLauncher = CompileAndLaunch(PrimaryApplication, WebDevLauncher);
				}


			}

			private static FileInfo CompileAndLaunch(Type PrimaryApplication, FileInfo WebDevLauncher)
			{
				WebDevLauncher = Compile(PrimaryApplication, WebDevLauncher);

				var WebDevLauncherAssembly = Assembly.LoadFile(WebDevLauncher.FullName);

				WebDevLauncherAssembly.EntryPoint.Invoke(null, new object[] { new string[0] });
				return WebDevLauncher;
			}

			private static FileInfo Compile(Type PrimaryApplication, FileInfo WebDevLauncher)
			{
				var r = new jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument
				{
					assembly = new FileInfo(PrimaryApplication.Assembly.Location),

					staging = new FileInfo(PrimaryApplication.Assembly.Location).Directory.CreateSubdirectory("staging.debug"),

					DisableWebServiceJava = true,
					DisableWebServicePHP = true,
					DisableWebServiceTypeMerge = true,
					InternalCreateNoWindow = true
					//IsRewriteOnly = true
				};


				r.AtWebServiceReady +=
					a =>
					{
						// what if there was no web service defined? :)

						WebDevLauncher = a.WebDevLauncher;
					};

				r.ProccessStatusChanged +=
					e =>
					{
						//Console.Write(".");
					};

				r.Invoke();
				return WebDevLauncher;
			}
		}
	}
}
