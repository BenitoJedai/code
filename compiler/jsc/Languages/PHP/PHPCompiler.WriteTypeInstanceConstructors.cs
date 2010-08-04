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
		private void WriteTypeInstanceConstructors(Type z)
		{
			ConstructorInfo[] zci = z.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

			if (zci.Length == 0)
				return;

			#region cool, we only have one constructor like it should be in PHP
			if (zci.Length == 1)
			{
				foreach (ConstructorInfo zc in zci)
				{
					WriteIndent();
					WriteCommentLine(zc.DeclaringType.FullName + ".ctor");
					WriteMethodSignature(z, zc, false);
					WriteMethodBody(zc);

				}
				WriteLine();

				return;
			}
			#endregion

			// we are about to support multiple constructors
			// yet if our base class only has a single constructor
			// we will fail currently

			var InvalidBaseType = IsMultipleConstructorsSupported(z);

			if (InvalidBaseType)
				Break("Types with multiple constructors must not inherit types with single constructor unless empty. " + z.FullName);

			WriteIndent();
			WriteKeywordSpace(Keywords._function);
			WriteKeyword(Keywords.___construct);
			Write("()");
			WriteLine();

			using (this.CreateScope())
			{
				WriteIndent();
				WriteCommentLine("Multiple constructors are supported via additional methods.");
			}

			WriteLine();

			foreach (ConstructorInfo zc in zci)
			{
				WriteIndent();
				WriteKeywordSpace(Keywords._function);
				WriteDecoratedMethodName(zc, false);

				Write("(");
				WriteMethodParameterList(zc);
				Write(")");
				WriteLine();

				WriteMethodBody(zc, null, null,
					delegate
					{
						WriteIndent();
						WriteKeywordSpace(Keywords._return);
						WriteSelf();
						Write(";");
						WriteLine();
					}
				);

				WriteLine();
			}
		}

		private static bool IsMultipleConstructorsSupported(Type z)
		{
			var InvalidBaseType = false;
			if (z.BaseType != null)
				if (z.BaseType != typeof(object))
				{
					var BaseConstructors = z.BaseType.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
					if (BaseConstructors.Length == 1)
					{
						// if it still is an empty constructor then we can skip it

						var BaseConstructor = BaseConstructors[0];

						if (BaseConstructor.GetParameters().Length == 0)
						{
							var BaseConstructorBlock = new ILBlock(BaseConstructor);

							var BaseConstructorCommands = BaseConstructorBlock.Prestatements.PrestatementCommands
								.Where(k => k.Instruction.OpCode != OpCodes.Nop)
								.Where(k => k.Instruction.OpCode != OpCodes.Ret)
								.ToArray();

							if (BaseConstructorCommands.Length == 1)
							{
								var BaseConstructorCall = BaseConstructorCommands[0];
								var BaseConstructorCallTargetConstructor = BaseConstructorCall.Instruction.TargetConstructor;

								if (BaseConstructorCallTargetConstructor != null)
								{
									if (BaseConstructorCallTargetConstructor.DeclaringType == z.BaseType.BaseType)
										return IsMultipleConstructorsSupported(z.BaseType);
								}

							}
						}

						return true;
					}
				}
			return InvalidBaseType;
		}

	}
}
