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
		public override void WriteDecoratedMethodName(MethodBase z, bool q)
		{

			if (q)
				Write("\"");

			WriteDecoratedMethodName(z);

			if (q)
				Write("\"");
		}

		private void WriteDecoratedMethodName(MethodBase z)
		{
			// some methods are treaded as special
			// for example Main will be translated to main
			if (z.DeclaringType.Assembly == this.MySession.Options.TargetAssemblyReference)
				if (z.Name == "Main" && z.IsStatic)
				{
					Write("main");
					return;
				}

			ScriptAttribute s = ScriptAttribute.Of(z);

			bool WithDecoration = true;

			if (s != null)
			{
				if (s.NoDecoration)
					WithDecoration = false;
			}



			s = ScriptAttribute.Of(z.DeclaringType);

			if (s != null)
			{
				if (s.IsNative)
					WithDecoration = false;
			}

			if (WithDecoration)
			{
				Write(GetDecoratedTypeName(z.DeclaringType, true, false));
				Write("_");
			}

			WriteSafeLiteral(z.Name);

			if (z.Name == "op_Implicit")
			{
				Write("_");
				WriteDecoratedTypeName(((MethodInfo)z).ReturnType);
			}

			if (WithDecoration && (!z.IsStatic || z.GetParameters().Length > 0))
			{
				if (IsOverloadedMethod(z))
				{
					Write("_");
					Write(TokenToString(z.MetadataToken));
				}
			}
		}

	}


}
