using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    public static class EmbedAttributeImplementation
    {
        public static void Resolve(string source, Action<string, string, string> yield)
        {
            var mimeType = default(string);
            var fontName = default(string);
            var embedAsCFF = default(string);

            mimeType = EmbedMimeTypes.Resolve(source);

            // we want to actualy use our fonts!
            if (source.EndsWith(".ttf"))
            {
                var i = source.LastIndexOf("/");
                var j = source.LastIndexOf(".");
                if (i > -1)
                    if (j > -1)
                        fontName = source.Substring(i + 1, j - i - 1);

                embedAsCFF = "false";
            }

            yield(mimeType, fontName, embedAsCFF);
        }
    }
}
