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
using Abstractatech.JavaScript.Forms.ChromeStyler;
using Abstractatech.JavaScript.Forms.ChromeStyler.Design;
using Abstractatech.JavaScript.Forms.ChromeStyler.HTML.Pages;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Runtime;

namespace Abstractatech.JavaScript.Forms.ChromeStyler
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
            FormStyler.AtFormCreated = FormStylerLikeChrome.LikeChrome;

            // http://adamschwartz.co/chrome-tabs/
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404

            // http://css-tricks.com/examples/MovingHighlight/


            new Form { Text = "Hello World" }.Show();

        }

    }
}

namespace System.Windows.Forms
{
    public static class FormStylerLikeChrome
    {
        public static void LikeChrome(FormStyler s)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404

            // border-radius:25px;

            //dynamic TargetOuterBorder_style = s.TargetOuterBorder.style;
            //dynamic TargetInnerBorder_style = s.TargetInnerBorder.style;

            var TargetBackground = new IHTMLDiv();

            //dynamic TargetBackground_style = TargetBackground.style;

            new IStyle(TargetBackground)
            {
                position = IStyle.PositionEnum.absolute,
                left = "0px",
                top = "0px",
                right = "0px",
                bottom = "0px",
                Opacity = 0.7,
                //boxShadow = "rgba(0, 0, 0, 0.2) 4px 4px 4px 0px"
            };

            s.TargetOuterBorder.parentNode.insertBefore(TargetBackground, s.TargetOuterBorder.parentNode.firstChild);


            s.TargetOuterBorder.style.boxShadow = "";
            s.TargetOuterBorder.style.borderColor = "black";
            s.TargetOuterBorder.style.borderWidth = "0px";

            // overkill via html, css, svg?
            var skin = new App();

            skin.shell.style.position = IStyle.PositionEnum.absolute;
            skin.shell.style.left = "0px";
            skin.shell.style.top = "0px";
            skin.shell.style.right = "0px";


            //skin.shell.style.zIndex = 0;


            s.Context.ShowIcon = false;

            skin.tabtitle.innerText = s.Context.Text;
            s.Context.TextChanged += delegate
            {
                skin.tabtitle.innerText = s.Context.Text;
            };

            s.CaptionContent.insertPreviousSibling(skin.shell);

            // div[style-id="2"]:not(:hover):not([await1="incomplete"]) 
            //((!s.TargetOuterBorder.css.hover) + skin.shell).style.Opacity = 0.5;
            // Error	39	The call is ambiguous between the following methods or properties: 'ScriptCoreLib.JavaScript.DOM.CSSStyleRuleMonkier.this[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement]' and 'ScriptCoreLib.JavaScript.DOM.CSSStyleRuleMonkier.this[System.Threading.Tasks.Task]'	X:\jsc.svn\market\javascript\Abstractatech.JavaScript.Forms.ChromeStyler\Abstractatech.JavaScript.Forms.ChromeStyler\Application.cs	95	14	Abstractatech.JavaScript.Forms.ChromeStyler
            // ncaught TypeError: Cannot read property 'VREABmASXzSNfpl_bHJLUOA' of null

            skin.shell.style.transition = "opacity 300ms linear";

            ((!s.TargetOuterBorder.css.hover)[(IHTMLElement)skin.shell]).style.Opacity = 0.7;
            
            s.ContentContainerPadding.style.boxShadow = "inset 1px 0 rgba(255, 255, 255, 0.6), inset 0 1px rgba(255, 255, 255, 0.6), rgba(0, 0, 0, 0.2) 4px 4px 4px 0px";



            //skin.shell.AttachTo();



            //new IStyle(s.CaptionContent)
            //{
            //    //paddingLeft = "12px",
            //    //paddingRight = "30px",
            //    //left = "",
            //    //right = "0px",
            //    height = "27px",
            //    backgroundColor = "  rgba(255,0,0,0.7)",
            //};
            s.CaptionContent.Hide();

            s.TargetInnerBorder.style.borderWidth = "0px";

            s.CloseButton.style.color = JSColor.Red;
            s.CloseButton.style.backgroundColor = JSColor.None;
            s.CloseButton.style.borderWidth = "0px";
            s.CloseButtonContent.style.borderWidth = "0px";

            s.CloseButton.Orphanize();
            s.CloseButtonContent.Orphanize();


            skin.tabbottombar.Orphanize();

            skin.tabclose.style.zIndex = 100;
            skin.tabclose.onclick +=
                delegate
                {
                    s.Context.Close();
                };

            new IStyle(s.TargetResizerPadding)
            {
                left = "0px",
                top = "0px",
                right = "0px",
                bottom = "0px"
            };



            s.TargetInnerBorder.style.backgroundColor = "";

            //background-image: -webkit-linear-gradient(#e3e3e3, #e0e0e0);


            s.ContentContainerPadding.style.backgroundColor = "#e3e3e3";
            //s.ContentContainerPadding.style.top = "25px";
            s.ContentContainerPadding.style.top = "26px";


            s.ContentContainerPadding.style.border = "1px solid rgba(0, 0, 0, 0.16)";


            //s.Caption.style.left = "";
            s.Caption.style.backgroundColor = "";



        }
    }
}