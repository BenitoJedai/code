using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.DOM
{
    // http://www.php.net/manual/en/class.domnode.php
    [Script(IsNative = true)]
    public class DOMNode
    {
        public string nodeValue;

        public DOMNode appendChild(DOMNode newnode)
        {
            return newnode;
        }
    }
}
