using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using System.Xml.Linq;

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

	

		public override void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod Method, SolutionBuilder Context)
		{
			if (Method.IsLambda)
			{
				File.Write(Keywords.fun);
				File.Write("(");
				InternalWriteParameters(File, Method.Parameters.ToArray());
				File.Write(")");
			}
			else
			{
				this.WriteSummary(File, Method.Summary, Method.Parameters.ToArray());


				File.WriteIndent();
				File.Write(Keywords.member);
				File.WriteSpace();
				File.Write("this");
				File.Write(".");
				File.Write(Method.Name);
				File.Write("(");
				InternalWriteParameters(File, Method.Parameters.ToArray());
				File.Write(")");
				File.WriteSpace();
				File.Write("=");
			}

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
						File.WriteIndent();
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

					if (Lambda.Method != null)
					{
						File.WriteIndent();
						File.Write(Keywords.@do);
						File.WriteSpace();
						WritePseudoCallExpression(File, Lambda, Context);
						if (Lambda.Method.ReturnType != null)
						{
							File.WriteSpace();
							File.Write("|>");
							File.WriteSpace();
							File.Write(Keywords.ignore);
						}

						File.WriteLine();
					}
				}
			}

			File.WriteIndent();
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
									File.WriteIndent();

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

		public override void WriteAssemblyAttribute(SolutionFile File, SolutionProjectLanguageAttribute Attribute, SolutionBuilder Context)
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
							}
							, Context
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
			File.WriteIndent();

			File.Write(Keywords.@open);
			File.WriteSpace();
			File.Write(item);

			File.WriteLine();
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
					"\"" + Value.Replace("\"", "\"\"") + "\""
				);
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
				File.Write("<");
				WriteTypeName(File, Type);
				File.Write(">");
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

			var Array = Parameter as PseudoArrayExpression;
			if (Array != null)
			{
				File.WriteLine();
				File.Indent(this,
					delegate
					{
						File.WriteIndent();

						//File.Write(Keywords.@new);
						//File.WriteSpace();

						//WriteTypeName(File, Array.ElementType);
						//File.Write("[]");

						//File.WriteSpace();
						File.Write("[");
						File.WriteLine();

						File.Indent(this,
							delegate
							{
								File.WriteIndent();

								Func<object, Action> AtWritePseudoExpression = k => () => WritePseudoExpression(File, k, Context);

								Action WriteSeparator =
									delegate
									{
										File.Write(",");
										File.WriteLine();
										File.WriteIndent();
									};

								Array.Items.ToArray().Select(AtWritePseudoExpression).SelectWithSeparator(WriteSeparator).Invoke();
								
							}
						);

						File.WriteLine();
						File.WriteIndent();

						File.Write("]");
						
					}
				);

				File.WriteLine();
				File.WriteIndent();

				return;
			}

		}

		public override void WriteSingleIndent(SolutionFile File)
		{
			File.Write(SolutionFileTextFragment.Indent, "    ");
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

			Action Body =
				delegate
				{
					var Parameters = Lambda.ParameterExpressions.ToArray();

					var FirstParameter = 0;

					if (Lambda.Method.IsExtensionMethod)
						FirstParameter = 1;

					for (int i = FirstParameter; i < Parameters.Length; i++)
					{
						if (i > 0)
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
