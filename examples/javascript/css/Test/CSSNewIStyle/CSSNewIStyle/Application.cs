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
using CSSNewIStyle;
using CSSNewIStyle.Design;
using CSSNewIStyle.HTML.Pages;

namespace CSSNewIStyle
{
    // test new cool naming
    using css = IStyle;

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
            //02000002 CSSNewIStyle.Application
            //script: error JSC1000: You tried to instance a class which seems to be marked as native.
            //script: error JSC1000: type has no callable constructor: [ScriptCoreLib.JavaScript.DOM.IStyle] Void .ctor()
            //script: error JSC1000: error at CSSNewIStyle.Application..ctor,
            // assembly: V:\CSSNewIStyle.Application.exe
            // type: CSSNewIStyle.Application, CSSNewIStyle.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x0008
            //  method:Void .ctor(CSSNewIStyle.HTML.Pages.IApp)

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140204
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IStyle.cs

            //h1 { color: blue; }

            new css
            {
                fontFamily = IStyle.FontFamilyEnum.Consolas
                //color = "red"
            };

            //new css.p(IHTMLElement.HTMLElementEnum.p)
            new IStyle(IHTMLElement.HTMLElementEnum.p)
            {
                backgroundColor = "yellow"
            };

            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\CSS\CSSStyleRule.cs

            new IStyle(page.Header.css)
            {
                backgroundColor = "cyan"
            };


            new IStyle(page.Header)
            {
                borderLeft = "1em solid red"
            };
        }

    }
}
