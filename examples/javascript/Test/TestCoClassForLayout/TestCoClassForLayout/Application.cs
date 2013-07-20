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
using TestCoClassForLayout.Design;
using TestCoClassForLayout.HTML.Pages;

namespace TestCoClassForLayout
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        //interface IX : ISpecialLayout, INodeConvertible<IHTMLDiv>
        //{ 
        //}

        //class X : SpecialLayout, IX
        //{
        //    IHTMLDiv INodeConvertible<IHTMLDiv>.InternalAsNode()
        //    {
        //        return this.Container;
        //    }
        //}

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // Error	1	The type 'TestCoClassForLayout.Application.X' cannot be used as type parameter 'T' in the generic type or method 'ScriptCoreLib.JavaScript.Extensions.Extensions.AttachToDocument<T>(T)'. There is no implicit reference conversion from 'TestCoClassForLayout.Application.X' to 'ScriptCoreLib.JavaScript.DOM.INode'.	X:\jsc.svn\examples\javascript\Test\TestCoClassForLayout\TestCoClassForLayout\Application.cs	42	13	TestCoClassForLayout

            //page.AsNode().

            //IConvertToOuterHTMLElementExtensions.AttachToDocument(x);


            //new ISpecialLayout { info = "more info 2" }.ThisShouldBeAutomaticAndShouldNotNeedBody.AttachTo(page.Output);
            new ISpecialLayout { info = "more info 2" }.AttachTo(page.Output).With(
                x =>
                {
                    x.HideThis.onclick +=
                    delegate
                    {
                        // public static T Orphanize<T>(this T e) where T : INodeConvertible<INode>;
                        x.Orphanize();
                    };
                }
            );


            {
                var x = new ISpecialLayout { info = "more info 1" };

                // public static T AttachToDocument<T>(this T e) where T : INode;
                // public static T AttachToDocument<T>(this T e) where T : INodeConvertible<INode>;
                //x.AttachToDocument();
                page.Output.appendChild(x);

                x.HideThis.onclick +=
                    delegate
                    {
                        // public static T Orphanize<T>(this T e) where T : INodeConvertible<INode>;
                        x.Orphanize();
                    };

            }

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
