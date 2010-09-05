namespace FSharpCamera.Components

    open ScriptCoreLib.ActionScript.flash.display
    open ScriptCoreLib.ActionScript.flash.media
    open ScriptCoreLib.ActionScript.Extensions

    [<Sealed>]
    type MySprite1() =
        do ()
        let cam1 = Camera.getCamera()

        do cam1.setMode(640, 480, Convert.ToInt32(1000 / 24))
        
        let vid1 = new Video(400, 300)

        

