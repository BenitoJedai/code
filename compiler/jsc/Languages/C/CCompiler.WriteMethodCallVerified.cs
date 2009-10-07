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
		public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
		{
			ScriptAttribute s = ScriptAttribute.Of(m.DeclaringType);

			if (s == null)
			{
				WriteDecoratedMethodName(ResolveImplementationMethod(m.DeclaringType, m), false);
			}
			else
			{
				if (m.DeclaringType.IsDelegate())
					if (m.Name == "Invoke")
						if (s.IsNative)
						{
							// A native delegate is a static function pointer

							Write("(");
							WriteTypeCast(m.DeclaringType);
							Emit(p, i.StackBeforeStrict[0], m.DeclaringType);
							Write(")");
							Write("(");
							WriteParameters(p, m, i.StackBeforeStrict.Skip(1).ToArray(), 0, m.GetParameters().Skip(1).ToArray(), false, ",");
							Write(")");
							return;
						}

				WriteDecoratedMethodName(m, false);
			}

			int offset = 1;

			if (m.IsStatic)
				offset = 0;

			WriteParameterInfoFromStack(m, p, i.StackBeforeStrict, offset);
		}

	}


}
