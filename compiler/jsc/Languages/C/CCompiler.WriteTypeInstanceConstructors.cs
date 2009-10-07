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
		private void WriteTypeInstanceConstructors(Type z)
		{
			ConstructorInfo[] zci = GetAllInstanceConstructors(z);

			foreach (ConstructorInfo zc in zci)
			{
				WriteLine();

				WriteMethodHint(zc);
				WriteMethodSignature(zc, false);

				if (WillEmitMethodBody())
					WriteMethodBody(zc, null,
						delegate
						{
							// yay, like a kid in a candy store.
							// seriously, initialize data!

							foreach (var f in z.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
							{
								if (f.FieldType.IsClass)
								{
									this.WriteIdent();


									WriteSelf();
									Write("->");
									WriteSafeLiteral(f.Name);
									WriteAssignment();
									WriteKeywordNull();

									this.WriteLine(";");
								}
							}
						}
					);

			}

			WriteLine();
		}
	}


}
