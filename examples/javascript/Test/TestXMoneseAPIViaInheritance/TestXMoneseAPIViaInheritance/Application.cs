using monese.experimental;
using ScriptCoreLib;
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
using TestXMoneseAPIViaInheritance;
using TestXMoneseAPIViaInheritance.Design;
using TestXMoneseAPIViaInheritance.HTML.Pages;

namespace TestXMoneseAPIViaInheritance
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application :
        //ApplicationWebService

        // what about multiple base classes?
        MoneseWebServices
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new IHTMLButton { "new user" }.AttachToDocument().WhenClicked(
                async button =>
                {
                    var RegisterUserShort_value = await this.RegisterUserShort("TestXMoneseAPIViaInheritance@", "1234");
                    // 0:39123ms 444 

                    //xx = 398
                    new IHTMLPre { new { RegisterUserShort_value } }.AttachToDocument();
                }
            );

        }

    }
}
