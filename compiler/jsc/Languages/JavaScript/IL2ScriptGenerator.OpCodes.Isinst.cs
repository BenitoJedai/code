
using System;

using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;

using ScriptCoreLib;

using jsc.Script;
using jsc.Languages.JavaScript;

namespace jsc
{
	using ilbp = ILBlock.Prestatement;
	using ili = ILInstruction;
	using ilfsi = ILFlow.StackItem;


	partial class IL2ScriptGenerator
	{
		static void WriteOperatorIs(IdentWriter w, ilbp p, ili _i, ilfsi _s)
		{
			var i = _s.SingleStackInstruction;
			var s = i.StackBeforeStrict.Single();

			w.Write("(");
			OpCodeHandler(w, p, i, s);

			w.WriteSpace();
			w.Write("instanceof");
			// http://developer.mozilla.org/en/Core_JavaScript_1.5_Reference/Operators/Special_Operators/instanceof_Operator
			w.WriteSpace();

			w.WriteDecoratedType(w.Session.ResolveImplementation(i.TargetType) ?? i.TargetType, false);
			w.Write(")");


			// il is like this:
			// (u as T) != null
			// yet javascript supports this:
			// u instanceof T
		}

		static void WriteOperatorAs(IdentWriter w, ilbp p, ili i, ilfsi s)
		{
			// this opcode should unwrap itself from cgt null
			// we might be inside double not operator

			// as operator

			Action<Action> Write =
				expression =>
				{
					w.Write("(");
					expression();

					w.WriteSpace();
					w.Write("instanceof");
					// http://developer.mozilla.org/en/Core_JavaScript_1.5_Reference/Operators/Special_Operators/instanceof_Operator
					w.WriteSpace();

					w.WriteDecoratedType(w.Session.ResolveImplementation(i.TargetType) ?? i.TargetType, false);

					w.WriteSpace();
					w.Write("?");
					w.WriteSpace();

					// this should be a variable

					expression();
					

					w.WriteSpace();
					w.Write(":");
					w.WriteSpace();

					w.Write("null");

					w.Write(")");
				};

			if (s.SingleStackInstruction.IsLoadLocal)
			{
				Write(
					delegate
					{
						OpCodeHandler(w, p, i, s);
					}
				);

			}
			else
			{
				#region as operator for nonlocal variable

				// uniqe variable
				var f = "c$" + i.Offset;

				w.Write("( function () { ");
				
				w.Write("var ");
				w.Write(f);
				w.Helper.WriteAssignment();
				OpCodeHandler(w, p, i, s);
				w.Write("; ");

				w.Write("return ");

				Write(
					delegate
					{
						w.Write(f);
					}
				);
				w.Write(";");

				w.Write(" } )()");

				#endregion
			}
		}

		static void OpCode_isinst(IdentWriter w, ilbp p, ili i, ilfsi[] s)
		{
			WriteOperatorAs(w, p, i, s[0]);
		}
	}

}