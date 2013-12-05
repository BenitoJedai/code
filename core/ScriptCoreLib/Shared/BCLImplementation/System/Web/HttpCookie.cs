using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpCookie))]
    internal class __HttpCookie
    {
        public __HttpCookie()
            : this(null, null)
        {

        }

        public __HttpCookie(string name)
            : this(name, null)
        {
        }

        public __HttpCookie(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }

        public DateTime Expires { get; set; }


        public string InternalValue;
        public NameValueCollection InternalValues;

        public string Value
        {
            get
            {
                if (InternalValues != null)
                {
                    var w = new StringBuilder();
                    // X:\jsc.svn\examples\javascript\Test\TestServiceNullStringField\TestServiceNullStringField\ApplicationWebService.cs

                    foreach (var item in this.InternalValues.AllKeys)
                    {
                        w
                            .Append(item)
                            .Append("=")
                            .Append(this.InternalValues[item])
                            .Append("&");
                    }

                    return w.ToString();
                }

                return InternalValue;
            }
            set
            {
                InternalValue = value;
            }
        }

        public string this[string key]
        {
            set
            {
                if (InternalValues == null)
                    InternalValues = new NameValueCollection();

                InternalValues[key] = value;
            }
        }


        //        Implementation not found for type import :
        //type: System.Web.HttpCookie
        //method: Void set_Item(System.String, System.String)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!

    }
}
