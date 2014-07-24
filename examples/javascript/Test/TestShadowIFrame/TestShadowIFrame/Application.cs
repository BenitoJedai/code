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
using TestShadowIFrame;
using TestShadowIFrame.Design;
using TestShadowIFrame.HTML.Pages;

namespace TestShadowIFrame
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
            // what about namespaces?
            // what about 3D objects?
            // what about nugets?
            // what about canvas?
            // what about threejs ? will it end up like XAML?


            // shall we automatically define custom elements?
            Native.document.registerElement("example-com",
                e =>
            {
                //new XAttribute().Changed

                // http://msdn.microsoft.com/en-us/library/bb387098(v=vs.110).aspx
                // http://msdn.microsoft.com/en-us/library/system.xml.linq.xobject.changed(v=vs.110).aspx
                e.AsXElement().Changed +=
                    (sender, args) =>
                    {
                        // MutationObserver

                        // server side events too?
                    };

                var s = e.createShadowRoot();

                //var i = new IHTMLIFrame { src = "http://example.com" };
                var i = new IHTMLIFrame
                {
                    // prevent cyclic reload
                    src = "about:blank"

                    //src = "http://example.com"
                };

                s.appendChild(
                    i
                );




                e.attributes.WithEach(
                    a =>
                    {
                        //i.src = a.value;

                        new IHTMLPre { "attribute " + new { a.name, a.value } }.AttachToDocument();

                        if (a.name == "foo")
                        {
                            // are we observing that too?
                            i.src = a.value;
                        }
                    }
                );

                // does it mean we are nowready to get events for XLinq?
                // is it suppsoed to work?
                new MutationObserver(

                    new MutationCallback(
                    (mutations, o) =>
                {
                    foreach (var item in mutations)
                    {
                        var value = e.getAttributeNode(item.attributeName).value;

                        //i.src = ;

                        new IHTMLPre { "MutationObserver " + new { item.attributeName, value, item.type, item.target } }.AttachToDocument();


                        if (item.attributeName == "foo")
                        {
                            // are we observing that too?
                            i.src = value;
                        }
                    }
                }

                        )
                ).observe(e, new { attributes = true });

            },


                // http://www.w3.org/TR/2014/WD-dom-20140710/
                // https://code.google.com/p/mutation-summary/wiki/DOMMutationObservers
                // http://stackoverflow.com/questions/5416822/dom-mutation-events-replacement
                attributeChangedCallback:
                    (attributeName, e) =>
                    {
                        // css conditionals


                        new IHTMLPre { new { attributeName
                            }
                        }.AttachToDocument();
                    }
            );

            var z = new IHTMLElement("example-com")
            {
                //$foo = "bar"
                //new XAttribute("foo", "bar")
            }.AttachToDocument();

            z.setAttribute("foo", "http://example.com");


        }

    }
}
