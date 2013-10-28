using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NextPageFromWebService;
using NextPageFromWebService.Design;
using NextPageFromWebService.HTML.Pages;
using System.Threading;
using System.Data;
using System.Windows.Forms;

namespace NextPageFromWebService
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // we are not yet merging correctly.
            // script would be reloaded after await
            Native.document.body.querySelectorAll("script").WithEach(
                x => x.Orphanize()
            );

            this.body = Native.document.body;


            var body_a = this.body.Attributes().ToArray();
            var body_n = this.body.Nodes().ToArray();

            page.GoNextPage.WhenClicked(
                async delegate
                {
                    var DataSource = await this.GoNextPage();
                    // by now the layout was changed!
                    var xpage = new TheNextPage.FromDocument();




                    xpage.FooButton.style.color = "red";

                    // um what if we want to go back?

                    xpage.FooButton.onclick +=
                        delegate
                        {
                            // script: error JSC1000: No implementation found for this native method, please implement [System.Xml.Linq.XElement.ReplaceAttributes(System.Object[])]
                            //this.body.ReplaceAttributes(body_a);
                            // script: error JSC1000: No implementation found for this native method, please implement [System.Xml.Linq.XContainer.ReplaceNodes(System.Object[])]
                            //this.body.ReplaceNodes(body_n);

                            this.body.RemoveNodes();

                            body_n.WithEach(x => this.body.Add(x));
                        };

                    var grid = new DataGridView { DataSource = DataSource };

                    grid.AttachControlTo(xpage.data);
                }
            );
        }

    }

    public partial class ApplicationWebService
    {
        public XElement body;


        // this wont work and android yet.
        // are we missing cookies?

        //        #12 POST /xml?WebMethod=06000001 HTTP/1.1 error:
        //#12 java.lang.RuntimeException
        //#12 java.lang.RuntimeException
        //       at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XDocument.Parse(__XDocument.java:59)
        //       at ScriptCoreLibJava.BCLImplementation.System.Xml.Linq.__XElement.Parse(__XElement.java:200)
        //       at ScriptCoreLib.Library.StringConversions.ConvertStringToXElement(StringConversions.java:150)
        //       at NextPageFromWebService.Global.Invoke(Global.java:185)
        //       at ScriptCoreLib.Ultra.WebService.InternalGlobalExtensions.InternalApplication_BeginRequest(InternalGlobalExtensions.java:350)
        //       at NextPageFromWebService.Global.Application_BeginRequest(Global.java:39)


        public Task<DataTable> GoNextPage()
        {
            // slow down
            Thread.Sleep(500);

            var x = XElement.Parse(
                TheNextPageSource.Text
            );

            x.Element("body").Element("h4").Value = "This text was set by the server.";


            body.ReplaceAttributes(x.Attributes());
            body.ReplaceNodes(x.Nodes());

            var data = foo.GetDataTable();


            var c = new TaskCompletionSource<DataTable>();
            c.SetResult(data);
            return c.Task;

            //return data.ToTaskResult();
        }
    }
}
