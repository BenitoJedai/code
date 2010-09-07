// For more information please visit us at:
// http://www.jsc-solutions.net/

namespace ToSyndicationFeed

    open System
    open System.Linq
    open System.Xml.Linq
    open System.ServiceModel.Syndication
    open ScriptCoreLib
    open ScriptCoreLib.Extensions
    open ScriptCoreLib.Delegates

    module InternalServerTierModule =
        let ToFeed uri =
            let u = new Uri(uri)

            let feed = u.ToSyndicationFeed()

            //XElement Helper http://langref.org/ruby+fsharp/xml/parsing/xml-parse
            let xname sname = XName.Get sname

            let html = 
                XElement(xname "div",
                    XElement(xname "h1", feed.Title.Text),
                    XElement(xname "ol", 
                        feed.Items.Select(fun (ii: SyndicationItem) -> XElement(xname "li", ii.Title.Text))
                    )
                )

            html

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    [<Sealed>]
    type ApplicationWebService() as me = 
        let this = me
        do ()

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript. JSC supports string data type for all platforms.</param>
        /// <param name="y">A callback to javascript. In the future all platforms will allow Action&lt;XElementConvertable&gt; delegates.</param>
        member this.WebMethod2(u : string, y : XElementAction) =
          
            do y.Invoke(InternalServerTierModule.ToFeed u)

            ()

