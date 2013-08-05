namespace FSharpFormsWebServiceWithDesignerViaNuget

    open FSharpFormsWebServiceWithDesignerViaNuget
    open System.Collections.Generic
    open System.ComponentModel
    open System.Drawing
    open System.Linq
    open System.Text
    open System.Windows.Forms

    [<Sealed>]
    type ApplicationControl() as me = 
        inherit UserControl() 
        let this = me
        do ()
        let mutable components : IContainer = null
        do this.InitializeComponent()

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        member this.InitializeComponent() =
            do this.Name <- "ApplicationControl"
            do this.Size <- new Size(400, 300)
            ()

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        override this.Dispose(disposing : bool) =
            // Note: This jsc project does not support unmanaged resources.
            do base.Dispose(disposing)
            ()

