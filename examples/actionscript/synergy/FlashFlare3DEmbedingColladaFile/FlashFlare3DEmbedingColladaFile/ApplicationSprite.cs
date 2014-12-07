using flare.basic;
using flare.core;
using flare.loaders;
// where is iterator defined??
using Flare3DWaterShipComponent;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FlashFlare3DEmbedingColladaFile
{
    public sealed class ApplicationSprite : Sprite
    {
        // alphawindow and flash debugger wont work. ppapi will.

        public readonly Camera3D camera = new Camera3D();

        public readonly Viewer3D scene;

        public ApplicationSprite()
        {
            //this.stage.scaleMode = StageScaleMode.SHOW_ALL;

            // http://wiki.flare3d.com/index.php?title=Test19_-_Embeding_Collada_File

            this.scene = new Viewer3D(this);
            camera.setPosition(200, 200, -300);
            camera.lookAt(0.0, 50, 0.0);

            scene.camera = camera;

            //scene.library.addItem("Soldier_new-1.jpg", new Texture3D(
            //    KnownEmbeddedResources.Default["assets/FlashFlare3DEmbedingColladaFile/Soldier_new-1.jpg"].ToBitmapAsset()
            //    ));



            //new knocked_out_7(scene);
            //new knocked_out_7(scene) { x = 200 };
            new HZCannon(scene);

            scene.autoResize = true;

            //this.scene.setViewport(0, 0, this.stage.stageWidth, this.stage.stageHeight);
            
            //this.stage.resize +=
            //    delegate
            //    {
            //        this.scene.setViewport(0, 0, this.stage.stageWidth, this.stage.stageHeight);
            //    };

            this.addChild(new net.hires.debug.Stats());
        }

    }

    public class knocked_out_7 : ColladaLoader
    {
        public knocked_out_7(Viewer3D scene)
            : base(
                KnownEmbeddedResources.Default["assets/FlashFlare3DEmbedingColladaFile/knocked_out_7.dae"].ToXMLAsset()
                )
        {
            this.parent = scene;
            this.load();
        }
    }

    public class HZCannon : ColladaLoader
    {
        public HZCannon(Viewer3D scene)
            : base(
                KnownEmbeddedResources.Default["assets/FlashFlare3DEmbedingColladaFile/HZCannon.dae"].ToXMLAsset()
                )
        {
            scene.library.addItem("HZCannon_capture_009_04032013_192834.png", new Texture3D(
                KnownEmbeddedResources.Default["assets/FlashFlare3DEmbedingColladaFile/HZCannon_capture_009_04032013_192834.png"].ToBitmapAsset()
                ));


            this.parent = scene;
            this.load();
        }
    }
}
