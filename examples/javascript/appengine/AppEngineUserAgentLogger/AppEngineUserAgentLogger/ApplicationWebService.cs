using AppEngineUserAgentLogger.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppEngineUserAgentLogger
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        [EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(
                               DesignerSerializationVisibility.Hidden)]
        [Obsolete("experimental")]
        public WebServiceHandler h { set; get; }

        public XElement body;

        public void SetScreenSize(int width, int height)
        {
            Console.WriteLine("Page size " + width + "x" + height);
            Console.WriteLine(h.Context.Request.UserAgent);
        }

        //public Task<DataTable> GoNextPage()
        public async Task GoNextPage()
        {
            Console.WriteLine(NextPageSource.Text);
            var x = XElement.Parse(
                NextPageSource.Text
            );

            x.Element("body").Element("h4").Value = "This text was set by the server.";


            body.ReplaceAttributes(x.Attributes());
            body.ReplaceNodes(x.Nodes());


            //var c = new TaskCompletionSource<DataTable>();
            //c.SetResult(data);
            //return c.Task;

            //return data.ToTaskResult();
        }

    }
}
