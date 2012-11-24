using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.Extensions;
using System;

namespace JellyworldExperiment.HardwareDetection
{
    public sealed class ApplicationSprite : Sprite, IApplicationSprite
    {
        //CreateType:  JellyworldExperiment.HardwareDetection.XApplicationSprite
        //error: System.InvalidOperationException: Unable to change after type has been created.
        //   at System.Reflection.Emit.TypeBuilder.ThrowIfCreated()
        //   at System.Reflection.Emit.TypeBuilder.DefineMethodNoLock(String name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
        //   at System.Reflection.Emit.TypeBuilder.DefineMethod(String name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
        //   at System.Reflection.Emit.TypeBuilder.DefineMethod(String name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
        //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyMethod(AssemblyBuilder a, ModuleBuilder m, MethodInfo SourceMethod, TypeBuilder DeclaringType, VirtualDictionary`2 NameObfuscation, Assembly PrimarySourceAssembly, Delegate codeinjecton, Func`2 codeinjectonparams, Action`2 ILOverride, Action`3 BeforeInstructions, ILTranslationContext context, RewriteToAssembly Command, VirtualDictionary`2 ShadowTypeCache, VirtualDictionary`2 ShadowSignatureTypeDefinitionCache, Func`4 AtCodeTraceDefineMethod, Action`1 AtCodeTraceDefineGenericParameters) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.CopyMethod.cs:line 366

        public event Action FoundCamera;

        public void RaiseFoundCamera()
        {
            if (FoundCamera != null)
                FoundCamera();
        }

        public ApplicationSprite()
        {
            this.InitializeContent();
        }
    }

    public interface IApplicationSprite
    {
        event Action FoundCamera;
        void RaiseFoundCamera();
    }

    public static class ApplicationSpriteContent
    {

        public static void InitializeContent<TApplicationSprite>(this TApplicationSprite that)
            where TApplicationSprite : Sprite, IApplicationSprite
        {
            Video video = new Video(500, 380);

            Camera.getCamera().With(
                camera =>
                {
                    camera.setMode(500, 380, 41);

                    video.attachCamera(camera);
                    video.AttachTo(that);

                    that.RaiseFoundCamera();
                }
            );
        }

    }
}
