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



            //C:\util\flex_sdk_4.6\bin\mxmlc.exe
            // -static-link-runtime-shared-libraries=true +configname=airmobile   -debug -verbose-stacktraces -sp=. -swf-version=22 --target-player=11.9.0  -locale en_US -strict -output="V:\web\FlashWorkerExperiment.ApplicationSprite.swf" FlashWorkerExperiment\ApplicationSprite.as
            //Loading configuration file C:\util\flex_sdk_4.6\frameworks\airmobile-config.xml
            //Adobe Flex Compiler (mxmlc)
            //Version 4.6.0 build 23201
            //Copyright (c) 2004-2011 Adobe Systems, Inc. All rights reserved.

            //C:\util\flex_sdk_4.6\frameworks\airmobile-config.xml(64): Error: unable to open 'libs/air/servicemonitor.swc'

            //        </library-path>



            //copy
            //[12:47:56 PM] Arvo Sulakatko: "C:\util\air13_sdk_win\frameworks\libs\air"
            //[12:47:57 PM] Arvo Sulakatko: to
            //[12:48:06 PM] Arvo Sulakatko: C:\util\flex_sdk_4.6\frameworks\libs\air

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
