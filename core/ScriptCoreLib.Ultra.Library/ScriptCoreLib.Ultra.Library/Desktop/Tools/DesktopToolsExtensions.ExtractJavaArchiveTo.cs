using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace ScriptCoreLib.Desktop.Tools
{
    public static partial class DesktopToolsExtensions
    {
        public static void ExtractJavaArchiveTo(this FileInfo target, string location, FileInfo jar)
        {
            var proccess_jar_args =
                "xf"
                // foo.jar
                + " \"" + target + "\""
                ;

            Directory.CreateDirectory(location);

            //Console.WriteLine(jar.FullName + " " + proccess_jar_args);

            var proccess_jar =
                new Process
                {
                    StartInfo = new ProcessStartInfo(
                        jar.FullName,
                        proccess_jar_args

                    )
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true,

                        WorkingDirectory = location,

                        //RedirectStandardOutput = true,
                    }
                };




            proccess_jar.Start();
            proccess_jar.WaitForExit();

            if (proccess_jar.ExitCode != 0)
                throw new ArgumentOutOfRangeException();
        }
    }
}
