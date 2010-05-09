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

		public override void WriteLinkCommentLine(SolutionFile File, Uri Link)
		{
			File.Write(SolutionFileTextFragment.Comment, "// ");
			File.Write(Link);
			File.WriteLine();
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
			File.CurrentIndent.Times(
				delegate
				{
					File.Write(SolutionFileTextFragment.Indent, "\t");
				}
			);
		}

		public override void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod m, SolutionBuilder Context)
		{
			this.WriteSummary(File, m.Summary, m.Parameters.ToArray());

			File.Region(
				delegate
				{
					this.WriteIndent(File);

					File.Write(Keywords.@public);
					File.WriteSpace();

					if (m.IsStatic)
					{
						File.Write(Keywords.@static);
						File.WriteSpace();
					}

					if (m.IsConstructor)
					{
						WriteTypeName(File, m.DeclaringType);
					}
					else
					{
						File.Write(Keywords.@void);
						File.WriteSpace();
						File.Write(m.Name);
					}

					File.Write("(");

					var Parameters = m.Parameters.ToArray();

					for (int i = 0; i < Parameters.Length; i++)
					{
						if (i > 0)
						{
							File.Write(",");
							File.WriteSpace();
						}

						this.WriteTypeName(File, Parameters[i].Type);

						File.WriteSpace();
						File.Write(Parameters[i].Name);
					}

					File.Write(")");
					File.WriteLine();

					this.WriteMethodBody(File, m.Code, Context);
				}
			);
		}

		public override void WriteMethodBody(SolutionFile File, SolutionProjectLanguageCode Code, SolutionBuilder Context)
		{
			// should this be an extension method to all languages?

			this.WriteIndent(File);
			File.WriteLine("{");
			File.CurrentIndent++;

			// ? :)
			foreach (var item in Code.History.ToArray())
			{
				{
					var Comment = item as string;
					if (Comment != null)
					{
						this.WriteIndent(File);
						this.WriteCommentLine(File, Comment);
					}
				}

				{
					var Comment = item as SolutionFileComment;
					if (Comment != null)
					{
						Comment.WriteTo(File, this, Context);
						return;
					}
				}

				var Lambda = item as PseudoCallExpression;

				if (Lambda != null)
				{
					if (Lambda.Comment != null)
						Lambda.Comment.WriteTo(File, this, Context);

					this.WriteIndent(File);
					WritePseudoCallExpression(File, Lambda);
					File.WriteLine(";");
				}
			}

			File.CurrentIndent--;
			this.WriteIndent(File);
			File.WriteLine("}");
		}

		public override void WritePseudoCallExpression(SolutionFile File, PseudoCallExpression Lambda)
		{
			var Objectless = true;

			if (Lambda.Method.IsExtensionMethod)
			{
				WritePseudoExpression(File, Lambda.ParameterExpressions[0]);
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
						WritePseudoExpression(File, Lambda.Object);
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
				File.Write("=");
				File.WriteSpace();
				WritePseudoExpression(File, Lambda.ParameterExpressions[0]);

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

					WritePseudoExpression(File, Parameter);
				}

				File.Write(")");
			}


		}

		public override void WritePseudoExpression(SolutionFile File, object Parameter)
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

			File.Write(Type);

			if (Type.Arguments.Count > 0)
			{
				File.Write("<");

				var Arguments = Type.Arguments.ToArray();

				for (int i = 0; i < Arguments.Length; i++)
				{
					if (i > 0)
					{
						File.Write(",");
						File.WriteSpace();
					}

					this.WriteTypeName(File, Arguments[i].Type);
				}

				File.Write(">");

			}
		}

		public override void WriteType(SolutionFile File, SolutionProjectLanguageType Type, SolutionBuilder Context)
		{
			File.Write(this, Context, Type.Comments);

			File.WriteUsingNamespaceList(this, Type);


			File.WriteLine();

			File.Region(
				delegate
				{
					File.Write(Keywords.@namespace);
					File.WriteSpace();
					File.Write(Type.Namespace);
					File.WriteLine();
					File.WriteLine("{");
					File.CurrentIndent++;


					this.WriteSummary(
						File,

						Type.Summary
					);

					File.Region(
						delegate
						{
							this.WriteIndent(File);
							File.Write(Keywords.@public);
							File.WriteSpace();

							if (Type.IsStatic)
							{
								File.Write(Keywords.@static);
								File.WriteSpace();
							}

							if (Type.IsSealed)
							{
								File.Write(Keywords.@sealed);
								File.WriteSpace();
							}

							File.Write(Keywords.@class);
							File.WriteSpace();
							File.Write(Type);
							File.WriteLine();

							this.WriteIndent(File);
							File.WriteLine("{");
							File.CurrentIndent++;




							foreach (var item in Type.Methods.ToArray())
							{
								this.WriteMethod(
									File,
									item,
									Context
								);

								File.WriteLine();
							}



							File.WriteLine();

							File.CurrentIndent--;
							this.WriteIndent(File);
							File.WriteLine("}");
						}
					);
				}
			);

			File.CurrentIndent--;
			File.WriteLine("}");
		}


		public override void WriteUsingNamespace(SolutionFile File, string item)
		{
			this.WriteIndent(File);
			File.Write(Keywords.@using);
			File.WriteSpace();
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
								ParameterExpressions = new[] {
										item.Value
									}
							}
						);
					}
				);

			}

			Action Separator = delegate
			{
				File.Write(", ");
			};

			var BeforeSeparator = args.ToArray();
			var AfterSeparator = BeforeSeparator.SelectWithSeparator(Separator).ToArray();

			AfterSeparator.Invoke();

			File.Write(")");
			File.Write("]");

			File.WriteLine();
		}




	}
}
