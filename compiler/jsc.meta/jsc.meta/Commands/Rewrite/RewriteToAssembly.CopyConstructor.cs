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
		public void CopyConstructor(
				ConstructorInfo source, TypeBuilder t, VirtualDictionary<Type, Type> tc, VirtualDictionary<ConstructorInfo, ConstructorInfo> mc,
				VirtualDictionary<Type, List<FieldBuilder>> TypeFieldCache)
		{
			var km = source.IsStatic ?
				t.DefineTypeInitializer() :

				t.DefineConstructor(
				source.Attributes,
				source.CallingConvention,
				source.GetParameters().Select(kp => tc[kp.ParameterType]).ToArray()
			);

			km.SetImplementationFlags(source.GetMethodImplementationFlags());

			mc[source] = km;

			if (source.GetMethodBody() == null)
				return;

			MethodBase mb = source;

			mb.EmitTo(km.GetILGenerator(),
				new ILTranslationExtensions.EmitToArguments
				{
					// we need to redirect any typerefs and methodrefs!

					TranslateTargetField = TargetField => TypeFieldCache[TargetField.DeclaringType].Single(k => k.Name == TargetField.Name),

				}
			);

		}


	}
}
