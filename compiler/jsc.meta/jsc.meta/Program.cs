using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ScriptCoreLib.Reflection.Options;
using jsc.meta.Commands;

namespace jsc.meta
{
	public partial class Program
	{
		// read: http://www.albahari.com/nutshell/ch17.aspx



		const string MetaScript = "MetaScript";

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
			var CurrentDirectory = Environment.CurrentDirectory;
			ShowLogo();



			args.AsParametersTo(
				new ExtendToWindowsFormsEverywhere().Invoke,
				new ExtendToJavaConsole().Invoke,
				new ReferenceTextComponent().Invoke
			);

			Environment.CurrentDirectory = CurrentDirectory;
		}



	}
}
