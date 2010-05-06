using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Documentation;
using System.Linq.Expressions;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public class VisualCSharpLanguage : SolutionProjectLanguage
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
			
			this.WriteComment(File, m.Summary, m.Parameters.ToArray());

			this.WriteIndent(File);

			File.Write(SolutionFileTextFragment.Keyword, "public");
			File.Write(SolutionFileTextFragment.None, " ");

			if (m.IsStatic)
			{
				File.Write(SolutionFileTextFragment.Keyword, "static");
				File.Write(SolutionFileTextFragment.None, " ");
			}

			if (m.IsConstructor)
			{
				WriteTypeName(File, m.DeclaringType);
			}
			else
			{
				File.Write(SolutionFileTextFragment.Keyword, "void");
				File.Write(SolutionFileTextFragment.None, " ");
				File.Write(SolutionFileTextFragment.None, m.Name);
			}

			File.Write(SolutionFileTextFragment.None, "(");

			var Parameters = m.Parameters.ToArray();

			for (int i = 0; i < Parameters.Length; i++)
			{
				if (i > 0)
				{
					File.Write(SolutionFileTextFragment.None, ", ");
				}

				this.WriteTypeName(File, Parameters[i].Type);
				
				File.Write(SolutionFileTextFragment.None, " ");
				File.Write(SolutionFileTextFragment.None, Parameters[i].Name);
			}

			File.Write(SolutionFileTextFragment.None, ")");
			File.WriteLine();

			this.WriteCode(File, m.Code);
		}

		public override void WriteCode(SolutionFile File, SolutionProjectLanguageCode Code)
		{
			// should this be an extension method to all languages?

			this.WriteIndent(File);
			File.WriteLine(SolutionFileTextFragment.None, "{");
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
					File.WriteLine(SolutionFileTextFragment.None, ";");
				}
			}

			File.CurrentIndent--;
			this.WriteIndent(File);
			File.WriteLine(SolutionFileTextFragment.None, "}");
		}

		private void WritePseudoCallExpression(SolutionFile File, PseudoCallExpression Lambda)
		{
			if (Lambda.Method.IsStatic)
			{
				WriteTypeName(File, Lambda.Method.DeclaringType);
			}
			else
			{
				WritePseudoExpression(File, Lambda.Object);
			}

			if (Lambda.Method.Name == "Invoke")
			{
				// in c# we can omit the .Invoke on a delegate
			}
			else
			{
				File.Write(SolutionFileTextFragment.None, ".");
				File.Write(
					new SolutionFile.WriteArguments
					{
						Fragment = SolutionFileTextFragment.None,
						Text = Lambda.Method.Name,
						Tag = Lambda.Method
					}
				);
			}
			File.Write(SolutionFileTextFragment.None, "(");

			var Parameters = Lambda.Parameters.ToArray();

			for (int i = 0; i < Parameters.Length; i++)
			{
				if (i > 0)
				{
					File.Write(SolutionFileTextFragment.None, ", ");
				}

				var Parameter = Parameters[i];

				WritePseudoExpression(File, Parameter);
			}

			File.Write(SolutionFileTextFragment.None, ")");


		}

		private void WritePseudoExpression(SolutionFile File, object Parameter)
		{
			var Code = Parameter as string;
			if (Code != null)
			{
				File.Write(SolutionFileTextFragment.None, Code);
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
				File.Write(SolutionFileTextFragment.Keyword, "typeof");
				File.Write(SolutionFileTextFragment.None, "(");
				WriteTypeName(File, Type);
				File.Write(SolutionFileTextFragment.None, ")");
				return;
			}

		}



		public override void WriteTypeName(SolutionFile File, SolutionProjectLanguageType Type)
		{
			if (Type.DeclaringType != null)
			{
				WriteTypeName(File, Type.DeclaringType);
				File.Write(SolutionFileTextFragment.None, ".");
			}

			if (Type.ElementType != null)
			{
				WriteTypeName(File, Type.ElementType);
				File.Write(SolutionFileTextFragment.None, "[]");

				return;
			}

			File.Write(
				new SolutionFile.WriteArguments
				{
					Fragment = SolutionFileTextFragment.Type,
					Tag = Type,
					Text = Type.Name
				}
			);

			if (Type.Arguments.Count > 0)
			{
				File.Write(SolutionFileTextFragment.None, "<");

				var Arguments = Type.Arguments.ToArray();

				for (int i = 0; i < Arguments.Length; i++)
				{
					if (i > 0)
					{
						File.Write(SolutionFileTextFragment.None, ", ");
					}

					this.WriteTypeName(File, Arguments[i].Type);
				}

				File.Write(SolutionFileTextFragment.None, ">");

			}
		}

		public override void WriteType(SolutionFile File, SolutionProjectLanguageType Type)
		{
			this.WriteCommentLine(File, Type.Comment);

			// should the namespaces be clickable?

			foreach (var item in Type.UsingNamespaces.ToArray())
			{
				File.Write(SolutionFileTextFragment.Keyword, "using");
				File.Write(SolutionFileTextFragment.None, " ");
				File.Write(SolutionFileTextFragment.None, item);
				File.WriteLine(SolutionFileTextFragment.None, ";");

			}

			File.WriteLine();

			File.Write(SolutionFileTextFragment.Keyword, "namespace");
			File.Write(SolutionFileTextFragment.None, " ");
			File.Write(SolutionFileTextFragment.None, Type.Namespace);
			File.WriteLine(SolutionFileTextFragment.None, "");
			File.WriteLine(SolutionFileTextFragment.None, "{");
			File.CurrentIndent++;


			this.WriteComment(
				File,
				Type.Summary
			);

			this.WriteIndent(File);
			File.Write(SolutionFileTextFragment.Keyword, "public");
			File.Write(SolutionFileTextFragment.None, " ");

			if (Type.IsStatic)
			{
				File.Write(SolutionFileTextFragment.Keyword, "static");
				File.Write(SolutionFileTextFragment.None, " ");
			}

			if (Type.IsSealed)
			{
				File.Write(SolutionFileTextFragment.Keyword, "sealed");
				File.Write(SolutionFileTextFragment.None, " ");
			}

			File.Write(SolutionFileTextFragment.Keyword, "class");
			File.Write(SolutionFileTextFragment.None, " ");
			File.Write(SolutionFileTextFragment.Type, Type.Name);
			File.WriteLine(SolutionFileTextFragment.None, "");

			this.WriteIndent(File);
			File.WriteLine(SolutionFileTextFragment.None, "{");
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
			File.WriteLine(SolutionFileTextFragment.None, "}");

			File.CurrentIndent--;
			File.WriteLine(SolutionFileTextFragment.None, "}");
		}

	}
}
