using AIRAvalonSVGApplication;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AIRAvalonSVGApplication
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





                    // this aint working either
                    //var svg = new SVGAnonymous.Avalon.Images.Anonymous_LogosSingleWings();
                    //svg.AttachTo(content);

                    var svg = new SVGAnonymous.ActionScript.Images.Anonymous_LogosSingleWings();
                    svg.AttachTo(this);
                }
            );
        }

    }
}
