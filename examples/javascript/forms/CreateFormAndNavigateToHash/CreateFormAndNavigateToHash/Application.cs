using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CreateFormAndNavigateToHash.Design;
using CreateFormAndNavigateToHash.HTML.Pages;
using System.Windows.Forms;

namespace CreateFormAndNavigateToHash
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
            Native.document.title =
                new { Native.document.location.hash }.ToString();

            if (Native.document.location.hash.StartsWith("#"))
                CreateWindowAndNavigate(
                    Native.document.location.hash.Substring(1)
                );
        }


        public void CreateWindowAndNavigate(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;

            var f = new Form
            {

                Text = url
            };
            var c = new WebBrowser();
            c.Dock = DockStyle.Fill;
            c.Size = new System.Drawing.Size(800, 600);
            f.Controls.Add(c);
            f.ClientSize = new System.Drawing.Size(800, 600);
            c.Navigate(url);
            //c.Url = new Uri(this.comboBox1.Text);
            f.Show();
        }
    }
}
