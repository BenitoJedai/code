using TrafficLight;
using TrafficLight.Design;
using TrafficLight.HTML.Pages;
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

namespace TrafficLight
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
            @"Hello world".ToDocumentTitle();

            foreach (Control c in content.Controls)
            {
                if (c is Panel)
                {
                    c.GetHTMLTarget().style.borderBottomLeftRadius = "15px";
                    c.GetHTMLTarget().style.borderBottomRightRadius = "15px";
                    c.GetHTMLTarget().style.borderTopLeftRadius = "15px";
                    c.GetHTMLTarget().style.borderTopRightRadius = "15px";
                    foreach (Control f in c.Controls)
                    {
                        if (f is Panel)
                        {
                            f.GetHTMLTarget().style.borderBottomLeftRadius = "30px";
                            f.GetHTMLTarget().style.borderBottomRightRadius = "30px";
                            f.GetHTMLTarget().style.borderTopLeftRadius = "30px";
                            f.GetHTMLTarget().style.borderTopRightRadius = "30px";

                        }
                    }
                }

            }
        }

    }
}
