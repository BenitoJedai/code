using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestDetectOpenFiles
{
    public static class x
    {
        public static IEnumerable<FileInfo> GetOpenFiles(this Process p) =>
            from x in p.GetOpenFileSystemInfos()
            let y = x as FileInfo
            where y != null
            select y;


        public static IEnumerable<FileSystemInfo> GetOpenFileSystemInfos(this Process p)
        {

            var f = VmcController.Services.DetectOpenFiles.GetOpenFilesEnumerator(p.Id);


            while (f.MoveNext())
            {
                var c = f.Current;

                yield return c;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // http://msdn.microsoft.com/en-us/library/bb432383%28v=vs.85%29.aspx
            // An optional pointer to a location where the function writes the actual size of the information requested. If that size is less than or equal to the ObjectInformationLength parameter, the function copies the information into the ObjectInformation buffer; otherwise, it returns an NTSTATUS error code and returns in ReturnLength the size of the buffer required to receive the requested information.

            //var p = Process.GetProcesses().FirstOrDefault(x => x.ProcessName == "vlc");
            var p = Process.GetProcesses().FirstOrDefault(x => x.ProcessName == "vlc");

        next:

            var mp4 = p.GetOpenFiles().ToArray().FirstOrDefault(f => f.Extension == ".mp4");

            // X:\opensource\github\taglib-sharp\src\TagLib\Mpeg4\AppleTag.cs
            // mp4 = {X:\media\Kryon - The Biology of Co Creation.mp4}


            Console.Title = mp4.Name;

            // done. wait for a new 
            //Console.WriteLine("awaiting for next");

        retry:
            var mp4next = p.GetOpenFiles().ToArray().FirstOrDefault(f => f.Extension == ".mp4");

            if (mp4next.FullName == mp4.FullName)
            {
                Console.Write(".");
                Thread.Sleep(1000);
                goto retry;
            }

            goto next;

            Debugger.Break();
        }
    }
}
