namespace FSharpCamera3.Components

    open System
    open System.Text
    open System.Linq
    open System.Xml.Linq
    open ScriptCoreLib.ActionScript.flash.display
    open ScriptCoreLib.ActionScript.flash.media
    open ScriptCoreLib.ActionScript.Extensions
    open ScriptCoreLib.Extensions

    [<Sealed>]
    type MySprite1() as me = 
        inherit Sprite()
        let this = me
        let cam1 = Camera.getCamera()
        let vid1 = new Video(this.stage.stageWidth,  this.stage.stageHeight)
        let vid1 = CommonExtensions.AttachTo(vid1, this)  

        do cam1.setMode(1024, 768, 1000.0 / 24.0)
        do vid1.attachCamera(cam1)
        do this.stage.scaleMode <- StageScaleMode.NO_BORDER
        do this.stage.add_resize(
            fun (e) -> 
                vid1.width <- float this.stage.stageWidth
                vid1.height <- float this.stage.stageHeight

        )

