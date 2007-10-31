using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://www.w3.org/TR/DOM-Level-2-Core/core.html#ID-102161490
    [Script(HasNoPrototype = true)]
    public class IDOMImplementation
    {
        public bool hasFeature(string feature, string version)
        {
            return default(bool);
        }
    }
}
