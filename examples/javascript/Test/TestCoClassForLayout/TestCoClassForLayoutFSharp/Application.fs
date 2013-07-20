namespace TestCoClassForLayoutFSharp

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
    open TestCoClassForLayoutFSharp.Design

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    [<Sealed>]
    type Application(page : App) as me = 
        let this = me
        do ()

        // http://apollo13cn.blogspot.com/2012/04/trick-in-f-interop.html
        let f = new SpecialLayoutClass()
        
        do page.PageContainer.Add(f.ThisShouldBeAutomaticAndShouldNotNeedBody)




        let service = new ApplicationWebService()
        do JavaScriptStringExtensions.ToDocumentTitle("Hello world") |> ignore
        // Send data from JavaScript to the server tier
        do service.WebMethod2(
            "A string from JavaScript.",
            fun value -> 
                // Show the server message as document title
                do JavaScriptStringExtensions.ToDocumentTitle(value) |> ignore
                ()

        )

