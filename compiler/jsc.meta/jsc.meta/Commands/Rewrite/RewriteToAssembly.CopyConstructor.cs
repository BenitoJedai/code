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
				VirtualDictionary<string, string> NameObfuscation)
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

			MethodBase mb = source;

			mb.EmitTo(km.GetILGenerator(),
				new ILTranslationExtensions.EmitToArguments
				{
					// we need to redirect any typerefs and methodrefs!
					TranslateTargetType = TargetType => tc[TargetType],
					TranslateTargetField = TargetField => TypeFieldCache[TargetField.DeclaringType].SingleOrDefault(k => k.Name == NameObfuscation[TargetField.Name]) ?? TargetField,

					TranslateTargetMethod = TargetMethod => MethodCache[TargetMethod],
					TranslateTargetConstructor = TargetConstructor => ConstructorCache[TargetConstructor],
				}
			);

		}


	}
}
