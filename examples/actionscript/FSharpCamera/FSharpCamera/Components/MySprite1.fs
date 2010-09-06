namespace FSharpCamera.Components

    open System
    open System.Text
    open System.Linq
    open System.Xml.Linq
    open ScriptCoreLib.ActionScript.flash.display
    open ScriptCoreLib.ActionScript.flash.media
    open ScriptCoreLib.ActionScript.Extensions
    open ScriptCoreLib.Extensions

    // why are extension methods disabled in here?
    // is it because we are in a folder?
    // fsharp cannot seem to be able to resolve the extensions due to overloading?


    [<Sealed>]
    type MySprite1() as me =
        inherit Sprite()
        let this = me
        let cam1 = Camera.getCamera()
        let vid1 = new Video(this.stage.stageWidth,  this.stage.stageHeight)
        let vid1 = CommonExtensions.AttachTo(vid1, this)  

        do cam1.setMode(640, 480, 1000.0 / 24.0)
        do vid1.attachCamera(cam1)
        do this.stage.scaleMode <- StageScaleMode.NO_BORDER
        do this.stage.add_resize(
            fun (e) -> 
                vid1.width <- float this.stage.stageWidth
                vid1.height <- float this.stage.stageHeight

        )


