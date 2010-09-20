namespace UsingXElement

    open UsingXElement.HTML.Pages
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
    open ScriptCoreLib.Shared.Lambda

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    [<Sealed>]
    type internal Application(page : IDefaultPage) as me = 
        let this = me
        do ()
        let service = new ApplicationWebService()
        do JavaScriptStringExtensions.ToDocumentTitle("Hello world") |> ignore
        
        let a = Enumerable.Range(0, 18)

        let a = 
            a.ForEach(
                fun (i:int) ->
                    fun (next:Action) ->

                        let uri = "http://ctocorner.com/fsharp/book/ch" + Convert.ToString( i) + ".aspx"

                        do uri.ToDocumentTitle() |> ignore

                        do service.WebMethod2(
                            uri,
                            fun value -> 
                                // Show the server message as document title
                                let div = new IHTMLDiv()

                                div.innerHTML <- value.Value

                                page.Content.Add(div)

                                do next.Invoke()

                                ()

                        )


                        ()
            )


