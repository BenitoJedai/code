using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class ASCIIStyledLoadAnimationExtensions
    {
        public static Action ToASCIIStyledLoadAnimation(this IHTMLElement e, string innerText)
        {
            var feed = new[] {
                "-", "/", "|", "\\"
            };

            var t = new ScriptCoreLib.JavaScript.Runtime.Timer(
                tt =>
                {
                    e.innerText = "Loading... " + feed.ElementAt(tt.Counter % feed.Length);


                }
            );

            t.StartInterval(300);

            return delegate
            {
                t.Stop();

                e.innerText = innerText;
            };
        }
    }
}
