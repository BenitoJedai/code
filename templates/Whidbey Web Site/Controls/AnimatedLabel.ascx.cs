using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.JavaScript, "JavaScript")]

namespace JavaScript
{
    using ScriptCoreLib.JavaScript;
    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.JavaScript.DOM;
    using ScriptCoreLib.JavaScript.DOM.HTML;


    using ScriptCoreLib;
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Drawing;


    [Script]
    public class AnimatedLabel
    {
        static AnimatedLabel()
        {
            Native.Window.onload +=
                delegate
                {
                    foreach (IHTMLElement v in Native.Document.getElementsByClassName("AnimatedLabel").ToArray())
                    {
                        IHTMLElement target = v;

                        target.style.color = Color.Red;

                        Timer.Interval(
                            delegate (Timer t)
                            {
                                target.style.color = (Color) Native.Math.floor( Native.Math.random() * 255);

                            },
                            1000
                        );

                    } 

                    
                };
        }
    }
}

namespace MyServer
{

    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    public partial class AnimatedLabel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            // MyServerHandler.Handler.RegisterClientScript<JavaScript.AnimatedLabel>(,  "Handler.ashx?src={0}", SharedHelper.LocalModules);
        }
    }

}