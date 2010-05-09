namespace UltraApplicationWithAssets


    open global.System
    open global.System.Xml.Linq
    open global.System.Reflection
    open global.ScriptCoreLib.Ultra.WebService


    /// <summary>Hello <b>UltraWebService</b></summary>
    [<Sealed>]
    type UltraWebService() =
        /// <summary>
        /// Method WebMethod2 is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript</param>
        /// <param name="y">A callback to javascript</param>
        member this.WebMethod2x(e : XElement, y : Action<XElement>) =
            ()

        /// <summary>Hello <see>UltraWebService</see></summary>
        member this.WebMethod2(x : string) =
    //        let y = x + "1"
            ()

        member this.WebMethod1(x : string,  yieldreturn : Action<string>) =
         
         let y = x + DateTime.Now.ToString()
         
         do yieldreturn.Invoke(y)
         ()

        member 
            this.Serve1( x : WebServiceHandler) =
                if x.Context.Request.Path = "/serve" then 
                    x.Context.Response.ContentType <- "text/html"
                    x.Context.Response.Write("hi")
                    x.CompleteRequest.Invoke() |> ignore