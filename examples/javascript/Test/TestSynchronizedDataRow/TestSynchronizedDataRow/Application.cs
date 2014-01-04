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
using System.Xml.Linq;
using TestSynchronizedDataRow;
using TestSynchronizedDataRow.Design;
using TestSynchronizedDataRow.HTML.Pages;

namespace TestSynchronizedDataRow
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
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            this.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }

    enum Book1Sheet1Row : long {}

    class Book1Sheet1Row
    {
        public Book1Sheet1Row Key;
        
        // ? thead Synchronized
        // ? bind to localStorage
        // ? bind to webservice db
        // what about subtables that could behave as bound lists?
        // lists of lists
        // what about depencency properties
        // where we get to know when a value is changed
        // why and by whom
    }
}
