#r @"c:\util\jsc\bin\ScriptCoreLib.dll"

namespace Ultra1

open ScriptCoreLib;
open ScriptCoreLib.JavaScript;
open ScriptCoreLib.JavaScript.DOM.HTML;
open ScriptCoreLib.JavaScript.DOM;

type Element1() =
    let x = new IHTMLDiv("hello")
    do Native.Document.body.appendChild(x)