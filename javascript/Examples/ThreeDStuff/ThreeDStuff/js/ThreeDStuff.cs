using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;

namespace ThreeDStuff.js
{

    [Script]
    partial class Isometric { }
    [Script]
    partial class IsometricRotating { }
    [Script]
    partial class IsometricRotatingAndInput { }
    [Script]
    partial class IsometricWithNatureBoy { }
    [Script]
    partial class IsometricWithNatureBoyAndInput { }
    [Script]
    partial class IsometricWithToolbar { }


    [Script, global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class ApplicationDescriptionAttribute : Attribute
    {
        public string Description;

        public string FlashMovie;
    }

    [Script, ScriptApplicationEntryPoint]
    public class ThreeDStuff
    {
        public ThreeDStuff()
        {
            Native.Document.body.style.backgroundColor = Color.Black;
            Native.Document.body.style.color = Color.White;


            var Menu = new IHTMLDiv().AttachToDocument();

            new IHTMLElement(IHTMLElement.HTMLElementEnum.h1,
                typeof(ThreeDStuff).Name).AttachTo(Menu);

            new IHTMLImage("assets/ThreeDStuff/Preview.png").AttachTo(Menu).style.Aggregate(
                s =>
                {
                    s.Float = ScriptCoreLib.JavaScript.DOM.IStyle.FloatEnum.right;
                    s.border = "1px solid red";
                }
            );

            var List = new IHTMLElement(IHTMLElement.HTMLElementEnum.ol).AttachTo(Menu);

            Func<string, string, Type, IHTMLAnchor> CreateAnchor =
                (href, text, t) =>
                {
                    var a = new IHTMLAnchor(href, "");

                    var caption = new IHTMLSpan(t.Name);
                    caption.style.fontWeight = "bold";
                    a.appendChild(caption);
                    a.appendChild(new IHTMLBreak());
                    a.appendChild(text);

                    a.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
                    a.onmouseover +=
                        ev =>
                        {
                            a.style.backgroundColor = Color.Red;
                            a.style.color = Color.Black;
                        };
                    a.onmouseout +=
                        ev =>
                        {
                            a.style.backgroundColor = "";
                            a.style.color = Color.White;
                        };

                    a.style.textDecoration = "none";
                    a.style.color = Color.White;

                    a.onclick +=
                        ev =>
                        {
                            Native.Document.body.style.background = "";

                            ev.PreventDefault();

                            Menu.Dispose();

                            t.CreateInstance();
                        };

                    return a;
                };


            foreach (var i in from t in typeof(ThreeDStuff).Assembly.GetTypes()
                              let d = (ApplicationDescriptionAttribute[])t.GetCustomAttributes(typeof(ApplicationDescriptionAttribute), false)
                              where d.Length > 0
                              let attribute = d.Random()
                              let anchor = CreateAnchor(t.Name + ".htm", attribute.Description, t)
                              orderby t.Name
                              select new { anchor, attribute })
            {
                var li = new IHTMLElement(IHTMLElement.HTMLElementEnum.li, i.anchor).AttachTo(List);

                /*
                <object width="425" height="355">
                 * <param name="movie" value="http://www.youtube.com/v/kCgCSMpRN40&rel=1"></param>
                 * <param name="wmode" value="transparent"></param>
                 * <embed src="http://www.youtube.com/v/kCgCSMpRN40&rel=1" type="application/x-shockwave-flash" wmode="transparent" width="425" height="355"></embed>
                </object>
                */

                /*
                if (i.attribute.FlashMovie.IsDefined())
                    new IHTMLAnchor(i.attribute.FlashMovie, "(see video)").AttachTo(li);
                */
            }


        }

        static ThreeDStuff()
        {
            typeof(ThreeDStuff).Spawn();
        }
    }
}
