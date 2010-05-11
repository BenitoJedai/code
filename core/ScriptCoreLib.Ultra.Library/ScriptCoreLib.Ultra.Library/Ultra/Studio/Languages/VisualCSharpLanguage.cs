using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Documentation;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

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
			if (!m.IsLambda)
				this.WriteSummary(File, m.Summary, m.Parameters.ToArray());

			File.Region(
				delegate
				{

					if (m.IsLambda)
					{
						File.Write(Keywords.@delegate);
						File.WriteSpace();
					}
					else
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

					if (!m.IsLambda)
					{
						File.WriteLine();
					}
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
					WritePseudoCallExpression(File, Lambda, Context);
					File.WriteLine(";");
				}
			}

			File.CurrentIndent--;
			this.WriteIndent(File);
			File.Write("}");
		}


		public override void WritePseudoExpression(SolutionFile File, object Parameter, SolutionBuilder Context)
		{
			var Code = Parameter as string;
			if (Code != null)
			{
				File.Write(Code);
				return;
			}

			var Argument = Parameter as SolutionProjectLanguageArgument;
			if (Argument != null)
			{
				File.Write(Argument.Name);
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


			var Array = Parameter as PseudoArrayExpression;
			if (Array != null)
			{
				File.CurrentIndent++;
				File.WriteLine();
				this.WriteIndent(File);

				File.Write(Keywords.@new);
				File.WriteSpace();

				WriteTypeName(File, Array.ElementType);
				File.Write("[]");

				File.WriteSpace();
				File.Write("{");

				File.CurrentIndent++;
				File.WriteLine();
				this.WriteIndent(File);

				Func<object, Action> AtWritePseudoExpression = k => () => WritePseudoExpression(File, k, Context);

				Action WriteSeparator =
					delegate
					{
						File.Write(",");
						File.WriteLine();
						this.WriteIndent(File);
					};

				Array.Items.ToArray().Select(AtWritePseudoExpression).SelectWithSeparator(WriteSeparator).Invoke();


				File.CurrentIndent--;
				File.WriteLine();
				this.WriteIndent(File);

				File.Write("}");

				File.CurrentIndent--;
				File.WriteLine();
				this.WriteIndent(File);

				return;
			}

			var Call = Parameter as PseudoCallExpression;
			if (Call != null)
			{
				WritePseudoCallExpression(File, Call, Context);
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

			var XElement = Parameter as XElement;
			if (XElement != null)
			{
				WritePseudoCallExpression(File, XElement.ToPseudoCallExpression(), Context);
				return;
			}

			var Method = Parameter as SolutionProjectLanguageMethod;
			if (Method != null)
			{
				WriteMethod(File, Method, Context);
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

		public override void WriteAssemblyAttribute(SolutionFile File, SolutionProjectLanguageAttribute Attribute, SolutionBuilder Context)
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
						this.WritePseudoExpression(File, item, Context);
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
							}, Context
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
