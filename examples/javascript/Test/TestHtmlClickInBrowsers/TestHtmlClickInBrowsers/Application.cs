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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using TestHtmlClickInBrowsers;
using TestHtmlClickInBrowsers.Design;
using TestHtmlClickInBrowsers.HTML.Pages;

namespace TestHtmlClickInBrowsers
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
            page.Butt.onclick += delegate
            {
                Native.window.alert("page.butt.onclick");
            };
            page.A.onclick += delegate
            {
                Native.window.alert("page.A.onclick");
            };
            page.div.onclick += delegate
            {
                Native.window.alert("page.div.onclick");
            };

            page.Butt2.onclick += delegate
            {
                Native.window.alert("page.Butt2.onclick");
            };


            new IFunction("e", @"

                        // # First create an event
                        var click_ev = document.createEvent('MouseEvent');
                        // # initialize the event
                        click_ev.initEvent('click', true /* bubble */, true /* cancelable */);
                        // # trigger the evevnt
                        this.dispatchEvent(click_ev);

                        ").apply(page.Butt);

            new IFunction("e", @"

                        // # First create an event
                        var click_ev = document.createEvent('MouseEvent');
                        // # initialize the event
                        click_ev.initEvent('click', true /* bubble */, true /* cancelable */);
                        // # trigger the evevnt
                        this.dispatchEvent(click_ev);

                        ").apply(page.A);

            new IFunction("e", @"

                        // # First create an event
                        var click_ev = document.createEvent('MouseEvent');
                        // # initialize the event
                        click_ev.initEvent('click', true /* bubble */, true /* cancelable */);
                        // # trigger the evevnt
                        this.dispatchEvent(click_ev);

                        ").apply(page.div);

            page.Butt2.click();

            

        }

    }
}
