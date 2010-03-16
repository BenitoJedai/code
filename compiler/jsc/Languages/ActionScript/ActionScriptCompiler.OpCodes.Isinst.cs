using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ScriptCoreLib;
using ScriptCoreLib.Shared;
using System.Reflection.Emit;
using jsc.Script;
using System.Runtime.InteropServices;

namespace jsc.Languages.ActionScript
{
	partial class ActionScriptCompiler
	{
		public void OpCodes_Isinst(CodeEmitArgs e)
		{
			//Write("/* is or as */");

			// http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/operators.html#as
			// http://crawlmsdn.microsoft.com/en-us/library/cscsdfbt.aspx
			// expression as type
			// expression is type ? (type)expression : (type)null

			if (e.i.StackBeforeStrict.Length == 1)
			{
				Write("(");

				EmitFirstOnStack(e);

				WriteSpace();
				WriteKeywordSpace(Keywords._as);
				WriteSpace();

				WriteDecoratedTypeNameOrImplementationTypeName(
					e.i.TargetType, false, false,
					IsFullyQualifiedNamesRequired(e.Method.DeclaringType, e.i.TargetType)
				);

				Write(")");
			}
			else
				throw new NotSupportedException();
		}

		public override void WriteInstanceOfOperator(ILInstruction value, Type type)
		{
			EmitInstruction(null, value);

			// http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/compilerWarnings.html
			//  	3556	The instanceof operator is deprecated, use the is operator instead.
			WriteSpace();
			WriteKeywordSpace(Keywords._is);


			WriteDecoratedTypeNameOrImplementationTypeName(
				type, false, false,
				IsFullyQualifiedNamesRequired(value.OwnerMethod.DeclaringType, type)
			);
			//WriteDecoratedTypeName(value.OwnerMethod.DeclaringType, ResolveImplementation(type) ?? type, WriteDecoratedTypeNameOrImplementationTypeNameMode.IgnoreImplementationType);
		}


	}


}
