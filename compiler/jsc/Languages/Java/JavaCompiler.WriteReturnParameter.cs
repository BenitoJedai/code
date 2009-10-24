
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
		public override void WriteReturnParameter(ILBlock.Prestatement _p, ILInstruction _i)
		{
			// is this method called at all?

			var ReturnType = typeof(void);

			if (_i.OwnerMethod is MethodInfo)
				ReturnType = ((MethodInfo)(_i.OwnerMethod)).ReturnType;

			


			if (ReturnType == typeof(bool))
			{
				if (_i.InlineAssigmentValue != null)
				{
					if (_i.InlineAssigmentValue.Instruction.IsStoreLocal)
					{

						WriteReturnParameter(_p, _i.InlineAssigmentValue.Instruction.StackBeforeStrict[0].SingleStackInstruction);

						return;
					}
				}

				if (_i == OpCodes.Ldc_I4_0)
				{
					WriteKeywordFalse();

					return;
				}

				if (_i == OpCodes.Ldc_I4_1)
				{
					WriteKeywordTrue();

					return;
				}
			}

			base.WriteReturnParameter(_p, _i);
		}


    }
}
