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
			TypeBuilder t,
			VirtualDictionary<string, string> NameObfuscation,
			Action<MethodBase, ILTranslationExtensions.EmitToArguments> ILOverride,
			ILTranslationContext context

			)
		{
			var km = SourceConstructor.IsStatic ?
				t.DefineTypeInitializer() :

				t.DefineConstructor(
				SourceConstructor.Attributes,
				SourceConstructor.CallingConvention,
				SourceConstructor.GetParameters().Select(kp => context.TypeCache[kp.ParameterType]).ToArray()
			);

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
