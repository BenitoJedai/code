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
using CSSConditionalStyle;
using CSSConditionalStyle.Design;
using CSSConditionalStyle.HTML.Pages;

namespace CSSConditionalStyle
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

            page.c1.css.@checked[
                page.PageContainer
            ].style.borderLeft = "1em solid yellow";

            page.c2.css.@checked[
                 page.PageContainer
             ].style.borderRight = "1em solid yellow";



            // the or.
            // would jsc need to be able to promote any bool to a document level hidden input
            // to make use of the css engine?
            //(page.c1.css.@checked | page.c2.css.@checked)[
            //     page.PageContainer
            // ].style.borderBottom = "1em solid yellow";


            // ? what about the & operator
            // it could try to have it both ways regardless which element comes first?


            //35ms { parent = { selectorText = input[style-id="1"] } } view-source:34909

            // view-source:34909
            //35ms { selectorElement =  } 

            //// 33ms { right_withElement = { selectorText = input[style-id="1"], selectorElement =  } } 
            //var right_withElement = page.c2.css;

            ////Console.WriteLine(new { right_withElement });
            //var right = right_withElement.@checked;
            //Console.WriteLine(new { right });


            //page.c1.css.@checked[page.c2].@checked[



            //(page.c1.css & page.c2.css).@checked[
            (page.c1.css.@checked & page.c2.css.@checked)[
                page.PageContainer
            ].style.borderBottom = "1em solid red";
        }

    }
}
