using jp.maaash.ObjectDetection;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;

namespace JellyworldExperiment.DualViewWithCamera
{

    public sealed class ApplicationSprite : Sprite, IApplicationSprite
    {
        public void InitializeContent()
        {
            this.InternalInitializeContent(
                (Left, Top, Width, Height) =>
                {
                    if (AverageChanged != null)
                        AverageChanged("" + Left, "" + Top, "" + Width, "" + Height);
                }
            );
        }

        // does not work?
        //public event Action<double, double, double, double> AverageChanged;

        //  Error: Implicit coercion of a value of type int to an unrelated type String.
        public event Action<string, string, string, string> AverageChanged;

    }

    public interface IApplicationSprite
    {
        event Action<string, string, string, string> AverageChanged;
    }

    public static class ApplicationSpriteContent
    {


        public static void InternalInitializeContent<TApplicationSprite>(
            this TApplicationSprite that,
             Action<double, double, double, double> RaiseAverageChanged
        )
            where TApplicationSprite : Sprite, IApplicationSprite
        {

            ApplicationCanvas content = new ApplicationCanvas();

            content.AverageChanged += RaiseAverageChanged;

            that.InvokeWhenStageIsReady(
                delegate()
                {


                    //ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.MoveTo(
                    //     content.t, cw, 0
                    //);

                    var x = new Main();

                    x.alpha = 0.8;

                    x.DisableSetupScene = 1;

                    x.AttachTo(that);

                    content.AttachToContainer(that);
                    content.AutoSizeTo(that.stage);

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

                                         //content.t.Text = "" + new { r.left, r.top, r.right, r.bottom, e.rects.Length };

                                         // faceSprite.graphics.drawRect( e.rects[0].x * scaleFactor, e.rects[0].y * scaleFactor, e.rects[0].width * scaleFactor, e.rects[0].height * scaleFactor );

                                         var scale = 0.2;
                                         var scaleFactor = 4.0;

                                         // flip
                                         //scaleFactor *= .2;

                                         //webcam.scaleX = -.2;
                                         //webcam.x += webcam.width;	

                                         ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.MoveTo(
                                              content.f,
                                             // huh? :p  
                                              (640 - r.x * 4.0) * scale - r.width * scaleFactor * scale
                                                , r.y * scaleFactor * scale
                                             );
                                         ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.SizeTo(
                                             content.f,
                                              r.width * scaleFactor * scale,
                                              r.height * scaleFactor * scale
                                             );




                                         var scalex = content.Width / 640;
                                         var scaley = content.Height / 480;

                                         content.MoveTrackerTo(

                                         //ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.MoveTo(
                                             //     content.r,
                                             // huh? :p  
                                              (640 - r.x * 4.0) * scalex - r.width * scaleFactor * scalex
                                                , r.y * scaleFactor * scaley
                                             //    );
                                             //ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.SizeTo(
                                             //    content.r
                                             ,
                                              r.width * scaleFactor * scalex,
                                              r.height * scaleFactor * scaley
                                             );

                                         //ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.MoveTo(
                                         //    content.f, cw - r.left - r.width / 2,

                                         //    r.top - r.height / 2
                                         //    );
                                         //ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.SizeTo(content.f, r.width, r.height);

                                         //content.f.MoveTo(r.left, r.top);
                                         //x.renderer.alpha = 0.1;

                                     }
                                    )
                                 , ObjectDetectorEvent.DETECTION_COMPLETE);


                        }
                    );
                }
            );
        }

    }
}
