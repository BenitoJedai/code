// For more information please visit us at:
// http://www.jsc-solutions.net/

namespace ServerSideBreakpoint

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
    open ServerSideBreakpoint.HTML.Pages
    open ServerSideBreakpoint

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    [<Sealed>]
    type Application(page : IDefaultPage) as me = 
        let this = me
        do ()
        let service = new ApplicationWebService()
        do "Hello world".ToDocumentTitle() |> ignore
        // Send data from JavaScript to the server tier
        do page.Send.add_onclick (
            fun (ee) ->
                do service.WebMethod2(
                    page.Data.value + "(From HTML via F# :)",
                    fun(value : String) -> 
                        // Show the server message as document title
                        do value.ToDocumentTitle() |> ignore
                        ()

                )
        )
