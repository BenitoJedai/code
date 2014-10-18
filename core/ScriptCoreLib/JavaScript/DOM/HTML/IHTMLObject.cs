using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLObjectElement.idl
    // https://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLObjectElement.cpp


    [Script(InternalConstructor = true)]
    public class IHTMLObject : IHTMLElement
    {


        #region Constructor

        public IHTMLObject()
        {
            // InternalConstructor
        }


        static IHTMLObject InternalConstructor()
        {
            return (IHTMLObject)IHTMLElement.InternalConstructor(HTMLElementEnum.@object);
        }

        #endregion

        public string classid;


        // X:\jsc.svn\examples\javascript\Test\TestEIDPIN2\TestEIDPIN2\Application.cs
        // needs https connection!
        // type = "application/x-digidoc"

        public string type;

        public string data;
        public string autostart;
        public string loop;
        //public string height;
        //public string width;

        public void Play()
        {

        }
    }
}
