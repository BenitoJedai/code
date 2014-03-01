using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace FlashWorkerExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //C:\util\flex_sdk_4.6\bin\mxmlc.exe
            // -static-link-runtime-shared-libraries=true +configname=airmobile   -debug -verbose-stacktraces -sp=. -swf-version=22 --target-player=11.9.0  -locale en_US -strict -output="V:\web\FlashWorkerExperiment.ApplicationSprite.swf" FlashWorkerExperiment\ApplicationSprite.as
            //Loading configuration file C:\util\flex_sdk_4.6\frameworks\airmobile-config.xml
            //V:\web\FlashWorkerExperiment\ApplicationSprite.as(42): col: 40 Error: Call to a possibly undefined method createWorker through a reference with static type Class.

            //                worker1 = WorkerDomain.createWorker(super.loaderInfo.bytes, false);
            //                                       ^





            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
