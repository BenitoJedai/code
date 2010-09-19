namespace RotatingImage

    open ScriptCoreLib.ActionScript.Extensions
    open ScriptCoreLib.ActionScript.flash.display
    open ScriptCoreLib.Extensions
    open ScriptCoreLib.ActionScript.Extensions

    [<Sealed>]
    type internal ApplicationSprite() as me = 
        inherit Sprite() 
        let this = me
        do ()
        let content = new ApplicationCanvas()
        do CommonExtensions.InvokeWhenStageIsReady(
            this,
            fun ()  -> 
                do AvalonExtensions.AttachToContainer(content, this) |> ignore
                do ActionScriptAvalonExtensions.AutoSizeTo(content, this.stage) |> ignore
                ()

        )

