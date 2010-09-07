// For more information please visit us at:
// http://www.jsc-solutions.net/

namespace ToSyndicationFeed

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
    open ToSyndicationFeed.HTML.Pages
    open ToSyndicationFeed

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    [<Sealed>]
    type Application(page : IDefaultPage) as me = 
        let this = me
        do ()
        
        do "Hello world".ToDocumentTitle() |> ignore


        let fetch uri = 
            let service = new ApplicationWebService()
            // Send data from JavaScript to the server tier
            do service.WebMethod2(
                uri,
                fun(html) -> 
                    // Show the server message as document title
                    page.Content.Add(html)
                    ()
            )
            uri
      
        do fetch "http://zproxy.wordpress.com/feed" |> ignore
        do fetch "http://services.community.microsoft.com/feeds/feed/FSharpDevCenterFeaturedContent" |> ignore

