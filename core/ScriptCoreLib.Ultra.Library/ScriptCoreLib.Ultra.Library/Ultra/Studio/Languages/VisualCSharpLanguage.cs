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
using ScriptCoreLib.Ultra.Studio.StockTypes;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
    public partial class VisualCSharpLanguage : SolutionProjectLanguage
    {
        public override string ProjectFileExtension { get { return ".csproj"; } }
        public override string CodeFileExtension { get { return ".cs"; } }
        public override string LanguageSpelledName { get { return "Visual CSharp"; } }
        public override string LanguageName { get { return "Visual C#"; } }

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



        public override void WriteMethod(SolutionFile File, SolutionProjectLanguageMethod m, SolutionBuilder Context)
        {
            if (!m.IsLambda)
                if (m.Summary != null)
                    this.WriteSummary(File, m.Summary, m.Parameters.ToArray());

            File.Region(
                delegate
                {
                    if (m.IsLambda)
                    {
                        if (m.Code.History.Count == 1)
                        {
                            var Parameters = m.Parameters.ToArray();

                            if (Parameters.Length != 1)
                                File.Write("(");


                            for (int i = 0; i < Parameters.Length; i++)
                            {
                                if (i > 0)
                                {
                                    File.WriteSpace(",");
                                }

                                File.Write(Parameters[i].Name);
                            }

                            if (Parameters.Length != 1)
                                File.WriteSpace(")");

                            File.WriteSpace("=>");

                            this.WriteMethodBody(File, m.Code, Context);
                            return;
                        }
                    }

                    if (m.IsLambda)
                    {
                        File.Write(Keywords.@delegate);
                        File.WriteSpace();
                    }
                    else
                    {
                        File.WriteIndent();

                        if (m.IsPrivate)
                        {
                            File.Write(Keywords.@private);
                        }
                        else if (m.IsProtected)
                        {
                            File.Write(Keywords.@protected);
                        }
                        else
                        {
                            File.Write(Keywords.@public);
                        }
                        File.WriteSpace();

                        if (m.IsOverride)
                        {
                            File.Write(Keywords.@override);
                            File.WriteSpace();
                        }


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

                    {
                        var Parameters = m.Parameters.ToArray();

                        for (int i = 0; i < Parameters.Length; i++)
                        {
                            if (i > 0)
                            {
                                File.WriteSpace(",");
                            }

                            this.WriteTypeName(File, Parameters[i].Type);

                            File.WriteSpace();
                            File.Write(Parameters[i].Name);
                        }
                    }
                    File.Write(")");
                    if (m.Code == null)
                    {
                        File.Write(";");
                        File.WriteLine();
                    }
                    else
                    {
                        File.WriteLine();

                        this.WriteMethodBody(File, m.Code, Context);

                        if (!m.IsLambda)
                        {
                            File.WriteLine();
                        }
                    }
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
                                File.WriteSpace(Keywords.@if);
                                File.Write("(");
                                WritePseudoExpression(File, If.Expression, Context);
                                File.Write(")");
                                File.WriteLine();

                                WriteMethodBody(File, If.TrueCase, Context);
                                File.WriteLine();

                                if (If.FalseCase != null)
                                {
                                    File.WriteIndent();
                                    File.WriteSpace(Keywords.@else);
                                    File.WriteLine();

                                    WriteMethodBody(File, If.FalseCase, Context);
                                }
                            }

                            return;
                        }

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
                                        File.WriteSpace(Keywords.@return);
                                    }

                                    WritePseudoCallExpression(File, Lambda, Context);
                                    File.WriteLine(";");
                                }
                            }
                        }
                    }
                };

            Action WriteCodeStatementsAsBlock =
                delegate
                {
                    File.WriteIndent();
                    File.WriteLine("{");
                    File.Indent(this, WriteCodeStatements);
                    File.WriteIndent();
                    File.Write("}");
                };

            Code.OwnerIfExpression.With(n => n.IsConditionalCompilationDirective, n => WriteCodeStatementsAsBlock = WriteCodeStatements);

            if (Code.IsLambdaExpression)
                WriteCodeStatementsAsBlock = WriteCodeStatements;

            WriteCodeStatementsAsBlock();
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
                    "@\"" + Value.Replace("\"", "\"\"") + "\""
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
                File.Write(Keywords.@this);
                return;
            }

            var Base = Parameter as PseudoBaseExpression;
            if (Base != null)
            {
                File.Write(Keywords.@base);
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

            // F# match would be awesome here? :)
            var Field = Parameter as SolutionProjectLanguageField;
            if (Field != null)
            {
                // DeclaringType Object?
                File.Write(Field.Name);
            }


            #region PseudoArrayExpression
            var Array = Parameter as PseudoArrayExpression;
            if (Array != null)
            {
                File.Indent(this,
                    delegate
                    {
                        File.WriteLine();
                        File.WriteIndent();

                        File.Write(Keywords.@new);
                        File.WriteSpace();

                        WriteTypeName(File, Array.ElementType);
                        File.Write("[]");

                        File.WriteSpace();
                        File.Write("{");

                        File.Indent(this,
                            delegate
                            {
                                File.WriteLine();
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

                        File.Write("}");

                    }
                );

                File.WriteLine();
                File.WriteIndent();

                return;
            }
            #endregion

        }



        public override void WriteTypeName(SolutionFile File, SolutionProjectLanguageType Type)
        {
            if (Type is KnownStockTypes.System.Boolean)
            {
                File.Write(Keywords.@bool);
                return;
            }

            if (Type is KnownStockTypes.System.String)
            {
                File.Write(Keywords.@string);
                return;
            }

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

        public override void WriteNamespace(SolutionFile File, string Namespace, Action Body)
        {
            File.Write(Keywords.@namespace);
            File.WriteSpace();
            File.Write(Namespace);
            File.WriteLine();
            File.WriteLine("{");
            File.Indent(this, Body);

            File.WriteLine("}");

        }

        public override void WriteType(SolutionFile File, SolutionProjectLanguageType Type, SolutionBuilder Context)
        {
            File.Write(this, Context, Type.Comments);

            File.WriteUsingNamespaceList(this, Type);


            File.WriteLine();

            File.Region(
                delegate
                {
                    WriteNamespace(File, Type.Namespace,
                        delegate
                        {

                            if (Type.Summary != null)
                                this.WriteSummary(
                                    File,
                                    Type.Summary
                                );

                            File.Region(
                                delegate
                                {
                                    File.WriteIndent();

                                    if (Type.IsInternal)
                                    {
                                        File.WriteSpace(Keywords.@internal);
                                    }
                                    else
                                    {
                                        File.WriteSpace(Keywords.@public);
                                    }

                                    if (Type.IsStatic)
                                    {
                                        File.WriteSpace(Keywords.@static);
                                    }


                                    if (Type.IsSealed)
                                    {
                                        File.WriteSpace(Keywords.@sealed);
                                    }

                                    if (Type.IsPartial)
                                    {
                                        File.WriteSpace(Keywords.@partial);
                                    }


                                    if (Type.IsInterface)
                                    {
                                        File.WriteSpace(Keywords.@interface);
                                    }
                                    else
                                    {
                                        File.WriteSpace(Keywords.@class);
                                    }

                                    File.Write(Type);

                                    if (Type.BaseType != null)
                                    {
                                        File.WriteSpaces(":");
                                        WriteTypeName(File, Type.BaseType);
                                    }

                                    File.WriteLine();

                                    File.WriteIndent();
                                    File.WriteLine("{");
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
                                                        File.WriteSpace(Keywords.@private);
                                                    }
                                                    else
                                                    {
                                                        File.WriteSpace(Keywords.@public);
                                                    }

                                                    if (Field.IsReadOnly)
                                                    {
                                                        File.WriteSpace(Keywords.@readonly);
                                                    }

                                                    WriteTypeName(File, Field.FieldType);
                                                    File.WriteSpace();
                                                    File.Write(Field.Name);

                                                    if (Field.FieldConstructor != null)
                                                    {
                                                        File.WriteSpace();
                                                        File.Write("=");
                                                        File.WriteSpace();
                                                        this.WritePseudoCallExpression(File, Field.FieldConstructor, Context);
                                                    }

                                                    File.Write(";");
                                                    File.WriteLine();

                                                    File.WriteLine();
                                                }
                                            );


                                            #endregion


                                            #region Properties
                                            foreach (var m in Type.Properties.ToArray())
                                            {
                                                File.WriteIndent();

                                                if (Type.IsInterface)
                                                {

                                                }
                                                else
                                                {
                                                    File.Write(Keywords.@public);
                                                    File.WriteSpace();

                                                    if (m.IsStatic)
                                                    {
                                                        File.Write(Keywords.@static);
                                                        File.WriteSpace();
                                                    }
                                                }

                                                WriteTypeName(File, m.PropertyType);
                                                File.WriteSpace();
                                                File.Write(m.Name);

                                                if (m.IsAutoProperty)
                                                {
                                                    File.WriteSpace();
                                                    File.Write("{");
                                                }
                                                else
                                                {
                                                    File.WriteLine();
                                                    File.WriteIndent();
                                                    File.WriteLine("{");
                                                }


                                                Action<SolutionProjectLanguageMethod, Keyword> Property = (mm, kk) =>
                                                {
                                                    if (mm != null)
                                                    {
                                                        if (m.IsAutoProperty)
                                                        {
                                                            File.WriteSpace();
                                                        }
                                                        else
                                                        {
                                                            File.WriteIndent();
                                                        }
                                                        File.Write(kk);
                                                        if (mm.Code == null)
                                                        {
                                                            File.Write(";");
                                                            if (m.IsAutoProperty)
                                                            {
                                                            }
                                                            else
                                                            {
                                                                File.WriteLine();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            File.WriteLine();
                                                            this.WriteMethodBody(File, mm.Code, Context);
                                                        }
                                                    }
                                                };

                                                Action PropertyBody = delegate
                                                {
                                                    Property(m.GetMethod, Keywords.get);
                                                    Property(m.SetMethod, Keywords.set);
                                                };

                                                Action<Action> PropertyIndent = Body => File.Indent(this, Body);

                                                if (m.IsAutoProperty)
                                                {
                                                    PropertyBody();
                                                    File.WriteSpace();
                                                }
                                                else
                                                {
                                                    File.Indent(this, PropertyBody);
                                                    File.WriteIndent();
                                                }


                                                File.WriteLine("}");
                                            }
                                            #endregion

                                            if (Type.Properties.Any())
                                                File.WriteLine();

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


                                        }
                                    );

                                    File.WriteIndent();
                                    File.WriteLine("}");
                                }
                            );
                        }
                    );


                }
            );

        }


        public override void WriteUsingNamespace(SolutionFile File, string item)
        {
            File.WriteIndent();
            File.Write(Keywords.@using);
            File.WriteSpace();
            File.Write(item);
            File.WriteLine(";");
        }

        public override void WriteAssemblyAttributeNamespace(SolutionFile File, string Namespace, Action Body)
        {
            Body();
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
