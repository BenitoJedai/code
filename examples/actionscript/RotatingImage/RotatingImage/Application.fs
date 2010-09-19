namespace RotatingImage

    open RotatingImage.HTML.Pages
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

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    [<Sealed>]
    type internal Application(page : IDefaultPage) as me = 
        let this = me
        do ()
        let service = new ApplicationWebService()
        let sprite = new ApplicationSprite()
        do SpriteExtensions.AutoSizeSpriteTo(sprite, page.ContentSize) |> ignore
        do SpriteExtensions.AttachSpriteTo(sprite, page.Content) |> ignore
        do JavaScriptStringExtensions.ToDocumentTitle("Hello world") |> ignore
        // Send data from JavaScript to the server tier
        do service.WebMethod2(
            "A string from JavaScript.",
            fun value -> 
                // Show the server message as document title
                do JavaScriptStringExtensions.ToDocumentTitle(value) |> ignore
                ()

        )

