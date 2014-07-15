using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/filesystem/DataTransferItemFileSystem.idl

    [Script(HasNoPrototype = true)]
    public class DataTransferItem
    {
        // tested by
        // X:\jsc.svn\examples\javascript\WebGL\WebGLGoldDropletTransactions\WebGLGoldDropletTransactions\Application.cs

        public string type;
    }
}
