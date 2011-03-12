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

            var Command = MicrosoftWindowsSDK + @"\bin\NETFX 4.0 Tools\PEVerify.exe";

            Console.WriteLine(Command);
            //Console.WriteLine(new { Command, SourceAssembly.FullName, SourceAssembly.FullName.Length });



            if (!File.Exists(Command))
            {
                Console.WriteLine("PEVerify was not found...");
                return;
            }

            var psi = new ProcessStartInfo(
                Command,
                "\"" + SourceAssembly.Name +  "\" /unique"
                )
                {
                    UseShellExecute = false,

                    WorkingDirectory = SourceAssembly.Directory.FullName
                };


            var p = Process.Start(psi);

            p.WaitForExit();

            if (p.ExitCode != 0)
            {
                throw new InvalidOperationException();
            }
		}
	}
}
