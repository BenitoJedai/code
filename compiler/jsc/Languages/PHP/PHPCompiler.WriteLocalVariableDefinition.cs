using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Script.PHP
{
	partial class PHPCompiler
	{

		public override void WriteLocalVariableDefinition(LocalVariableInfo v, MethodBase u)
		{
			WriteIdent();

			this.Write("$_" + v.LocalIndex);

			//WriteVariableName(u.DeclaringType, u, v);
			WriteAssignment();

			Action<string> Write = e => this.WriteLine(e + ";");

			var LocalType = v.LocalType;

			if (!LocalType.IsValueType)
			{
				Write("NULL");
				return;
			}


			if (LocalType.IsEnum)
				LocalType = Enum.GetUnderlyingType(LocalType);

			if (LocalType.IsPrimitive)
			{
				if (LocalType == typeof(bool))
				{
					Write("false");
					return;
				}

				if (LocalType == typeof(long))
				{
					Write("0");
					return;
				}

				if (LocalType == typeof(int))
				{
					Write("0");
					return;
				}

				if (LocalType == typeof(uint))
				{
					Break("uint is not supported yet. see: http://ee2.php.net/manual/en/language.types.php");

					Write("0");
					return;
				}

				if (LocalType == typeof(byte))
				{
					Write("0");
					return;
				}


				if (LocalType == typeof(char))
				{
					Write("0");
					return;
				}

				if (LocalType == typeof(double))
				{
					Write("0.0");
					return;
				}
			}
			else
			{
				if (LocalType.IsValueType)
				{
					var r = this.ResolveImplementation(LocalType) ?? LocalType;
					var ctor = r.GetConstructor(new Type [] { });

					if (ctor != null)
					{
						this.WriteKeywordSpace(Keywords._new);
						this.WriteDecoratedTypeName(u.DeclaringType, r);
						this.Write("()");
						this.WriteLine(";");
						return;
					}
				}
			}

			BreakToDebugger("WriteLocalVariableDefinition, " + v.LocalType.Name);
		}
	}
}
