using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.DOM
{
    // http://www.php.net/manual/en/class.domattr.php
    [Script(IsNative = true)]
    public class DOMAttr : DOMNode
    {
        readonly public string name;
        public string value;

    }
}
