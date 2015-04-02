using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Ultra.WebService
{
    /// <summary>
    /// This type is used to serve custom content from the web server.
    /// </summary>

    [Obsolete("experimental")]
    public class WebServiceHandler
    {
		// X:\jsc.svn\examples\javascript\xml\ServerSideContent\ServerSideContent\ApplicationWebService.cs


		// saved before Serve and for Invoke
		public InternalWebMethodInfo WebMethod;


        public HttpContext Context;

        public Action CompleteRequest;


        public Action Diagnostics;
        public Action Default;
        public Action Redirect;

        public WebServiceScriptApplication[] Applications;



        [Obsolete]
        public Func<InternalFileInfo[]> GetFiles;


        public bool IsDefaultPath
        {
            get
            {
                var e = this.Context.Request.Path;

                return InternalIsDefaultPath(e);
            }
        }

        internal static bool InternalIsDefaultPath(string e)
        {
            if (e == "/")
                return true;

            if (e == "/default.htm")
                return true;

            if (e == "/default.aspx")
                return true;

            return false;
        }


        public Action<WebServiceScriptApplication> WriteSource;




        //public static void 
    }
}
