
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    // http://referencesource.microsoft.com/#System.Web/xsp/system/Web/HttpCachePolicy.cs
    // see also: Y:\jsc.svn\core\ScriptCoreLib\PHP\BCLImplementation\System\Web\HttpCachePolicy.cs

    [Script(Implements = typeof(global::System.Web.HttpCachePolicy))]
    public class __HttpCachePolicy
    {
        public __HttpResponse InternalResponse;


        public void SetCacheability(HttpCacheability cacheability)
        {
            // Cache-Control:private

            if (cacheability == HttpCacheability.Private)
            {
                this.InternalResponse.AddHeader("Cache-Control", "private");
                return;
            }
            this.InternalResponse.AddHeader("Cache-Control", "public");

        }

        public void SetExpires(DateTime date)
        {
            this.InternalResponse.AddHeader("Expires", "Wed, 19 Sep 2022 08:37:33 GMT");

        }
    }
}
