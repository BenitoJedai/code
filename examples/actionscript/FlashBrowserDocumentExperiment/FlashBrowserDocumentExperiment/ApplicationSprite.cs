using Abstractatech.ActionScript.ConsoleFormPackage;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FlashBrowserDocumentExperiment
{
    public sealed class ApplicationSprite : FlashBrowserDocument.ActionScript.FlashBrowserDocument
    {
        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                   () =>
                   {
                       //content.AttachToContainer(this);
                       //content.AutoSizeTo(this.stage);

                       //content.MouseLeftButtonUp +=
                       //    (sender, e) =>
                       //    {
                       //        var p = e.GetPosition(content);

                       //        Console.WriteLine(new { p.X, p.Y });
                       //    };

                       //ConsoleFormPackageExperience.Diagnostics =
                       //    x => content.t.Text = x;

                   }
             );
        }

    }
}
