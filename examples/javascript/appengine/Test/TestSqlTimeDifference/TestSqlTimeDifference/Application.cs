using TestSqlTimeDifference;
using TestSqlTimeDifference.Design;
using TestSqlTimeDifference.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TestSqlTimeDifference
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            content.AttachControlToDocument();

            content.button1.Click += async delegate
            {
                Console.WriteLine("Click");
                await this.InsertNewRow("s");
                var res = await this.getLastEntry();


                Console.WriteLine(res.Timestamp.ToString());
                var now = DateTime.Now;
                Console.WriteLine(now.ToString());
                content.sTime.Text = res.Timestamp.ToString();

                content.lTime.Text = now.ToString();
            };
        }

    }
}
