namespace FSharpFormsWebServiceWithDesignerViaNuget

    open FSharpFormsWebServiceWithDesignerViaNuget
    open ScriptCoreLib
    open ScriptCoreLib.Delegates
    open ScriptCoreLib.Extensions
    open System
    open System.ComponentModel
    open System.Linq
    open System.Xml.Linq

    type Foo() as me = 
        inherit FormsWebServiceWithDesigner.Library.XDesignedComponent() 
        let this = me
        do ()
        let mutable components : IContainer = null
        do this.InitializeComponent()



        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        member this.InitializeComponent() =
            ()
