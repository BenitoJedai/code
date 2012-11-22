using System;
using jp.maaash.ObjectDetection;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Extensions;

namespace IntegrationToFaceInput
{
    internal sealed class ApplicationSprite : ApplicationSpriteX
    {
    
    }

    public class ApplicationSpriteX : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSpriteX()
        {

            this.InvokeWhenStageIsReady(
                delegate()
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);

                    var cw = 640 * 0.2;

                    ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.MoveTo(
                         content.t, cw, 0
                    );

                    var x = new Main().AttachTo(this);

                    x.InvokeWhenStageIsReady(
                        delegate
                        {
                            CommonExtensions.CombineDelegate(
                                 x.detector, 
                                 new Action<ObjectDetectorEvent>(
                                     e =>
                                    {
                                        if (e.rects.Length < 1)
                                            return;

                                        var r = (Rectangle)e.rects[0];

                                        content.t.Text = "" + new { r.left, r.top, r.right, r.bottom };
                                    }
                                    )
                                 , ObjectDetectorEvent.DETECTION_COMPLETE);

                            //x.detector.detectionComplete +=
                            //    e =>
                            //    {
                            //        if (e.rects.Length < 1)
                            //            return;

                            //        var r = e.rects[0];

                            //        content.t.Text = "" + new { r.left, r.top, r.right, r.bottom };
                            //    };
                        }
                    );
                }
            );
        }

    }
}
