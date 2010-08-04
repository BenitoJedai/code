// For more information visit:
// http://studio.jsc-solutions.net/

namespace BrowserApplication1

    open System
    open System.Text
    open System.Linq
    open System.Xml.Linq
    open ScriptCoreLib
    open ScriptCoreLib.Avalon
    open ScriptCoreLib.JavaScript
    open ScriptCoreLib.JavaScript.DOM
    open ScriptCoreLib.JavaScript.DOM.HTML
    open ScriptCoreLib.JavaScript.Components
    open ScriptCoreLib.JavaScript.Extensions
    open ScriptCoreLib.Extensions
    open BrowserApplication1.HTML.Pages
    open BrowserApplication1
    open ScriptCoreLib.Shared.Avalon.Extensions

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    [<Sealed>]
    type Application(page : IDefaultPage) =



        do ()


        do page.Animation.Clear()  |> ignore



        let c = new JSCSolutionsNETWhiteCarouselCanvas()

        do AvalonExtensions.AttachToContainer(c.Container, page.Animation)  |> ignore

        let top =  (JSCSolutionsNETWhiteCarouselCanvas.DefaultHeight / -2).ToString()

        do
            page.Animation.style.marginTop <- top + "px"
            c.CloseOnClick <- false
            c.add_AtLogoClick(
                fun () -> do AvalonSharedExtensions.NavigateTo( new Uri("http://www.jsc-solutions.net"))
            )

        do "Hello world".ToDocumentTitle() |> ignore
        do (new ApplicationWebService()).WebMethod2(
            "hello",
            fun e -> 
                do e.ToDocumentTitle() |> ignore
                ()

        )

