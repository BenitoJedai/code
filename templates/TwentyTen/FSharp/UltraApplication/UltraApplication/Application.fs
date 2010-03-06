// Learn more about F# at http://fsharp.net


namespace UltraApplication


// http://en.wikibooks.org/wiki/F_Sharp_Programming/Modules_and_Namespaces
open global.ScriptCoreLib.JavaScript.DOM.HTML
open global.ScriptCoreLib.JavaScript
open global.ScriptCoreLib.JavaScript.Extensions
open global.System
open global.System.Reflection
open global.ScriptCoreLib.ActionScript.flash.display
open global.ScriptCoreLib.ActionScript.Extensions

// http://stackoverflow.com/questions/2269625/using-assembly-attributes-in-f
[<assembly: AssemblyTitleAttribute("Ultra Application")>] 
[<assembly: AssemblyDescriptionAttribute("Ultra Application. Write javascript, flash and java applets within a F# project. http://jsc-solutions.net")>] 
[<assembly: AssemblyCompanyAttribute("jsc-solutions.net")>] 
do ()


    
[<Sealed>]
 type UltraWebService() =
    member 
        this.WebMethod1(x : string,  yieldreturn : Action<string>) =
     let y = x + x
     do yieldreturn.Invoke(y)

[<Sealed>]
 type UltraSprite() =
    inherit Sprite()

    [<Literal>]
    let DefaultWidth = 100
    [<Literal>]
    let DefaultHeight = 100

    let BackgroundSprite = new Sprite()
    do BackgroundSprite.graphics.beginFill(0x7070u)
    do BackgroundSprite.graphics.drawRect(0., 0., (float)DefaultWidth, (float)DefaultHeight)
//    do BackgroundSprite.useHandCursor = true
//    do BackgroundSprite.buttonMode = true
    do base.add(BackgroundSprite)


[<Sealed>]
 type UltraApplication(e : IHTMLElement) =
    let HelloWorld = new IHTMLButton("FSharp: Hello World")
    do HelloWorld.style.color <- "blue"

    do Native.Document.body.appendChild(HelloWorld)
    do HelloWorld.add_onclick(
        fun (e) ->
            let w = new UltraWebService()
            do w.WebMethod1("client fsharp. ",
                fun (y) ->
                    let news = new IHTMLDiv("FSharp: server: " + y)
                    do Native.Document.body.appendChild(
                           news
                    )
            )

    )

