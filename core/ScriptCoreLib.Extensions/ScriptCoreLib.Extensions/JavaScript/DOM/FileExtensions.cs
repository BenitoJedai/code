using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Obsolete("how many times have we implemented this?")]
    public static class FileExtensions
    {
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\DOM\FileEntryAsyncExtensions.cs
        // X:\jsc.svn\examples\javascript\io\DropFileIntoSQLite\DropFileIntoSQLite\Application.cs

        public static IEnumerable<File> AsEnumerable(this FileList f)
        {
            return Enumerable.Range(0, (int)f.length).Select(k => f[(uint)k]);
        }

        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\DOM\FileEntryAsyncExtensions.cs

        public static Task<string> getAsString(this DataTransferItem x)
        {
            var y = new TaskCompletionSource<string>();


            x.getAsString(
                new Action<string>(
                    value =>
                    {
                        y.SetResult(value);
                    }
                )
            );

            return y.Task;
        }

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

        public static Task<byte[]> readAsBytes(this File f)
        {
            // X:\jsc.svn\examples\javascript\io\WebApplicationSelectingFile\WebApplicationSelectingFile\Application.cs
            // X:\jsc.svn\examples\javascript\io\DropFileForMD5Experiment\DropFileForMD5Experiment\Application.cs



            var y = new TaskCompletionSource<byte[]>();

            var x = new FileReader();
            //Console.WriteLine("readAsText FileReader");
            x.onload =
                new Action(
                    delegate
                    {
                        var a = (ArrayBuffer)x.result;

                        var u8c = new Uint8ClampedArray(array: a);

                        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\WebClient.cs

                        y.SetResult((byte[])u8c);
                    }
                );


            x.readAsArrayBuffer(f);
            //Console.WriteLine("readAsText FileReader readAsText");

            return y.Task;
        }

        public static Task<string> readAsText(this File f)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeCSVFileHandler\ChromeCSVFileHandler\Application.cs

            var y = new TaskCompletionSource<string>();

            var x = new FileReader();
            //Console.WriteLine("readAsText FileReader");
            x.onload =
                new Action(
                    delegate
                    {
                        //Console.WriteLine("readAsText FileReader onload");
                        y.SetResult((string)x.result);
                    }
                );


            x.readAsText(f, "UTF-8");
            //Console.WriteLine("readAsText FileReader readAsText");

            return y.Task;
        }

    }
}
