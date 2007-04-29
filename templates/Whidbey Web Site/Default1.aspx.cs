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



    /// <summary>
    /// Summary description for Class2
    /// </summary>
    [Script]
    public class Class2
    {
        static Class2()
        {
            
            Native.Window.onload +=
                delegate
                {
                    IHTMLButton btn = new IHTMLButton("asp.net c# : hello world");

                    Native.Document.body.appendChild(btn);

                    btn.style.color = Color.Red;

                    btn.onclick += delegate { Native.Window.alert("button clicked!"); };

                    Console.Log("asp net hello world : c#");
                };
        }
    }

}

namespace MyServer
{
    using System.Data;
    using System.Diagnostics;
    using System.Configuration;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using System.Query;
    using System.Reflection;

    using jsc.server;

    public partial class Default1 : Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            WebTools.CompileAndRegisterClientScript(this);
        }

        protected void AnimatedLabel1_Load(object sender, System.EventArgs e)
        {
            
        }
}

}