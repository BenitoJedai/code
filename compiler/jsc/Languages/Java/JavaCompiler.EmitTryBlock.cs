
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
using System.Linq;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;
using ScriptCoreLib.CSharp.Extensions;
namespace jsc.Languages.Java
{

	partial class JavaCompiler
	{

		public override bool EmitTryBlock(ILBlock.Prestatement p)
		{
			// http://stackoverflow.com/questions/416183/in-java-critical-sections-what-should-i-synchronize-on
			
			// this shall be updated for .net 4:
			// http://www.danielmoth.com/Blog/2009/08/net-4-monitorenter-replaced-by.html

			Action<object> Monitor_Enter = System.Threading.Monitor.Enter;
			Action<object> Monitor_Exit = System.Threading.Monitor.Exit;


			// should we emit a synchronized block instead?

			Action EmitTryBlockContent =
				delegate
				{
					ILBlock.PrestatementBlock b = p.Block.Prestatements;

					bool _pop = false;
					bool _leave = b.Last.IsAnyOpCodeOf(OpCodes.Leave_S, OpCodes.Leave) && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First;

					EmitScope(b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last));
				};

			Func<ILBlock, ILInstruction> CheckFinallyBlockForMonitorExit =
				FinallyBlock =>
				{
					if (FinallyBlock.IsHandlerBlock && FinallyBlock.Clause.Flags != ExceptionHandlingClauseOptions.Clause)
					{
						#region tidy up the finally block
						ILBlock.PrestatementBlock b = FinallyBlock.Prestatements;

						bool _pop = b.First == OpCodes.Pop && (FinallyBlock.Clause.Flags == ExceptionHandlingClauseOptions.Clause);
						bool _leave =
							b.Last == OpCodes.Endfinally
						||
							(b.Last.IsAnyOpCodeOf(OpCodes.Leave_S, OpCodes.Leave) && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First);

						b = b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last);

						b.RemoveNopOpcodes();
						#endregion


						if (b.PrestatementCommands.Count == 1)
						{
							var Monitor_Exit_Call_Instruction = b.PrestatementCommands.Single().Instruction;

							if (Monitor_Exit_Call_Instruction.OpCode == OpCodes.Call)
								if (Monitor_Exit_Call_Instruction.ReferencedMethod == Monitor_Exit.Method)
								{



									return Monitor_Exit_Call_Instruction;
								}

						}
					}

					return null;
				};

			if (p.Block.IsTryBlock)
			{
				// .net 4 changes the way it works!
				// see: http://msdn.microsoft.com/en-us/library/dd289498%28VS.100%29.aspx

				#region are we supposed to emit synchronized block? lets find out
				if (p.Block.Flow != null)
				{
					var ParentFlowBranchInstruction = p.Block.Flow.Parents.Single().Branch;

					#region we can tolerate a Nop instruction
					if (ParentFlowBranchInstruction.OpCode == OpCodes.Nop)
					{
						if (ParentFlowBranchInstruction.BranchSources.Count > 0)
							throw new NotSupportedException();

						ParentFlowBranchInstruction = ParentFlowBranchInstruction.Prev;
					}
					#endregion

					if (ParentFlowBranchInstruction != null)
						if (ParentFlowBranchInstruction.OpCode == OpCodes.Call)
						{
							if (ParentFlowBranchInstruction.ReferencedMethod == Monitor_Enter.Method)
							{
								var Monitor_Exit_Call_Instruction = CheckFinallyBlockForMonitorExit(p.Block.Next);

								if (Monitor_Exit_Call_Instruction != null)
								{
									this.WriteIdent();
									this.WriteKeywordSpace(Keywords._synchronized);
									this.Write("(");
									this.Emit(p.Prev, Monitor_Exit_Call_Instruction.StackBeforeStrict[0]);
									this.Write(")");
									this.WriteLine();
									EmitTryBlockContent();

									return true;
								}
							}
						}
				}
				#endregion


				this.WriteIdent();
				this.WriteKeyword(Keywords._try);
				this.WriteLine();

				EmitTryBlockContent();
			}
			else if (p.Block.IsHandlerBlock)
			{


				WriteIdent();



				ILBlock.PrestatementBlock b = p.Block.Prestatements;

				bool _pop = b.First == OpCodes.Pop && (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause);
				bool _leave =
					b.Last == OpCodes.Endfinally
				||
					(b.Last.IsAnyOpCodeOf(OpCodes.Leave_S, OpCodes.Leave) && b.Last.TargetInstruction == b.OwnerBlock.NextNonClauseBlock.First);

				b = b.ExtractBlock(_pop ? b.First.Next : b.First, _leave ? b.Last.Prev : b.Last);

				b.RemoveNopOpcodes();

				if (p.Block.Clause.Flags == ExceptionHandlingClauseOptions.Clause)
				{
					DebugBreak(p.DeclaringMethod.ToScriptAttribute());

					this.WriteKeywordSpace(Keywords._catch);
					Write("(");

					if (p.Block.Clause.CatchType == typeof(object))
					{
						Write("java.lang.Throwable");
						WriteSpace();
						WriteExceptionVar();
					}
					else
					{
						var ExceptionType = MySession.ResolveImplementation(p.Block.Clause.CatchType) ?? p.Block.Clause.CatchType;
						var ExceptionTypeAttribute = ExceptionType.ToScriptAttribute();

						if (ExceptionTypeAttribute != null && ExceptionTypeAttribute.ImplementationType != null)
							Write(GetDecoratedTypeName(ExceptionTypeAttribute.ImplementationType, true));
						else
							Write(GetDecoratedTypeName(ExceptionType, true));

						WriteSpace();

						ILBlock.Prestatement set_exc = p.Block.Prestatements.PrestatementCommands[0];

						if (set_exc.Instruction.TargetVariable == null)
							Write("__ExceptionValue");
						else
							WriteVariableName(p.Block.OwnerMethod.DeclaringType, p.Block.OwnerMethod, set_exc.Instruction.TargetVariable);

						// remove the set command if there is one
						if (set_exc.Instruction.TargetVariable != null)
							b.PrestatementCommands.RemoveAt(0);

					}


					WriteLine(")");

					EmitScope(b);
					// additional space
					WriteLine();
				}
				else
				{
					// it seems for some reason we cannot look back and check previous block clause at this time
					// so we only assume we are right...

					var Monitor_Exit_Call_Instruction = CheckFinallyBlockForMonitorExit(p.Block);

					if (Monitor_Exit_Call_Instruction == null)
					{
						// just a finally block
						this.WriteKeyword(Keywords._finally);
						this.WriteLine();

						EmitScope(b);
					
					}

					// additional space
					WriteLine();
				}


			}
			else
			{
				return false;
			}

			return true;

		}



	}
}
