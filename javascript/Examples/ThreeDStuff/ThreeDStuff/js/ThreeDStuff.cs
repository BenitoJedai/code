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
    [Script, global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class ApplicationDescriptionAttribute : Attribute
    {
        public string Description;
    }

    [Script, ScriptApplicationEntryPoint]
    class ThreeDStuff
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
                            a.style.backgroundColor = Color.FromRGB(0x60, 0, 0);
                            a.style.color = Color.Red;
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
                              let anchor = CreateAnchor(t.Name + ".htm", d.Random().Description, t)
                              select new { anchor })
            {
                new IHTMLElement(IHTMLElement.HTMLElementEnum.li, i.anchor).AttachTo(List);
            }


        }

        static ThreeDStuff()
        {
            typeof(ThreeDStuff).Spawn();
        }
    }
}
