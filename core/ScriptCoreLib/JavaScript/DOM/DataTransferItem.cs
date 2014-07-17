using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // https://github.com/adobe/webkit/blob/master/Source/WebCore/dom/DataTransferItem.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/filesystem/DataTransferItemFileSystem.idl

    [Script(HasNoPrototype = true)]
    public class DataTransferItem
    {
        // tested by
        // X:\jsc.svn\examples\javascript\WebGL\WebGLGoldDropletTransactions\WebGLGoldDropletTransactions\Application.cs

        public string type;

        public void getAsString(Action<string> callback)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\JavaScript\DOM\FileExtensions.cs
        }
    }
}
