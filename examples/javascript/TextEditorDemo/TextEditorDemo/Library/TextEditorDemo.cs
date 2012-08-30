using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace TextEditorDemo.source.js.Controls
{
    public class TextEditorDemo
    {

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }


        public TextEditorDemo()
        {
            Control.AttachToDocument();

            var text = new TextEditor(Control);

            text.InnerHTML = "<p>jsc:javascript <b>TextEditor</b></p>";

            text.IsFadeEnabled = false;

            var cookie = new Cookie("TextData");



            var save = new IHTMLButton("save to cookie");
            var load = new IHTMLButton("load from cookie");

            save.onclick += delegate { cookie.Value = text.InnerHTML; };
            load.onclick += delegate { text.InnerHTML = cookie.Value; };

            this.Control.appendChild(save, load);
        }



    }


}
