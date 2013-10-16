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
using CSSPrintMediaExperiment;
using CSSPrintMediaExperiment.Design;
using CSSPrintMediaExperiment.HTML.Pages;
using System.Windows.Forms;

namespace CSSPrintMediaExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // http://davidwalsh.name/add-rules-stylesheets
            // http://stackoverflow.com/questions/5359943/javascript-set-print-stylesheet

            var pstyle = new IHTMLStyle
            {
                type = "text/css",
                media = "print"
            };

            //pstyle.AttachTo(
            //    Native.document.head
            //);

            // .StyleSheet is available only if attached to DOM?
            pstyle["h1"].border = "13px solid green";
            //pstyle["h1"].backgroundColor = "yellow";




            //pstyle["h1"].color = "green";

            //IStyleSheet.Default["@media print { h1 }"].style.color = "yellow";

            // http://www.w3schools.com/css/css_mediatypes.asp

            #region test
            var mp = IStyleSheet.Default["@media print"];

            // { type = 4 } 
            Console.WriteLine(
                new
                {
                    mp.type,
                    CSSRuleTypes.MEDIA_RULE
                }
            );

            if (mp.type == CSSRuleTypes.MEDIA_RULE)
            {
                var mpr = (CSSMediaRule)(object)mp;

                foreach (var media in mpr.media)
                {
                    // { media = print } 
                    Console.WriteLine(new { media });
                }
            }
            #endregion

            //     __awIABmCv4jKFf2Sg5roQag(__bQIABjls6jSgl0UFIggGlQ()[1], 'h1').style.color = 'purple';

            //page.Header.cl

            //page.Header.EnsureID();

            // http://www.w3schools.com/css/css_attribute_selectors.asp

            //page.Header.setAttribute("style-id", "45");

            IStyleSheet.Default[CSSMediaTypes.print][page.Header].style.color =
                "blue";

            IStyleSheet.Default[CSSMediaTypes.print][page.Header].style.boxShadow =
             "inset 0 0 0 10000px yellow";


            //Console.WriteLine(
            //    new
            //    {
            //        page.Header.style.parentRule,
            //        page.Header.style.cssText,
            //        //IStyleSheet.Default[CSSMediaTypes.print]["h1"].style.cssText
            //    }
            //    );

            //            @media print
            //  {
            //    h1 {
            //        border: 3px solid blue;
            //    }

            //}

            //new IHTMLDiv { }.AttachToHead().innerHTML =
            //    @"<style type='text/css' media='print'>h1{ border: 13px solid yellow;}</style>";



            //f.FormBorderStyle = FormBorderStyle.None;
            new Form1().AttachFormTo(page.output);

            //f.GetHTMLTarget().AttachTo(page.output);

            //f.Show();


            //f.WindowState = FormWindowState.Maximized;

            IStyleSheet.Default
                [CSSMediaTypes.print]
                [Native.document.body].style.overflow =
                    IStyle.OverflowEnum.hidden;


            page.Print.onclick +=
                delegate
                {
                    Native.window.print();
                };

            IStyleSheet.Default
                [CSSMediaTypes.print]
                [page.Print].style.display = IStyle.DisplayEnum.none;


            var s = new SpecialLayout();

            var i = new IHTMLIFrame();

            // hide it from plain sight
            i.style.display = IStyle.DisplayEnum.none;

            new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox }.AttachToDocument().With(
                x =>
                {
                    x.onclick += delegate
                    {
                        i.ToggleVisible();
                    };

                }
            );


            // it will never load with out this call!
            i.AttachToDocument();

            // Uncaught TypeError: Cannot read property 'document' of undefined 

            page.PrintFromIframe.disabled = true;

            i.onload += delegate
            {
                var idoc = i.contentWindow.document;

                // http://stackoverflow.com/questions/16649943/css-to-set-a4-paper-size
                idoc.body.style.minWidth = "21cm";

                var x = new IHTMLStyle().AttachTo(idoc.body);

                s.AttachTo(idoc.body);



                x.StyleSheet[CSSMediaTypes.print][s.Header].style.color =
                    "yellow";

                x.StyleSheet[CSSMediaTypes.print][s.Header].style.boxShadow =
                 "inset 0 0 0 10000px red";


                x.StyleSheet[CSSMediaTypes.print][s.SpecialText].before.style.content = "'print hello world'";
                x.StyleSheet[CSSMediaTypes.print][s.SpecialText].before.style.color = "red";
                x.StyleSheet[CSSMediaTypes.print][s.SpecialText].before.style.borderBottom = "1px solid red";

                // http://www.w3schools.com/cssref/pr_gen_content.asp
                //x.StyleSheet
                //     //[CSSMediaTypes.print]
                //     ["#" + s.SpecialText.id + ":before"]
                //     .style.setProperty("content", "'hello world'", "");


                new Form1 { Text = "for print" }.AttachFormTo(s.output);

                page.PrintFromIframe.disabled = false;
                page.PrintFromIframe.onclick +=
                    delegate
                    {
                        i.contentWindow.print();
                    };
            };

            i.src = "about:blank";


        }

    }
}
