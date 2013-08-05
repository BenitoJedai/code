namespace FSharpFormsWebServiceWithDesignerViaNuget

    open FSharpFormsWebServiceWithDesignerViaNuget
    open ScriptCoreLib
    open ScriptCoreLib.Delegates
    open ScriptCoreLib.Extensions
    open System
    open System.ComponentModel
    open System.Linq
    open System.Xml.Linq

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    [<Sealed>]
    type ApplicationWebService() as me = 
        inherit Component() 
        let this = me
        do ()
        let mutable components : IContainer = null
        do this.InitializeComponent()

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        member this.WebMethod2(e : string, y : Action<string>) =
            // Send it back to the caller.
            do y.Invoke(e)
            ()

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        member this.InitializeComponent() =
            ()

