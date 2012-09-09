using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Xml.Linq
{
    [Script(Implements = typeof(XAttribute))]
    internal class __XAttribute : __XObject
    {
        internal __XContainer InternalParentElement;

        public XName Name { get; set; }

        public string InternalValue;

        public string Value
        {
            get
            {
                if (this.InternalParentElement == null)
                    return InternalValue;

                var x = this.InternalParentElement.InternalElement;

                return x["@" + this.Name.LocalName].ToString();
            }
            set
            {
                var x = this.InternalParentElement.InternalElement;

                // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/operators.html#attribute_identifier

                x["@" + this.Name.LocalName] = value;
            }
        }

        public __XAttribute(XName name, object value)
        {
            this.Name = name;
            this.InternalValue = value as string;

        }

        public static implicit operator XAttribute(__XAttribute a)
        {
            return (XAttribute)(object)a;
        }
    }
}
