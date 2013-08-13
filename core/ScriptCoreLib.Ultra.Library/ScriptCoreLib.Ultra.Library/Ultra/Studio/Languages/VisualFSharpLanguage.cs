﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
    public partial class VisualFSharpLanguage : SolutionProjectLanguage
    {
        // roslyn vs reflection emit vs compilerjob

        public override string ProjectFileExtension { get { return ".fsproj"; } }
        public override string CodeFileExtension { get { return ".fs"; } }
        public override string LanguageSpelledName { get { return "Visual FSharp"; } }
        public override string LanguageName { get { return "Visual F#"; } }

        public override string Kind
        {
            get { return "{F2A71F9B-5D33-465A-A702-920D77279786}"; }
        }

        public override void WriteLinkCommentLine(SolutionFile File, Uri Link)
        {
            File.Write(SolutionFileTextFragment.Comment, "// ").WriteLine(Link);
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
            #region WriteMethodBody
            Action WriteMethodBody =
                delegate
                {
                    this.WriteMethodBody(File, Method.Code, Context);

                    if (!Method.IsConstructor)
                    {
                        File.WriteIndent();

                        // empty?
                        var IsStatic = Method.IsStatic;

                        if (Method.DeclaringType != null)
                            if (Method.DeclaringType.IsStatic)
                                IsStatic = true;

                        if (IsStatic)
                        {
                            File.Write("0");
                        }
                        else
                        {
                            if (Method.ReturnType == null)
                                File.Write("()");
                        }

                        File.WriteLine();
                    }
                };
            #endregion


            if (Method.IsLambda)
            {
                var Parameters = Method.Parameters.ToQueue();

                var rec = default(Action<bool>);

                rec = WriteIndent =>
                {
                    if (WriteIndent)
                        File.WriteIndent();

                    File.WriteSpace(Keywords.fun);

                    if (Parameters.Count == 0)
                    {
                        File.Write("(").Write(")").WriteSpace();
                    }
                    else
                    {
                        InternalWriteParameters(File, Method, Parameters.Dequeue());
                    }

                    File.WriteSpaces("->").WriteLine();

                    File.Indent(this,
                        delegate
                        {
                            if (Parameters.Count == 0)
                                WriteMethodBody();
                            else
                                rec(true);
                        }
                    );

                };

                rec(false);

            }
            else
            {
                this.WriteSummary(File, Method.Summary, Method.Parameters.ToArray());


                File.WriteIndent();

                if (Method.Name == "Main")
                {
                    File.Write("[<Microsoft.FSharp.Core.EntryPoint>]");
                    File.WriteLine();
                    File.WriteIndent();
                }

                if (Method.DeclaringType.IsStatic)
                {
                    File.WriteSpace(Keywords.let);
                }
                else
                {
                    if (Method.IsOverride)
                        File.WriteSpace(Keywords.@override);
                    else
                        File.WriteSpace(Keywords.member);

                    File.Write("this").Write(".");
                }
                File.Write(Method.Name);
                File.Write("(");
                InternalWriteParameters(File, Method, Method.Parameters.ToArray());
                File.Write(")");
                File.WriteSpace().WriteLine("=");
                File.Indent(this, WriteMethodBody);
            }


        }

        private void InternalWriteParameters(SolutionFile File, SolutionProjectLanguageMethod Method, params SolutionProjectLanguageArgument[] Parameters)
        {

            for (int i = 0; i < Parameters.Length; i++)
            {
                if (i > 0)
                {
                    File.WriteSpace(",");
                }

                File.Write(Parameters[i].Name);

                if (Method.IsLambda)
                {
                    // no types for lamdas!
                }
                else
                {
                    Parameters[i].Type.With(
                        ParameterType =>
                        {
                            File.WriteSpaces(":");

                            this.WriteTypeName(File, Parameters[i].Type);
                        }
                    );
                }
            }
        }

        public override void WriteMethodBody(SolutionFile File, SolutionProjectLanguageCode Code, SolutionBuilder Context)
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
                    if (If.IsConditionalCompilationDirective)
                    {
                        this.WriteConditionalCompilation(File, If, Context);

                    }
                    else
                    {

                        File.WriteIndent();
                        File.WriteSpace(Keywords.@if);
                        WritePseudoExpression(File, If.Expression, Context);
                        File.WriteSpace();
                        File.Write(Keywords.@then);
                        File.WriteLine();

                        WriteMethodBody(File, If.TrueCase, Context);

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
                #endregion

                #region Lambda
                var Lambda = item as PseudoCallExpression;

                if (Lambda != null)
                {
                    if (Lambda.Comment != null)
                        Lambda.Comment.WriteTo(File, this, Context);

                    if (Lambda.Method != null)
                    {
                        File.WriteIndent();

                        if (IsReturnStatement)
                        {
                            WritePseudoCallExpression(File, Lambda, Context);
                        }
                        else
                        {
                            var ImplicitField = default(SolutionProjectLanguageField);

                            if (Lambda.Method.IsProperty && Lambda.Object is PseudoThisExpression)
                                ImplicitField = Code.OwnerMethod.DeclaringType.Fields.FirstOrDefault(
                                    k => k.Name == Lambda.Method.Name.SkipUntilIfAny("set_") && k.IsReadOnly
                                );

                            if (ImplicitField != null)
                            {
                                File.WriteSpace(Keywords.let).Write(ImplicitField.Name).WriteSpaces("=");


                                WritePseudoExpression(File, Lambda.ParameterExpressions[0], Context);
                            }
                            else
                            {

                                File.WriteSpace(Keywords.@do);

                                // we could group next similar statements in a single do
                                WritePseudoCallExpression(File, Lambda, Context);

                                if (Lambda.Method.ReturnType != null)
                                {
                                    File.WriteSpaces("|>");
                                    File.Write(Keywords.ignore);
                                }
                            }
                        }

                        File.WriteLine();
                    }
                }
                #endregion

            }



        }

        public override void WriteTypeName(SolutionFile File, SolutionProjectLanguageType Type)
        {
            if (Type == null)
                return;

            if (Type is KnownStockTypes.System.String)
            {
                File.Write("string");
                return;
            }

            if (Type is KnownStockTypes.System.Boolean)
            {
                File.Write("bool");
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
            File.WriteSpace(Keywords.@namespace).WriteLine(Namespace);

            File.WriteLine();

            File.Indent(this, Body);
        }

        public override void WriteType(SolutionFile File, SolutionProjectLanguageType Type, SolutionBuilder Context)
        {
            // http://msdn.microsoft.com/en-us/library/dd233205.aspx

            File.Write(this, Context, Type.Comments);

            File.Region(
                delegate
                {
                    WriteNamespace(File, Type.Namespace,
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
                                        File.WriteSpace(Keywords.@module);
                                        WriteTypeName(File, Type);
                                        File.WriteSpace();
                                        File.Write("=");

                                        File.WriteLine();

                                    }
                                    else
                                    {
                                        File.Write("[<Sealed>]");
                                        File.WriteLine();
                                        File.WriteIndent();

                                        File.WriteSpace(Keywords.type);

                                        if (Type.IsInternal)
                                        {
                                            File.WriteSpace(Keywords.@internal);
                                        }

                                        WriteTypeName(File, Type);
                                        File.Write("(");

                                        if (Constructor != null)
                                            this.InternalWriteParameters(File, Constructor, Constructor.Parameters.ToArray());

                                        File.WriteSpace(")");

                                        File.WriteSpace(Keywords.@as);
                                        File.WriteSpace("me");

                                        File.WriteSpace("=");
                                        File.WriteLine();

                                        File.Indent(this,
                                            delegate
                                            {
                                                if (Type.BaseType != null)
                                                {
                                                    File.WriteIndent();
                                                    File.WriteSpace(Keywords.@inherit);

                                                    WriteTypeName(File, Type.BaseType);
                                                    File.Write("(");
                                                    File.WriteSpace(")");
                                                    File.WriteLine();
                                                }

                                                // only need this if there are any members beyond ctor?
                                                File.WriteIndent();
                                                File.WriteSpace(Keywords.@let);
                                                File.Write("this");
                                                File.WriteSpaces("=");
                                                File.Write("me");
                                                File.WriteLine();

                                                File.WriteLine();

                                                File.WriteIndent();
                                                File.WriteSpace(Keywords.@do);
                                                File.Write("()");
                                                File.WriteLine();

                                                File.WriteLine();
                                            }
                                        );


                                    }

                                    // .ctor !

                                    File.Indent(this,
                                        delegate
                                        {
                                            if (!Type.IsStatic)
                                            {
                                                #region Fields with FieldConstructor
                                                Type.Fields.WithEach(
                                                    Field =>
                                                    {
                                                        // http://msdn.microsoft.com/en-us/library/dd469494.aspx




                                                        if (Field.FieldConstructor != null)
                                                        {
                                                            File.WriteIndent().WriteSpace(Keywords.let).Write(Field.Name).WriteSpaces("=");
                                                            this.WritePseudoCallExpression(File, Field.FieldConstructor, Context);
                                                        }
                                                        else
                                                        {
                                                            // first asignment shall do a let
                                                            if (Field.IsReadOnly)
                                                                return;

                                                            File.WriteIndent().WriteSpace(Keywords.let).WriteSpace(Keywords.mutable);
                                                            File.Write(Field.Name).WriteSpaces(":");
                                                            WriteTypeName(File, Field.FieldType);

                                                            File.WriteSpaces("=").Write(Keywords.@null);
                                                        }

                                                        File.WriteLine();
                                                    }
                                                );
                                                #endregion


                                                if (Constructor != null)
                                                {
                                                    this.WriteMethodBody(
                                                        File, Constructor.Code, Context
                                                    );
                                                }

                                                File.WriteLine();
                                                File.WriteLine();
                                            }

                                            foreach (var item in (from m in Type.Methods where !m.IsConstructor select m).ToArray())
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
                                }
                            );
                        }
                    );
                }
            );

        }

        public override void WriteAssemblyAttributeNamespace(SolutionFile File, string Namespace, Action Body)
        {
            this.WriteNamespace(File, Namespace, Body);
        }

        public override void WriteAssemblyAttribute(SolutionFile File, SolutionProjectLanguageAttribute Attribute, SolutionBuilder Context)
        {
            // http://msdn.microsoft.com/en-us/library/dd233179.aspx

            File.WriteIndent();
            File.Write("[");
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
                                IsAttributeContext = true,
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
            File.Write("]");

            File.WriteSpace();
            File.Write(Keywords.@do);
            File.Write("()");

            File.WriteLine();
        }

        public override void WriteUsingNamespace(SolutionFile File, string item)
        {
            File.WriteIndent().WriteSpace(Keywords.@open).WriteLine(item);
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

            #region PseudoStringConstantExpression
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
            #endregion

            #region PseudoInt32ConstantExpression
            {
                var Constant = Parameter as PseudoInt32ConstantExpression;
                if (Constant != null)
                {
                    File.Write("" + Constant.Value);
                    return;
                }
            }
            #endregion

            #region PseudoDoubleConstantExpression
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
            #endregion


            var Call = Parameter as PseudoCallExpression;
            if (Call != null)
            {
                WritePseudoCallExpression(File, Call, Context);
                return;
            }

            var This = Parameter as PseudoThisExpression;
            if (This != null)
            {
                File.Write("this");
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

            // F# match would be awesome here? :)
            var Field = Parameter as SolutionProjectLanguageField;
            if (Field != null)
            {
                // DeclaringType Object?
                File.Write(Field.Name);
                return;
            }

            #region PseudoArrayExpression
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
                        File.Write("[|");
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

                        File.Write("|]");

                    }
                );

                File.WriteLine();
                File.WriteIndent();

                return;
            }
            #endregion


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

                    // anonymous method!
                    if (k is SolutionProjectLanguageMethod)
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

                    if (IsExtensionMethod(Lambda))
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

        public override bool SupportsDependentUpon()
        {
            return false;
        }

        public override bool SupportsPartialTypes()
        {
            return false;
        }
    }
}
