using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.IO;
using System.Linq;

namespace ZIPDecoderExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // X:\jsc.svn\examples\actionscript\io\ZipExample2\ZipExample2\web\assets\ZipExample2

            //{ p = 67131 }
            //{ start_of_central_directory = 65011 }
            //{ FileIndex = 0, Count = 1 }
            //CDFH_AddPosition { start_of_central_directory = 65011, Position = 65064, Count = 1, Number_of_central_directory_records_on_this_disk = 40 }
            //{ FileIndex = 1, Count = 2 }
            //CDFH_AddPosition { start_of_central_directory = 65011, Position = 65117, Count = 2, Number_of_central_directory_records_on_this_disk = 40 }
            //{ FileIndex = 2, Count = 3 }


            //var m = new MemoryStream(
            //    File.ReadAllBytes(
            //        @"X:\jsc.svn\examples\actionscript\io\ZipExample2\ZipExample2\web\assets\ZipExample2\dude5.zip"
            //    )
            //    );

            //Application.GetEntries(m).ToArray();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
