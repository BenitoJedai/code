using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/fileapi/FileReader.idl

    [Script(HasNoPrototype = true, ExternalTarget = "FileReader")]
    public class FileReader : IEventTarget
    {
        public readonly object result;

        public IFunction onload;

        public void readAsText(Blob blob, string encoding)
        { }
    }
}
