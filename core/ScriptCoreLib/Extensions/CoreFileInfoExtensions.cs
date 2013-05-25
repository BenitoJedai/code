using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class CoreFileInfoExtensions
    {
        // for override

        public static void ReadAllText(this FileInfo f, Action<string> yield)
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestDropFile\TestDropFile\ApplicationControl.cs

            // for CLR testing:
            yield(
                File.ReadAllText(
                    f.FullName
                )
            );
        }
    }
}
