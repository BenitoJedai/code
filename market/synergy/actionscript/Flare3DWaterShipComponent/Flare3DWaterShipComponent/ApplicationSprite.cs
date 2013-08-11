using flare.basic;
using flare.core;
using flare.loaders;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace Flare3DWaterShipComponent
{
    public class ship : Flare3DLoader
    {
        public ship()
            : base(KnownEmbeddedResources.Default["assets/Flare3DWaterShipComponent/ship.zf3d"])
        {
            this.load();
        }
    }

    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {

            var scene = new Viewer3D(this, "", 0.2);
            scene.camera = new Camera3D();
            scene.camera.setPosition(120, 40, -30);
            scene.camera.lookAt(0, 0, 0);

            var ship1 = new ship();
            scene.addChild(ship1);
        }

    }
}
