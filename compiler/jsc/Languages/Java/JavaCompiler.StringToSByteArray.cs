
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
		public override bool StringToSByteArrayProviderImplementation(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
		{
			var x = StringToSByteArrayProvider.GetProvideImplementation(m);

			if (x != null)
			{

				WriteDecoratedTypeNameOrImplementationTypeName(x.TargetMethod.DeclaringType);
				Write(".");
				WriteDecoratedMethodName(x.TargetMethod, false);
				Write("(");

				for (int j = 0; j < x.Arguments.Length; j++)
				{
					if (j > 0)
						Write(", ");

					var SingleStackInstruction = x.Arguments[j].SingleStackInstruction;

					if (SingleStackInstruction.TargetParameter != null)
					{
						Emit(p, i.StackBeforeStrict[SingleStackInstruction.TargetParameter.Position]);
						continue;
					}

					if (SingleStackInstruction.TargetMethod == x.ToSBytes.Method)
					{
						// whats the string ?
						var TheStringParameter = SingleStackInstruction.StackBeforeStrict[0].SingleStackInstruction.TargetParameter;
						var TheStringValue = i.StackBeforeStrict[TheStringParameter.Position].SingleStackInstruction.TargetLiteral;

						var SBytes = x.ToSBytes(TheStringValue);

						WriteKeywordSpace(Keywords._new);
						WriteDecoratedTypeName(typeof(sbyte));
						
						// http://www.janeg.ca/scjp/lang/arrays.html

						Write("[]");

						Write(" { ");

						for (int ii = 0; ii < SBytes.Length; ii++)
						{
							if (ii > 0)
								Write(", ");

							Write(SBytes[ii]);
						}

						Write(" } ");

						continue;
					}

					throw new NotSupportedException();
				}

				Write(")");

				return true;
			}

			return false;
		}
    }
}
