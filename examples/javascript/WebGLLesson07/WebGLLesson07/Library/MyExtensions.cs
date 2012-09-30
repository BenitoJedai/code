using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace WebGLLesson07.Library
{
    public static class MyExtensions
    {
        public static int ToInt32(this bool i)
        {
            if (i)
                return 1;
            return 0;
        }
        public static float ToFloat(this IHTMLInput i)
        {
            return float.Parse(i.value);
        }
    }
}
