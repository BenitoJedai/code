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
using TestShadowDOM;
using TestShadowDOM.Design;
using TestShadowDOM.HTML.Pages;

namespace TestShadowDOM
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
            // http://www.w3.org/TR/shadow-dom/
            // looks like android html and xaml will merge.


            // http://blog.teamtreehouse.com/working-with-shadow-dom
            // http://glazkov.com/2011/01/14/what-the-heck-is-shadow-dom/

            //var shadow = page.PageContainer.createShadowRoot();

            // http://thejackalofjavascript.com/web-components-future-web/

            //new IHTMLPre { "!!!" }.AttachTo(shadow);


            // this feels like XSLT??


            // http://www.html5rocks.com/en/tutorials/webcomponents/customelements/

            dynamic prototype = new IFunction("return Object.create(HTMLElement.prototype)").apply(null);


            // first attempt to allow <element> inheritace ctor?
            prototype.createdCallback =
                 new IFunction("y", "return function () { y(this); };").apply(null,
                     (IFunction)new Action<IHTMLElement>(
                         e =>
                {
                    var shadow = e.createShadowRoot();

                    shadow.appendChild("???");
                    shadow.appendChild(new IHTMLPre { "!!!" });
                }
                     )
                 );



            // http://html5-demos.appspot.com/shadowdom-visualizer
            // http://component.kitchen/components/CustomElements
            var __foo = Native.document.registerElement("foo-bar",
                new { prototype = (object)prototype }
            );

            // https://coderwall.com/p/h8qrfa

            // https://code.google.com/p/chromium/issues/detail?id=383749

            // http://www.html5rocks.com/en/tutorials/webcomponents/customelements/



            new IHTMLElement("foo-bar").AttachToDocument();
            new IHTMLElement("foo-bar").AttachToDocument();


            // Show Details	Severity	Code	Description	Project	File	Line
            //Error CS1660  Cannot convert lambda expression to type 'object' because it is not a delegate type TestShadowDOM   Application.cs  91

            // public IFunction registerElement<TElement>(string name, Action<TElement> createdCallback, Action<string, TElement> attributeChangedCallback = null) where TElement : IHTMLElement;

            Native.document.registerElement(
                name: "z-z",

                createdCallback:
                //async 
                (IHTMLElement e) =>
                {
                    e.innerText = "z-z";

                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/20150101/async
                    //await e.async.onmouseover;

                    //e.style.color = "blue";

                    //await e.async.onclick;

                    //e.style.color = "red";
                }
            );


            new IHTMLElement("z-z").AttachToDocument();


            // Error	1	Argument 2: cannot convert from 'ScriptCoreLib.JavaScript.DOM.ShadowRoot' to 'System.Windows.Forms.Control'	X:\jsc.svn\examples\javascript\Test\TestShadowDOM\TestShadowDOM\Application.cs	42	52	TestShadowDOM


            //((IHTMLHeader1)root.querySelectorAll("h1").First()).innerText = "shadoowwww";

            //new IHTMLPre { "in shadow?" }.AttachTo((IHTMLElement)root.getElementById("Header"));
            // http://jsfiddle.net/7y6kp/7/


        }

    }
}
