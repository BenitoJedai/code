using System;
using System.Reflection;
using jsc.meta.Commands.Analytics;
using jsc.meta.Commands.Configuration;
using jsc.meta.Commands.Extend;
using jsc.meta.Commands.Reference;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Commands.Rewrite.RewriteToInstaller;
using jsc.meta.Commands.Rewrite.RewriteToJavaScript;
using jsc.meta.Commands.Rewrite.RewriteToReplacedReferences;
using jsc.meta.Commands.Rewrite.RewriteToSplashScreen;
using jsc.meta.Commands.Rewrite.RewriteToUltraLibrary;
using jsc.meta.Commands.Rewrite.RewriteToVSProjectTemplate;
using jsc.meta.Commands.Test;
using ScriptCoreLib.Reflection.Options;

namespace jsc.meta
{
	public partial class Program
	{
		// read: http://www.albahari.com/nutshell/ch17.aspx

		// should we use MEF for different Commands instead?
		


		internal static void ShowLogo()
		{
			//Microsoft (R) Visual C# 2005 Compiler version 8.00.50727.42
			//for Microsoft (R) Windows (R) 2005 Framework version 2.0.50727
			//Copyright (C) Microsoft Corporation 2001-2005. All rights reserved.


			Console.WriteLine(((AssemblyDescriptionAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description);
			Console.WriteLine(((AssemblyCompanyAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0]).Company);
			Console.WriteLine(((AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright);
			Console.WriteLine();
		}

        public static Action<string, string> DelayedMoveFile;

		internal static void InternalMain(string[] args)
		{
			// This is the front-end compiler for jsc solutions...

			//Debugger.Launch();


			ShowLogo();

            //if (Debugger.IsAttached)
            //{
            //    jsc.meta.Library.CostumAttributeBuilderExtensions.TestFeature();
            //    jsc.meta.Commands.Rewrite.Templates.InternalToTypeTestConcept.TestConcept();
            //}

			args.AsParametersTo(
				new ExtendToWindowsFormsEverywhere().Invoke,
				new ExtendToNativeConsole().Invoke,
				new ExtendToJavaConsole().Invoke,
				new ExtendToAvalonEverywhere().Invoke,
				new ExtendToGoogleAppEngineWebService().Invoke,
				new ExtendToGoogleAppEngineWebApplication().Invoke,

				new ExtendToPHPWebService().Invoke,

				new ReferenceWaveComponent().Invoke,
				new ReferenceTextComponent().Invoke,
				new ReferenceTextualUserControl().Invoke,
				new ReferenceWebSource().Invoke,
				new ReferenceJavaScriptDocument().Invoke,
				new ReferenceAddClonedProject(),

				new RewriteToAssembly().Invoke,
                new RewriteToJavaScript(),
				new RewriteToJavaScriptDocument(),
				new RewriteToMVSProjectTemplate(),
				new RewriteToInstaller(),
				new RewriteToUltraLibrary(),
				new RewriteToSplashScreen(),
                new RewriteToReplacedReferences(),

				new AnalyticsForFlagCounter().Invoke,

                new ConfigurationCreateProjectTemplates(),
                new ConfigurationInitialize(),
                new ConfigurationPrecompile(),
                new ConfigurationIncrementApplicationVersion(),
                new ConfigurationDisposeSubst(),
                new ConfigurationClean(),

                new TestChooser(),
                new TestVirtualDriveCleanup()
			);

		}



	}
}
