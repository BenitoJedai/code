// For more information please visit us at:
// http://www.jsc-solutions.net/

namespace FSharpCamera

    open System
    open System.Linq
    open System.Xml.Linq
    open ScriptCoreLib
    open ScriptCoreLib.Extensions
    open ScriptCoreLib.Delegates

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    [<Sealed>]
    type ApplicationWebService() =
        do ()

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript. JSC supports string data type for all platforms.</param>
        /// <param name="y">A callback to javascript. In the future all platforms will allow Action&lt;XElementConvertable&gt; delegates.</param>
        member this.WebMethod2(e : String, y : StringAction) =
            // Send it back to the caller.
            do y.Invoke(e)
            ()

