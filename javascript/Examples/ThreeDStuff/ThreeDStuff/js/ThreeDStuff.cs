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
            Func<string, string, IHTMLAnchor> CreateAnchor =
                (href, text) =>
                {
                    var a = new IHTMLAnchor(href, text);

                    a.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.block;
                    a.onmouseover +=
                        ev =>
                        {
                            a.style.color = Color.Red;
                        };
                    a.onmouseout +=
                        ev =>
                        {
                            a.style.color = Color.None;
                        };

                    a.style.textDecoration = "none";

                    return a;
                };

            foreach (var i in from t in typeof(ThreeDStuff).Assembly.GetTypes()
                              let d = (ApplicationDescriptionAttribute[])t.GetCustomAttributes(typeof(ApplicationDescriptionAttribute), false)
                              where d.Length > 0
                              let anchor = CreateAnchor(t.Name + ".htm", t.Name + " - " + d.Random().Description)
                              select new { anchor })
            {
                i.anchor.AttachToDocument();
            }


        }

        static ThreeDStuff()
        {
            typeof(ThreeDStuff).SpawnTo(i => new ThreeDStuff());
        }
    }
}
