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
using CSSDomainConditional;
using CSSDomainConditional.Design;
using CSSDomainConditional.HTML.Pages;
using System.Linq.Expressions;

namespace CSSDomainConditional
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            //Native.css[
            // IStyleSheetRule.AddRule error { text = html[domain!='my.monese.com'].Form:after{/**/} }
            css(() => Native.document.domain == "goo.com");

        }

        static CSSStyleRuleMonkier css(Expression<Func<bool>> x)
        {
            // add domain as xattribute
            // or title

            return null;
        }

    }
}
