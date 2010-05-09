using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public partial class VisualFSharpLanguage : SolutionProjectLanguage
	{
		public override string ProjectFileExtension { get { return ".fsproj"; } }
		public override string CodeFileExtension { get { return ".fs"; } }

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
			File.IndentStack.Invoke();
		}

		public override void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod Method, SolutionBuilder Context)
		{
			this.WriteSummary(File, Method.Summary, Method.Parameters.ToArray());


			WriteIndent(File);
			File.Write(Keywords.member);
			File.WriteSpace();
			File.Write("this");
			File.Write(".");
			File.Write(Method.Name);
			File.Write("(");

			var Parameters = Method.Parameters.ToArray();
			InternalWriteParameters(File, Parameters);

			File.Write(")");
			File.WriteSpace();
			File.Write("=");
			File.WriteLine();

			File.Indent(this,
				delegate
				{
					WriteMethodBody(File, Method.Code, Context);
				}
			);
		}

		private void InternalWriteParameters(SolutionFile File, SolutionProjectLanguageArgument[] Parameters)
		{

			for (int i = 0; i < Parameters.Length; i++)
			{
				if (i > 0)
				{
					File.Write(",");
					File.WriteSpace();
				}

				File.Write(Parameters[i].Name);

				File.WriteSpace();
				File.Write(":");
				File.WriteSpace();

				this.WriteTypeName(File, Parameters[i].Type);
			}
		}

		public override void WriteMethodBody(SolutionFile File, SolutionProjectLanguageCode Code, SolutionBuilder Context)
		{

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
					File.Write(Keywords.@do);
					File.WriteSpace();
					WritePseudoCallExpression(File, Lambda);
					if (Lambda.Method.ReturnType != null)
					{
						File.WriteSpace();
						File.Write("|>");
						File.WriteSpace();
						File.Write("ignore");
					}

					File.WriteLine();
				}
			}

			WriteIndent(File);
			File.Write("()");
			File.WriteLine();

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

			File.Region(
				delegate
				{
					File.Write(Keywords.@namespace);
					File.WriteSpace();
					File.Write(Type.Namespace);
					File.WriteLine();

					File.WriteLine();

					File.Indent(this,
						delegate
						{
							File.WriteUsingNamespaceList(this, Type);

							File.WriteLine();

							this.WriteSummary(
								File,
								Type.Summary
							);

							File.Region(
								delegate
								{
									WriteIndent(File);

									var Constructor = Type.Methods.SingleOrDefault(k => k.IsConstructor);


									if (Type.IsStatic)
									{
										File.Write(Keywords.@module);
										File.WriteSpace();
										WriteTypeName(File, Type);
										File.WriteSpace();
										File.Write("=");

										File.WriteLine();

									}
									else
									{
										File.Write(Keywords.type);
										File.WriteSpace();
										WriteTypeName(File, Type);
										File.Write("(");

										if (Constructor != null)
											this.InternalWriteParameters(File, Constructor.Parameters.ToArray());

										File.Write(")");
										File.WriteSpace();
										File.Write("=");
										File.WriteSpace();
										File.Write(Keywords.@do);

										File.WriteLine();

										
									}

									// .ctor !

									File.Indent(this,
										delegate
										{
											if (!Type.IsStatic)
											{

												if (Constructor != null)
												{
													this.WriteMethodBody(
														File, Constructor.Code, Context
													);
												}

												File.WriteLine();
											}

											foreach (var item in (from m in Type.Methods where !m.IsConstructor select m).ToArray())
											{
												this.WriteMethod(
													File,
													item,
													Context
												);

												File.WriteLine();
											}



										}
									);
								}
							);
						}
					);
				}
			);

		}

		public override void WriteAssemblyAttribute(SolutionFile File, SolutionProjectLanguageAttribute Attribute)
		{
			// http://msdn.microsoft.com/en-us/library/dd233179.aspx

			File.Write("<");
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
				File.Write(",");
				File.WriteSpace();
			};

			var BeforeSeparator = args.ToArray();
			var AfterSeparator = BeforeSeparator.SelectWithSeparator(Separator).ToArray();

			AfterSeparator.Invoke();

			File.Write(")");
			File.Write(">");

			File.WriteSpace();
			File.Write(Keywords.@do);
			File.Write("()");

			File.WriteLine();
		}

		public override void WriteUsingNamespace(SolutionFile File, string item)
		{
			WriteIndent(File);

			File.Write(Keywords.@open);
			File.WriteSpace();
			File.Write(item);

			File.WriteLine();
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
					"\"" + Value.Replace("\"", "\"\"") + "\""
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
				File.Write("<");
				WriteTypeName(File, Type);
				File.Write(">");
				return;
			}
		}

		public override void WritePseudoCallExpression(SolutionFile File, ScriptCoreLib.Ultra.Studio.PseudoExpressions.PseudoCallExpression Lambda)
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
	}
}
