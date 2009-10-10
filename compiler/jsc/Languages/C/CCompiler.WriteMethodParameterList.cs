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
		public override void WriteMethodParameterList(MethodBase m)
		{
			ParameterInfo[] mp = m.GetParameters();

			ScriptAttribute ma = ScriptAttribute.Of(m);

			bool bStatic = (!m.IsStatic && AlwaysDefineAsStatic) || (ma != null && ma.DefineAsStatic);

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
					Write(GetDecoratedTypeName(m.DeclaringType, true, true));

				}
				else
				{
					Write(GetDecoratedTypeName(sa.Implements, true, true));
				}

				if (this.IsHeaderOnlyMode)
				{
					if (!HideParameterNameInHeaderFiles)
					{
						WriteSpace();
						Write("/* ");
						WriteSelf();
						Write(" */");
					}
				}
				else
				{
					WriteSpace();
					WriteSelf();
				}
			}
			else
			{
				if (mp.Length == 0)
					Write("void");
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

				if (p.ParameterType.IsDelegate())
				{
					// C compiler anly seems to allow
					// anonymous method casts within function body
					Write(GetDecoratedTypeNameOrPointerName(typeof(object)));
				}
				else
				{
					if (za.Implements == null || m.DeclaringType.GUID != p.ParameterType.GUID)
						Write(GetDecoratedTypeName(p.ParameterType, true, true));
					else
						Write(GetDecoratedTypeName(za.Implements, true, true));
				}

				if (this.IsHeaderOnlyMode)
				{
					if (!HideParameterNameInHeaderFiles)
					{
						WriteSpace();
						Write("/* ");
						WriteDecoratedMethodParameter(p);
						Write(" */");
					}
				}
				else
				{
					WriteSpace();
					WriteDecoratedMethodParameter(p);
				}

			}
		}
	}


}
