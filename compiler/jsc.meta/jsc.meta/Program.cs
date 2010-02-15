﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ScriptCoreLib.Reflection.Options;
using jsc.meta.Commands;
using jsc.meta.Commands.Extend;
using jsc.meta.Commands.Reference;
using jsc.meta.Commands.Rewrite;
using System.Diagnostics;
using jsc.meta.Commands.Analytics;

namespace jsc.meta
{
	public partial class Program
	{
		// read: http://www.albahari.com/nutshell/ch17.aspx

		// should we use MEF for different Commands instead?
		


		static void ShowLogo()
		{
			//Microsoft (R) Visual C# 2005 Compiler version 8.00.50727.42
			//for Microsoft (R) Windows (R) 2005 Framework version 2.0.50727
			//Copyright (C) Microsoft Corporation 2001-2005. All rights reserved.


			Console.WriteLine(((AssemblyDescriptionAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description);
			Console.WriteLine(((AssemblyCompanyAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0]).Company);
			Console.WriteLine(((AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright);
			Console.WriteLine();
		}

		public static void Main(string[] args)
		{
			// This is the front-end compiler for jsc solutions...

			//Debugger.Launch();


			ShowLogo();

			jsc.meta.Commands.Rewrite.Templates.InternalToTypeTestConcept.TestConcept();

			args.AsParametersTo(
				new ExtendToWindowsFormsEverywhere().Invoke,
				new ExtendToNativeConsole().Invoke,
				new ExtendToJavaConsole().Invoke,
				new ExtendToAvalonEverywhere().Invoke,
				new ExtendToGoogleAppEngineWebService().Invoke,
				new ExtendToGoogleAppEngineWebApplication().Invoke,

				new ExtendToPHPWebService().Invoke,
				new ExtendToPHPWebApplication().Invoke,

				new ReferenceWaveComponent().Invoke,
				new ReferenceTextComponent().Invoke,
				new ReferenceTextualUserControl().Invoke,
				new ReferenceWebSource().Invoke,
				new ReferenceJavaScriptDocument().Invoke,
				new ReferenceAddClonedProject(),

				new RewriteToAssembly().Invoke,
				new RewriteToJavaScriptDocument(),

				new AnalyticsForStatCounter().Invoke,
				new AnalyticsForFlagCounter().Invoke
			);

		}



	}
}
