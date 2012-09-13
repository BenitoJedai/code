namespace VS2012FSharp3WebApplication

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
    open VS2012FSharp3WebApplication.Design
    open VS2012FSharp3WebApplication.HTML.Pages

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    [<Sealed>]
    type Application(page : IDefaultPage) as me = 
        let this = me
        // FSC: error FS1108: The type 'BigInteger' is required here and is unavailable. You must add a reference to assembly 'System.Numerics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.
        do ()
        let service = new ApplicationWebService()

        let x = new IHTMLButton()

        do x.innerText <- "hello world"

        do ScriptCoreLib.JavaScript.Extensions.Extensions.AttachToDocument(x)  |> ignore
            

        do JavaScriptStringExtensions.ToDocumentTitle("Hello world") |> ignore
        // Send data from JavaScript to the server tier
        do service.WebMethod2(
            "A string from JavaScript.",
            fun value -> 
                // Show the server message as document title
                do JavaScriptStringExtensions.ToDocumentTitle(value) |> ignore
                ()

        )

