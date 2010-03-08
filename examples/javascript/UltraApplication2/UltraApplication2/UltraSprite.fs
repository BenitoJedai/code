// Learn more about F# at http://fsharp.net


namespace UltraApplication2


// http://en.wikibooks.org/wiki/F_Sharp_Programming/Modules_and_Namespaces
open global.System
open global.System.Reflection
open global.ScriptCoreLib.ActionScript.flash.display
open global.ScriptCoreLib.ActionScript.Extensions

[<Sealed>]
 type UltraSprite() =
    inherit Sprite()

    [<Literal>]
    let DefaultWidth = 100
    [<Literal>]
    let  DefaultHeight = 100


    // http://www.caribousoftware.com/BobsBlog/archive/2009/09/05/f-classes-in-progress.aspx
    let BackgroundSprite = new Sprite()

    do
        BackgroundSprite.useHandCursor <- true
        BackgroundSprite.buttonMode <- true
        BackgroundSprite.graphics.beginFill(0x7070u)
        BackgroundSprite.graphics.drawRect(0., 0., (float)DefaultWidth, (float)DefaultHeight)

    // http://stackoverflow.com/questions/324947/f-this-expression-should-have-type-unit-but-has-type-consolekeyinfo
        base.addChild(BackgroundSprite) |> ignore

        base.add_click(
            fun (y) -> 
                do
                    BackgroundSprite.graphics.beginFill(0x700000u)
                    BackgroundSprite.graphics.drawRect(0., 0., (float)DefaultWidth, (float)DefaultHeight)
        )

    member this.AtClick(h: Action) = 
        base.add_click(
            fun (y) -> 
                do
                    BackgroundSprite.graphics.beginFill(0x70u)
                    BackgroundSprite.graphics.drawRect(0., 0., (float)DefaultWidth, (float)DefaultHeight)
                    h.Invoke()
        )
