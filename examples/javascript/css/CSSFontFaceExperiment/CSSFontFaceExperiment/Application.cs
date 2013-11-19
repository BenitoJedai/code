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
using CSSFontFaceExperiment;
using CSSFontFaceExperiment.Design;
using CSSFontFaceExperiment.HTML.Pages;

namespace CSSFontFaceExperiment
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
            //C:\Users\Arvo>taskkill /F /T /FI "IMAGENAME eq chrome.exe"
            //SUCCESS: The process with PID 4188 (child process of PID 5096) has been terminated.
            //SUCCESS: The process with PID 7092 (child process of PID 5096) has been terminated.
            //SUCCESS: The process with PID 3052 (child process of PID 5096) has been terminated.
            //SUCCESS: The process with PID 6656 (child process of PID 5096) has been terminated.
            //SUCCESS: The process with PID 3148 (child process of PID 5096) has been terminated.
            //SUCCESS: The process with PID 3336 (child process of PID 5096) has been terminated.
            //SUCCESS: The process with PID 6600 (child process of PID 5096) has been terminated.
            //SUCCESS: The process with PID 8468 (child process of PID 5096) has been terminated.
            //SUCCESS: The process with PID 7732 (child process of PID 5096) has been terminated.
            //SUCCESS: The process with PID 5096 (child process of PID 2716) has been terminated.


            // https://github.com/christiannaths/Redacted-Font/tree/master/fonts

            // should jsc learn how to convert ttf to any other format?

            // http://stackoverflow.com/questions/13751412/font-names-with-quotes-what-is-the-right-usage


            // http://stackoverflow.com/questions/8749225/loading-external-font-in-html-page-with-inline-css

            // http://www.fontsquirrel.com/fonts

            // http://www.w3.org/TR/css3-fonts/

            //IStyleSheet.all.AddRule("@font-face").With(
            //    r =>
            //    {
            //        r.style.setProperty("font-family", "'Blokk <dynamic>'", "");

            //        //TTF - Works in most browsers except IE and iPhone.
            //        //EOT - IE only. 
            //        //WOFF - Compressed, emerging standard. 
            //        //SVG - iPhone/iPad.


            //        // IE plays stupid for ttf? needs eot?
            //        // works for safari and chrome.
            //        //r.style.setProperty("src", "url('assets/CSSFontFaceExperiment/BLOKKRegular.ttf') format('truetype')", "");
            //        //r.style.setProperty("src", "url('assets/CSSFontFaceExperiment/BLOKKRegular.ttf')", "");

            //        var src = Fonts.redacted_script_regular.GetSource();


            //        r.style.setProperty("src", "url('" + src + "')", "");
            //    }
            //);


            var r = IStyleSheet.all.AddFontFaceRule(
                "'Blokk <dynamic>'",
                FontFaces.redacted_script_regular.GetSource()
            );


            // http://www.w3schools.com/cssref/css3_pr_font-face_rule.asp

            //(IStyleSheet.all["*"].style as dynamic).fontFamily = "'Blokk <dynamic>'";
            IStyleSheet.all["*"].style.fontFamily = r.style.fontFamily;





            IStyleSheet.all["p"].style.fontFamily = new FontFaces.open_sans_regular();
            //IStyleSheet.all["p"].style.fontFamily = new FontFaces.BLOKKRegular("a/xblokk").rule.style.fontFamily;
            //IStyleSheet.all["p"].style.fontFamily = new XBLOKKRegular();
        }

    }

    class XBLOKKRegular : FontFaces.BLOKKRegular
    {
        public XBLOKKRegular(string f = "axblokk")
            : base(f)
        {

        }

        public static implicit operator IStyle.FontFamilyEnum(XBLOKKRegular x)
        {
            return x.rule.style.fontFamily;
        }
    }
}
