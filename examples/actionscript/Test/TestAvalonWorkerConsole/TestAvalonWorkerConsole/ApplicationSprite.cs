using TestAvalonWorkerConsole;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib.ActionScript.flash.text;

namespace TestAvalonWorkerConsole
{
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                () =>
                {
                    var t = new TextField
                    {
                        multiline = true,

                        //backgroundColor = 0xff000000u,
                        //textColor = 0xffffffffu,

                        autoSize = TextFieldAutoSize.LEFT,

                        text = "..."

                        // X:\jsc.svn\examples\actionscript\Test\TestWorkerConsole\TestWorkerConsole\ApplicationSprite.cs
                    }.AttachToSprite().AsConsole();

                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);
                }
            );
        }

    }
}
