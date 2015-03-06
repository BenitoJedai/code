using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/filesystem/FileEntry.idl

    // .WebFileSystem
    // .IOFileSystem ?
    [Script(HasNoPrototype = true, ExternalTarget = "")]
    public class FileEntry : Entry
    {
        // 35	    void file(FileCallback successCallback, optional ErrorCallback errorCallback);
        // hide it?
        public void file(IFunction successCallback)
        {
        }

        // jsc nowadays unwraps CLR delegates to IFunctions
        public void file(Action<File> successCallback)
        {
            // tested by?

        }

    }
}
