using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml.Linq;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
    [Script(Implements = typeof(global::System.Xml.Linq.XAttribute))]
    internal class __XAttribute : __XObject
    {
        public __XContainer InternalElement;

        public XName Name { get; set; }

        public string InternalValue;

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
                // http://msdn.microsoft.com/en-us/library/system.xml.linq.xattribute.value.aspx
                this.InternalElement.InternalElement.setAttribute(this.Name.LocalName,
                    __android_workaround(value)
                );
            }
        }

        static string __android_workaround(string e)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            if (null == e)
                return "";

            return e;
        }

        public __XAttribute()
        {
        }

        public __XAttribute(XName name, object value)
        {
            this.Name = name;

            // android does not like null value while jvm is ok with it..
            this.InternalValue = __android_workaround("" + value);

        }

        public static implicit operator XAttribute(__XAttribute a)
        {
            return (XAttribute)(object)a;
        }


        //__XContainer Add { content = ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XAttribute@41fc6738 }
        //__XContainer Add { Name = TableName, SourceAttributeValue = GetInterfaces }
        //__XContainer Add { content = <Columns/> }
        //__XContainer Add { content = ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XAttribute@42082620 }
        //__XContainer Add { Name = ReadOnly, SourceAttributeValue =  }
        //__XContainer Add { content = Name }



        public override string ToString()
        {
            // X:\jsc.svn\examples\java\android\AndroidServiceUDPNotification\AndroidServiceUDPNotification\ApplicationActivity.cs

            return Value;
            //return new { Name, Value }.ToString();
        }
    }
}
