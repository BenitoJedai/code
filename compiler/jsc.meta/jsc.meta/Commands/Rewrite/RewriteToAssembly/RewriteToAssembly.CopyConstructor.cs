using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;
using System.Reflection.Emit;
using jsc.Languages.IL;
using jsc.Library;

namespace jsc.meta.Commands.Rewrite
{
    public partial class RewriteToAssembly
    {
        static void CopyConstructor(
            ConstructorInfo SourceConstructor,
            TypeBuilder DeclaringType,
            VirtualDictionary<string, string> NameObfuscation,
            Action<MethodBase, ILTranslationExtensions.EmitToArguments> ILOverride,
            ILTranslationContext context,
            RewriteToAssembly Command
            )
        {

            var DeclaringConstructor = default(ConstructorBuilder);

            if (SourceConstructor.IsStatic)
            {
                DeclaringConstructor = DeclaringType.DefineTypeInitializer();
            }
            else
            {
                // +		$exception	{"Unable to change after type has been created."}	
                // System.Exception {System.InvalidOperationException}

                var ParameterTypes = context.TypeCache[SourceConstructor.GetParameterTypes()];

                DeclaringConstructor = DeclaringType.DefineConstructor(
                    SourceConstructor.Attributes,
                    SourceConstructor.CallingConvention,
                    ParameterTypes
                );
            }

            DeclaringConstructor.SetImplementationFlags(SourceConstructor.GetMethodImplementationFlags());

            foreach (var SourceParameter in SourceConstructor.GetParameters())
            {
                // http://msdn.microsoft.com/en-us/library/system.reflection.emit.methodbuilder.defineparameter.aspx

                // The position of the parameter in the parameter list. 
                // Parameters are indexed beginning with the number 1 for the first parameter; the number 0 represents the return value of the method. 


                var DeclaringParameter = DeclaringConstructor.DefineParameter(SourceParameter.Position + 1, SourceParameter.Attributes, SourceParameter.Name);

                if ((SourceParameter.Attributes & ParameterAttributes.HasDefault) == ParameterAttributes.HasDefault)
                    DeclaringParameter.SetConstant(SourceParameter.RawDefaultValue);

                // should we copy attributes? should they be opt-out?
                foreach (var item in SourceParameter.GetCustomAttributes(false).Select(kk => kk.ToCustomAttributeBuilder()))
                {
                    DeclaringParameter.SetCustomAttribute(item(context));
                }
            }
            context.ConstructorCache[SourceConstructor] = DeclaringConstructor;

            if (SourceConstructor.GetMethodBody() == null)
                return;

            var MethodBody = SourceConstructor.GetMethodBody();

            var ExceptionHandlingClauses = MethodBody.ExceptionHandlingClauses.ToArray();

            #region EnableSwitchRewrite
            if (Command.EnableSwitchRewrite)
            {
                var xb = new ILBlock(SourceConstructor);

                if (xb.Instructrions.Any(k => k.OpCode == OpCodes.Switch))
                {


                    WriteSwitchRewrite(
                        SourceConstructor, 
                        DeclaringType, 
                        context, 
                        SourceConstructor.GetParameterTypes(), 
                        typeof(void),
                        ILOverride,
                        ExceptionHandlingClauses,
                        xb,

                        DeclaringConstructor.GetILGenerator()
                    );

                   
                    return;
                }
            }
            #endregion


            var x = CreateMethodBaseEmitToArguments(
                SourceConstructor,
                ILOverride,
                ExceptionHandlingClauses,
                context
            );


            SourceConstructor.EmitTo(DeclaringConstructor.GetILGenerator(), x);

        }


    }
}
