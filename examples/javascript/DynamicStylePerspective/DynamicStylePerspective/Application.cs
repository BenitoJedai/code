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
using DynamicStylePerspective.Design;
using DynamicStylePerspective.HTML.Pages;

namespace DynamicStylePerspective
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
        public Application(IDefaultPage page)
        {
            var container = new IHTMLDiv().AttachToDocument().With(
                e =>
                {

                    var style = (XIStyle)(object)e.style;

                    style.height = "300px";
                    style.width = "600px";
                    style.margin = "5em";
                    style.border = "2px solid blue";
                    style.perspective = "500px";
                }
            );


            //new IHTMLDiv()
            new IHTMLIFrame ()
                .AttachTo(container).With(
             parent =>
             {

                 var style = (XIStyle)(object)parent.style;

                 style.height = "280px";
                 style.width = "580px";

                 style.margin = "10px";

                 style.border = "2px solid red";
                 style.transformStyle = "preserve-3d";

                 Action loop = null;
                 var y = 0;

                 loop = delegate
                 {
                     y = (y + 1) % 360;

                     style.transform = "rotateY(" + y + "deg)";

                     Native.Window.requestAnimationFrame += loop;
                 
                 };

                 Native.Window.requestAnimationFrame += loop;
             }
         );

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }

    [Script(HasNoPrototype = true)]
    public class InternalXIStyle
    {
        public string perspective;
        public string webkitPerspective;
        public string MozPerspective;

        public string MozTransformStyle;
        public string webkitTransformStyle;
        public string transformStyle;

        public string MozTransform;
        public string webkitTransform;
        public string transform;
    }

    [Script(HasNoPrototype = true)]
    public class XIStyle : IStyle
    {
        public string perspective
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return "";
            }
            [Script(DefineAsStatic = true)]
            set
            {
                var style = (InternalXIStyle)(object)this;

                style.MozPerspective = value;
                style.webkitPerspective = value;
                style.perspective = value;
            }
        }

        public string transformStyle
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return "";
            }
            [Script(DefineAsStatic = true)]
            set
            {
                var style = (InternalXIStyle)(object)this;

                style.transformStyle = value;
                style.MozTransformStyle = value;
                style.webkitTransformStyle = value;
            }
        }

        public string transform
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return "";
            }
            [Script(DefineAsStatic = true)]
            set
            {
                var style = (InternalXIStyle)(object)this;

                style.transform = value;
                style.webkitTransform = value;
                style.MozTransform = value;
            }
        }
    }
}
