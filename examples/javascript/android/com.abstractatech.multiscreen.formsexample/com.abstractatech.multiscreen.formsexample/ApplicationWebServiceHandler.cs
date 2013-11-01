using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Data.SQLite;
using System.Linq;
using System.Xml.Linq;

namespace com.abstractatech.multiscreen.formsexample
{
    public sealed partial class ApplicationXWebService
    {
        public void Handler(WebServiceHandler h)
        {
            // device dedection
            //Console.WriteLine(h.Context.Request.UserAgent);
        }
    }
}
