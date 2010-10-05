﻿using System;
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
                        if (Method.IsFunction)
                            File.WriteSpace(Keywords.Function);
                        else
                            File.WriteSpace(Keywords.Sub);
                    }
                    else
                    {
                        File.WriteIndent();

                        if (m.IsProtected)
                            File.WriteSpace(Keywords.Protected);
                        else
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

                        if (m.IsOverride)
                            File.WriteSpace(Keywords.Overrides);

                        if (Method.IsFunction)
                            File.WriteSpace(Keywords.Function);
                        else
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

                    #region Parameters
                    var Parameters = m.Parameters.ToArray();

                    for (int i = 0; i < Parameters.Length; i++)
                    {
                        if (i > 0)
                        {
                            File.WriteSpace(",");
                        }


                        File.Write(Parameters[i].Name);

                        if (Method.Code.IsLambdaExpression)
                        {
                            // omit type ? :)
                        }
                        else
                        {
                            Parameters[i].Type.With(
                                ParameterType =>
                                {
                                    File.WriteSpaces(Keywords.As);

                                    this.WriteTypeName(File, ParameterType);
                                }
                           );
                        }
                    }
                    #endregion

                    File.Write(")");

                    if (Method.Code.IsLambdaExpression)
                    {
                        File.WriteSpace();
                        this.WriteMethodBody(File, m.Code, Context);
                    }
                    else
                    {
                        File.WriteLine();

                        this.WriteMethodBody(File, m.Code, Context);

                        File.WriteIndent();

                        File.WriteSpace(Keywords.End);
                        if (Method.IsFunction)
                            File.WriteSpace(Keywords.Function);
                        else
                            File.WriteSpace(Keywords.Sub);
                    }
                    File.WriteLine();

                }
            );
        }

        public override void WriteMethodBody(SolutionFile File, SolutionProjectLanguageCode Code, SolutionBuilder Context)
        {
            // should this be an extension method to all languages?

            Action WriteCodeStatements =
             delegate
             {

                 var History = Code.History.ToArray();

                 for (int i = 0; i < History.Length; i++)
                 {
                     var IsReturnStatement = false;

                     Code.OwnerMethod.With(
                          m =>
                          {
                              if (m.ReturnType == null)
                                  return;

                              if (m.IsConstructor)
                                  return;

                              IsReturnStatement = i == History.Length - 1;
                          }
                     );


                     var item = History[i];


                     #region Comment
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
                     #endregion


                     #region If
                     var If = item as PseudoIfExpression;

                     if (If != null)
                     {
                         Func<SolutionFile> WriteDirectiveOrIndent = File.WriteIndent;


                         if (If.IsConditionalCompilationDirective)
                         {
                             WriteDirectiveOrIndent = File.WriteDirective;
                         }

                         WriteDirectiveOrIndent().WriteSpace(Keywords.If);
                         WritePseudoExpression(File, If.Expression, Context);
                         File.WriteSpace();
                         File.WriteLine(Keywords.Then);

                         WriteMethodBody(File, If.TrueCase, Context);

                         if (If.FalseCase != null)
                         {
                             WriteDirectiveOrIndent().WriteLine(Keywords.Else);

                             WriteMethodBody(File, If.FalseCase, Context);
                         }

                         WriteDirectiveOrIndent().WriteSpace(Keywords.End).WriteLine(Keywords.@If);

                         return;
                     }
                     #endregion


                     #region Lambda
                     var Lambda = item as PseudoCallExpression;

                     if (Lambda != null)
                     {
                         if (Code.IsLambdaExpression)
                         {
                             WritePseudoCallExpression(File, Lambda, Context);
                         }
                         else
                         {

                             if (Lambda.Comment != null)
                                 Lambda.Comment.WriteTo(File, this, Context);

                             if (Lambda.Method != null)
                             {
                                 File.WriteIndent();

                                 if (IsReturnStatement)
                                 {
                                     File.WriteSpace(Keywords.@Return);
                                 }


                                 WritePseudoCallExpression(File, Lambda, Context);
                                 File.WriteLine();
                             }
                         }
                     }
                     #endregion

                 }

             };

            if (Code.IsConditionalCompilationDirectiveCode)
                WriteCodeStatements();
            else
                File.Indent(this, WriteCodeStatements);


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

                                    if (Type.IsPartial)
                                        File.WriteSpace(Keywords.Partial);

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
                                            Type.BaseType.With(
                                                BaseType =>
                                                {
                                                    File.WriteIndent().WriteSpace(Keywords.Inherits);
                                                    WriteTypeName(File, BaseType);
                                                    File.WriteLine();
                                                }
                                            );

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
            File.WriteIndent().WriteSpace(Keywords.@Imports).WriteLine(item);
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

            {
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
            }


            {
                var Constant = Parameter as PseudoInt32ConstantExpression;
                if (Constant != null)
                {
                    File.Write("" + Constant.Value);
                    return;
                }
            }

            {
                var Constant = Parameter as PseudoDoubleConstantExpression;
                if (Constant != null)
                {
                    var Value = "" + Constant.Value;
                    if (!Value.Contains("."))
                        Value += ".0";

                    File.Write(Value);
                    return;
                }
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

            var Base = Parameter as PseudoBaseExpression;
            if (Base != null)
            {
                File.Write(Keywords.MyBase);
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
