// For more information visit:
// http://studio.jsc-solutions.net/

namespace BrowserApplication1

    open System
    open System.Linq
    open System.Xml.Linq
    open ScriptCoreLib
    open ScriptCoreLib.Delegates
    open ScriptCoreLib.Extensions
    open JustinTV

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    [<Sealed>]
    type ApplicationWebService() =
        do ()

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript</param>
        /// <param name="y">A callback to javascript</param>
        member this.WebMethod2(e : string, y : Action<string>) =
            // Send it to the caller.
            do y.Invoke(e + "jsc")
            ()

        member this.GetChannels(y : XElementAction) =
            let i = new JustinTV.ApplicationWebService()

            do i.WebMethod2("", y)