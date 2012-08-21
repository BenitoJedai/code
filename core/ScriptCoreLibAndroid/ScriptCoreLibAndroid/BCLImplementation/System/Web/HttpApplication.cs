﻿using ScriptCoreLibJava.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpApplication))]
    public class __HttpApplication : __IHttpAsyncHandler, __IHttpHandler, __IComponent, IDisposable
    {
        public HttpRequest Request { get; set; }
        public HttpResponse Response { get; set; }

        HttpContext _Context;
        public HttpContext Context
        {
            get
            {
                if (_Context == null)
                    _Context = (HttpContext)(object)new __HttpContext { Request = this.Request, Response = this.Response };

                return _Context;
            }
        }

        public void CompleteRequest()
        {

        }

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(global::System.Web.HttpContext context)
        {
            throw new NotImplementedException();
        }

        public event EventHandler Disposed;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
