extern alias gflare3d;

using flare.basic;
using flare.core;
using Flare3DMeetsStarlingExperiment.Library;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.display3D;
using ScriptCoreLib.Extensions;
using starling.core;
using System;



namespace Flare3DMeetsStarlingExperiment
{

    static class __Scene3D
    {
        [Script(OptimizedCode = "s.render();")]
        public static void __render(this Scene3D s)
        {

        }
    }

    public sealed class ApplicationSprite : Sprite
    {
        private Scene3D scene;



        public ApplicationSprite()
        {
            scene = new Viewer3D(this);
            scene.autoResize = true;
            scene.clearColor.setTo(1, 1, 1);

            Action<ScriptCoreLib.ActionScript.flash.events.Event> contextCreateEvent =
                e =>
                {
                    starlingBack = new Starling(typeof(StarlingBack).ToClassToken(), stage, null, stage.stage3Ds[scene.stageIndex]);
                    starlingBack.start();

                    starlingTop = new Starling(typeof(StarlingTop).ToClassToken(), stage, null, stage.stage3Ds[scene.stageIndex]);
                    starlingTop.start();
                };

            scene.addEventListener(ScriptCoreLib.ActionScript.flash.events.Event.CONTEXT3D_CREATE,
                contextCreateEvent.ToFunction()
            );

            Action<ScriptCoreLib.ActionScript.flash.events.Event> renderEvent =
                e =>
                {
                    // prevents the 3d scene to render.
                    // we'll handle the render by our own.
                    e.preventDefault();

                    // draw starling background.
                    starlingBack.nextFrame();

                    // starling writes the depth buffer, so we need to clear it before draw the 3D stuff.
                    scene.context.clear(0, 0, 0, 1, 1, 0, (uint)Context3DClearMask.DEPTH);

                    // render 3D scene.


                    // jsc: conflicting event vs member
                    scene.__render();

                    // draw starling ui.
                    starlingTop.nextFrame();
                };

            scene.addEventListener(Scene3D.RENDER_EVENT, renderEvent.ToFunction());

            model = scene.addChildFromFile(Model.ToByteArrayAsset());
        }


        private Class Model = KnownEmbeddedResources.Default["assets/Flare3DMeetsStarlingExperiment/vaca.zf3d"];


        private Pivot3D model;
        private Starling starlingBack;
        private Starling starlingTop;




    }
}
