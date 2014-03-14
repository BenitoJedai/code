using Abstractatech.ActionScript.ConsoleFormPackage;
using AIRGravatarExperiment;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AIRGravatarExperiment
{
    public sealed class ApplicationSprite : Sprite
    {

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                () =>
                {

                    //Error: Error #2067: The ExternalInterface is not available in this container. ExternalInterface requires Internet Explorer ActiveX, Firefox, Mozilla 1.7.5 and greater, or other browsers that support NPRuntime.
                    //    at Error$/throwError()
                    //    at flash.external::ExternalInterface$/call()
                    //    at Abstractatech.ActionScript.ConsoleFormPackage::ConsoleFormPackageExperience$/Initialize_df02d3dd_06000074()[V:\web\Abstractatech\ActionScript\ConsoleFormPackage\ConsoleFormPackageExperience.as:57]
                    //    at AIRGravatarExperiment::ApplicationSprite/__ctor_b__0_df02d3dd_06000002()[V:\web\AIRGravatarExperiment\ApplicationSprite.as:55]

                    ConsoleFormPackageExperience.Initialize();

                    var content = new ApplicationCanvas();
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);


                }
            );
        }

    }
}
