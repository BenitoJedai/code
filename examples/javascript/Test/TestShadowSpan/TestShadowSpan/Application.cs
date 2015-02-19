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
using TestShadowSpan;
using TestShadowSpan.Design;
using TestShadowSpan.HTML.Pages;

namespace TestShadowSpan
{
    // what would it mean? rename, custom element?
    // does jsc allow it yet? 
    //public class span() : IHTMLSpan() { }

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
            #region hr
            Action hr = delegate
            {
                //new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();
                new IHTMLHorizontalRule().AttachToDocument();
            };
            #endregion


            {
                var x = new IHTMLSpan { "hello world" }.AttachToDocument();
            }
            hr();

            {
                var x = new IHTMLSpan { "hello world" }.AttachToDocument();


                x.css.before.contentText = "look we are using ::before content string";
            }
            hr();

            {
                var x = new IHTMLSpan { "hello world" }.AttachToDocument();

                //x.css.before.contentText = "look we are using ::before content string";
                //x.createShadowRoot(

                x.shadow.appendChild("this is a shadow fragment");

            }
            hr();

            {
                var x = new IHTMLSpan { "hello world" }.AttachToDocument();

                // is it visible if there is shadow defined?
                x.css.before.contentText = "[::before content string with shadow] ";

                //x.createShadowRoot(

                x.shadow.appendChild("this is a shadow fragment");


            }
            hr();



            {
                var x = new IHTMLSpan { "hello world" }.AttachToDocument();

                // is it visible if there is shadow defined?
                x.css.before.contentText = "[::before content string with shadow] ";

                //x.createShadowRoot(


                // what about multiple shadows?
                x.shadow.appendChild("this is a shadow fragment with content: ");

                // can we style the content ? no
                new IHTMLContent { }.AttachTo(x.shadow).style.border = "1px solid red";

            }
            hr();

        }

    }
}
