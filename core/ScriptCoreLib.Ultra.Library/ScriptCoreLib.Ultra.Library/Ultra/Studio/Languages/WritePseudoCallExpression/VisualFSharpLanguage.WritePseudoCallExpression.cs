using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	partial class VisualFSharpLanguage
	{
		public override void WritePseudoCallExpression(SolutionFile File, ScriptCoreLib.Ultra.Studio.PseudoExpressions.PseudoCallExpression Lambda, SolutionBuilder Context)
		{
			var Objectless = true;

			if (Lambda.Method.IsExtensionMethod)
			{
				WritePseudoExpression(File, Lambda.ParameterExpressions[0], Context);
				Objectless = false;
			}
			else
			{
				if (Lambda.Method.IsStatic)
				{
					if (Lambda.Method.DeclaringType != null)
					{
						WriteTypeName(File, Lambda.Method.DeclaringType);
						Objectless = false;
					}
				}
				else
				{
					if (Lambda.Object != null)
					{
						WritePseudoExpression(File, Lambda.Object, Context);
						Objectless = false;
					}
				}
			}



			var Target = Lambda.Method.Name;

			if (Lambda.Method.IsProperty)
			{
				Target = Target.SkipUntilIfAny("set_").SkipUntilIfAny("get_");

			}

			if (!Objectless)
			{
				File.Write(".");
			}

			File.Write(
				new SolutionFileWriteArguments
				{
					Fragment = SolutionFileTextFragment.None,
					Text = Target,
					Tag = Lambda.Method
				}
			);

			if (Lambda.Method.IsProperty)
			{
				File.WriteSpace();
				File.Write("=");
				File.WriteSpace();
				WritePseudoExpression(File, Lambda.ParameterExpressions[0], Context);

			}
			else
			{

				File.Write("(");

				var Parameters = Lambda.ParameterExpressions.ToArray();

				var FirstParameter = 0;

				if (Lambda.Method.IsExtensionMethod)
					FirstParameter = 1;

				for (int i = FirstParameter; i < Parameters.Length; i++)
				{
					if (i > 0)
					{
						File.Write(",");
						File.WriteSpace();
					}

					var Parameter = Parameters[i];

					WritePseudoExpression(File, Parameter, Context);
				}

				File.Write(")");
			}
		}

	}
}
