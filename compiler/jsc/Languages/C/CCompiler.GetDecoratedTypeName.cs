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
		public string GetDecoratedTypeName(Type z, bool bExternalAllowed, bool bPointer)
		{
			if (z.IsEnum)
				return GetDecoratedTypeName(Enum.GetUnderlyingType(z), bExternalAllowed);

			if (z.IsArray)
			{
				return GetDecoratedTypeName(z.GetElementType(), true) + "*";
			}

			if (z == typeof(IntPtr)) return "void*";
			if (z == typeof(object)) return "void*";

			// see: http://www.space.unibe.ch/comp_doc/c_manual/C/CONCEPT/data_types.html

			if (z == typeof(bool)) return "int";
			if (z == typeof(int)) return "int";
			if (z == typeof(int)) return "int";
			if (z == typeof(short)) return "short int";
			if (z == typeof(ushort)) return "unsigned short int";

			if (z == typeof(uint)) return "unsigned int";
			if (z == typeof(void)) return "void";
			if (z == typeof(string)) return "char*";
			if (z == typeof(char)) return "unsigned char";
			if (z == typeof(long)) return "signed long";
			if (z == typeof(double)) return "double";

			ScriptAttribute t = ScriptAttribute.Of(z);

			if (ScriptAttribute.IsAnonymousType(z))
				t = new ScriptAttribute();

			if (t != null)
			{
				if (t.HasNoPrototype)
				{
					if (bPointer)
						return GetPointerName(z);

					return z.Name;
				}

				if (t.IsNative)
					return "void*";

				string u = GetSafeTypeFullname(z);

				if (bPointer)
					return GetPointerName(z);

				return u;
			}

			if (z.IsGenericParameter)
				return "void*";

			Type impl = ResolveImplementation(z);

			if (impl != null)
				return GetDecoratedTypeName(impl, bExternalAllowed, bPointer);
			//if (z.BaseType == typeof(global::System.MulticastDelegate))
			//    return "void*";

			Script.CompilerBase.BreakToDebugger("type not supported: " + z.FullName);

			return "unknown";
		}
	}


}
