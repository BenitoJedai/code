using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace jsc.meta.Tools
{
	static partial class ToolsExtensions
	{
		public static void ToC(this FileInfo SourceAssembly, string TargetName, DirectoryInfo MicrosoftVisualStudio, DirectoryInfo MicrosoftWindowsSDK)
		{
			// fixme: currently jsc build only one set of c and h files
			// for the target assembly
			// we might want to fix that

			jsc.Program.TypedMain(
				new jsc.CompileSessionInfo
				{
					Options = new jsc.CommandLineOptions
					{
						TargetAssembly = SourceAssembly,
						IsC = true,
						IsNoLogo = true
					}
				}
			);
			// something to look out for:
			//---------------------------
			//cl.exe - Unable To Locate Component
			//---------------------------
			//This application has failed to start because mspdb80.dll was not found. Re-installing the application may fix this problem. 
			//---------------------------
			//OK   
			//---------------------------

			// see: http://social.msdn.microsoft.com/Forums/en-US/Vsexpressinstall/thread/2a3c57c5-de79-43e6-9769-35043f732d68

			var obj_web = Path.Combine(SourceAssembly.Directory.FullName, "web");

			var psi = new ProcessStartInfo(
				MicrosoftVisualStudio + @"\vc\bin\cl.exe",
				"/TC /Zm200 /nologo /EHsc *.c /Fe" + TargetName)
			{
				UseShellExecute = false,

				WorkingDirectory = obj_web
			};

			psi.EnvironmentVariables["PATH"] += Path.Combine(MicrosoftVisualStudio.FullName, @"Common7\Tools") + ";";
			psi.EnvironmentVariables["PATH"] += Path.Combine(MicrosoftVisualStudio.FullName, @"Common7\IDE") + ";";
			psi.EnvironmentVariables["LIB"] += Path.Combine(MicrosoftVisualStudio.FullName, @"VC\lib") + ";";
			psi.EnvironmentVariables["LIB"] += Path.Combine(MicrosoftWindowsSDK.FullName, @"lib") + ";";
			psi.EnvironmentVariables["INCLUDE"] += Path.Combine(MicrosoftVisualStudio.FullName, @"VC\include") + ";";
			psi.EnvironmentVariables["INCLUDE"] += Path.Combine(MicrosoftWindowsSDK.FullName, @"include") + ";";

			var proccess_cl = Process.Start(psi);

			proccess_cl.WaitForExit();


		}
	}
}
