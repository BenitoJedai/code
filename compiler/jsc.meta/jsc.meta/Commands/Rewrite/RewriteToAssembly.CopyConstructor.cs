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
				ConstructorInfo source, TypeBuilder t,
				VirtualDictionary<Type, Type> tc,
				VirtualDictionary<ConstructorInfo, ConstructorInfo> mc,
				VirtualDictionary<Type, List<FieldBuilder>> TypeFieldCache,
				VirtualDictionary<ConstructorInfo, ConstructorInfo> ConstructorCache,
				VirtualDictionary<MethodInfo, MethodInfo> MethodCache,
				VirtualDictionary<string, string> NameObfuscation,
				Action<MethodBase,ILTranslationExtensions.EmitToArguments> ILOverride
			
			)
		{
			var km = source.IsStatic ?
				t.DefineTypeInitializer() :

				t.DefineConstructor(
				source.Attributes,
				source.CallingConvention,
				source.GetParameters().Select(kp => tc[kp.ParameterType]).ToArray()
			);

			km.SetImplementationFlags(source.GetMethodImplementationFlags());

			source.GetParameters().CopyTo(km);

			mc[source] = km;

			if (source.GetMethodBody() == null)
				return;

			var MethodBody = source.GetMethodBody();

			var ExceptionHandlingClauses = MethodBody.ExceptionHandlingClauses.ToArray();


			var x = CreateMethodBaseEmitToArguments(source, tc, TypeFieldCache, ConstructorCache, MethodCache, NameObfuscation, ILOverride, ExceptionHandlingClauses);


			source.EmitTo(km.GetILGenerator(), x);

		}


	}
}
