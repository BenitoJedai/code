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

namespace MultiAppViaStaticClass
{
    // activated via referer host
    static class referer
    {
        [Description("idea-remixer.tumblr.com")]
        static class idearemixer
        {

        }
    }

    // activated via host
    static class domains
    {
        [Description("abstractatech.com")]
        public static class abstractatech
        {

        }

        [Description("xavalon.net")]
        public static class xavalon
        {

        }

        [Description("jsc-solutions.net")]
        public static class jscsolutons
        {

        }

        public static class dev
        {

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


                // will be the folder for sub apps
                [Description("foo-pages")]
                public static class pages
                {
                    // activated via /foo-pages/Zoo
                    // activated via /foo-pages when only one app in this module
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

        }
    }
}
