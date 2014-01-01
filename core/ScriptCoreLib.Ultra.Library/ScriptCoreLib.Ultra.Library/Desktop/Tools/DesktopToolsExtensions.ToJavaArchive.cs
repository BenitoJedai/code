using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ScriptCoreLib.Desktop.Tools
{
    public static partial class DesktopToolsExtensions
    {
        public static void ToJavaArchive(this DirectoryInfo source, string target, FileInfo jar)
        {
            if (!source.Exists)
            {
                throw new DirectoryNotFoundException(new { source }.ToString());
            }

            if (!Directory.Exists(Path.GetDirectoryName(target)))
            {
                throw new DirectoryNotFoundException(new { target }.ToString());
            }

            /*
                Usage: jar {ctxui}[vfm0Me] [jar-file] [manifest-file] [entry-point] [-C dir] files ...
                Options:
                    -c  create new archive
                    -t  list table of contents for archive
                    -x  extract named (or all) files from archive
                    -u  update existing archive
                    -v  generate verbose output on standard output
                    -f  specify archive file name
                    -m  include manifest information from specified manifest file
                    -e  specify application entry point for stand-alone application 
                        bundled into an executable jar file
                    -0  store only; use no ZIP compression
                    -M  do not create a manifest file for the entries
                    -i  generate index information for the specified jar files
                    -C  change to the specified directory and include the following file
                If any file is a directory then it is processed recursively.
                The manifest file name, the archive file name and the entry point name are
                specified in the same order as the 'm', 'f' and 'e' flags.

                Example 1: to archive two class files into an archive called classes.jar: 
                       jar cvf classes.jar Foo.class Bar.class 
                Example 2: use an existing manifest file 'mymanifest' and archive all the
                           files in the foo/ directory into 'classes.jar': 
                       jar cvfm classes.jar mymanifest -C foo/ .             
             */

            var proccess_jar_args =
                "cvfM"
                // foo.jar
                + " \"" + target + "\""
                // root of class files
                + " -C \"" + source.FullName + "\""
                // root in the jar
                + " .";

            Console.WriteLine();
            Console.WriteLine("\"" + jar.FullName + "\" " + proccess_jar_args);
            Console.WriteLine(source.FullName);

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

                        WorkingDirectory = source.FullName,

                        //RedirectStandardOutput = true,
                    }
                };

            if (File.Exists(target))
            {
                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201401/20140101

                while (true)
                    try
                    {
                        File.Delete(target);
                        break;
                    }
                    catch
                    {
                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131101
                        if (
                            MessageBox.Show(

                            "File in use, terminate the process using the file and try again!\n\n" + target,
                            "jsc", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1
                        ) == DialogResult.Retry
                            )
                            continue;

                        throw;
                    }
            }


            proccess_jar.Start();
            proccess_jar.WaitForExit();

            if (proccess_jar.ExitCode != 0)
            {
                // wtf
                //if (Debugger.IsAttached)
                //    Debugger.Break();

                //MessageBox.Show("eek");

                throw new ArgumentOutOfRangeException(new { proccess_jar.ExitCode }.ToString());
            }
        }
    }
}
