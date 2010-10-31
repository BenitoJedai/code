using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.Extensions
{
    public static class DisplayObjectContainerExtensions
    {
      

        public static IEnumerable<DisplayObject> GetChildren(this DisplayObject e)
        {
            var c = e as DisplayObjectContainer;
            var numChildren = 0;

            if (c != null)
                numChildren = c.numChildren;

            return Enumerable.Range(0, numChildren).Select(i => c.getChildAt(i));
        }
    }
}
