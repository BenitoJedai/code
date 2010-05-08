using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Documentation;
using System.Linq.Expressions;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public partial class VisualCSharpLanguage : SolutionProjectLanguage
	{
		public override string ProjectFileExtension { get { return ".csproj"; } }
		public override string CodeFileExtension { get { return ".cs"; } }

		public override string Kind
		{
			get { return "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"; }
		}

		public override void WriteCommentLine(SolutionFile File, string Text)
		{
			File.WriteLine(SolutionFileTextFragment.Comment, "// " + Text);
		}

		public override void WriteXMLCommentLine(SolutionFile File, string Text)
		{
			// http://msdn.microsoft.com/en-us/library/aa288481(VS.71).aspx

			File.WriteLine(SolutionFileTextFragment.Comment, "/// " + Text);
		}

		public override void WriteIndent(SolutionFile File)
		{
			File.Write(SolutionFileTextFragment.None,
				"".PadLeft(
					File.CurrentIndent,
				// F# will use 4x spaces
					'\t'
				)
			);
		}

		public override void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod m)
		{

			this.WriteSummary(File, m.Summary, m.Parameters.ToArray());

			this.WriteIndent(File);

			File.Write(Keywords.@public);
			File.Write(" ");

			if (m.IsStatic)
			{
				File.Write(Keywords.@static);
				File.Write(" ");
			}

			if (m.IsConstructor)
			{
				WriteTypeName(File, m.DeclaringType);
			}
			else
			{
				File.Write(Keywords.@void);
				File.Write(" ");
				File.Write(m.Name);
			}

			File.Write("(");

			var Parameters = m.Parameters.ToArray();

			for (int i = 0; i < Parameters.Length; i++)
			{
				if (i > 0)
				{
					File.Write(", ");
				}

				this.WriteTypeName(File, Parameters[i].Type);

				File.Write(" ");
				File.Write(Parameters[i].Name);
			}

			File.Write(")");
			File.WriteLine();

			this.WriteCode(File, m.Code);
		}

		public override void WriteCode(SolutionFile File, SolutionProjectLanguageCode Code)
		{
			// should this be an extension method to all languages?

			this.WriteIndent(File);
			File.WriteLine("{");
			File.CurrentIndent++;

			// ? :)
			foreach (var item in Code.History.ToArray())
			{
				var Comment = item as string;

				if (Comment != null)
				{
					this.WriteIndent(File);
					this.WriteCommentLine(File, Comment);
				}

				var Lambda = item as PseudoCallExpression;

				if (Lambda != null)
				{
					this.WriteIndent(File);
					WritePseudoCallExpression(File, Lambda);
					File.WriteLine(";");
				}
			}

			File.CurrentIndent--;
			this.WriteIndent(File);
			File.WriteLine("}");
		}

		private void WritePseudoCallExpression(SolutionFile File, PseudoCallExpression Lambda)
		{
			var Objectless = true;

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
					WritePseudoExpression(File, Lambda.Object);
					Objectless = false;
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
				File.Write(" = ");
				WritePseudoExpression(File, Lambda.Parameters[0]);

			}
			else
			{

				File.Write("(");

				var Parameters = Lambda.Parameters.ToArray();

				for (int i = 0; i < Parameters.Length; i++)
				{
					if (i > 0)
					{
						File.Write(", ");
					}

					var Parameter = Parameters[i];

					WritePseudoExpression(File, Parameter);
				}

				File.Write(")");
			}


		}

		private void WritePseudoExpression(SolutionFile File, object Parameter)
		{
			var Code = Parameter as string;
			if (Code != null)
			{
				File.Write(Code);
				return;
			}

			var Constant = Parameter as PseudoConstantExpression;
			if (Constant != null)
			{
				var Value = (string)Constant.Value;
				File.Write(SolutionFileTextFragment.String,
					// jsc escape string
					"@\"" + Value.Replace("\"", "\"\"") + "\""
				);
				return;
			}

			var Call = Parameter as PseudoCallExpression;
			if (Call != null)
			{
				WritePseudoCallExpression(File, Call);
				return;
			}

			var Type = Parameter as SolutionProjectLanguageType;
			if (Type != null)
			{
				File.Write(Keywords.@typeof);
				File.Write("(");
				WriteTypeName(File, Type);
				File.Write(")");
				return;
			}

		}



		public override void WriteTypeName(SolutionFile File, SolutionProjectLanguageType Type)
		{
			if (Type.DeclaringType != null)
			{
				WriteTypeName(File, Type.DeclaringType);
				File.Write(".");
			}

			if (Type.ElementType != null)
			{
				WriteTypeName(File, Type.ElementType);
				File.Write("[]");

				return;
			}

			File.Write(
				new SolutionFileWriteArguments
				{
					Fragment = SolutionFileTextFragment.Type,
					Tag = Type,
					Text = Type.Name
				}
			);

			if (Type.Arguments.Count > 0)
			{
				File.Write("<");

				var Arguments = Type.Arguments.ToArray();

				for (int i = 0; i < Arguments.Length; i++)
				{
					if (i > 0)
					{
						File.Write(", ");
					}

					this.WriteTypeName(File, Arguments[i].Type);
				}

				File.Write(">");

			}
		}

		public override void WriteType(SolutionFile File, SolutionProjectLanguageType Type)
		{
			this.WriteCommentLine(File, Type.Comment);

			// should the namespaces be clickable?

			foreach (var item in Type.UsingNamespaces.ToArray())
			{
				WriteUsingNamespace(File, item);
			}

			File.WriteLine();

			File.Write(Keywords.@namespace);
			File.Write(" ");
			File.Write(Type.Namespace);
			File.WriteLine();
			File.WriteLine("{");
			File.CurrentIndent++;


			this.WriteSummary(
				File,

				Type.Summary
			);

			this.WriteIndent(File);
			File.Write(Keywords.@public);
			File.Write(" ");

			if (Type.IsStatic)
			{
				File.Write(Keywords.@static);
				File.Write(" ");
			}

			if (Type.IsSealed)
			{
				File.Write(Keywords.@sealed);
				File.Write(" ");
			}

			File.Write(Keywords.@class);
			File.Write(" ");
			File.Write(SolutionFileTextFragment.Type, Type.Name);
			File.WriteLine();

			this.WriteIndent(File);
			File.WriteLine("{");
			File.CurrentIndent++;

			foreach (var item in Type.Methods.ToArray())
			{
				this.WriteMethod(
					File,
					item
				);

				File.WriteLine();
			}



			File.WriteLine();

			File.CurrentIndent--;
			this.WriteIndent(File);
			File.WriteLine("}");

			File.CurrentIndent--;
			File.WriteLine("}");
		}

		public override void WriteUsingNamespace(SolutionFile File, string item)
		{
			this.WriteIndent(File);
			File.Write(Keywords.@using);
			File.Write(" ");
			File.Write(item);
			File.WriteLine(";");
		}

		public override void WriteAssemblyAttribute(SolutionFile File, SolutionProjectLanguageAttribute Attribute)
		{
			File.Write("[");
			File.Write(Keywords.assembly);
			File.Write(": ");

			this.WriteTypeName(File, Attribute.Type);
			File.Write("(");

			var args = new List<Action>();

			InternalAddAttributeParameters(File, Attribute, args);


			Action Separator = delegate
			{
				File.Write(", ");
			};

			InternalInvokeWithSeparator(args, Separator);


			File.Write(")");
			File.Write("]");

			File.WriteLine();
		}

		private static void InternalInvokeWithSeparator(List<Action> args, Action Separator)
		{
			var BeforeSeparator = args.ToArray();
			var AfterSeparator = BeforeSeparator.SelectWithSeparator(Separator).ToArray();

			AfterSeparator.Invoke();
		}

		private void InternalAddAttributeParameters(SolutionFile File, SolutionProjectLanguageAttribute Attribute, List<Action> args)
		{
			if (Attribute.Parameters != null)
			{
				args.AddRange(
					from item in Attribute.Parameters
					select (Action)delegate
					{
						this.WritePseudoExpression(File, item);
					}
				);
			}


			if (Attribute.Properties != null)
			{
				args.AddRange(
					from item in Attribute.Properties.ToArray()
					select (Action)delegate
					{
						this.WritePseudoCallExpression(File,
							new PseudoCallExpression
							{
								Method = item.Key,
								Parameters = new[] {
										item.Value
									}
							}
						);
					}
				);

			}
		}
	}
}
