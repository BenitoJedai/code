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
		public string GetDecoratedTypeNameOrPointerName(Type z)
		{
			if (z == typeof(void)) 
				return "void";


			if (z == typeof(object))
				return "void*";

			if (z.IsGenericParameter)
				return "void*";

			if (z != typeof(string) && !z.IsArray && !z.IsPrimitive && z.IsClass)
			{
				return ("struct tag_" + GetDecoratedTypeName(z, false) + "*");
			}

			return GetDecoratedTypeName(z, false, true);
		}

		public string GetDecoratedTypeName(Type z, bool bExternalAllowed, bool bPointer)
		{
			if (z == typeof(void))
				return "void";

			if (z.IsDelegate())
				if ((ResolveImplementation(z) ?? z).ToScriptAttributeOrDefault().IsNative)
				{
					var _Invoke = z.GetMethod("Invoke");
					var _Parameters = _Invoke.GetParameters();

					var w = new StringBuilder();

					w.Append(GetDecoratedTypeNameOrPointerName(_Invoke.ReturnType));
					w.Append("(*)");
					w.Append("(");

					for (int i = 0; i < _Parameters.Length; i++)
					{
						if (i > 0)
							w.Append(", ");

						w.Append(GetDecoratedTypeNameOrPointerName(_Parameters[i].ParameterType));
					}

					w.Append(")");

					return w.ToString();
				}

			if (z.IsEnum)
				return GetDecoratedTypeName(Enum.GetUnderlyingType(z), bExternalAllowed);

			if (z.IsArray)
			{
				return GetDecoratedTypeName(z.GetElementType(), true) + "*";
			}

			if (z == typeof(IntPtr)) return "void*";
			if (z == typeof(object)) return "void*";

			// see: http://www.space.unibe.ch/comp_doc/c_manual/C/CONCEPT/data_types.html
			// see: http://msdn.microsoft.com/en-us/library/s3f49ktz%28VS.71%29.aspx

			if (z == typeof(bool)) return "int";

			if (z == typeof(byte)) return "unsigned char";

			if (z == typeof(int)) return "int";
			if (z == typeof(uint)) return "unsigned int";

			if (z == typeof(short)) return "short";
			if (z == typeof(ushort)) return "unsigned short";
			if (z == typeof(char)) return "unsigned char";

			if (z == typeof(string)) return "char*";
			if (z == typeof(long)) return "long long";
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

				// we could use full type names
				return u;

				// or we could show metadata tokens
				//return "___" + z.MetadataToken.ToString("x8");
			}

			if (z.IsGenericParameter)
				return "void*";

			Type impl = ResolveImplementation(z);

			if (impl != null)
				return GetDecoratedTypeName(impl, bExternalAllowed, bPointer);
			//if (z.BaseType == typeof(global::System.MulticastDelegate))
			//    return "void*";

			Script.CompilerBase.BreakToDebugger("type not supported: " + z.FullName + " ; consider adding [ScriptAttribute]");

			return "unknown";
		}
	}


}
