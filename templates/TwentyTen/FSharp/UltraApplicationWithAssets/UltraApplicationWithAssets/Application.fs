// Learn more about F# at http://fsharp.net


namespace UltraApplicationWithAssets


// http://en.wikibooks.org/wiki/F_Sharp_Programming/Modules_and_Namespaces
open global.ScriptCoreLib.JavaScript.DOM
open global.ScriptCoreLib.JavaScript.DOM.HTML
open global.ScriptCoreLib.JavaScript
open global.ScriptCoreLib.JavaScript.Extensions
open global.ScriptCoreLib.Ultra.Components.HTML.Pages
open global.System
open global.System.Reflection
open global.UltraApplicationWithAssets.HTML.Pages
open global.UltraApplicationWithAssets.HTML.Audio.FromAssets
open global.ScriptCoreLib.JavaScript.Runtime


// http://stackoverflow.com/questions/2269625/using-assembly-attributes-in-f
[<assembly: AssemblyTitleAttribute("Ultra Application With Assets")>] 
[<assembly: AssemblyDescriptionAttribute("Ultra Application With Assets. Write javascript, flash and java applets within a F# project. http://jsc-solutions.net")>] 
[<assembly: AssemblyCompanyAttribute("jsc-solutions.net")>] 
do ()
   
   // http://lorgonblog.spaces.live.com/blog/cns!701679AD17B6D310!725.entry

[<Sealed>]
 type Application(a : IAboutJSC) = do
    
    Native.Document.title <- "UltraApplicationWithAssets"

    a.Inline1.add_onclick(
        fun (e) ->
            let t = 
                new Timer(
                    fun t -> 
                        do a.Inline1.style.color <- ""
                )

            t.StartTimeout(5000) |> ignore
    )

    a.Inline1.add_onclick(
        fun e ->
            try 
                let r = new rooster()

                r.play()
            with 
                e -> ()
    )

    a.WebService_GetTime.add_onclick(
        fun e -> 
            let w = new UltraWebService()
     
            do w.WebMethod1("client fsharp. ",
                fun y ->
                    let news = new IHTMLDiv("FSharp: server: " + y)
                    do a.WebServiceContainer.Add(
                           news
                    )
            )

    )

  

