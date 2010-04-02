// Learn more about F# at http://fsharp.net


namespace UltraApplicationWithJavaScript


// http://en.wikibooks.org/wiki/F_Sharp_Programming/Modules_and_Namespaces
open global.ScriptCoreLib.JavaScript.DOM
open global.ScriptCoreLib.JavaScript.DOM.HTML
open global.ScriptCoreLib.JavaScript
open global.ScriptCoreLib.JavaScript.Extensions
open global.ScriptCoreLib.Ultra.Components.HTML.Pages
open global.System
open global.System.Reflection

// http://stackoverflow.com/questions/2269625/using-assembly-attributes-in-f
[<assembly: AssemblyTitleAttribute("Ultra Application")>] 
[<assembly: AssemblyDescriptionAttribute("Ultra Application. Write javascript, flash and java applets within a F# project. http://jsc-solutions.net")>] 
[<assembly: AssemblyCompanyAttribute("jsc-solutions.net")>] 
do ()
   
[<Sealed>]
 type Application(app : IApplicationLoader) = do
    
    app.LoadingAnimation.FadeOut() |> ignore

    Native.Document.title <- "UltraApplicationWithJavaScript"

    let ApplicationView = Extensions.Extensions.AttachToDocument( new IHTMLDiv())
    do
        ApplicationView.style.position <- IStyle.PositionEnum.absolute
        ApplicationView.style.width <-"100%"
        ApplicationView.style.height <- "100%"
        ApplicationView.style.overflow <- IStyle.OverflowEnum.auto

    let c = new IHTMLDiv()

    c.add_onmouseover(
        fun (e) ->
            c.style.backgroundColor <- "#efefff"
    )

    c.add_onmouseout(
        fun (e) ->
            c.style.backgroundColor <- "#"
    )

    c.style.margin <- "2em"
    c.style.padding <- "2em"
    c.style.border <- "1px solid #777777"
    c.style.borderLeft <- "2em solid #777777"


    let header = new IHTMLDiv();
    let header_a = new IHTMLAnchor();

    header_a.innerText <- "Write javascript, flash and java applets within a F# project."
    header_a.href <- "http://www.jsc-solutions.net"

    header.Add(header_a)
    c.Add(header)

    ApplicationView.Add(c)

    let HelloWorld = new IHTMLButton("FSharp: Hello World")
    do HelloWorld.style.color <- "blue"

    do c.Add(HelloWorld)
    do HelloWorld.add_onclick(
        fun (e) ->
            let w = new UltraWebService()
            do w.WebMethod1("client fsharp. ",
                fun (y) ->
                    let news = new IHTMLDiv("FSharp: server: " + y)
                    do c.Add(
                           news
                    )
            )

    )
