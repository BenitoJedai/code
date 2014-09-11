extern alias gflare3d;
using flare.basic;
using flare.core;
using Flare3DMeetsStarlingExperiment.Library;
using net.hires.debug;
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
    //........175c:01:01 RewriteToAssembly error: System.Reflection.ReflectionTypeLoadException: Unable to load one or more of the requested types. Retrieve the LoaderExceptions property for more information.
    //   at System.Reflection.RuntimeModule.GetTypes(RuntimeModule module)
    //   at System.Reflection.RuntimeModule.GetTypes()
    //   at System.Reflection.Assembly.GetTypes()
    //   at ScriptCoreLib.ScriptAttribute.OfProvider(ICustomAttributeProvider m) in x:\jsc.svn\compiler\ScriptCoreLibA\ScriptAttribute.OfProvider.cs:line 42
    //   at ScriptCoreLib.CSharp.Extensions.ScriptAttributeExtensions.ToScriptAttributeOrDefault(ICustomAttributeProvider p) in x:\jsc.svn\compiler\ScriptCoreLibA\CSharp\Extensions\ScriptAttributeExtensions.cs:line 18

    static class __Scene3D
    {
        // still need it?
        [Script(OptimizedCode = "s.render();")]
        public static void __render(this Scene3D s)
        {

        }
    }

    // go faster!
    [SWF(frameRate = 70)]
    public sealed class ApplicationSprite : Sprite
    {
        private Scene3D scene;

        //    Error: Error #3709: The depthAndStencil flag in the application descriptor must match the enableDepthAndStencil Boolean passed to configureBackBuffer on the Context3D object.
        //at flash.display3D::Context3D/configureBackBuffer()
        //at flare.basic::Scene3D/stageContextEvent()[Z:\projects\flare3d 2.5\src\flare\basic\Scene3D.as:393]



        public ApplicationSprite()
        {
            scene = new Viewer3D(this);


            scene.autoResize = true;

            // this wont work for partial builds
            // 
            scene.clearColor.setTo(1, 1, 1);

            Action<ScriptCoreLib.ActionScript.flash.events.Event> contextCreateEvent =
                e =>
                {
                    // https://github.com/PrimaryFeather/Starling-Framework/issues/74

                    starlingBack = new Starling(typeof(StarlingBack).ToClassToken(), stage, null, stage.stage3Ds[scene.stageIndex]);
                    starlingBack.start();

                    starlingTop = new Starling(typeof(StarlingTop).ToClassToken(), stage, null, stage.stage3Ds[scene.stageIndex]);
                    starlingTop.start();


                    // http://www.flare3d.com/support/index.php?topic=1101.0
                    this.addChild(new Stats());
                };


            // why isnt the event found by jsc flash natives gen?
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

                    // this wont work for partial builds
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
