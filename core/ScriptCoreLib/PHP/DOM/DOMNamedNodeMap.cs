using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.DOM
{
    // http://www.php.net/manual/en/class.domnamednodemap.php
    [Script(IsNative = true)]
    public class DOMNamedNodeMap : DOMNode
    {
        readonly public int length;
        public DOMNode item(int index)
        {
            return default(DOMNode);
        }
    }
}
