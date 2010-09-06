// For more information please visit us at:
// http://www.jsc-solutions.net/

namespace FSharpCamera

    open System
    open System.Text
    open System.Linq
    open System.Xml.Linq
    open ScriptCoreLib
    open ScriptCoreLib.JavaScript
    open ScriptCoreLib.JavaScript.DOM
    open ScriptCoreLib.JavaScript.DOM.HTML
    open ScriptCoreLib.JavaScript.Components
    open ScriptCoreLib.JavaScript.Extensions
    open ScriptCoreLib.Extensions
    open ScriptCoreLib.Delegates
    open FSharpCamera.HTML.Pages
    open FSharpCamera.Components
    open FSharpCamera

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    [<Sealed>]
    type Application(page : IDefaultPage) =
        do ()
        let service = new ApplicationWebService()
        // Initialize MySprite1
        let s = new MySprite1()
        let e = s.ToHTMLElement()

        do s.AttachSpriteTo(page.PageContainer) |> ignore

        let Update() =
            let w = page.SizeShadow.scrollWidth
            let h = page.SizeShadow.scrollHeight
            do e.style.SetSize(w, h)
            0

        do Update() |> ignore

        do Native.Window.add_onresize(
            fun (_) -> Update() |> ignore
        )

        do "Hello world".ToDocumentTitle() |> ignore
        // Send data from JavaScript to the server tier
        do service.WebMethod2(
            "A string from JavaScript.",
            fun(value : String) -> 
                // Show the server message as document title
                do value.ToDocumentTitle() |> ignore
                ()
        )

