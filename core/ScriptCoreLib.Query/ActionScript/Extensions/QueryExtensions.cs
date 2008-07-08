using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.Extensions
{
    [Script]
    public static class QueryExtensions
    {
        public static IEnumerable<DisplayObject> Children(this DisplayObjectContainer c)
        {
            return Enumerable.Range(0, c.numChildren).Select(i => c.getChildAt(i));

        }
    }
}
