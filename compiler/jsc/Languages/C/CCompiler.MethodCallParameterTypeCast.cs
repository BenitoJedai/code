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
		public override void MethodCallParameterTypeCast(Type context, Type p, ILFlow.StackItem si)
		{
			// we know what the method expects
			// it may be int, but it may also be T that we might not know yet
			// we should be told if the parameter had been built from generic type
			// we also know the opcode that will give us the expected value

			//WriteBoxedComment("typecast for method call: " + p);

			// if we are calling to a generic slot then we should know 
			// that we cannot write more than 32bit's at this time
			// to write more bits we'd probably need another version
			// of that class somewhere

			Write("(");
			if (p.IsGenericType)
			{
				Write("void*");
			}
			else
			{
				Write(GetDecoratedTypeName(p.ToScriptAttributeOrDefault().Implements == typeof(string) ? typeof(string) : p, true, true));
			}
			Write(")");
		}
	}


}
