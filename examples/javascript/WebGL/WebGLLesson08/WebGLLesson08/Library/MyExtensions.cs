using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace WebGLLesson08.Library
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
            //  No implementation found for this native method, please implement [static System.Single.Parse(System.String)]

            return (float)double.Parse(i.value);
        }
    }
}
