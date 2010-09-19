namespace RotatingImage

    open ScriptCoreLib.Extensions
    open ScriptCoreLib.Extensions.Avalon
    open ScriptCoreLib.Shared.Avalon.Extensions
    
    open System
    open System.Linq
    open System.Text
    open System.Windows
    open System.Windows.Controls
    open System.Windows.Media
    open System.Windows.Shapes
    open System.Windows.Markup
    open System.Xml
    open System.Xml.Linq

    [<Sealed>]
    type ApplicationCanvas() as me = 
        inherit Canvas() 
        let this = me
        do ()
        let r = new Rectangle()
        do r.Fill <- Brushes.Red
        do SupportsContainerExtensions.AttachTo(r, this) |> ignore
        do SupportsContainerExtensions.MoveTo(r, 8, 8) |> ignore

        
        do this.add_SizeChanged(
            fun s -> 
                fun e -> 
                    do SupportsContainerExtensions.SizeTo(r, this.Width - 16.0, this.Height - 16.0) |> ignore
                    ()

        )


        
        let i1 = SupportsContainerExtensions.AttachTo(new Avalon.Images.Image1(), this)
        
        let i2 = 
            SupportsContainerExtensions.MoveTo(
                SupportsContainerExtensions.AttachTo(new Avalon.Images.Image1(), this),
                64,
                0
            )
            
        let f a x y (c:IAddChild) =
            let img = new Avalon.Images.Image1()

            let i3 = 
                SupportsContainerExtensions.MoveTo(
                    
                    SupportsContainerExtensions.AttachTo(img, c),
                    (float)x,
                    (float)y
                )

            do i3.RenderTransform <- new RotateTransform(a, 32.0, 32.0)

            
        let start (x: float) (y: float) =
            let cc = new Canvas()

            let rect = new Rect(8.0, 8.0, 48.0, 48.0)
            let clip = new RectangleGeometry(rect)


            cc.Clip <- clip

            let cc = SupportsContainerExtensions.AttachTo(cc, this)
            let cc = SupportsContainerExtensions.MoveTo(cc, x, y)
            
            (1000 / 30).AtIntervalWithTimerAndCounter(
                fun t ->
                    fun c ->

                        let a = (float) (c + 2) * -7.0

                        if a > 360.0 then t.Stop()

                        let xx = 0.0
                        let yy = 0.0

                        do f a xx yy cc
            ) |> ignore 

        do this.MouseLeftButtonUp.Add(
            fun e ->
                let p = e.GetPosition(this)

                let x = p.X- 32.0
                let y = p.Y - 32.0

                start x y
        )
        
 
        
