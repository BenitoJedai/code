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
    }
}
