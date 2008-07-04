﻿using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using System.Text;
using System;
using System.Linq;
using System.Collections.Specialized;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ConvertASToCS.js.Any;


namespace ConvertASToCS.js.Any
{
    [Script]
    public class ProxyConverter : ConverterBase
    {
        /// <summary>
        /// Without colors
        /// </summary>
        /// <param name="r"></param>
        /// <param name="Write"></param>
        public static void RenderProxyTo(ProxyProvider r, Action<string> Write, TypeInfo Domain)
        {
            RenderProxyTo(r, c => Write, Write, Domain);
        }

        public static void RenderProxyTo(ProxyProvider r, Func<Color, Action<string>> ToColorWrite, Action<string> Write, TypeInfo Domain)
        {

            Action WriteLine = () => Write("\r\n");
            Action WriteSpace = () => Write(" ");

            #region Write<Color>
            Action<string> WriteBlue = ToColorWrite(Color.Blue);
            Action<string> WriteBlack = ToColorWrite(Color.Black);
            Action<string> WriteGray = ToColorWrite(Color.FromRGB(0x80, 0x80, 0x80));
            Action<string> WriteCyan = ToColorWrite(Color.FromRGB(0, 0x80, 0x80));
            Action<string> WriteGreen = ToColorWrite(Color.FromRGB(0, 0x80, 0));
            #endregion

            int Indent = 0;

            Action WriteIdent = () => Write(new string(' ', 4 * Indent));

            #region CodeBlock
            Func<IDisposable> CodeBlock =
                delegate
                {
                    WriteIdent();
                    Write("{");
                    WriteLine();

                    Indent++;

                    return new Disposable(
                        delegate
                        {
                            Indent--;

                            WriteIdent();
                            Write("}");
                            WriteLine();
                        }
                    );
                };
            #endregion



            #region Region
            Func<string, IDisposable> Namespace =
                text =>
                {
                    WriteIdent();
                    WriteBlue("namespace");
                    WriteSpace();
                    Write(text);
                    WriteLine();

                    return CodeBlock();
                };
            #endregion

            #region Region
            Func<string, IDisposable> Region =
                text =>
                {
                    WriteIdent();
                    WriteBlue("#region");
                    WriteSpace();
                    Write(text);
                    WriteLine();

                    return new Disposable(
                        delegate
                        {
                            WriteIdent();
                            WriteBlue("#endregion");
                            WriteLine();
                        }
                    );
                };
            #endregion


            #region WriteSummary
            Action<string> WriteSummary =
                text =>
                {
                    WriteIdent();
                    WriteGray("/// <summary>");
                    WriteLine();

                    WriteIdent();
                    WriteGray("/// ");
                    WriteGreen(text);
                    WriteLine();

                    WriteIdent();
                    WriteGray("/// </summary>");
                    WriteLine();
                };
            #endregion


            #region WriteTypeName
            Action<string> WriteTypeName =
                text =>
                {
                    var z = FixTypeName(text.Trim());

                    if (CSharpKeywords.Contains(z))
                        WriteBlue(z);
                    else
                        WriteCyan(z);
                };
            #endregion

            Action<string, string> WriteVariableDefinition =
                (TypeName, VariableName) =>
                {
                    WriteTypeName(TypeName);
                    WriteSpace();
                    Write(FixVariableName(VariableName));
                };


            #region IndentLine
            Func<IDisposable> IndentLine =
                 () =>
                 {
                     WriteIdent();

                     return new Disposable(
                         delegate
                         {
                             WriteLine();
                         }
                     );
                 };
            #endregion

            Action<string> WriteAttributeLine =
                Name =>
                {
                    using (IndentLine())
                    {
                        Write("[");
                        WriteCyan(Name);
                        Write("]");
                    }
                };

            #region Conditional
            Func<string, IDisposable> Conditional =
                Expression =>
                {
                    WriteGray("#if " + Expression);
                    WriteLine();


                    return new Disposable(
                        delegate
                        {
                            WriteGray("#endif");
                            WriteLine();
                        }
                    );
                };
            #endregion


            #region WriteCommentLine
            Action<string> WriteCommentLine =
                 text =>
                 {
                     using (IndentLine())
                     {
                         WriteGreen("// " + text);
                     }
                 };
            #endregion


            #region InlineCodeBlock
            Func<IDisposable> InlineCodeBlock =
                () =>
                {
                    Write("{");
                    WriteSpace();

                    return new Disposable(
                        delegate
                        {
                            WriteSpace();
                            Write("}");
                        }
                    );
                };
            #endregion


            #region Parentheses
            Func<IDisposable> Parenthesis =
                 () =>
                 {
                     Write("(");


                     return new Disposable(
                         delegate
                         {
                             Write(")");
                         }
                     );
                 };
            #endregion

            #region Parentheses
            Func<IDisposable> Quotes =
                 () =>
                 {
                     Write("\"");


                     return new Disposable(
                         delegate
                         {
                             Write("\"");
                         }
                     );
                 };
            #endregion

            Action WriteAssignment =
                delegate
                {
                    WriteSpace();
                    Write("=");
                    WriteSpace();
                };

            Action<string, string> WriteStaticMethodName =
                (TypeName, MethodName) =>
                {
                    WriteCyan(TypeName);
                    Write(".");
                    Write(MethodName);


                };

            Action<string, string> WriteInstanceMethodName =
                  (VariableName, MethodName) =>
                  {
                      Write(VariableName);
                      Write(".");
                      Write(MethodName);


                  };

            #region DefineType
            Func<TypeInfo, IDisposable> DefineType =
               i =>
               {
                   if (i == null)
                       return new Disposable(null);

                   var ns = default(IDisposable);

                   if (!string.IsNullOrEmpty(i.Namespace))
                   {
                       ns = Namespace(i.Namespace);
                   }

                   var re = Region(i.Name);

                   if (!i.NoAttributes)
                   {
                       using (Conditional("!NoAttributes"))
                           WriteAttributeLine("Script");

                       WriteAttributeLine("CompilerGenerated");
                   }

                   WriteIdent();
                   WriteBlue("public");

                   if (i.IsSealed)
                   {
                       WriteSpace();
                       WriteBlue("sealed");
                   }

                   if (i.IsAbstract)
                   {
                       WriteSpace();
                       WriteBlue("abstract");
                   }

                   WriteSpace();
                   WriteBlue("partial");

                   WriteSpace();
                   WriteBlue("class");
                   WriteSpace();
                   WriteCyan(i.Name);

                   if (!string.IsNullOrEmpty(i.BaseTypeName))
                   {
                       WriteSpace();
                       Write(":");
                       WriteSpace();
                       WriteCyan(i.BaseTypeName);
                   }

                   WriteLine();

                   var c = CodeBlock();

                   if (i.Fields != null)
                   {
                       foreach (var f in i.Fields)
                       {
                           if (!string.IsNullOrEmpty(f.AccessedThroughProperty))
                           {
                               using (IndentLine())
                               {
                                   Write("[");
                                   WriteCyan("AccessedThroughProperty");

                                   using (Parenthesis())
                                   using (Quotes())
                                       Write(f.AccessedThroughProperty);

                                   Write("]");
                               }
                           }

                           using (IndentLine())
                           {
                               if (f.IsPrivate)
                               {
                                   WriteBlue("private");
                                   WriteSpace();
                               }
                               else
                               {
                                   WriteBlue("public");
                                   WriteSpace();
                               }

                               if (f.IsReadOnly)
                               {
                                   WriteBlue("readonly");
                                   WriteSpace();
                               }

                               WriteVariableDefinition(f.TypeName, f.FieldName);
                               Write(";");
                           }

                       }
                   }

                   return new Disposable(
                    delegate
                    {
                        c.Dispose();
                        re.Dispose();

                        if (ns != null)
                            ns.Dispose();
                    }
                );
               };
            #endregion

            Action<string> UsingNamespace =
                text =>
                {
                    WriteIdent();
                    WriteBlue("using");
                    WriteSpace();
                    Write(text);
                    Write(";");
                    WriteLine();
                };

            var UsingNamespaces = UsingNamespace.AsParamsAction();


            if (Domain != null)
            {

                UsingNamespaces(
                    "System",
                    "System.Collections.Generic",
                    "System.Text",
                    "System.Diagnostics",
                    "System.Runtime.CompilerServices"
                );

                using (Conditional("!NoAttributes"))
                    UsingNamespace("ScriptCoreLib");
            }

            using (DefineType(Domain))
            {
                var MessagesEnumName = "Messages";

                #region Messages
                using (Region(MessagesEnumName))
                {
                    using (Conditional("!NoAttributes"))
                        WriteAttributeLine("Script");

                    WriteAttributeLine("CompilerGenerated");

                    WriteIdent();
                    WriteBlue("public");
                    WriteSpace();
                    WriteBlue("enum");
                    WriteSpace();
                    WriteCyan(MessagesEnumName);
                    WriteLine();

                    using (CodeBlock())
                    {
                        WriteIdent();
                        Write("None");
                        WriteAssignment();
                        Write("100");
                        Write(",");
                        WriteLine();

                        foreach (var v in r.MethodDefinitions)
                        {
                            WriteIdent();
                            Write(v.Name);
                            Write(",");
                            WriteLine();
                        }
                    }
                }
                #endregion

                WriteLine();


                #region RemoteMessages
                using (DefineType(
                        new TypeInfo
                        {
                            IsSealed = true,
                            Name = "RemoteMessages",
                            BaseTypeName = "IMessages",
                            Fields = new[]
                        {
                            new FieldInfo { FieldName = "Send", TypeName = "Action<SendArguments>" }
                        }
                        }
                    ))
                {

                    #region SendArguments
                    using (DefineType(
                            new TypeInfo
                            {
                                IsSealed = true,
                                Name = "SendArguments",
                                Fields = new[]
                            {
                                new FieldInfo { FieldName = "i", TypeName = MessagesEnumName },
                                new FieldInfo { FieldName = "args", TypeName = "object[]" },
                            }
                            }
                        ))
                    {

                    }
                    #endregion

                    foreach (var v in r.MethodDefinitions)
                    {
                        //public void TeleportTo(int x, int y)


                        WriteIdent();
                        WriteBlue("public");
                        WriteSpace();
                        WriteBlue("void");
                        WriteSpace();
                        Write(v.Name);

                        using (Parenthesis())
                            for (int k = 0; k < v.ParametersInfo.Parameters.Length; k++)
                            {
                                if (k > 0)
                                {
                                    Write(",");
                                    WriteSpace();
                                }

                                WriteVariableDefinition(v.ParametersInfo.Parameters[k].TypeName, v.ParametersInfo.Parameters[k].Name);
                            }

                        WriteLine();

                        //{
                        //    Send(new SendArguments { i = Messages.TeleportTo, args = new object[] { x, y } });
                        //}

                        using (CodeBlock())
                        {
                            var SingleArray = v.ParametersInfo.SingleArrayParameter;
                            var SingleObjectArray = SingleArray != null ? SingleArray.ElementTypeName == "object" : false;
                            var SignleArrayConverted = SingleArray != null && !SingleObjectArray;

                            if (SignleArrayConverted)
                            {
                                using (IndentLine())
                                {
                                    WriteBlue("var");
                                    WriteSpace();
                                    Write("args");
                                    WriteAssignment();
                                    WriteBlue("new");
                                    WriteSpace();
                                    WriteBlue("object");
                                    Write("[");
                                    Write(SingleArray.Name);
                                    Write(".");
                                    Write("Length");
                                    Write("]");
                                    Write(";");
                                }

                                using (IndentLine())
                                {
                                    WriteStaticMethodName("Array", "Copy");

                                    using (Parenthesis())
                                    {
                                        Write(SingleArray.Name);

                                        Write(",");
                                        WriteSpace();

                                        Write("args");

                                        Write(",");
                                        WriteSpace();

                                        WriteInstanceMethodName(SingleArray.Name, "Length");
                                    }

                                    Write(";");
                                }
                            }

                            using (IndentLine())
                            {
                                WriteBlack("Send");
                                using (Parenthesis())
                                {
                                    WriteBlue("new");
                                    WriteSpace();
                                    WriteCyan("SendArguments");
                                    WriteSpace();

                                    using (InlineCodeBlock())
                                    {
                                        Write("i");
                                        WriteAssignment();

                                        WriteCyan(MessagesEnumName);
                                        Write(".");
                                        Write(v.Name);

                                        Write(",");
                                        WriteSpace();

                                        Write("args");
                                        WriteAssignment();

                                        if (SignleArrayConverted)
                                        {
                                            Write("args");
                                        }
                                        else if (SingleObjectArray)
                                        {
                                            Write(SingleArray.Name);
                                        }
                                        else
                                        {
                                            WriteBlue("new");
                                            WriteSpace();
                                            WriteBlue("object");
                                            Write("[]");
                                            WriteSpace();

                                            using (InlineCodeBlock())
                                            {
                                                for (int k = 0; k < v.ParametersInfo.Parameters.Length; k++)
                                                {
                                                    if (k > 0)
                                                    {
                                                        Write(",");
                                                        WriteSpace();
                                                    }

                                                    Write(v.ParametersInfo.Parameters[k].Name);
                                                }
                                            }
                                        }
                                    }
                                }
                                Write(";");

                            }
                        }
                    }
                }
                #endregion


                WriteLine();

                var RemoteEvents_DispatchTable = new FieldInfo { FieldName = "DispatchTable", TypeName = "Dictionary<" + MessagesEnumName + ", Action<DispatchHelper>>", IsPrivate = true, IsReadOnly = true };
                var RemoteEvents_DispatchTableDelegates = new FieldInfo { FieldName = "DispatchTableDelegates", TypeName = "Dictionary<" + MessagesEnumName + ", Converter<object, Delegate>>", IsPrivate = true, IsReadOnly = true };

                // client -> server -> other clients
                // DoMyProcedure(...)
                // UserDoMyProcedure(..., int user, ...)
                var WithUserArguments_user = new FieldInfo { FieldName = "user", TypeName = "int" };
                var WithUserArguments =
                    new TypeInfo
                    {
                        IsSealed = false,
                        IsAbstract = true,
                        Name = "WithUserArguments",
                        Fields = new[]
                                 {
                                     WithUserArguments_user
                                 }.ToArray()
                    };

                var WithUserArgumentsRouter_RemoteMessages = new FieldInfo { FieldName = "Target", TypeName = "RemoteMessages" };
                var WithUserArgumentsRouter =
                    new TypeInfo
                    {
                        IsSealed = true,
                        Name = "WithUserArgumentsRouter",
                        BaseTypeName = WithUserArguments.Name,
                        Fields = new[]
                                     {
                                         WithUserArgumentsRouter_RemoteMessages
                                     }.ToArray()
                    };


                var RemoteEvents_Router = new FieldInfo
                {
                    FieldName = "_Router",
                    TypeName = WithUserArgumentsRouter.Name,
                    IsPrivate = true,
                    AccessedThroughProperty = "Router"
                };


                #region RemoteEvents
                using (DefineType(
                        new TypeInfo
                        {
                            IsSealed = true,
                            Name = "RemoteEvents",
                            Fields = new[]
                            {
                                RemoteEvents_DispatchTable,
                                RemoteEvents_DispatchTableDelegates,
                                RemoteEvents_Router
                            }
                        }
                    ))
                {
                    var KnownConverters = new Dictionary<string, string>
                    {
                        { "int", "GetInt32" },
                        { "double", "GetDouble" },
                        { "string", "GetString" },
                        
                        { "int[]", "GetInt32Array" },
                        { "double[]", "GetDoubleArray" },
                        { "string[]", "GetStringArray" },

                        { "object[]", "GetArray" },
                    };



                    #region DispatchHelper
                    using (DefineType(
                            new TypeInfo
                            {
                                IsSealed = false,
                                Name = "DispatchHelper",
                                Fields =
                                    KnownConverters.AsEnumerable().Select(
                                        i => new FieldInfo { FieldName = i.Value, TypeName = "Converter<uint, " + i.Key + ">" }
                                    ).ToArray()
                            }
                        ))
                    {
                    }
                    #endregion

                    // public bool Dispatch(Messages e, DispatchHelper h)
                    //{
                    //    if (!DispatchTable.ContainsKey(e))
                    //        return false;

                    //    DispatchTable[e](h);

                    //    return true;
                    //}

                    #region Dispatch
                    using (IndentLine())
                    {
                        WriteBlue("public");
                        WriteSpace();

                        WriteBlue("bool");
                        WriteSpace();

                        Write("Dispatch");

                        using (Parenthesis())
                        {
                            WriteVariableDefinition(MessagesEnumName, "e");
                            Write(",");
                            WriteSpace();
                            WriteVariableDefinition("DispatchHelper", "h");

                        }
                    }

                    using (CodeBlock())
                    {
                        using (IndentLine()) Write("if (!DispatchTableDelegates.ContainsKey(e)) return false;");
                        using (IndentLine()) Write("if (DispatchTableDelegates[e](null) == null) return false;");
                        using (IndentLine()) Write("if (!DispatchTable.ContainsKey(e)) return false;");
                        using (IndentLine()) Write("DispatchTable[e](h);");
                        using (IndentLine()) Write("return true;");
                    }
                    #endregion


                    #region WithUserArguments



                    using (DefineType(WithUserArguments))
                    {
                    }

                    #endregion

                    Func<ProxyProvider.MethodParametersInfo.ParamInfo, bool> IsUserParameter =
                           i => i.Name == WithUserArguments_user.FieldName && i.TypeName == WithUserArguments_user.TypeName;

                    var IsNotUserParameter = IsUserParameter.AsNegative();

                    Func<ProxyProvider.MethodDefinition, bool> IsUserArguments =
                        v => v.Name.StartsWith("User") && v.ParametersInfo.Parameters.Any(IsUserParameter);





                    #region WithUserArgumentsRouter



                    using (DefineType(WithUserArgumentsRouter))
                    {
                        using (Region("Routing"))
                        {
                            foreach (var v in r.MethodDefinitions.Where(IsUserArguments))
                            {
                                //public void UserTeleportTo(TeleportToArguments e)
                                //{
                                //    Target.UserTeleportTo(this.user, e.x, e.y);
                                //}

                                using (IndentLine())
                                {
                                    WriteBlue("public");
                                    WriteSpace();

                                    WriteBlue("void");
                                    WriteSpace();

                                    Write(v.Name);

                                    using (Parenthesis())
                                    {
                                        // remove User prefix
                                        WriteVariableDefinition(v.Name.Substring(4) + "Arguments", "e");
                                    }
                                }

                                using (CodeBlock())
                                using (IndentLine())
                                {
                                    //WriteBlue("return");
                                    //WriteSpace();

                                    Write(WithUserArgumentsRouter_RemoteMessages.FieldName);
                                    Write(".");
                                    Write(v.Name);

                                    using (Parenthesis())
                                    {
                                        for (int i = 0; i < v.ParametersInfo.Parameters.Length; i++)
                                        {
                                            if (i > 0)
                                                Write(", ");

                                            var p = v.ParametersInfo.Parameters[i];

                                            if (p.Name == WithUserArguments_user.FieldName)
                                            {
                                                WriteBlue("this");
                                            }
                                            else
                                            {
                                                Write("e");
                                            }
                                            Write(".");
                                            Write(p.Name);
                                        }
                                    }

                                    Write(";");
                                }

                                //WriteCommentLine(v.Name);
                            }
                        }

                    }
                    #endregion


                    #region events
                    foreach (var v in r.MethodDefinitions)
                    {
                        var SelectedArguments = default(TypeInfo);

                        if (IsUserArguments(v))
                        {
                            SelectedArguments = new TypeInfo
                            {
                                IsSealed = true,
                                Name = v.Name + "Arguments",
                                BaseTypeName = WithUserArguments.Name,
                                Fields = v.ParametersInfo.Parameters.Where(IsNotUserParameter).Select(i =>
                                    new FieldInfo { FieldName = i.Name, TypeName = i.TypeName }
                                ).ToArray()
                            };
                        }
                        else
                        {
                            SelectedArguments = new TypeInfo
                            {
                                IsSealed = true,
                                Name = v.Name + "Arguments",
                                Fields = v.ParametersInfo.Parameters.Select(i =>
                                    new FieldInfo { FieldName = i.Name, TypeName = i.TypeName }
                                ).ToArray()
                            };
                        }

                        #region ~Arguments
                        using (DefineType(SelectedArguments))
                        {
                            // ToString

                            WriteAttributeLine("DebuggerHidden");

                            using (IndentLine())
                            {
                                WriteBlue("public");
                                WriteSpace();
                                WriteBlue("override");
                                WriteSpace();
                                WriteBlue("string");
                                WriteSpace();
                                Write("ToString");
                                Write("()");
                            }

                            using (CodeBlock())
                            using (IndentLine())
                            {


                                WriteBlue("return");
                                WriteSpace();
                                WriteBlue("new");
                                WriteSpace();
                                WriteCyan("StringBuilder");
                                Write("()");

                                for (int i = 0; i < v.ParametersInfo.Parameters.Length; i++)
                                {
                                    var IsFirst = i == 0;
                                    var IsLast = i == v.ParametersInfo.Parameters.Length - 1;

                                    var p = v.ParametersInfo.Parameters[i];

                                    Write(".");
                                    Write("Append");

                                    using (Parenthesis())
                                    using (Quotes())
                                    {
                                        if (IsFirst)
                                            Write("{ ");
                                        else
                                            Write(", ");

                                        Write(p.Name);

                                        WriteAssignment();
                                    }

                                    Write(".");
                                    Write("Append");

                                    using (Parenthesis())
                                    {
                                        WriteBlue("this");
                                        Write(".");
                                        Write(p.Name);
                                    }

                                    if (IsLast)
                                    {
                                        Write(".");
                                        Write("Append");

                                        using (Parenthesis())
                                        using (Quotes())
                                        {
                                            Write(" }");
                                        }

                                    }
                                }

                                Write(".");
                                Write("ToString");
                                Write("()");
                                Write(";");

                            }

                            //[DebuggerHidden]
                            //public override string ToString()
                            //{
                            //    StringBuilder builder = new StringBuilder();
                            //    builder.Append("{ bullets = ");
                            //    builder.Append(this.<bullets>i__Field);
                            //    builder.Append(", runaways = ");
                            //    builder.Append(this.<runaways>i__Field);
                            //    builder.Append(", gore = ");
                            //    builder.Append(this.<gore>i__Field);
                            //    builder.Append(", score = ");
                            //    builder.Append(this.<score>i__Field);
                            //    builder.Append(" }");
                            //    return builder.ToString();
                            //}





                        }
                        #endregion

                        // public event Action<TeleportToArguments> TeleportTo;

                        using (IndentLine())
                        {
                            WriteBlue("public");
                            WriteSpace();
                            WriteBlue("event");
                            WriteSpace();
                            WriteVariableDefinition("Action<" + v.Name + "Arguments" + ">", v.Name);
                            Write(";");
                        }

                    }
                    #endregion


                    #region ctor
                    using (IndentLine())
                    {
                        WriteBlue("public");
                        WriteSpace();

                        Write("RemoteEvents");

                        using (Parenthesis())
                        {
                        }
                    }
                    using (CodeBlock())
                    {
                        #region DispatchTable
                        using (IndentLine())
                        {
                            Write("DispatchTable");
                            WriteAssignment();
                            WriteBlue("new");
                            WriteSpace();
                            WriteCyan("Dictionary<" + MessagesEnumName + ", Action<DispatchHelper>>");
                        }

                        Indent += 2;

                        using (CodeBlock())
                        {
                            foreach (var v in r.MethodDefinitions)
                            {
                                using (IndentLine())
                                {
                                    using (InlineCodeBlock())
                                    {
                                        WriteCyan(MessagesEnumName);
                                        Write(".");
                                        Write(v.Name);

                                        Write(",");
                                        WriteSpace();


                                        Write("e => ");

                                        using (InlineCodeBlock())
                                        {

                                            Write(v.Name);

                                            using (Parenthesis())
                                            {
                                                WriteBlue("new");
                                                WriteSpace();
                                                WriteCyan(v.Name + "Arguments");
                                                WriteSpace();

                                                using (InlineCodeBlock())
                                                {
                                                    for (int k = 0; k < v.ParametersInfo.Parameters.Length; k++)
                                                    {
                                                        if (k > 0)
                                                        {
                                                            Write(",");
                                                            WriteSpace();
                                                        }

                                                        var p = v.ParametersInfo.Parameters[k];

                                                        Write(p.Name);
                                                        WriteAssignment();
                                                        Write("e");
                                                        Write(".");



                                                        Write(KnownConverters[p.TypeName]);

                                                        using (Parenthesis())
                                                            Write("" + k);

                                                    }
                                                }


                                            }

                                            Write(";");
                                        }
                                    }

                                    Write(",");
                                }

                            }
                        }

                        Indent -= 2;

                        using (IndentLine())
                            Write(";");

                        #endregion

                        #region DispatchTableDelegates
                        using (IndentLine())
                        {
                            Write("DispatchTableDelegates");
                            WriteAssignment();
                            WriteBlue("new");
                            WriteSpace();
                            WriteCyan(RemoteEvents_DispatchTableDelegates.TypeName);
                        }
                        Indent += 2;
                        using (CodeBlock())
                        {
                            foreach (var v in r.MethodDefinitions)
                            {
                                using (IndentLine())
                                {
                                    using (InlineCodeBlock())
                                    {
                                        WriteCyan(MessagesEnumName);
                                        Write(".");
                                        Write(v.Name);

                                        Write(",");
                                        WriteSpace();

                                        Write("e => ");

                                        Write(v.Name);
                                    }

                                    Write(",");
                                }

                            }
                        }
                        Indent -= 2;
                        using (IndentLine())
                            Write(";");

                        #endregion

                    }
                    #endregion


                    #region Router { get; set; }
                    using (IndentLine())
                    {
                        WriteBlue("public");
                        WriteSpace();
                        WriteVariableDefinition(
                            RemoteEvents_Router.TypeName,
                            RemoteEvents_Router.AccessedThroughProperty
                        );
                    }



                    using (CodeBlock())
                    {
                        #region get
                        WriteAttributeLine("DebuggerNonUserCode");

                        using (IndentLine())
                            WriteBlue("get");

                        using (CodeBlock())
                        using (IndentLine())
                        {
                            WriteBlue("return");
                            WriteSpace();
                            WriteBlue("this");
                            Write(".");
                            Write(RemoteEvents_Router.FieldName);
                            Write(";");
                        }
                        #endregion

                        WriteAttributeLine("DebuggerNonUserCode");

                        using (IndentLine())
                        {
                            Write("[");
                            WriteCyan("MethodImpl");

                            using (Parenthesis())
                            {
                                WriteCyan("MethodImplOptions");
                                Write(".");
                                Write("Synchronized");
                            }

                            Write("]");
                        }

                        using (IndentLine())
                            WriteBlue("set");

                        using (CodeBlock())
                        {
                            // do like the vb does

                            

                            #region remove
                            using (IndentLine())
                            {
                                WriteBlue("if");
                                using (Parenthesis())
                                {
                                    Write(RemoteEvents_Router.FieldName);
                                    WriteSpace();
                                    Write("!=");
                                    WriteSpace();
                                    WriteBlue("null");
                                }
                            }
                            using (CodeBlock())
                            {
                                //_X.C -= action;
                                //_X.B -= action2;
                                //_X.A -= action3;

                                foreach (var v in r.MethodDefinitions.Where(IsUserArguments))
                                {
                                    using (IndentLine())
                                    {
                                        WriteBlue("this");
                                        Write(".");
                                        Write(v.Name.Substring(4));
                                        WriteSpace();
                                        Write("-=");
                                        WriteSpace();

                                        Write(RemoteEvents_Router.FieldName);
                                        Write(".");
                                        Write(v.Name);
                                        Write(";");
                                    }

                                }
                            }
                            #endregion

                            using (IndentLine())
                            {
                                Write(RemoteEvents_Router.FieldName);
                                WriteAssignment();
                                WriteBlue("value");
                                Write(";");
                            }

                            #region add
                            using (IndentLine())
                            {
                                WriteBlue("if");
                                using (Parenthesis())
                                {
                                    Write(RemoteEvents_Router.FieldName);
                                    WriteSpace();
                                    Write("!=");
                                    WriteSpace();
                                    WriteBlue("null");
                                }
                            }
                            using (CodeBlock())
                            {
                                //_X.C += action;
                                //_X.B += action2;
                                //_X.A += action3;

                                foreach (var v in r.MethodDefinitions.Where(IsUserArguments))
                                {
                                    using (IndentLine())
                                    {
                                        WriteBlue("this");
                                        Write(".");
                                        Write(v.Name.Substring(4));
                                        WriteSpace();
                                        Write("+=");
                                        WriteSpace();

                                        Write(RemoteEvents_Router.FieldName);
                                        Write(".");
                                        Write(v.Name);
                                        Write(";");
                                    }

                                }
                            }
                            #endregion

                            // -=

                            // +=
                        }
                    }
                    #endregion
                }
                #endregion
            }

        }



    }

}
