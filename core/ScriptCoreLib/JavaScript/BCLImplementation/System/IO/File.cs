using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.IO
{
    // http://referencesource.microsoft.com/#mscorlib/system/io/file.cs

    [Script(Implements = typeof(global::System.IO.File))]
    public static class __File
    {
        

        public static IEnumerable<ScriptCoreLib.JavaScript.DOM.File> InternalFiles;


        public static bool Exists(string path)
        {
            // what files do we know about?
            if (InternalFiles != null)
                return InternalFiles.Any(k => k.name == path);

            return false;
        }

        public static string ReadAllText(string path)
        {
            var f = InternalFiles.FirstOrDefault(k => k.name == path);

            if (f != null)
            {
                // https://code.google.com/p/chromium/issues/detail?id=48176
                // FileReaderSync has already been implemented. Please note that it could only be constructed from the web workers per the File API spec.

                var value = new FileReaderSync().readAsText(f, null);
                return value;
            }

            return null;
        }

    }
}