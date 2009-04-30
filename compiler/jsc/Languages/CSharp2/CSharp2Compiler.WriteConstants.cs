using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.CSharp2
{
	partial class CSharp2Compiler
	{
		public void WriteConstants(Type z)
		{
			foreach (var f in z.GetFields(BindingFlags.Public | BindingFlags.Static).Where(k => k.IsLiteral))
			{
				WriteIdent();
				WriteKeywordSpace(Keywords._public);
				WriteKeywordSpace(Keywords._const);
				WriteDecoratedTypeName(z, f.FieldType);
				WriteSpace();
				WriteSafeLiteral(f.Name);
				WriteAssignment();

				var value = f.GetRawConstantValue();

				if (value is int)
				{
					WriteNumeric((int)value);
				}
				else throw new NotSupportedException();

				WriteLine(";");
			}
		}
	}
}
