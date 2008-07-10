using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.XML.XLinq
{
    using AS3_QName = global::ScriptCoreLib.ActionScript.QName;
    using AS3_XML = global::ScriptCoreLib.ActionScript.XML;
    using AS3_XMLList = global::ScriptCoreLib.ActionScript.XMLList;


    [Script(Implements = typeof(XElement))]
    internal class __XElement : __XContainer
    {
        __XName _Name;

        public __XName Name
        {
            get
            {
                if (_Name == null)
                {
                    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/XML.html#name()
                    _Name = new __XName { InternalValue = (AS3_QName)this.InternalValue.name() };
                }

                return _Name;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public __XElement()
            : this(null)
        {
        }

        public __XElement(XName name)
        {
            // implement
        }
    }
}
