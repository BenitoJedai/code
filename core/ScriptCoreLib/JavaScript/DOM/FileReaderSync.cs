using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "FileReaderSync")]
    public class FileReaderSync
    {
        public FileReaderSync() { }

        //public ArrayBuffer readAsArrayBuffer(Blob blob);
        public string readAsBinaryString(Blob blob) { throw null; }
        public string readAsDataURL(Blob blob) { throw null; }
        public string readAsText(Blob blob, string encoding) { throw null; }
    }
}
