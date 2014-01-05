using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "URL")]
    // static class?
    public class URL
    {
        // http://dev.w3.org/2006/webapi/FileAPI/#url

        [Script(ExternalTarget = "URL.createObjectURL")]
        public static string createObjectURL(Blob blob)
        {
            return default(string);
        }
    }
}
