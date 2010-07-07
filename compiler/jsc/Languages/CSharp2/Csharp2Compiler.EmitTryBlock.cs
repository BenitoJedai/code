using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;
using ScriptCoreLib.Extensions;

namespace jsc.Languages.CSharp2
{
    partial class CSharp2Compiler
    {
        public override bool EmitTryBlock(ILBlock.Prestatement p)
        {
            if (p.Block.IsTryBlock)
            {

                WriteIdent();
                WriteLine("try");


                ILBlock.PrestatementBlock b = p.Block.Prestatements;

                bool _pop = false;
                bool _leave = OpCodeExtensions.IsOpCodeLeave(b.Last) && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First;

                EmitScope(b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last));


            }
            else if (p.Block.IsHandlerBlock)
            {


                WriteIdent();



                ILBlock.PrestatementBlock b = p.Block.Prestatements;

                bool _pop = b.First == OpCodes.Pop && (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause);
                bool _leave =
                    b.Last == OpCodes.Endfinally
                ||
                    (OpCodeExtensions.IsOpCodeLeave(b.Last) && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First);

                b = b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last);

                b.RemoveNopOpcodes();

                if (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause)
                {
                    Write("catch");

                    if (p.Block.Clause.CatchType == typeof(object))
                    {
                        WriteLine();
                    }
                    else
                    {
                        Write(" (");
                        ILBlock.Prestatement set_exc = p.Block.Prestatements.PrestatementCommands[0];
                        WriteVariableName(p.Block.OwnerMethod.DeclaringType, p.Block.OwnerMethod, set_exc.Instruction.TargetVariable);
                        Write(":");
                        WriteDecoratedTypeNameOrImplementationTypeName(p.Block.Clause.CatchType, true, true, IsFullyQualifiedNamesRequired(p.Block.OwnerMethod.DeclaringType, p.Block.Clause.CatchType));


                        // remove the set command
                        b.PrestatementCommands.RemoveAt(0);
                        WriteLine(")");
                    }


                    

                    EmitScope(b);
                }
                else
                {
                    WriteLine("finally");
                    EmitScope(b);
                }

                // additional space
                WriteLine();
            }
            else
            {
                return false;
            }

            return true;
        }

   
    }
}
