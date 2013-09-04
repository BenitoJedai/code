using FlashAsyncWhenReady;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FlashAsyncWhenReady
{
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                () =>
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);
                }
            );
        }

        public void WhenReady(Action<string> yield)
        {
            yield("ok");
        }
    }

    public static class X
    {

        public static TaskAwaiter<string> GetAwaiter(this ApplicationSprite sprite)
        {
            var s = new TaskCompletionSource<string>();


            sprite.WhenReady(s.SetResult);

            return s.Task.GetAwaiter();
        }
    }
}
