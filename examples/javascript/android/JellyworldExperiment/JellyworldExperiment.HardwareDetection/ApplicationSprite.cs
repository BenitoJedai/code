using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.Extensions;
using System;

namespace JellyworldExperiment.HardwareDetection
{
    public sealed class ApplicationSprite : Sprite, IApplicationSprite
    {
        public event Action FoundMutedCamera;
        public event Action FoundUnmutedCamera;

        public event Action FoundCamera;
        public event Action LookingForCamera;

        public void RaiseLookingForCamera()
        {
            if (LookingForCamera != null)
                LookingForCamera();
        }


        public void RaiseFoundCamera()
        {
            if (FoundCamera != null)
                FoundCamera();
        }

        public void RaiseFoundMutedCamera()
        {
            if (FoundMutedCamera != null)
                FoundMutedCamera();
        }

        public void RaiseFoundUnmutedCamera()
        {
            if (FoundUnmutedCamera != null)
                FoundUnmutedCamera();
        }

        public ApplicationSprite()
        {
        }

        public void InitializeContent()
        {
            this.InternalInitializeContent();
        }
    }

    public interface IApplicationSprite
    {
        void RaiseFoundCamera();
        void RaiseLookingForCamera();
        void RaiseFoundMutedCamera();
        void RaiseFoundUnmutedCamera();
    }

    public static class ApplicationSpriteContent
    {


        public static void InternalInitializeContent<TApplicationSprite>(this TApplicationSprite that)
            where TApplicationSprite : Sprite, IApplicationSprite
        {
            that.RaiseLookingForCamera();


            Video video = new Video(500, 380);

            Camera.getCamera().With(
                camera =>
                {
                    // http://help.adobe.com/en_US/ActionScript/3.0_ProgrammingAS3/WS5b3ccc516d4fbf351e63e3d118a9b90204-7d49.html

                    camera.status +=
                        e =>
                        {
                            if (e.code == "Camera.Muted")
                                that.RaiseFoundMutedCamera();


                            if (e.code == "Camera.Unmuted")
                                that.RaiseFoundUnmutedCamera();
                        };

                    camera.setMode(500, 380, 41);

                    video.attachCamera(camera);
                    video.AttachTo(that);

                    that.RaiseFoundCamera();
                }
            );
        }

    }
}
