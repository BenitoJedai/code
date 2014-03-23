using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Obsolete("how many times have we implemented this?")]
    public static class FileExtensions
    {
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\DOM\FileEntryAsyncExtensions.cs

        public static IEnumerable<File> AsEnumerable(this FileList f)
        {
            return Enumerable.Range(0, (int)f.length).Select(k => f[(uint)k]);
        }

    }
}
