﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.DOM
{
    public static class FileEntryAsyncExtensions
    {

        public static Task<File> file(this FileEntry f)
        {
            var y = new TaskCompletionSource<File>();

            f.file(
                new Action<File>(
                    ff =>
                    {
                        y.SetResult(ff);
                    }
                )
            );

            return y.Task;
        }

        public static Task<string> readAsText(this File f)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeCSVFileHandler\ChromeCSVFileHandler\Application.cs

            var y = new TaskCompletionSource<string>();

            var x = new FileReader();
            Console.WriteLine("readAsText FileReader");
            x.onload =
                new Action(
                    delegate
                    {
                        Console.WriteLine("readAsText FileReader onload");
                        y.SetResult((string)x.result);
                    }
                );


            x.readAsText(f, "UTF-8");
            Console.WriteLine("readAsText FileReader readAsText");

            return y.Task;
        }
    }
}
