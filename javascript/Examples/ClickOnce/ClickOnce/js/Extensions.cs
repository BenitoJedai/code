using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib;

namespace ClickOnce.js
{
    [Script]
    public static class Extensions
    {
        public static IHTMLElement AttachTo(this IHTMLElement e, IHTMLElement c)
        {
            c.appendChild(e);

            return e;
        }

        public static T Deserialize<T>(this IXMLDocument e, object[] k)
                    where T : class, new()
        {
            return new IXMLSerializer<T>(k).Deserialize(e);
        }



        public static void SpawnTo<T>(this Type alias, object[] KnownTypes, Action<T> h)
            where T : class, new()
        {
            ScriptCoreLib.JavaScript.Native.Spawn(alias.Name,
                i =>
                {
                    var tag = (IHTMLScript)i;
                    var text = i.text;

                    if (tag.type == "text/xml")
                    {
                        var doc = IXMLDocument.Parse(text);

                        h(doc.Deserialize<T>(KnownTypes));
                    }
                    else if (tag.type == "text/json")
                    {
                        // reflection info will be lost here?

                        h((T)(object)Expando.FromJSON(text));
                    }
                }
            );
        }

    }
}
