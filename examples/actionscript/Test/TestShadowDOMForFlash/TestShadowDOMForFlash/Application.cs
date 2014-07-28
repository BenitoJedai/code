using TestShadowDOMForFlash;
using TestShadowDOMForFlash.Design;
using TestShadowDOMForFlash.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
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
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;

namespace TestShadowDOMForFlash
{
    public sealed class ApplicationSprite : Sprite
    {
        // this is also IElement !

        public ApplicationSprite()
        {
            // X:\jsc.svn\core\ScriptCoreLib.Avalon\ScriptCoreLib.Avalon\ActionScript\BCLImplementation\System\Windows\Controls\TextBox.cs

            //var x = new TextField { text = "hello world" }.AttachTo(this);
            var x = new TextField {
                type = TextFieldType.INPUT,
                text = "hello world"
            }.AttachTo(this);

            x.change +=
                delegate
            {
                // time to let the DOM know

                if (xChanged != null)
                    xChanged(x.text);
            };

        }

        public event Action<string> xChanged;
    }

    [Script(HasNoPrototype = true, ExternalTarget = "ApplicationSpriteElement")]
    public class ApplicationSpriteElement : IHTMLElement
    {
        // X:\jsc.svn\examples\javascript\WebGL\WebGLSpiral\WebGLSpiral\Application.cs

        //XAttribute foo;
        public string foo
        {
            // like for svg we need to make it work ourselves?
            [Script(DefineAsStatic = true)]
            get
            {
                return (string)this.getAttribute("foo");
            }
            [Script(DefineAsStatic = true)]
            set
            {
                this.setAttribute("foo", value);
            }
        }

        class __ApplicationSpriteElement
        {
            public readonly ApplicationSprite sprite = new ApplicationSprite();

            public __ApplicationSpriteElement(ApplicationSpriteElement e)
            {
                sprite.AttachSpriteTo(e.shadow);

                sprite.xChanged +=
                    ee =>
                {
                    e.foo = ee;
                };
            }

            public static void registerElement()
            {
                // X:\jsc.svn\examples\javascript\async\AsyncWorkerCustomElementExperiment\AsyncWorkerCustomElementExperiment\Application.cs

                // Uncaught ReferenceError: WebGLSpiralElementXwAABAFL0jC0lC1d7ZU4wQ is not defined
                // HasNoPrototype wont like our static ctor with statc fields?

                // Uncaught SyntaxError: Failed to execute 'registerElement' on 'Document': Registration failed for type 'webglspiral'. The type name is invalid. 

                // when shall we teach jsc to do this correctly?
                (Native.window as dynamic).ApplicationSpriteElement = Native.document.registerElement("x-ApplicationSpriteElement",
                     (ApplicationSpriteElement e) =>
                     {
                         //var s = e.createShadowRoot();

                         //s.appendChild("hello");
                         var ss = new __ApplicationSpriteElement(e);
                     }
                );
            }
        }


        static ApplicationSpriteElement()
        {
            __ApplicationSpriteElement.registerElement();
        }
    }

    public sealed class Application : ApplicationWebService
    {

        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140718-shadowdom
            // https://sites.google.com/a/jsc-solutions.net/backlog/3rd-party-libraries/ml

            // Initialize ApplicationSprite

                // why wont these work?
            Native.css["x-applicationspriteelement"].style.borderLeft = "1em solid yellow";

            //Native.css["x-ApplicationSpriteElement"].before.contentAttribute = "foo";
            Native.css["x-applicationspriteelement"].before.contentXAttribute = new XAttribute("foo", "");



            // we want to demonstrate flash can also integrate with html and we can
            // send data in and out by attributes. we wont be using jsc data layer in this example yet
            new ApplicationSpriteElement().AttachToDocument();
        }

    }
}
