// Learn more about F# at http://fsharp.net

module Module1


// error FS0841: This attribute is not valid for use on this language element.
//  Assembly attributes should be attached to a 'do ()' declaration, if necessary
//  within an F# module.

// http://stackoverflow.com/questions/2269625/using-assembly-attributes-in-f
[<assembly: System.Reflection.Obfuscation(Feature = "merge")>] 
do ()

let square x = x * x

let WashText (text:string) =
    text.Replace("&quot;", "\"")
        .Replace("&amp;", "&")
        .Replace("&lt;", "<")
        .Replace("&gt;", ">")




// Error	1	Unexpected keyword 'member' in definition. Expected incomplete 
// structured construct at or before this point or other token.	C:\work\jsc.svn\examples\javascript\UltraApplicationWithFSharp\Library1\Module1.fs	24	5	Library1


type MyContract() =
    let magicHtml = new ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDiv ("hello")
    member this.GetText() = magicHtml.innerText
    member this.GetContainer() = magicHtml

    member this.MakeBlue() = magicHtml.style.color = "blue" ignore