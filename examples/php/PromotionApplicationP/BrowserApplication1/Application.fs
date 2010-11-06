// For more information visit:
// http://studio.jsc-solutions.net/

namespace BrowserApplication1

    open System
    open System.Text
    open System.Linq
    open System.Xml.Linq
    open ScriptCoreLib
    open ScriptCoreLib.Avalon
    open ScriptCoreLib.JavaScript
    open ScriptCoreLib.JavaScript.DOM
    open ScriptCoreLib.JavaScript.DOM.HTML
    open ScriptCoreLib.JavaScript.Components
    open ScriptCoreLib.JavaScript.Extensions
    open ScriptCoreLib.Extensions
    open BrowserApplication1.HTML.Pages
    open BrowserApplication1
    open ScriptCoreLib.Shared.Avalon.Extensions
    open TestSolutionBuilderV1.Views;
    open System.Windows.Controls
    open System.Windows.Shapes
    open System.Windows.Media
    open ScriptCoreLib.ActionScript
    open ScriptCoreLib.ActionScript.Extensions
    open ScriptCoreLib.ActionScript.flash.display
    open ScriptCoreLib.Extensions
    
    [<Sealed>]
    type ApplicationCanvas() as me = 
        inherit Canvas() 
        let this = me
        do ()
        let r = new Rectangle()

        do r.Opacity <- 0.5

        do r.Fill <- Brushes.Red
        do SupportsContainerExtensions.AttachTo(r, this) |> ignore
        do SupportsContainerExtensions.MoveTo(r, 8, 8) |> ignore
        do this.add_SizeChanged(
            fun s -> 
                fun e -> 
                    do SupportsContainerExtensions.SizeTo(r, this.Width - 16.0, this.Height - 16.0) |> ignore
                    ()
 
        )

        let c = new JSCSolutionsNETWhiteCarouselCanvas()
        
        do c.CloseOnClick <- false

        do SupportsContainerExtensions.AttachTo(c.Container, this) |> ignore
        




    [<Sealed>]
    [<SWF(backgroundColor = 0x000000u)>]
    type ApplicationSprite() as me = 
        inherit Sprite() 
        let this = me
        do ()

        do this.stage.scaleMode <- StageScaleMode.NO_SCALE

        let player = new JustinTV.Components.MySprite1()

        // um, flash is doing the centering for us :)

//        do 
//            player.x <- 128.0
//            player.y <- 128.0

        let content = new ApplicationCanvas()

        do ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.MoveTo(content, -128, -128) |> ignore

        do CommonExtensions.InvokeWhenStageIsReady(
            this,
            fun ()  -> 
                do this.addChild(player) |> ignore
                do AvalonExtensions.AttachToContainer(content, player.sprite2) |> ignore
                ()
 
        )

        do this.add_click(
            fun e ->
                do this.stage.SetFullscreen(true) |> ignore
        )

        member this.Play (channel : string) =

            // F# try catch not supported by jsc?
            do player.api_play_live(channel)


//            let Intro = new PromotionBrandIntro.ApplicationCanvas()
//            do Intro.Background <- Brushes.Transparent
//
//            do ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.SizeTo(Intro, 640, 480) |> ignore
//            do AvalonExtensions.AttachToContainer(Intro, player.sprite2) |> ignore
//
//            Intro.add_AnimationCompleted(
//                fun () ->
//                    do ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.Orphanize(Intro) |> ignore
//            )
//
//            do Intro.PrepareAnimation.Invoke().Invoke()


            ()

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    [<Sealed>]
    type Application(page : IDefaultPage) =



        do ()

        let ws = new ApplicationWebService()

        do page.HeaderA.style.Opacity <- 0.7

        do page.Animation.Clear()  |> ignore

        let ``is tv`` = Native.Document.location.hash.StartsWith("#/tv");
        let IsStudio = Native.Document.location.hash.StartsWith("#/studio");

        let ActivateTV () =
            page.PageContainer.Clear()
            
            do ScriptCoreLib.JavaScript.Extensions.Extensions.Show(page.HeaderA) |> ignore
            do ScriptCoreLib.JavaScript.Extensions.Extensions.Show(page.InfoA) |> ignore

            do page.InfoA.innerText <- "
            This application was written in F# and C#. 
            TV functionality is provided by JustinTV REST and Flash API's. 
            Click on a thumbnail to change the channel. 
            Click on the video to go fullscreen.
            Notice that the flash content runs layered between this HTML text and the HTML background gradient.
            The server is running on PHP.
            "

            let sprite = new ApplicationSprite()

            sprite.ToTransparentSprite()

            do SpriteExtensions.AutoSizeSpriteTo(sprite, page.ContentSize) |> ignore
            do SpriteExtensions.AttachSpriteTo(sprite, page.Content) |> ignore

            do ws.GetChannels(
                fun list -> 

                    do JustinTV.Foo.ListElements(
                        list, 
                        page.HeaderA,
                        fun channel ->
                            do sprite.Play(channel) |> ignore
                            ()
                    ) 
            )

            ()

        let ActivateStudio () =
             page.PageContainer.Clear();
             let studio = new StudioView(null)
             let studio = Extensions.AttachToDocument( studio.Content)
             ()

        let c = new JSCSolutionsNETWhiteCarouselCanvas()

        do ScriptCoreLib.JavaScript.Extensions.AvalonExtensions.AttachToContainer(c.Container, page.Animation)  |> ignore

//        let top =  (JSCSolutionsNETWhiteCarouselCanvas.DefaultHeight / -2).ToString()

        do
//            page.Animation.style.marginTop <- top + "px"
            c.CloseOnClick <- false
            c.add_AtLogoClick(
                fun () -> 
//                    do AvalonSharedExtensions.NavigateTo( new Uri("http://www.jsc-solutions.net"))
                    Native.Document.location.hash <- "#/studio";
                    ActivateStudio();
            )

            if IsStudio then 
                ActivateStudio()

            if ``is tv`` then
                ActivateTV()

        do "jsc.ee".ToDocumentTitle() |> ignore
        do ws.WebMethod2(
            "jsc.ee - powered by ",
            fun e -> 
                do e.ToDocumentTitle() |> ignore
                ()

        )

