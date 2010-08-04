using System;
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

namespace jsc.Languages.CSharp2
{
    partial class CSharp2Compiler : Script.CompilerCLike
    {
        public static string FileExtension = "cs";

        



        public override void WriteTypeSignature(Type z, ScriptAttribute za)
        {
            throw new NotImplementedException();
        }

     

  


     
     

        public override Type[] GetActiveTypes()
        {
            throw new NotImplementedException();
        }


        public override void WriteTypeConstructionVerified()
        {
            throw new NotImplementedException();
        }

  

        public MethodBase ResolveMethod(MethodBase m)
        {
            return
                (m.DeclaringType.ToScriptAttribute() == null
                            ? ResolveImplementationMethod(m.DeclaringType, m)
                            : m);
        }

        public MethodBase ResolveMethod(Type t, MethodBase m)
        {
            var s = m.DeclaringType.ToScriptAttributeOrDefault();

            return
                (s == null
                            ? ResolveImplementationMethod(t, m)
                            : m);
        }

        public bool IsFullyQualifiedNamesRequired(Type context, Type subject)
        {
            if (context != subject && context.Name == subject.Name)
                return true;

            // there is a field with the same name as the type we would be importing
            if (context.GetField(subject.Name) != null)
                return true;

            return GetImportTypes(context).Count(i => i.Name == subject.Name) > 1;
        }


        /// <summary>
        /// a special opcode emit mode
        /// </summary>
        bool WriteCall_DebugTrace_Assign_Active;

        public override void WriteCall_DebugTrace_AfterAssign(MethodInfo m, ILBlock.Prestatement p)
        {
            if (p.Instruction.TargetVariable == null)
                return;

            WriteIndent();
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

            WriteIndent();
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
    }


}
