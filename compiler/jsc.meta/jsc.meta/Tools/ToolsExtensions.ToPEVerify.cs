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
		public static void ToPEVerify(this FileInfo SourceAssembly, DirectoryInfo MicrosoftWindowsSDK)
		{
            // http://stackoverflow.com/questions/1915182/where-can-i-download-peverify-exe-tool

            // http://www.telerik.com/community/forums/orm/general-discussions/peverify-error.aspx


            var psi = new ProcessStartInfo(
                MicrosoftWindowsSDK + @"\bin\NETFX 4.0 Tools\PEVerify.exe",
                SourceAssembly.Name +  " /unique"
                )
                {
                    UseShellExecute = false,

                    WorkingDirectory = SourceAssembly.Directory.FullName
                };

            Console.WriteLine(psi.FileName);
            Console.WriteLine(SourceAssembly.FullName);

            var p = Process.Start(psi);

            p.WaitForExit();
		}
	}
}
