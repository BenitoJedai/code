using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FlashHeatZeeker
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            stage.align = StageAlign.TOP_LEFT;
            stage.scaleMode = StageScaleMode.NO_SCALE;

            #region bg fill
            var fill = new Sprite().AttachTo(this);

            fill.graphics.beginFill(0xA26D41);
            fill.graphics.drawRect(0, 0, this.stage.stageWidth, this.stage.stageHeight);

            this.stage.resize +=
                delegate
                {
                    fill.graphics.beginFill(0xA26D41);
                    fill.graphics.drawRect(0, 0, this.stage.stageWidth, this.stage.stageHeight);
                };
            #endregion

            var egocenter = new Sprite();

            var map = new Sprite().AttachTo(egocenter);

            map.graphics.beginFill(0xB27D51);
            map.graphics.drawRect(0, 0, 600, 400);


            // http://wiki.openoffice.org/wiki/SVG_User_Experiences
            //Enclosed Exception:
            //The current document is unable to create an element of the requested type (namespace: http://www.w3.org/2000/svg, name: flowRoot).

            //        [Embed(source = "/assets/FlashHeatZeeker/touchdown.svg", mimeType = "image/svg-xml")]
            //        ^

            //U:\web\FlashHeatZeeker\ApplicationSprite.as(104): col: 9: Error: Unable to transcode /assets/FlashHeatZeeker/touchdown.svg.



            KnownEmbeddedResources.Default["assets/FlashHeatZeeker/touchdown.svg"].ToSprite().AttachTo(map).MoveTo(100, 0).With(
                svg =>
                {
                    svg.scaleX = 0.5;
                    svg.scaleY = 0.5;
                }
            );

            // unit 1
            KnownEmbeddedResources.Default["assets/FlashHeatZeeker/greentank.svg"].ToSprite().AttachTo(map).MoveTo(100, 0).With(
             svg =>
             {
                 //svg.scaleX = 0.5;
                 //svg.scaleY = 0.5;
             }
         );

            // unit 2
            // left bottom
            KnownEmbeddedResources.Default["assets/FlashHeatZeeker/greentank.svg"].ToSprite().AttachTo(map).MoveTo(-200, 200).With(
             svg =>
             {
                 //svg.scaleX = 0.5;
                 //svg.scaleY = 0.5;
             }
         );


            KnownEmbeddedResources.Default["assets/FlashHeatZeeker/hill0.svg"].ToSprite().AttachTo(map).MoveTo(0, 0).With(
                svg =>
                {
                    svg.scaleX = 0.2;
                    svg.scaleY = 0.2;
                }
            );

            KnownEmbeddedResources.Default["assets/FlashHeatZeeker/tree0.svg"].ToSprite().AttachTo(map).MoveTo(400, 0).With(
                svg =>
                {
                    svg.scaleX = 0.2;
                    svg.scaleY = 0.2;
                }
            );

            // ego is in center
            map.MoveTo(-300, -200);

            #region egocrosshair
            var egocrosshair = new Sprite().AttachTo(egocenter);

            egocrosshair.graphics.lineStyle(2, 0x007f00, 1);

            egocrosshair.graphics.moveTo(-32, 0);
            egocrosshair.graphics.lineTo(32, 0);


            egocrosshair.graphics.moveTo(0, -32);
            egocrosshair.graphics.lineTo(0, 32);
            #endregion

            egocenter.AttachTo(this);



            egocenter.MoveTo(this.stage.stageWidth / 2, this.stage.stageHeight / 2);
            this.stage.resize +=
               delegate
               {
                   egocenter.MoveTo(this.stage.stageWidth / 2, this.stage.stageHeight / 2);
               };
        }

    }
}
