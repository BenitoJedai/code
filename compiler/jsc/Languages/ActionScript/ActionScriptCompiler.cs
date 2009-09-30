﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ScriptCoreLib;
using System.Reflection.Emit;
using jsc.Script;
using System.Runtime.InteropServices;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler : Script.CompilerCLike
    {
        public static string FileExtension = "as";

        public readonly AssamblyTypeInfo MySession;

        public ActionScriptCompiler(TextWriter xw, AssamblyTypeInfo xs)
            : base(xw)
        {
            MySession = xs;

            CreateInstructionHandlers();
        }







        Type CachedArrayEnumeratorType;

        public Type GetArrayEnumeratorType()
        {
            return CachedArrayEnumeratorType ?? (CachedArrayEnumeratorType = (from i in MySession.ImplementationTypes
                                                                              let a = i.ToScriptAttribute()
                                                                              where a != null
                                                                              where a.IsArrayEnumerator
                                                                              select i).SingleOrDefault());
        }

        public override void WriteArrayToCustomArrayEnumeratorCast(Type Enumerable, Type ElementType, ILBlock.Prestatement p, ILFlow.StackItem s)
        {
            var x = GetArrayEnumeratorType();
            if (x == null)
                throw new Exception("SZArrayEnumerator is missing");

            var ArrayToEnumerator = x.GetImplicitOperators(null, null).Single();

            WriteDecoratedTypeNameOrImplementationTypeName(x, false, false, IsFullyQualifiedNamesRequired(p.DeclaringMethod.DeclaringType, x));
            Write(".");
            WriteDecoratedMethodName(ArrayToEnumerator, false);
            Write("(");

            Emit(p, s);

            Write(")");

        }

   




        public override void WriteAbstractMethodBody(MethodBase m)
        {
            WriteIdent();
            WriteLine("{ throw new Error(\"Abstract method not implemented\"); }");
        }

        public override Predicate<ILBlock.Prestatement> MethodBodyFilter
        {
            get
            {
                return
                 delegate(ILBlock.Prestatement p)
                 {
                     // note that instance constructor returns pointer to instance

                     #region remove redundant returns
                     if (p.Instruction != null)
                         if (p.Instruction == OpCodes.Ret)
                             if (p.Instruction.Next == null)
                                 if (p.Instruction.StackBeforeStrict.Length == 0)
                                 {
                                     return true;
                                 }
                     #endregion


                     return false;
                 };
            }
        }

        /// <summary>
        /// a special opcode emit mode
        /// </summary>
        bool WriteCall_DebugTrace_Assign_Active;

        public override void WriteCall_DebugTrace_AfterAssign(MethodInfo m, ILBlock.Prestatement p)
        {
            if (p.Instruction.TargetVariable == null)
                return;

            WriteIdent();
            WriteDecoratedMethodName(m, false);
            Write("(");

            Write("\"");

            Write(" [ ");
            WriteVariableName(p.Instruction.OwnerMethod.DeclaringType, p.Instruction.OwnerMethod, p.Instruction.TargetVariable);
            Write(" ] \" + ");
            WriteVariableName(p.Instruction.OwnerMethod.DeclaringType, p.Instruction.OwnerMethod, p.Instruction.TargetVariable);

            Write(")");
            WriteLine(";");
        }

        public override void WriteCall_DebugTrace_Assign(MethodInfo m, ILBlock.Prestatement p)
        {
            if (WriteCall_DebugTrace_Assign_Active)
                return;

            WriteIdent();
            WriteDecoratedMethodName(m, false);
            Write("(");

            Write("\"");

            WriteCall_DebugTrace_Assign_Active = true;
            WriteLine_NewLineEnabled = false;
            WriteIdent_Enabled = false;

            EmitInstruction(p, p.Instruction);

            WriteIdent_Enabled = true;
            WriteLine_NewLineEnabled = true;
            WriteCall_DebugTrace_Assign_Active = false;

            Write("\"");

            Write(")");
            WriteLine(";");
        }

		public void WriteSafeLiteralWithoutTypeNameClash(string e)
		{
			// Ideally we would use the escape sign like @ in c#
			// we need to do that for each keyword and native type it seems...

			if (e == "Error")
			{
				WriteSafeLiteral("_" + e);
				return;
			}

			WriteSafeLiteral(e);
		}
    }


}

