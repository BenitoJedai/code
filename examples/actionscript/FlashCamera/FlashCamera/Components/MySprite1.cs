using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.flash.media;

namespace FlashCamera.Components
{
    internal sealed class MySprite1 : Sprite
    {
        public const int DefaultWidth = 640;
        public const int DefaultHeight = 480;

        readonly Video video = new Video(DefaultWidth, DefaultHeight);

        public MySprite1()
        {
            Camera.getCamera().With(InitializeContent);
        }

        private void InitializeContent(Camera camera)
        {
            camera.setMode(DefaultWidth, DefaultHeight, 1000 / 24);

            video.attachCamera(camera);
            video.AttachTo(this);
        }

    }
}
