namespace BrowserAvalonApplication3

    open ScriptCoreLib.Extensions
    open ScriptCoreLib.Shared.Avalon.Extensions
    open System
    open System.Linq
    open System.Text
    open System.Windows
    open System.Windows.Controls
    open System.Windows.Media
    open System.Windows.Shapes
    open System.Xml
    open System.Xml.Linq
    open System.Windows.Input;

    [<Sealed>]
    type ApplicationCanvas() as me = 
        inherit BrowserAvalonApplicationWithAdobeFlash2.ApplicationCanvas()
        let this = me
        do ()
        let r = new Rectangle()
        
            
        do r.Fill <- Brushes.Red
        do r.Cursor <- Cursors.Hand
        do SupportsContainerExtensions.AttachTo(r, this) |> ignore
        do SupportsContainerExtensions.MoveTo(r, 8, 8) |> ignore
        do this.add_SizeChanged(
            fun s -> 
                fun e -> 
                    do SupportsContainerExtensions.SizeTo(r, this.Width - 16.0, (this.Height - 16.0) * 0.3) |> ignore
                    ()

        )

        do r.add_MouseLeftButtonUp(
            fun s ->
                fun e ->
                    do r.Fill <- Brushes.Green
        )

          member this.WebMethod2(e : string, y : Action<string>) =
            do Console.WriteLine() 
            ()

