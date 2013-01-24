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
using MultiAppViaStaticClass.Design;
using MultiAppViaStaticClass.HTML.Pages;
using System.Reflection;
using System.ComponentModel;

namespace ScriptCoreLib.Shared
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class when : Attribute
    {
        public string host { get; set; }
        public string referer { get; set; }
        public string path { get; set; }

        //Error	1	'f' is not a valid named attribute argument because it is not a valid attribute parameter type	X:\jsc.svn\examples\javascript\MultiAppViaStaticClass\MultiAppViaStaticClass\Application.cs	37	48	MultiAppViaStaticClass
        public Type f;

        public when()
        {


        }

        public when(Type f)
        {
            this.f = f;
        }
    }
}

namespace MultiAppViaStaticClass
{
    using ScriptCoreLib.Shared;

    // activated via referer host



    [when(typeof(where))]
    static class idearemixer
    {
        // Error	1	Default parameter value for 'g' must be a compile-time constant	X:\jsc.svn\examples\javascript\MultiAppViaStaticClass\MultiAppViaStaticClass\Application.cs	55	88	MultiAppViaStaticClass
        delegate bool where(string referer = "idea-remixer.tumblr.com");


    }

    // activated via host

    [when(host = "abstractatech.com")]
    public static class abstractatech
    {

    }

    [when(host = "xavalon.net")]
    public static class xavalon
    {

    }

    [when(host = "jsc-solutions.net")]
    public static class jscsolutons
    {

    }



    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }



        // activated via /foo-pages/Zoo
        // activated via /foo-pages when only one app in this module


        [when(path = "/zoo")]
        public sealed class Zoo
        {
            public readonly ApplicationWebService service = new ApplicationWebService();

            /// <summary>
            /// This is a javascript application.
            /// </summary>
            /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
            public Zoo(IApp page)
            {
                @"Zoo".ToDocumentTitle();
                // Send data from JavaScript to the server tier
                service.WebMethod2(
                    @"A string from JavaScript.",
                    value => value.ToDocumentTitle()
                );
            }


            // will be the folder for sub apps
            static class pages
            {

            }
        }
    }

}
