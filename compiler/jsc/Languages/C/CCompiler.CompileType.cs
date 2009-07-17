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

				if (z.DeclaringType != null)
					if (z.DeclaringType.ToScriptAttributeOrDefault().IsNative)
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


			if (!IsHeaderOnlyMode)
			{
				#region static variables
				FieldInfo[] sfields = GetAllFields(z);

				foreach (FieldInfo sfield in sfields)
				{
					if (!sfield.IsStatic)
						continue;

					// constants will be inlined
					if (sfield.IsLiteral)
						continue;

					WriteIdent();

					Write(GetDecoratedTypeName(sfield.FieldType, false, true));

					WriteSpace();

					WriteDecoratedTypeName(z);
					Write("_");
					Write(sfield.Name);

					WriteLine(";");
				}

				WriteLine();
				#endregion
			}

			WriteTypeStaticMethods(z, za);



			WriteTypeInstanceMethods(z, za);

			return true;
		}
	}


}
