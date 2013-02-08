﻿using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    public static class IHTMLButtonExtensions
    {
        public static IHTMLButton WhenClicked(this IHTMLButton e, Action h)
        {
            e.onclick +=
                delegate
                {
                    h();
                };

            return e;
        }
    }
}
