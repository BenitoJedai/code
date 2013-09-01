using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLVideo : IHTMLMedia
    {

        #region Constructor

        public IHTMLVideo()
        {
            // InternalConstructor
        }

        static IHTMLVideo InternalConstructor()
        {
            return (IHTMLVideo)IHTMLElement.InternalConstructor(HTMLElementEnum.video);
        }

        #endregion


        public byte[] bytes
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var c = new CanvasRenderingContext2D(this.clientWidth, this.clientHeight);

                //1:00e2:008a ScriptCoreLib create ScriptCoreLib.JavaScript.DOM.HTML.ImageData
                //RewriteToAssembly error: System.ArgumentNullException: Value cannot be null.
                //Parameter name: meth
                //   at System.Reflection.Emit.ILGenerator.Emit(OpCode opcode, MethodInfo meth)
                //   at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.<.ctor>b__3e(ILRewriteContext e) in x:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs:line 288
                //   at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.<>c__DisplayClassc3.<set_Item>b__c1(ILRewriteContext e) in x:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs:line 984
                //   at jsc.Languages.IL.ILTranslationExtensions.EmitTo(MethodBase SourceMethod, ILGenerator il, EmitToArguments x, TypeBuilder DeclaringType) in x:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.cs:line 270

                //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyMethod(AssemblyBuilder a, ModuleBuilder m, MethodInfo SourceMethod, TypeBuilder DeclaringType, VirtualDictionary`2 NameObfuscation, Assembly PrimarySourceAssembly, Delegate codeinjecton, Func`2 codeinjectonparams, Action`2 ILOverride, Action`3 BeforeInstructions, ILTranslationContext context, RewriteToAssembly Command, VirtualDictionary`2 ShadowTypeCache, VirtualDictionary`2 ShadowSignatureTypeDefinitionCache, Func`4 AtCodeTraceDefineMethod, Action`1 AtCodeTraceDefineGenericParameters) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.CopyMethod.cs:line 947

                //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass3cc.<InternalInvoke>b__32e(MethodInfo SourceMethod) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.cs:line 1974
                //   at jsc.Library.VirtualDictionary`2.RaiseResolve(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 142


                //{ SourceMethod = Void drawImage(ScriptCoreLib.JavaScript.DOM.HTML.IHTMLVideo, Single, Single, Single, Single), TargetMethod =  }
                c.drawImage(
                    this, 0, 0, c.canvas.width, c.canvas.height
                );

                return c.bytes;
            }

        }
    }
}
