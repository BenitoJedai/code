
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
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Languages.Java
{

	partial class JavaCompiler
	{
		private List<Type> GetMethodExceptions(MethodBase m)
		{
			DebugBreak(ScriptAttribute.Of(m));

			var list = new List<Type>();

			GetMethodExceptionsFromAttribute(m, list);

			if (!m.IsAbstract)
			{
				FindThrownExceptions(new ILBlock(m), list);
			}

			return list;
		}

		private static void GetMethodExceptionsFromAttribute(MethodBase m, List<Type> list)
		{
			ScriptMethodThrows[] throws = ScriptMethodThrows.ArrayOfProvider(m);

			if (throws.Length > 0)
			{
				for (int i = 0; i < throws.Length; i++)
				{
					list.Add(throws[i].ThrowType);
				}
			}
		}


		private void FindThrownExceptions(ILBlock b, List<Type> list)
		{
			// this method does not have IL
			if (b.Instructrions == null)
				return;


			foreach (ILBlock.Prestatement x in b.Prestatements.PrestatementCommands)
			{
				if (x.Block != null)
				{
					FindThrownExceptions(x.Block, list);
				}

				// todo: declare uncatched native method declared throws automatically

				//if (x.Instruction != null)
				//{
				//    MethodBase nmethod = x.Instruction.TargetMethod;

				//    if (nmethod != null)
				//    {
				//        Type ntype = nmethod.DeclaringType;

				//        ScriptAttribute ntypea = ScriptAttribute.Of(ntype);

				//        if (ntype != null && ntypea.IsNative)
				//        {
				//            // add native throws

				//            GetMethodExceptionsFromAttribute(nmethod, list);
				//        }
				//    }
				//}

				if (x.Instruction == OpCodes.Throw)
				{
					ILInstruction[] s = x.Instruction.StackBeforeStrict[0].StackInstructions;

					// we mus walk up the stack to find out if anyone is throwing anything

					foreach (ILInstruction xs in s)
					{
						ILBlock o = xs.Flow.OwnerBlock;

						Type cexc = xs.ReferencedType;



						if (cexc == null)
							Break("unable to detect thrown exception");
						else
							FindThrownExceptionsInCatchBlock(list, cexc, o);


					}
				}
			}
		}

		private void FindThrownExceptionsInCatchBlock(List<Type> list, Type cexc, ILBlock o)
		{
			if (o.IsRoot || o.Parent.IsRoot)
			{
				if (
					//cexc.Equals(typeof(Exception)) ||
					cexc.Equals(typeof(NotImplementedException)))
					return;

				// 

				var ResolvedType = this.ResolveImplementation(cexc) ?? cexc;

				if (ResolvedType.ToScriptAttributeOrDefault().ImplementationType != null)
					if (ResolvedType.ToScriptAttributeOrDefault().ImplementationType.Name == "RuntimeException")
						return;

				if (!list.Contains(cexc))
					list.Add(cexc);
			}
			else
			{
				if (!FindThrownExceptionsInTryBlock(list, cexc, o.Parent))
					Break("unable to detect thrown exception");
			}
		}

		private bool FindThrownExceptionsInTryBlock(List<Type> list, Type cexc, ILBlock o)
		{
			bool bIsTry = false;

			if (o.IsTryBlock)
			{
				// we might have a protecting catch clause

				if (o.Next.IsHandlerBlock)
				{
					if (o.Next.Clause.Flags == ExceptionHandlingClauseOptions.Clause)
					{
						if (cexc.IsSubclassOf(o.Next.Clause.CatchType) || o.Next.Clause.CatchType == cexc)
						{
							// safe
						}
						else
						{
							FindThrownExceptionsInCatchBlock(list, cexc, o.Next);
						}
					}
					else
						Break("catch block excpected");
				}
				else
					Break("malformed try/catch block");

				bIsTry = true;
			}
			return bIsTry;
		}



	}
}
