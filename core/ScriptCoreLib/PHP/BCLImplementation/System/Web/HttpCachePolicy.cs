using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Web
{
    // see also: Y:\jsc.svn\core\ScriptCoreLibJava.Web\ScriptCoreLibJava.Web\BCLImplementation\System\Web\HttpCachePolicy.cs

    [Script(Implements = typeof(global::System.Web.HttpCachePolicy))]
    internal class __HttpCachePolicy
    {
        public __HttpResponse InternalResponse;

        public void SetCacheability(HttpCacheability cacheability)
        {
            // http://www.mnot.net/blog/2007/05/15/expires_max-age
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/201209/20120925-varnish


            this.InternalResponse.AddHeader("Cache-Control", "max-age=3600");

        }

        public void SetExpires(DateTime date)
        {

            //this.InternalResponse.AddHeader("Expires", "Wed, 19 Sep 2022 08:37:33 GMT");
        }
    }
}
