namespace FSharpHelloExperiment

    open ScriptCoreLib
    open ScriptCoreLib.Delegates
    open ScriptCoreLib.Extensions
    open System
    open System.Linq
    open System.Xml.Linq

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    [<Sealed>]
    type ApplicationWebService() as me = 
        let this = me
        do ()


        // Attempt by method 'FSharpHelloExperiment.Global.Invoke(ScriptCoreLib.Ultra.WebService.InternalWebMethodInfo)' 
        // to access field 'FSharpHelloExperiment.ApplicationWebService.init@14' failed.

        member this.WebMethod2(e : string, y : Action<string>) =
            // Send it back to the caller.
            let ee = DateTime.Now.ToString() + " " + e
            do y.Invoke(ee)
            ()

