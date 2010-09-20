namespace UsingXElement

    open ScriptCoreLib
    open ScriptCoreLib.Delegates
    open ScriptCoreLib.Extensions
    open System
    open System.Linq
    open System.Net
    open System.Xml.Linq

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
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
        member this.WebMethod2(e : string, y : Action<XElement>) =
            // Send it back to the caller.
            let source = "<item> value <!-- comment --></item>"

            let doc = XDocument.Parse(source)

            let ch1 = new WebClient();
            
            ch1.Encoding <- System.Text.Encoding.UTF8

            let ch2 = ch1.DownloadString(e);

            let body = ch2.SkipUntilIfAny("<body>").TakeUntilLastIfAny("</body>").Replace("src=\"", "src=\"" + e.TakeUntilLastIfAny("/") + "/")
            let style = ch2.SkipUntilIfAny("<style type=\"text/css\">").TakeUntilLastIfAny("</style>")

            doc.Root.Value <- "<style>" + style + "</style><div>" + body + "</div>";

//            doc.Root.Value <- doc.Root.Value + " yay :)"

            do y.Invoke(doc.Root)
            ()


//        member this.WebMethod3(e : (string * string), y : StringAction) =
//            // Send it back to the caller.
//           
//            
//            ()