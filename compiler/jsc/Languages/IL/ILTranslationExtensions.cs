using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace jsc.Languages.IL
{

	/// <summary>
	/// Here we are years later, actually rewriting IL for a later phase!
	/// </summary>
	public static partial class ILTranslationExtensions
	{

		public static void EmitTo(this Delegate source, ILGenerator il, EmitToArguments a)
		{
			source.Method.EmitTo(il, a);
		}


		public static void EmitTo(this MethodBase m, ILGenerator il)
		{
			EmitTo(m, il, new EmitToArguments());
		}

		public static void EmitTo(this MethodBase m, ILGenerator il, EmitToArguments x)
		{

			var body = m.GetMethodBody();



			if (body == null)
				return;

			var locals = Enumerable.ToArray(
				from local in body.LocalVariables
				let declared = il.DeclareLocal(x.TranslateTargetType(local.LocalType))
				select new { local, declared }
			).ToDictionary(
				k => k.local.LocalIndex,
				k => k.declared.LocalIndex
			);

			x.TranslateLocalIndex = LocalIndex => locals[LocalIndex];

			var xb = new ILBlock(m);

			var missing = xb.Instructrions.Where(k => !x.Configuration.ContainsKey(k.OpCode)).Select(k => k.OpCode.ToString()).Distinct().ToArray();

			if (missing.Any())
			{
				throw new NotSupportedException(
					"Some OpCodes are not supported yet: " + string.Join(", ", missing) + " at " + m.DeclaringType + " " + m.Name
				);
			}

			// http://msdn.microsoft.com/en-us/library/system.reflection.emit.ilgenerator.marklabel.aspx
			var labels = Enumerable.ToDictionary(
				from i in xb.Instructrions
				select new { i, label = il.DefineLabel()}
			, k => k.i, k => k.label);

			var IsComplete = false;

			Action Complete = delegate
			{
				IsComplete = true;
			};

			if (x.BeforeInstructions != null)
				x.BeforeInstructions(new EmitToArguments.ILRewriteContext { SourceMethod = m, il = il, Complete = Complete, Labels = labels, Context = xb });

			foreach (var i in xb.Instructrions)
			{
				if (IsComplete)
					break;

				il.MarkLabel(labels[i]);

				if (x.BeforeInstruction != null)
					x.BeforeInstruction(new EmitToArguments.ILRewriteContext { SourceMethod = m, i = i, il = il, Complete = Complete, Labels = labels });

				x.Configuration[i.OpCode](new EmitToArguments.ILRewriteContext { SourceMethod = m, i = i, il = il, Complete = Complete, Labels = labels });

				if (x.AfterInstruction != null)
					x.AfterInstruction(new EmitToArguments.ILRewriteContext { SourceMethod = m, i = i, il = il, Complete = Complete, Labels = labels });


			}


			if (x.AfterInstructions != null)
				x.AfterInstructions(new EmitToArguments.ILRewriteContext { SourceMethod = m, il = il, Complete = Complete, Labels = labels });

		}



	}
}
