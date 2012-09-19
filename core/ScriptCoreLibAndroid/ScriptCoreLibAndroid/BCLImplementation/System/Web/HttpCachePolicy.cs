﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    // see also: Y:\jsc.svn\core\ScriptCoreLib\PHP\BCLImplementation\System\Web\HttpCachePolicy.cs

    [Script(Implements = typeof(global::System.Web.HttpCachePolicy))]
    internal class __HttpCachePolicy
    {
        public __HttpResponse InternalResponse;


        public void SetCacheability(HttpCacheability cacheability)
        {
            this.InternalResponse.AddHeader("Cache-Control", "public");

        }

        public void SetExpires(DateTime date)
        {
            this.InternalResponse.AddHeader("Expires", "Wed, 19 Sep 2022 08:37:33 GMT");

        }
    }
}
