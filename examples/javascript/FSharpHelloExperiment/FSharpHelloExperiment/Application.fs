namespace FSharpHelloExperiment

    open ScriptCoreLib
    open ScriptCoreLib.Delegates
    open ScriptCoreLib.Extensions
    open ScriptCoreLib.JavaScript
    open ScriptCoreLib.JavaScript.Components
    open ScriptCoreLib.JavaScript.DOM
    open ScriptCoreLib.JavaScript.DOM.HTML
    open ScriptCoreLib.JavaScript.Extensions
    open System
    open System.Linq
    open System.Text
    open System.Xml.Linq
    open FSharpHelloExperiment.Design
    open FSharpHelloExperiment.HTML.Pages

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    [<Sealed>]
    type Application(page : IDefaultPage) as me = 
        let this = me
        do ()
        do 
            let service = new ApplicationWebService()

            Native.Document.title <- "HelloFSharpTeam2"

            let ApplicationView = Extensions.Extensions.AttachToDocument( new IHTMLDiv())
              
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

            let text1 = new IHTMLSpan()

            text1.innerText <- "Hello FSharp Team!"

            text1.add_onclick(
                fun (e) ->
                    Native.Window.alert("Howdy")
            )
            header.Add(text1)

            ApplicationView.Add(c)

            let HelloWorld = new IHTMLButton("FSharp: Hello World")
            
            HelloWorld.style.color <- "blue"

            do c.Add(HelloWorld)
            do HelloWorld.add_onclick(
                fun (e) ->
                    let w = new ApplicationWebService()
                    do w.WebMethod2("client fsharp. ",
                        fun (y) ->
                            let news = new IHTMLDiv("FSharp: server: " + y)
                            do c.Add(
                                   news
                            )
                    )

            )


//            let ShowSplash = new IHTMLButton("Show Splash")
//            do ShowSplash.style.color <- "blue"
//
//            do c.Add(ShowSplash)
//            do ShowSplash.add_onclick(
//                fun (e) ->
//                    let logo = new PromotionWebApplication.AvalonLogo.AvalonLogoCanvas()
//                    let logoc = new IHTMLDiv()
//
//                    logoc.style.SetSize(
//                        PromotionWebApplication.AvalonLogo.AvalonLogoCanvas.DefaultWidth,
//                        PromotionWebApplication.AvalonLogo.AvalonLogoCanvas.DefaultHeight
//                    )
//
//                    logoc.style.position <- IStyle.PositionEnum.relative
//
//                    c.Add(logoc)
//
//                    logo.add_AtClose(
//                        fun () ->
//                            global.ScriptCoreLib.JavaScript.Extensions.Extensions.Orphanize(logoc) |> ignore
//                    )
//
//                    ScriptCoreLib.JavaScript.Extensions.AvalonExtensions.AttachToContainer(logo.Container, logoc) |> ignore
//            
//            )
