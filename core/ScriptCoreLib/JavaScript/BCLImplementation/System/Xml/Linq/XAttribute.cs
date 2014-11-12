using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Xml.Linq
{
    // https://github.com/dotnet/corefx/blob/master/src/System.Xml.XDocument/System/Xml/Linq/XAttribute.cs

    [Script(Implements = typeof(XAttribute))]
    internal class __XAttribute : __XObject
    {
        internal __XContainer InternalElement;

        public XName Name { get; set; }

        public string InternalValue;

      

        public void Remove()
        {
            // X:\jsc.svn\examples\javascript\css\Test\CSSSearchUserFeedback\CSSSearchUserFeedback\Application.cs

            this.InternalElement.InternalElement.removeAttribute(this.Name.LocalName);
        }

        public string Value
        {
            get
            {
                if (this.InternalElement == null)
                    return InternalValue;

                return (string)this.InternalElement.InternalElement.getAttribute(this.Name.LocalName);
            }
            set
            {
                this.InternalElement.InternalElement.setAttribute(this.Name.LocalName, value);
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

        public static implicit operator __XAttribute(XAttribute a)
        {
            return (__XAttribute)(object)a;
        }


        public static explicit operator bool(__XAttribute attribute)
        {
            return Convert.ToBoolean(attribute.Value);
        }

        public override string ToString()
        {
            // X:\jsc.svn\examples\javascript\CSS\CSSConditionalScroll\CSSConditionalScroll\Application.cs
            return this.Value;
        }
    }
}
