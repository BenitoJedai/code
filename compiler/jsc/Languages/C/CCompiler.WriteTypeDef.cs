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
		private void WriteTypeDef(Type e)
		{
			// we need to reference abstracts also by type pointer name
			if (e.IsAbstract && e.IsSealed)
				return;

			if (e.IsEnum)
				return;

			ScriptAttribute a = ScriptAttribute.Of(e);

			//if (a.Implements != null)
			//    return;


			if (IsHeaderOnlyMode)
			{


				string _typename = GetDecoratedTypeName(e, false, false);
				string _pname = GetPointerName(e);

				#region instance struct

				if (a == null || !a.HasNoPrototype)
				{
					WriteLine();

					#region typedef
					WriteIndent();
					WriteCommentLine(e.FullName);
					WriteIndent();
					WriteLine("typedef struct tag_" + GetDecoratedTypeName(e, false));

					using (CreateScope(false))
					{
						var FieldCount = 0;

						Stack<Type> u = new Stack<Type>();

						Type p = e;

						while (p != typeof(object))
						{
							u.Push(p);
							p = p.BaseType;
						}

						while (u.Count > 0)
						{
							p = u.Pop();

							FieldInfo[] fields = GetAllFields(p);

							if (fields.Length == 0)
							{
		
							}
							else
							{
								foreach (FieldInfo field in fields)
								{
									if (field.IsStatic)
										continue;

									FieldCount++;

									WriteIndent();

									if (field.FieldType.IsDelegate())
										if ((ResolveImplementation(field.FieldType) ?? field.FieldType).ToScriptAttributeOrDefault().IsNative)
										{
											WriteDecoratedTypeName(typeof(object));

											WriteSpace();
											WriteSafeLiteral(field.Name);
											WriteLine(";");
											continue;
										}


									Write(GetDecoratedTypeNameOrPointerName(field.FieldType));

									WriteSpace();
									WriteSafeLiteral(field.Name);
									WriteLine(";");
								}
							}
						}

						if (FieldCount == 0)
						{
							WriteIndent();
							WriteLine("void* __empty;");
						}
					}
					WriteLine(" " + _typename + ", *" + _pname + ";");
					#endregion
				}



				if (!e.IsAbstract)
				{
					WriteLine("#define __new_" + _typename + "(count) \\");
					WriteLine("    (" + _pname + ") malloc(sizeof(" + _typename + ") * count)");
				}

				WriteLine();

				#endregion
			}
		}

	}


}
