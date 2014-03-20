using AppEngineUserAgentLogger.HTML.Pages;
using AppEngineUserAgentLogger.Schema;
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


        //Error	3	Assembly 'AppEngineUserAgentLogger.AssetsLibrary, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' uses 'System.Data.SQLite, Version=1.0.89.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' which has a higher version than referenced assembly 'System.Data.SQLite, Version=1.0.60.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139'	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLogger\AppEngineUserAgentLogger\bin\staging.AssetsLibrary\AppEngineUserAgentLogger.AssetsLibrary.dll	AppEngineUserAgentLogger


        public void SetScreenSize(int width, int height)
        {
            //Console.WriteLine("Page size " + width + "x" + height);
            //Console.WriteLine(h.Context.Request.UserAgent);
            new FirstTable().Insert(
              new FirstTableQueries.InsertMeta
                {
                    width = width,
                    height = height,
                    ip = h.Context.Request.UserHostAddress,
                    useragent = h.Context.Request.UserAgent
                }
          );
        }

        //public Task<DataTable> GoNextPage()
        public async Task GoNextPage()
        {
            //Console.WriteLine(NextPageSource.Text);
            var x = XElement.Parse(
                NextPageSource.Text
            );

            x.Element("body").Element("h4").Value = "This text was set by the server.";


            

            Action<string> WriteLine = t => 
            {
                var s = new XElement("div");
                s.Value = t;
                x.Element("body").Element("output").Add(s);
            };

            new FirstTable().SelectAll(
                 xx =>
                 {

                     long
                         width = xx.width,
                         heigth = xx.height;


                     string
                         ip = xx.ip,
                         useragent = xx.useragent;

                     WriteLine(new { width, heigth, ip, useragent }.ToString());
                     //Console.WriteLine(new { width, heigth, ip, useragent });

                 });

            body.ReplaceAttributes(x.Attributes());
            body.ReplaceNodes(x.Nodes());

            //var c = new TaskCompletionSource<DataTable>();
            //c.SetResult(data);
            //return c.Task;

            //return data.ToTaskResult();
        }

    }
}
