﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://www.w3.org/TR/html5/scripting-1.html#the-template-element
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/HTMLTemplateElement.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLShadowElement.idl

    [Script(InternalConstructor = true)]
    public class IHTMLShadow : IHTMLElement<IHTMLShadow>
    {
        // once template becomes popular AssetsLibrary needs to learn it too

        public IHTMLElement[] getDistributedNodes() { return null; }
        // tested by ?
        // X:\jsc.svn\examples\javascript\test\TestShadowDOM\TestShadowDOM\Application.cs



        public IHTMLShadow() { }

        internal static IHTMLShadow InternalConstructor()
        {
            return (IHTMLShadow)((object)new IHTMLElement(HTMLElementEnum.shadow));
        }


    }
}
