namespace UltraApplicationWithFSharp1
// http://en.wikibooks.org/wiki/F_Sharp_Programming/Modules_and_Namespaces
open global.ScriptCoreLib.JavaScript.DOM.HTML
open global.ScriptCoreLib.JavaScript
open global.System
    
[<Sealed>]
 type UltraWebService() =
    member 
        this.WebMethod1(x : string,  yieldreturn : Action<string>) =
     let y = x + x
     do yieldreturn.Invoke(y)


[<Sealed>]
 type UltraApplication(e : IHTMLElement) =
    let HelloWorld = new IHTMLButton("FSharp: Hello World")
    do HelloWorld.style.color <- "blue"

    do Native.Document.body.appendChild(HelloWorld)
    do HelloWorld.add_onclick(
        fun (e) ->
            Native.Window.alert("FSharp: Invoke WebService...")
            let w = new UltraWebService()
            do w.WebMethod1("client fsharp. ",
                fun (y) ->
                    Native.Window.alert("FSharp: server: " + y)
            )

    )

