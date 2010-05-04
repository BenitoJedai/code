

open global.System
open global.System.Reflection
open global.ScriptCoreLib.Ultra.WebService

namespace UltraApplicationWithAssets

[<Sealed>]
 type UltraWebService() =
    member 
        this.WebMethod1(x : string,  yieldreturn : Action<string>) =
     
     let y = x + DateTime.Now.ToString()
     
     do yieldreturn.Invoke(y)

    member 
        this.Serve1( x : WebServiceHandler) =
            if x.Context.Request.Path = "/serve" then 
                x.Context.Response.ContentType <- "text/html"
                x.Context.Response.Write("hi")
                x.CompleteRequest.Invoke() |> ignore