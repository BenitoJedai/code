using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.DOM
{
    // http://www.php.net/manual/en/class.domelement.php
    [Script(IsNative = true)]
    public class DOMElement : DOMNode
    {
        readonly public string tagName;

        public string getAttribute(string name)
        {
            return default(string);
        }

        public DOMAttr setAttribute(string name, string value)
        {
            return default(DOMAttr);
        }


    }
}
