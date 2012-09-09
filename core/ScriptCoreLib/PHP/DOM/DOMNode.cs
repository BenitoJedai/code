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
        public readonly string nodeName;
        public readonly int nodeType;
        public string nodeValue;

        public readonly DOMDocument ownerDocument;


        public DOMNode appendChild(DOMNode newnode)
        {
            return newnode;
        }

        public readonly DOMNamedNodeMap attributes;

        public readonly DOMNodeList childNodes;
    }
}
