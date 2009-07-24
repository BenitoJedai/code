
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Threading;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {

		public override void WriteMethodParameterList(MethodBase m)
		{
			ParameterInfo[] mp = m.GetParameters();

			ScriptAttribute ma = ScriptAttribute.Of(m);

			bool bStatic = (ma != null && ma.DefineAsStatic);

			if (bStatic)
			{
				if (m.IsStatic)
				{
					Break("method is already static, but is marked to be declared out of band : " + m.DeclaringType.FullName + "." + m.Name);
				}


				DebugBreak(ma);


				ScriptAttribute sa = ScriptAttribute.Of(m.DeclaringType, false);

				if (sa.Implements == null)
				{
					//WriteDecoratedTypeName(m.DeclaringType);
					WriteDecoratedTypeNameOrImplementationTypeName(m.DeclaringType, true, true);


				}
				else
				{
					//WriteDecoratedTypeName(sa.Implements);
					WriteDecoratedTypeNameOrImplementationTypeName(sa.Implements, true, true);
				}

				// this parameter is on the argument list

				WriteSpace();
				WriteSelf();
			}

			for (int mpi = 0; mpi < mp.Length; mpi++)
			{
				if (mpi > 0 || bStatic)
				{
					Write(",");
					WriteSpace();
				}

				ParameterInfo p = mp[mpi];

				ScriptAttribute za = ScriptAttribute.Of(m.DeclaringType, true);

				// why would we want to write the actual typename of BCL type?
				// it wont be around!
				// maybe for strings and such?
				// but they have to define implementation type then




				if (za.Implements != null && m.DeclaringType == p.ParameterType)
					WriteDecoratedTypeNameOrImplementationTypeName(za.Implements, true, true);
				else
					WriteDecoratedTypeNameOrImplementationTypeName(p.ParameterType, true, true);


				WriteSpace();



				if (string.IsNullOrEmpty(p.Name))
				{
					Write(GetSpecialChar() + "arg" + p.Position);
				}
				else
				{
					Write(p.Name);
				}
			}
		}


    }
}
