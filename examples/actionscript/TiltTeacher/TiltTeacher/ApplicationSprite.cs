using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.flash.sensors;

namespace TiltTeacher
{
    internal sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        Accelerometer a;

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                delegate()
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);


                    if (Accelerometer.isSupported)
                    {
                        a = new Accelerometer();

                        a.update +=
                          e =>
                          {

                              content.Accelerate(
                                  e.accelerationX,
                                  e.accelerationY,
                                  e.accelerationZ
                              );
                          };

                        a.setRequestedUpdateInterval(1000 / 60);
                    }
                }
            );


            this.click +=
                delegate
                {
                    this.stage.SetFullscreen(true);
                };
        }

    }
}
