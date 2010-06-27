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
            ILTranslationContext context
            )
        {

            var km = default(ConstructorBuilder);

            if (SourceConstructor.IsStatic)
            {
                km = DeclaringType.DefineTypeInitializer();
            }
            else
            {
                // +		$exception	{"Unable to change after type has been created."}	
                // System.Exception {System.InvalidOperationException}

                var ParameterTypes = context.TypeCache[SourceConstructor.GetParameterTypes()];

                km = DeclaringType.DefineConstructor(
                    SourceConstructor.Attributes,
                    SourceConstructor.CallingConvention,
                    ParameterTypes
                );
            }

            km.SetImplementationFlags(SourceConstructor.GetMethodImplementationFlags());

            SourceConstructor.GetParameters().CopyTo(km);

            context.ConstructorCache[SourceConstructor] = km;

            if (SourceConstructor.GetMethodBody() == null)
                return;

            var MethodBody = SourceConstructor.GetMethodBody();

            var ExceptionHandlingClauses = MethodBody.ExceptionHandlingClauses.ToArray();


            var x = CreateMethodBaseEmitToArguments(
                SourceConstructor,
                NameObfuscation,
                ILOverride,
                ExceptionHandlingClauses,
                context
            );


            SourceConstructor.EmitTo(km.GetILGenerator(), x);

        }


    }
}
