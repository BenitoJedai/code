using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/FileReader.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/fileapi/FileReader.idl

    [Script(HasNoPrototype = true, ExternalTarget = "FileReader")]
    public class FileReader : IEventTarget
    {
        public readonly object result;

        public IFunction onload;

        public void readAsText(Blob blob, string encoding)
        { }



        public void readAsBinaryString(Blob blob)
        { }



        public void readAsArrayBuffer(Blob blob)
        {
        }


        // extended by
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\JavaScript\DOM\FileExtensions.cs
    }
}
