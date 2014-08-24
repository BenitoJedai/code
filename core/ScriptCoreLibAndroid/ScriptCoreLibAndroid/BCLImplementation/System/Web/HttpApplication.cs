using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using java.io;
using System.IO;
using ScriptCoreLibJava.BCLImplementation.System.IO;


namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    // http://referencesource.microsoft.com/#System.Web/xsp/system/Web/HttpApplication.cs

    [Script(Implements = typeof(global::System.Web.HttpApplication))]
    public class __HttpApplication : __IHttpAsyncHandler, __IHttpHandler, __IComponent, IDisposable
    {
        // X:\jsc.svn\examples\java\android\ApplicationWebService\ApplicationWebService\ApplicationActivity.cs

        public HttpRequest Request { get; set; }
        public HttpResponse Response { get; set; }

        __HttpContext _Context;
        public HttpContext Context
        {
            get
            {
                if (_Context == null)
                    _Context = new __HttpContext { Request = this.Request, Response = this.Response };

                return (HttpContext)(object)_Context;
            }
        }

        public void CompleteRequest()
        {
            Response.Close();


            //InternalStream.Close();
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
            CompleteRequest();
        }

        public global::System.ComponentModel.ISite Site
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }



        public __HttpApplication()
        {
            // tested by X:\jsc.svn\examples\javascript\forms\android\LANClickOnce\LANClickOnce\Application.cs
            // tested by X:\jsc.svn\examples\javascript\android\DCIMCameraAppWithThumbnails\DCIMCameraAppWithThumbnails\ApplicationWebService.cs


            ScriptCoreLibJava.BCLImplementation.System.IO.__File.InternalOpenRead =
                path =>
                {
                    Console.WriteLine("InternalOpenRead " + new { path });

                    var x = default(__FileStream);
                    try
                    {
                        var Response = (__HttpResponse)(object)this._Context.Response;

                        // assets only?
                        var assets = Response.InternalContext.getResources().getAssets();

                        var s = assets.open(path).ToNetworkStream();

                        x = new __FileStream { InternalStream = s };
                    }
                    catch
                    {
                        // no file
                    }
                    return x;
                };


            ScriptCoreLibJava.BCLImplementation.System.IO.__File.InternalReadAllBytes =
                path =>
                {
                    // I/System.Console(17391): InternalReadAllBytes { path = ScriptCoreLib.dll.js }
                    Console.WriteLine("InternalReadAllBytes " + new { path });


                    var bytes = default(byte[]);


                    try
                    {
                        var Response = (__HttpResponse)(object)this._Context.Response;

                        // assets only?
                        var assets = Response.InternalContext.getResources().getAssets();

                        var s = assets.open(path).ToNetworkStream();

                        var value = new MemoryStream();
                        s.CopyTo(value);

                        bytes = value.ToArray();
                    }
                    catch
                    {
                        // no file
                    }

                    return bytes;
                };
        }
    }
}
