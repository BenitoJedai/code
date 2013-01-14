using ScriptCoreLib.Shared.BCLImplementation.System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpCookieCollection))]
    internal class __HttpCookieCollection : __NameObjectCollectionBase
    {
        List<HttpCookie> InternalItems = new List<HttpCookie>();

        public void Add(HttpCookie c)
        {
            InternalItems.Add(c);
        }

        public HttpCookie this[string name]
        {
            get
            {
                var y = InternalItems.FirstOrDefault(x => x.Name == name);
 
                return y;
            }
        }
    }
}
