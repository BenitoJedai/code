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

//no implementation for Microsoft.FSharp.Core.FSharpRef`1[FSharpHelloExperiment.Application] 6e4c211e-6a34-3d82-b5fa-c2daa12f13bc
//script: error JSC1000: No implementation found for this native method, please implement [Microsoft.FSharp.Core.FSharpRef`1.set_contents(FSharpHelloExperiment.Application)]



//067c:02:01 RewriteToAssembly error: System.IO.FileLoadException: The given assembly name or codebase was invalid. (Exception from HRESULT: 0x80131047)
//   at System.Reflection.AssemblyName.nInit(RuntimeAssembly& assembly, Boolean forIntrospection, Boolean raiseResolveEvent)
//   at System.Reflection.AssemblyName..ctor(String assemblyName)
//   at System.Reflection.Assembly.LoadWithPartialName(String partialName)
//   at jsc.meta.Commands.Reference.ReferenceJavaScriptDocument.<>c__DisplayClass175.<InternalInvoke>b__237(<>f__AnonymousType54`2 <>h__TransparentIdentifier3)

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    [<Sealed>]
    type Application(page : IDefault ) as me = 
        let this = me
        do ()
        do 
            let service = new ApplicationWebService()

            Native.document.title <- "HelloFSharpTeam2"

            // fck u fsharp
            // Error	1	A unique overload for method 'AttachToDocument' could not be determined based on type information prior to this program point. A type annotation may be needed. Candidates: Extensions.AttachToDocument<'T when 'T :> INodeConvertible<INode>>(e: 'T) : 'T, Extensions.AttachToDocument<'T when 'T :> INodeConvertible<INode>>(e: Collections.Generic.IEnumerable<'T>) : Collections.Generic.IEnumerable<'T>	X:\jsc.svn\examples\javascript\fsharp\FSharpHelloExperiment\FSharpHelloExperiment\Application.fs	30	35	FSharpHelloExperiment


//            let ApplicationView = Extensions.Extensions.AttachToDocument( new IHTMLDiv())
            let ApplicationView =  new IHTMLDiv()

            do page.body.appendChild(ApplicationView )

              
              // Error	3	Lookup on object of indeterminate type based on information prior to this program point. A type annotation may be needed prior to this program point to constrain the type of the object. This may allow the lookup to be resolved.	X:\jsc.svn\examples\javascript\fsharp\FSharpHelloExperiment\FSharpHelloExperiment\Application.fs	32	13	FSharpHelloExperiment


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
                    Native.window.alert("Howdy")
            )
            header.Add(text1)

            ApplicationView.Add(c)

            let HelloWorld = new IHTMLButton("FSharp: Hello World")
            
            HelloWorld.style.color <- "blue"

            do c.Add(HelloWorld)

            // Severity	Description	Project	File	Line
//Error	A unique overload for method 'add_onclick' could not be determined based on type information prior to this program point. A type annotation may be needed. Candidates: IHTMLElement.add_onclick(value: Action<IEvent<IHTMLButton>>) : unit, IHTMLElement.add_onclick(value: Action<IEvent>) : unit	FSharpHelloExperiment	Application.fs	99


//            HelloWorld.add_onclick()

            do HelloWorld.add_onclick(
                fun (e : IEvent<IHTMLButton>) ->
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
