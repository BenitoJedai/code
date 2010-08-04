using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Linq;

using IntPtr = global::System.IntPtr;

using ScriptCoreLib;

using jsc.Script;
using ScriptCoreLib.CSharp.Extensions;


namespace jsc.Languages.C
{
	partial class CCompiler
	{
		public override void WriteMethodSignature(MethodBase m, bool dStatic)
		{
			WriteIndent();

			if (m is MethodInfo)
			{
				MethodInfo mi = m as MethodInfo;

				if (mi.ReturnType.IsDelegate() && (ResolveImplementation(mi.ReturnType) ?? mi.ReturnType).ToScriptAttributeOrDefault().IsNative)
					Write(GetDecoratedTypeName(typeof(object), false));
				else
					Write(GetDecoratedTypeName(mi.ReturnType, true, true));
				//Write(GetDecoratedTypeNameWithinNestedName( mi.ReturnType));

			}
			else
			{
				Write(GetDecoratedTypeName(m.DeclaringType, true, true));
			}

			WriteSpace();

			WriteDecoratedMethodName(m, false);

			Write("(");
			WriteMethodParameterList(m);
			Write(")");

			if (!WillEmitMethodBody())
			{
				Write(";");
			}


			WriteLine();
		}
	}


}
