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
					"Some OpCodes are not supported yet: " + string.Join(", ", missing)
				);
			}

			foreach (var i in xb.Instructrions)
			{
				if (x.BeforeInstruction != null)
					x.BeforeInstruction(new EmitToArguments.ILRewriteContext { i = i, il = il });

				x.Configuration[i.OpCode](new EmitToArguments.ILRewriteContext { i = i, il = il });

				if (x.AfterInstruction != null)
					x.AfterInstruction(new EmitToArguments.ILRewriteContext { i = i, il = il });

			}

	
		}



	}
}
