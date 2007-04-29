using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.JavaScript, "JavaScript")]

namespace JavaScript.ASPNET.UI
{
    using ScriptCoreLib.JavaScript;
    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.JavaScript.DOM;
    using ScriptCoreLib.JavaScript.DOM.HTML;


    using ScriptCoreLib;
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Drawing;


    [Script]
    public class TextEditor
    {
        static TextEditor()
        {
            Native.Window.onload +=
                delegate
                {
                    foreach (IHTMLElement v in Native.Document.getElementsByClassName("TextEditor").ToArray())
                    {
                        IHTMLElement target = v;

                        new ScriptCoreLib.JavaScript.Controls.TextEditor(target);

                    } 

                    
                };
        }
    }
}



public partial class Controls_TextEditor : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, System.EventArgs e)
    {

    }
}
