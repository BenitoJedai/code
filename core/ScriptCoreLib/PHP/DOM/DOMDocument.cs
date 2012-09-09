using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.DOM
{
    // http://www.php.net/manual/en/class.domdocument.php
    [Script(IsNative = true)]
    public class DOMDocument : DOMNode
    {
        public DOMDocument(string version = "1.0", string encoding = "utf-8")
        {

        }

        public readonly DOMElement documentElement;

        public DOMElement createElement(string name)
        {
            return default(DOMElement);
        }

        public DOMText createTextNode(string content)
        {
            return default(DOMText);
        }

        public string saveXML()
        {
            return default(string);
        }

        public string saveXML(DOMNode node)
        {
            return default(string);
        }

        public bool loadXML(string source)
        {
            return default(bool);
        }

        public DOMNode importNode(DOMNode importedNode, bool deep)
        {
            return default(DOMNode);
        }
    }
}
