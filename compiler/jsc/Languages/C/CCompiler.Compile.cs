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
		public void Compile(Assembly a)
		{
			WriteMachineGeneratedWarning();

			// we will need to specify the headers

			if (IsHeaderOnlyMode)
			{
				//foreach (var h in SharedHelper.LoadReferencedAssemblies(a, false))
				//{
				//    if (h.GetCustomAttributes<ScriptTypeFilterAttribute>().Any(k => k.Type == ScriptType.C))
				//        WriteLine("#include \"" + Path.GetFileName(h.Location) + ".h\"");
				//}


				foreach (var ReferencedLibrary in Enumerable.Distinct(
					from u in this.MySession.Types
					from m in u.GetMembers()
					let k = m.ToScriptAttribute()
					where k != null
					where k.LibraryImport != null
					where k.LibraryImport.EndsWith(".lib")
					select k.LibraryImport.Substring(0,k.LibraryImport.Length - ".lib".Length)
				))
				{
					Write("#pragma comment( lib, \"" + ReferencedLibrary + "\" )");

				}

				WriteLine();

				#region write include headers
				foreach (Type u in this.MySession.Types)
				{
					//if (u.Assembly != a)
					//    continue;

					ScriptAttribute s = ScriptAttribute.Of(u);

					if (s == null)
						continue;




					if (s.IsNative)
						if (s.Header != null)
						{
							Write("#include ");
							Write(s.IsSystemHeader ? '<' : '"');
							Write(s.Header);
							Write(s.IsSystemHeader ? '>' : '"');
							WriteLine();
						}



				}
				#endregion

			}
			else
			{
				WriteLine("#include \"" + HeaderFileName + "\"");
			}

			foreach (Type u in this.MySession.Types)
			{
				//if (u.Assembly != a)
				//    continue;


				ScriptAttribute s = ScriptAttribute.Of(u);

				// native types are just a placehodlers, so we skip em
				if (s == null || s.IsNative)
					continue;

				if (u.IsSealed && u.IsAbstract)
				{
					// skip
				}
				else
				{
					// we should actually avoid multiple typedefs'
					// http://stackoverflow.com/questions/888386/resolve-circular-typedef-dependency

					// WriteTypeDefPrototype(u);
				}
			}

			foreach (Type u in this.MySession.Types)
			{
				//if (u.Assembly != a)
				//    continue;

				ScriptAttribute s = ScriptAttribute.Of(u);

				// native types are just a placehodlers, so we skip em
				if (s == null || s.IsNative)
					continue;

				WriteTypeDef(u);
			}



			foreach (Type u in this.MySession.Types)
			{
				//if (u.Assembly != a)
				//    continue;

				ScriptAttribute s = ScriptAttribute.Of(u);

				// native types are just a placehodlers, so we skip em
				if (s == null || s.IsNative)
					continue;

				CompileType(u);
			}

		}

	}


}
