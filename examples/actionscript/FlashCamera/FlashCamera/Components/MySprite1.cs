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

        public MySprite1()
        {
            Camera.getCamera().With(InitializeContent);
        }

        private void InitializeContent(Camera cam1)
        {
            cam1.setMode(DefaultWidth, DefaultHeight, 1000 / 24);

            var vid1 = new Video(DefaultWidth, DefaultHeight);

            vid1.attachCamera(cam1);
            vid1.AttachTo(this);
        }

    }
}
