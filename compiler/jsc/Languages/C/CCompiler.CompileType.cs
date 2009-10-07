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
		public override bool CompileType(Type z)
		{
			if (z.IsDelegate())
			{
				// native function pointers 

				if ((ResolveImplementation(z) ?? z).ToScriptAttributeOrDefault().IsNative)
					return false;
			}

			Console.WriteLine(z.FullName);


			ScriptAttribute za = ScriptAttribute.Of(z);


			if (za.Implements == null || !za.Implements.IsPrimitive && za.Implements != typeof(string))
			{
				// not all impl types can have ctors...

				if (z.IsAbstract && z.IsSealed)
				{
					// skip em
				}
				else
				{
					WriteTypeInstanceConstructors(z);
				}
			}



			WriteTypeStaticMethods(z, za);



			WriteTypeInstanceMethods(z, za);

			return true;
		}

		private void WriteStaticFields(Type z)
		{
			// see: http://ee.hawaii.edu/~tep/EE160/Book/chap14/subsection2.1.1.6.html

			FieldInfo[] sfields = z.GetFields(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public);

			foreach (FieldInfo sfield in sfields)
			{
				if (!sfield.IsStatic)
					continue;

				// constants will be inlined
				if (sfield.IsLiteral)
					continue;

				WriteIdent();

				if (sfield.FieldType.IsDelegate() && (ResolveImplementation(sfield.FieldType) ?? sfield.FieldType).ToScriptAttributeOrDefault().IsNative)
				{
					Write(GetDecoratedTypeName(typeof(object), false, true));
				}
				else
					Write(GetDecoratedTypeNameOrPointerName(sfield.FieldType));

				WriteSpace();

				WriteDecoratedTypeName(z);
				Write("_");
				WriteSafeLiteral(sfield.Name);

				WriteLine(";");
			}

			WriteLine();
		}
	}


}
