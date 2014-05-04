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
    type internal Application(page : IDefault ) as me = 
        let this = me

        (* Let's port the WebGLSpiral C# template into F#

        01. Add the .vert and .frag files to create the new AssetsLibrary by editing .fsproj
        02. Test run this project to verify it still works
        04. Commit to svn

        *)


        // wtf?
//enter ReferencedConcepts
//0958:02:01 RewriteToAssembly error: System.IO.FileLoadException: The given assembly name or codebase was invalid. (Exception from HRESULT: 0x80131047)
//   at System.Reflection.AssemblyName.nInit(RuntimeAssembly& assembly, Boolean forIntrospection, Boolean raiseResolveEvent)
//   at System.Reflection.AssemblyName..ctor(String assemblyName)
//   at System.Reflection.Assembly.LoadWithPartialName(String partialName)
//   at jsc.meta.Commands.Reference.ReferenceJavaScriptDocument.<>c__DisplayClass29c.<InternalInvoke>b__23e(<>f__AnonymousType$3418$64`2 <>h__TransparentIdentifier200)



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

