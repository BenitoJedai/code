using flare.basic;
using flare.core;
using flare.loaders;
using Flare3DWaterShipComponent;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FlashFlare3DEmbedingColladaFile
{
    public sealed class ApplicationSprite : Sprite
    {
        public readonly Camera3D camera = new Camera3D();

        public readonly Viewer3D scene;

        public ApplicationSprite()
        {
            // http://wiki.flare3d.com/index.php?title=Test19_-_Embeding_Collada_File

            this.scene = new Viewer3D(this);
            camera.setPosition(200, 200, -300);
            camera.lookAt(0.0, 50, 0.0);

            scene.camera = camera;

            scene.library.addItem("Soldier_new-1.jpg", new Texture3D(
                KnownEmbeddedResources.Default["assets/FlashFlare3DEmbedingColladaFile/Soldier_new-1.jpg"].ToBitmapAsset()
                ));



            new knocked_out_7(scene);
            new knocked_out_7(scene) { x = 200 };
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
}
