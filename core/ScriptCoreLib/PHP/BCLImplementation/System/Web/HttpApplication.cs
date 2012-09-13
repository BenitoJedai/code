using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ScriptCoreLib.PHP.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Web
{
    [Script(Implements = typeof(global::System.Web.HttpApplication))]
    internal class __HttpApplication : __IHttpAsyncHandler, __IHttpHandler, __IComponent, IDisposable
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
            Native.API.flush();
            Native.API.exit();
        }

        #region __IHttpHandler Members

        public bool IsReusable
        {
            get { throw new NotImplementedException("NotImplementedException"); }
        }

        public void ProcessRequest(global::System.Web.HttpContext context)
        {
            throw new NotImplementedException("NotImplementedException");
        }

        #endregion

        #region __IComponent Members

        public event EventHandler Disposed;

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException("NotImplementedException");
        }

        #endregion


        public global::System.ComponentModel.ISite Site
        {
            get
           ;
            set
            ;
        }
    }
}
