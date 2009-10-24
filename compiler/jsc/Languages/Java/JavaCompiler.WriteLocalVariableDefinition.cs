
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Threading;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {
		public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
		{
			WriteIdent();

			WriteDecoratedTypeNameOrImplementationTypeName(v.LocalType, true, true);
			WriteSpace();

			//WriteVariableType(v.LocalType, true);

			WriteVariableName(u.DeclaringType, u, v);

			if (v.LocalType.IsValueType && !v.LocalType.IsPrimitive && !v.LocalType.IsEnum)
			{
				var z = MySession.ResolveImplementation(v.LocalType) ?? v.LocalType;

				// define default ctor
				if (z.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new Type[0], null) == null)
					Break("valuetype " + z.ToString() + " - " + z.Namespace + "." + z.Name + " must define a default .ctor");


				WriteAssignment();
				WriteKeywordSpace(Keywords._new);
				WriteDecoratedTypeNameOrImplementationTypeName(z, true, true);
				Write("()");
			}


			WriteLine(";");
		}

    }
}
