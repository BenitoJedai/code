using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
//using starling.filters;

namespace TestVectorOfNumber
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.TestDriversWithAudio\Library\StarlingGameSpriteWithTestDriversWithAudio.cs

            //var n = new Vector<Number>();

            //var c = new starling.filters.ColorMatrixFilter();


            ////       public static implicit operator Vector<T>(T[] e);
            //c.concat(
            //    (Vector<double>)
            //    new double[] { 5 });


            // V:\web\TestVectorOfNumber\ApplicationSprite.as(49): col: 28 Error: Implicit coercion of a value of type __AS3__.vec:Vector.<String> to
            // an unrelated type __AS3__.vec:Vector.<Number>.

            //c.concat(
            //    (Vector<string>)
            //    new string[] { "?" });

            // http://stackoverflow.com/questions/3432408/change-stage-background-color-in-as3
            // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/display/Stage.html#color
            this.stage.color = 0xff0000;


        }

    }
}
