using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using System.Xml.Linq;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
	public partial class VisualBasicLanguage : SolutionProjectLanguage
	{
		public override string ProjectFileExtension { get { return ".vbproj"; } }
        public override string CodeFileExtension { get { return ".vb"; } }
        public override string LanguageSpelledName { get { return "Visual Basic"; } }
        public override string LanguageName { get { return "Visual Basic"; } }

		public override string Kind
		{
			get { return "{F184B08F-C81C-45F6-A57F-5ABD9991F28F}"; }
		}

		public override void WriteLinkCommentLine(SolutionFile File, Uri Link)
		{
			File.Write(SolutionFileTextFragment.Comment, "' ");
			File.Write(Link);
			File.WriteLine();
		}

		public override void WriteCommentLine(SolutionFile File, string Text)
		{
			File.WriteLine(SolutionFileTextFragment.Comment, "' " + Text);
		}

		public override void WriteXMLCommentLine(SolutionFile File, string Text)
		{
			File.WriteLine(SolutionFileTextFragment.Comment, "''' " + Text);
		}

		
		public override void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod Method, SolutionBuilder Context)
		{
			var m = Method;

			if (!m.IsLambda)
				this.WriteSummary(File, m.Summary, m.Parameters.ToArray());


			File.Region(
				delegate
				{
					if (m.IsLambda)
					{
                        File.WriteSpace(Keywords.Sub);
					}
					else
					{
						File.WriteIndent();

                        File.WriteSpace(Keywords.Public);

						if (m.IsStatic)
						{
							var IsModule = false;

							if (m.DeclaringType != null)
							{
								if (m.DeclaringType.IsStatic)
								{
									IsModule = true;
								}
							}

							if (IsModule)
							{
							}
							else
							{
                                File.WriteSpace(Keywords.Shared);
							}
						}



                        File.WriteSpace(Keywords.Sub);

						if (m.IsConstructor)
						{
							File.Write(Keywords.New);
						}
						else
						{
							File.Write(m.Name);
						}
					}

					File.Write("(");

					var Parameters = m.Parameters.ToArray();

					for (int i = 0; i < Parameters.Length; i++)
					{
						if (i > 0)
						{
                            File.WriteSpace(",");
						}

                         
                        File.WriteSpace(
                            //Keywords.ByVal,
                            Parameters[i].Name
                        );

                        Parameters[i].Type.With(
                            ParameterType =>
                            {
                                File.WriteSpace(
                                  Keywords.As
                              );

                                this.WriteTypeName(File, ParameterType);
                            }
                       );
					}

                    File.WriteLine(")");

					this.WriteMethodBody(File, m.Code, Context);

				}
			);
		}

		public override void WriteMethodBody(SolutionFile File, SolutionProjectLanguageCode Code, SolutionBuilder Context)
		{
			// should this be an extension method to all languages?

			File.Indent(this,
				delegate
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

                        var If = item as PseudoIfExpression;

                        if (If != null)
                        {
                            if (If.IsConditionalCompilationDirective)
                            {
                                File.WriteSpace(new SolutionFileWriteArguments { Fragment = SolutionFileTextFragment.Keyword, Text = "#if" });
                                WritePseudoExpression(File, If.Expression, Context);
                                File.WriteLine();

                                WriteMethodBody(File, If.TrueCase, Context);
                                File.WriteLine();

                                if (If.FalseCase != null)
                                {
                                    File.WriteLine(new SolutionFileWriteArguments { Fragment = SolutionFileTextFragment.Keyword, Text = "#else" });
                                    WriteMethodBody(File, If.FalseCase, Context);
                                    File.WriteLine();

                                }

                                File.WriteLine(new SolutionFileWriteArguments { Fragment = SolutionFileTextFragment.Keyword, Text = "#endif" });
                            }
                            else
                            {

                                File.WriteIndent();
                                File.WriteSpace(Keywords.If);
                                WritePseudoExpression(File, If.Expression, Context);
                                File.WriteSpace();
                                File.WriteLine(Keywords.Then);

                                WriteMethodBody(File, If.TrueCase, Context);
                                File.WriteLine();

                                if (If.FalseCase != null)
                                {
                                    File.WriteIndent();
                                    File.WriteLine(Keywords.Else);

                                    WriteMethodBody(File, If.FalseCase, Context);
                                }

                                File.WriteIndent();
                                File.WriteSpace(Keywords.End);
                                File.WriteLine(Keywords.@If);
                            }

                            return;
                        }

						var Lambda = item as PseudoCallExpression;

						if (Lambda != null)
						{
							if (Lambda.Comment != null)
								Lambda.Comment.WriteTo(File, this, Context);

							if (Lambda.Method != null)
							{
								File.WriteIndent();
								WritePseudoCallExpression(File, Lambda, Context);
								File.WriteLine();
							}
						}
					}

				}
			);

			File.WriteIndent();

			File.Write(Keywords.End);
			File.WriteSpace();
			File.Write(Keywords.Sub); // function?
			File.WriteLine();
		}

		public override void WriteTypeName(SolutionFile File, SolutionProjectLanguageType Type)
		{
            if (Type == null)
                return;

			if (Type.DeclaringType != null)
			{
				WriteTypeName(File, Type.DeclaringType);
				File.Write(".");
			}

			if (Type.ElementType != null)
			{
				WriteTypeName(File, Type.ElementType);
				File.Write("()");

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
				File.Write("(");
				File.Write(Keywords.Of);
				File.WriteSpace();

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

				File.Write(")");

			}
		}

        public override void WriteNamespace(SolutionFile File, string Namespace, Action Body)
        {
            var RootlessNamespace = Namespace.SkipUntilOrEmpty(".");

            if (string.IsNullOrEmpty(RootlessNamespace))
            {
                Body();
                return;
            }

            File.Write(Keywords.Namespace);
            File.WriteSpace();
            File.Write(Namespace);
            File.WriteLine();

            File.Indent(this, Body);

            File.Write(Keywords.End);
            File.WriteSpace();
            File.Write(Keywords.Namespace);
        }

		public override void WriteType(SolutionFile File, SolutionProjectLanguageType Type, SolutionBuilder Context)
		{
			File.Write(this, Context, Type.Comments);




			// should the namespaces be clickable?


			File.WriteUsingNamespaceList(this, Type);

			File.WriteLine();

			File.Region(
				delegate
				{


                    WriteNamespace(File, Type.Namespace,
						delegate
						{


							this.WriteSummary(
									File,

									Type.Summary
								);


							File.Region(
								delegate
								{

									File.WriteIndent();
                                    File.WriteSpace(Keywords.Public);

									if (Type.IsSealed)
									{
                                        File.WriteSpace(Keywords.NotInheritable);
									}

									if (!Type.IsStatic)
									{
                                        File.WriteSpace(Keywords.Class);
									}
									else
									{
                                        File.WriteSpace(Keywords.Module);
									}

									File.Write(Type);
									File.WriteLine();

									File.Indent(this,
										delegate
                                        {
                                            #region Fields
                                            Type.Fields.WithEach(
                                                Field =>
                                                {
                                                    this.WriteSummary(File, Field.Summary);

                                                    File.WriteIndent();

                                                    if (Field.IsPrivate)
                                                    {
                                                        File.WriteSpace(Keywords.Private);
                                                    }
                                                    else
                                                    {
                                                        File.WriteSpace(Keywords.Public);
                                                    }

                                                    if (Field.IsReadOnly)
                                                    {
                                                        File.WriteSpace(Keywords.ReadOnly);
                                                    }

                                                    File.WriteSpace(Field.Name);
                                                    File.WriteSpace(Keywords.As);

                                                    if (Field.FieldConstructor == null)
                                                    {
                                                        WriteTypeName(File, Field.FieldType);
                                                    }
                                                    else
                                                    {
                                                        WritePseudoCallExpression(File, Field.FieldConstructor, Context);
                                                    }

                                                    File.WriteLine();

                                                    File.WriteLine();
                                                }
                                            );

                                            
                                            #endregion

                                            #region Methods
                                            foreach (var item in Type.Methods.ToArray())
											{
                                                if (item.DeclaringType == null)
                                                    item.DeclaringType = Type;

												this.WriteMethod(
													File,
													item,
													Context
												);


												File.WriteLine();
                                            }
                                            #endregion



                                            File.WriteLine();
										}
									);

									File.WriteIndent();
									File.Write(Keywords.End);
									File.WriteSpace();
									if (!Type.IsStatic)
									{
										File.Write(Keywords.Class);
									}
									else
									{
										File.Write(Keywords.Module);
									}
								}
							);
							File.WriteLine();

						}
					);

		
				}
			);
			File.WriteLine();

		}


        public override void WriteAssemblyAttributeNamespace(SolutionFile File, string Namespace, Action Body)
        {
            Body();
        }

		public override void WriteAssemblyAttribute(SolutionFile File, SolutionProjectLanguageAttribute Attribute, SolutionBuilder Context)
		{
			File.Write("<");
			File.Write(Keywords.Assembly);
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
								IsAttributeContext = true,
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
			File.Write(">");

			File.WriteLine();
		}

		public override void WriteUsingNamespace(SolutionFile File, string item)
		{
			File.WriteIndent();
			File.Write(Keywords.@Imports);
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


			var Constant = Parameter as PseudoStringConstantExpression;
			if (Constant != null)
			{
				var Value = (string)Constant.Value;
				File.Write(SolutionFileTextFragment.String,
					// jsc escape string
					"\"" + Value.Replace("\"", "\"\"") + "\""
				);
				return;
			}


            var ConstantInt32 = Parameter as PseudoInt32ConstantExpression;
            if (ConstantInt32 != null)
            {
                File.Write("" + ConstantInt32.Value);
                return;
            }

			var Call = Parameter as PseudoCallExpression;
			if (Call != null)
			{
				WritePseudoCallExpression(File, Call, Context);
				return;
			}

            var This = Parameter as PseudoThisExpression;
            if (This != null)
            {
                File.Write(Keywords.Me);
                return;
            }

			var Type = Parameter as SolutionProjectLanguageType;
			if (Type != null)
			{
				File.Write(Keywords.@GetType);
				File.Write("(");
				WriteTypeName(File, Type);
				File.Write(")");
				return;
			}

			var XElement = Parameter as XElement;
			if (XElement != null)
			{
				var x = File.IndentStack;

				var xx = XElement.Nodes().Last() as XText;
				if (xx != null)
				{
					var Padding = xx.Value.SkipUntilLastOrEmpty("\n");
					File.Write(Padding);
				}

				File.IndentStack = new Stack<Action>();
				File.WriteXElement(XElement);

				File.IndentStack = x;
				File.WriteLine();
				File.WriteIndent();
				return;
			}

			var Method = Parameter as SolutionProjectLanguageMethod;
			if (Method != null)
			{
				WriteMethod(File, Method, Context);
				return;
			}

            // F# match would be awesome here? :)
            var Field = Parameter as SolutionProjectLanguageField;
            if (Field != null)
            {
                // DeclaringType Object?
                File.Write(Field.Name);
                return;
            }

		}

		public override void WriteSingleIndent(SolutionFile File)
		{
			File.Write(SolutionFileTextFragment.Indent, "\t");
		}

		public override bool SupportsDependentUpon()
		{
			return true;
		}

	}
}
