using ScriptCoreLib;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

using System.Linq;

using global::System.Collections.Generic;

namespace ScriptCoreLib.JavaScript.Cards
{
    [Script]
    internal static class Extensions
    {
        public static T Pop<T>(this List<T> e)
        {
            var i = e.Count - 1;
            var x = e.ElementAt(i);

            e.RemoveAt(i);

            return x;
        }
    }
}
