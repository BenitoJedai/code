using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/JSON/stringify

    [Script(HasNoPrototype = true, ExternalTarget = "JSON")]
    // static class?
    public class JSON
    {
        // http://dev.w3.org/2006/webapi/FileAPI/#url

        [Script(ExternalTarget = "JSON.stringify")]
        public static string stringify(object e)
        {
            return default(string);
        }

        [Script(ExternalTarget = "JSON.parse")]
        public static object parse(string e)
        {
            return default(object);
        }
    }
}
