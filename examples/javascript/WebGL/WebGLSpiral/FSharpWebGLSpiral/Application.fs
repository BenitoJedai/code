namespace FSharpWebGLSpiral

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
    open FSharpWebGLSpiral.HTML.Pages

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    [<Sealed>]
    type internal Application(page : IDefaultPage) as me = 
        let this = me

        (* Let's port the WebGLSpiral C# template into F#

        01. Add the .vert and .frag files to create the new AssetsLibrary by editing .fsproj
        02. Test run this project to verify it still works
        04. Commit to svn

        *)



        do
            let Button1 = new IHTMLButton()

            Button1.innerText <- "button 1"

            Button1.add_onclick( 
                fun (e) ->
                    do Button1.style.color <- "red"
            )
            
            do JavaScript.Extensions.Extensions.AttachTo(Button1, page.Content) |> ignore

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

