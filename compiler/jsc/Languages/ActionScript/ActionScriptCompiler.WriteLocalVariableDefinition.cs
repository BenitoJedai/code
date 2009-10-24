using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler
    {

		public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
		{
			WriteIdent();

			Write("var ");
			WriteVariableName(u.DeclaringType, u, v);
			Write(":");

			WriteDecoratedTypeNameOrImplementationTypeName(v.LocalType, true, true, IsFullyQualifiedNamesRequired(u.DeclaringType, v.LocalType));

			if (v.LocalType.IsValueType && !v.LocalType.IsPrimitive && !v.LocalType.IsEnum)
			{
				var z = MySession.ResolveImplementation(v.LocalType) ?? v.LocalType;

				// define default ctor
				if (z.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new Type[0], null) == null)
					Break("valuetype " + z.ToString() + " - " + z.Namespace + "." + z.Name + " must define a default .ctor");


				WriteAssignment();
				WriteKeywordSpace(Keywords._new);
				WriteDecoratedTypeNameOrImplementationTypeName(z, true, true, IsFullyQualifiedNamesRequired(u.DeclaringType, z));
				Write("()");
			}

			WriteLine(";");
		}



    }
}
