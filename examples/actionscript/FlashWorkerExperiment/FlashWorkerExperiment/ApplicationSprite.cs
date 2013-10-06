using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace FlashWorkerExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201310/20131006-air

            //System.ArgumentException: Parameter count does not match passed in argument value count.
            //   at System.Reflection.Emit.CustomAttributeBuilder.InitCustomAttributeBuilder(ConstructorInfo con, Object[] constructorArgs, PropertyInfo[] namedProperties, Object[] propertyValues, FieldInfo[] namedFields, Object[] fieldValues)
            //   at System.Reflection.Emit.CustomAttributeBuilder..ctor(ConstructorInfo con, Object[] constructorArgs, PropertyInfo[] namedProperties, Object[] propertyValues, FieldInfo[] namedFields, Object[] fieldValues)
            //   at jsc.meta.Library.CostumAttributeBuilderExtensions.<>c__DisplayClass7c.<ToCustomAttributeBuilder>b__68(ILTranslationContext context)
            //   at jsc.meta.Commands.Reference.ReferenceAssetsLibrary.    .     (TypeBuilder , FileInfo , String )
            //   at jsc.meta.Commands.Reference.ReferenceAssetsLibrary.    .     (AssemblyRewriteArguments )
            //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.InternalInvoke()
            //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.InternalInvokeWithCache()


            //        ReferenceError: Error #1065: Variable flash.system::WorkerDomain is not defined.
            //at FlashWorkerExperiment::ApplicationSprite()[S:\web\FlashWorkerExperiment\ApplicationSprite.as:31]

            //{ isSupported = true }
            // http://esdot.ca/site/2012/intro-to-as3-workers-hello-world


            if (Worker.current.isPrimordial)
            {
                // http://forums.adobe.com/thread/1171498

                var t = new TextField
                {
                    text = new
                    {
                        WorkerDomain.isSupported,
                        Worker.current.isPrimordial,

                        this.loaderInfo.bytes.length
                    }.ToString(),

                    autoSize = TextFieldAutoSize.LEFT
                };

                t.AttachTo(this);

                //                C:\util\flex_sdk_4.6\bin\mxmlc.exe
                // -static-link-runtime-shared-libraries=true +configname=airmobile   -debug -verbose-stacktraces -sp=. -swf-version=17 --target-player=11.4.0  -locale en_US -strict -output="V:\web\FlashWorkerExperiment.ApplicationSprite.swf" FlashWorkerExperiment\ApplicationSprite.as
                //Loading configuration file C:\util\flex_sdk_4.6\frameworks\airmobile-config.xml
                //V:\web\FlashWorkerExperiment\ApplicationSprite.as(42): col: 40 Error: Call to a possibly undefined method createWorker through a reference with static type Class.

                //                worker1 = WorkerDomain.createWorker(super.loaderInfo.bytes, false);
                //                                       ^

                // http://jacksondunstan.com/articles/1968
                // "C:\util\flex_sdk_4.6\frameworks\libs\player\11.9\playerglobal.swc"
                // "C:\util\air3-9_sdk_sa_win\frameworks\libs\player\11.9\playerglobal.swc"

                var w = WorkerDomain.createWorker(
                    this.loaderInfo.bytes
                );

                w.start();
            }
            else
            {

            }
        }

    }
}
