using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public partial class VisualBasicLanguage
	{
		public override void WritePseudoCallExpression(SolutionFile File, PseudoCallExpression Lambda, SolutionBuilder Context)
		{
			if (Lambda.Method.Name == SolutionProjectLanguageMethod.op_Implicit)
			{
				WritePseudoExpression(File, Lambda.ParameterExpressions[0], Context);
				return;
			}

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


			if (Lambda.Method.Name == "Invoke")
			{
				// in c# we can omit the .Invoke on a delegate
			}
			else
			{
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
			}

			if (Lambda.Method.IsProperty)
			{
				File.WriteSpace();

				if (Lambda.IsAttributeContext)
				{
					File.Write(":=");
				}
				else
				{
					File.Write("=");
				}

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
					if (i > FirstParameter)
					{
						File.Write(", ");
					}

					var Parameter = Parameters[i];

					WritePseudoExpression(File, Parameter, Context);
				}

				File.Write(")");
			}


		}

	}
}
