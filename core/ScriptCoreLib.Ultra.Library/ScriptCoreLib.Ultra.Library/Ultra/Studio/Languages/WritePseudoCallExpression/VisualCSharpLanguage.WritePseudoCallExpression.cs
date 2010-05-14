using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Extensions;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public partial class VisualCSharpLanguage
	{
		public override void WritePseudoCallExpression(SolutionFile File, PseudoCallExpression Lambda, SolutionBuilder Context)
		{
			if (Lambda.Method.Name == SolutionProjectLanguageMethod.op_Implicit)
			{
				WritePseudoExpression(File, Lambda.ParameterExpressions[0], Context);
				return;
			}

			if (Lambda.Method.IsConstructor)
			{
				File.Write(Keywords.@new);
				File.WriteSpace();

				this.WriteTypeName(File, Lambda.Method.DeclaringType);
				InternalWriteParameterList(File, Lambda, Context);
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
				if (Lambda.ParameterExpressions.Length == 1)
				{
					File.WriteSpace();
					File.Write("=");
					File.WriteSpace();
					WritePseudoExpression(File, Lambda.ParameterExpressions[0], Context);

				}
			}
			else
			{

				InternalWriteParameterList(File, Lambda, Context);
			}


		}

		private void InternalWriteParameterList(SolutionFile File, PseudoCallExpression Lambda, SolutionBuilder Context)
		{
			File.Write("(");

			var HasComplexParameter = Lambda.ParameterExpressions.Any(
				k =>
				{
					if (k is XElement)
						return true;

					var Call = k as PseudoCallExpression;
					if (Call != null)
					{
						if (Call.XLinq != null)
							return true;
					}

					return false;
				}
			);

			Action Body = delegate
			{

				var Parameters = Lambda.ParameterExpressions.ToArray();

				var FirstParameter = 0;

				if (Lambda.Method.IsExtensionMethod)
					FirstParameter = 1;

				for (int i = FirstParameter; i < Parameters.Length; i++)
				{
					if (i > FirstParameter)
					{
						if (HasComplexParameter)
						{
							File.Write(",");
							File.WriteLine();
							File.WriteIndent();
						}
						else
						{
							File.Write(",");
							File.WriteSpace();
						}
					}

					var Parameter = Parameters[i];

					WritePseudoExpression(File, Parameter, Context);
				}
			};


			if (HasComplexParameter)
			{
				File.WriteLine();
				File.Indent(this,
					delegate
					{
						File.WriteIndent();
						Body();
						File.WriteLine();
					}
				);
				File.WriteIndent();
			}
			else
			{
				Body();

			}

			File.Write(")");
		}

	}
}
