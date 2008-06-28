using ScriptCoreLib;
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


namespace ConvertASToCS.js
{
    partial class ConvertASToCS
    {
      
        private static void RenderProxyTo(ProxyProvider r, IHTMLElement pre)
        {

            IHTMLElement Target = pre;

            Func<Color, Action<string>> ToColorWrite =
                color =>
                    text =>
                    {
                        var s = new IHTMLSpan { innerText = text };

                        s.style.color = color;
                        s.AttachTo(Target);
                    };

            Func<string, IHTMLSpan> Write = text => new IHTMLSpan(text).AttachTo(Target);

            Action WriteLine = () => Write("\r\n");
            Action WriteSpace = () => Write(" ");

            #region Write<Color>
            Action<string> WriteBlue = ToColorWrite(Color.Blue);
            Action<string> WriteBlack = ToColorWrite(Color.Black);
            Action<string> WriteGray = ToColorWrite(Color.FromRGB(0x80, 0x80, 0x80));
            Action<string> WriteCyan = ToColorWrite(Color.FromRGB(0, 0x80, 0x80));
            Action<string> WriteGreen = ToColorWrite(Color.FromRGB(0, 0x80, 0));
            #endregion

            int Indent = 1;

            Action WriteIdent = () => Write(new string(' ', 4 * Indent));

            #region Region
            Func<string, IDisposable> Region =
                text =>
                {
                    WriteIdent();
                    WriteBlue("#region");
                    WriteSpace();
                    var Collapsible = Write(text);
                    WriteLine();

                    var PreviousTarget = Target;
                    var CurrentTarget = new IHTMLElement(IHTMLElement.HTMLElementEnum.pre).AttachTo(PreviousTarget); ;

                    Collapsible.style.cursor = IStyle.CursorEnum.pointer;
                    Collapsible.onclick +=
                        delegate
                        {
                            CurrentTarget.ToggleVisible();
                        };

                    Target = CurrentTarget;

                    return new Disposable(
                        delegate
                        {


                            WriteIdent();
                            WriteBlue("#endregion");
                            WriteLine();

                            Target = PreviousTarget;
                        }
                    );
                };
            #endregion


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

            Action<string> WriteTypeName =
                text =>
                {
                    var z = FixTypeName(text.Trim());

                    if (CSharpKeywords.Contains(z))
                        WriteBlue(z);
                    else
                        WriteCyan(z);
                };

            Action<string, string> WriteVariableDefinition =
                (TypeName, VariableName) =>
                {
                    WriteTypeName(TypeName);
                    WriteSpace();
                    Write(FixVariableName(VariableName));
                };

            Action<string> WriteAttributeLine =
                Name =>
                {
                    WriteIdent();
                    Write("[");
                    WriteCyan(Name);
                    Write("]");
                    WriteLine();
                };

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

            Func<IDisposable> Parentheses =
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
                   var re = Region(i.Name);

                   using (Conditional("!NoAttributes"))
                       WriteAttributeLine("Script");

                   WriteAttributeLine("CompilerGenerated");

                   WriteIdent();
                   WriteBlue("public");

                   if (i.IsSealed)
                   {
                       WriteSpace();
                       WriteBlue("sealed");
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
                    }
                );
               };
            #endregion

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

                    using (Parentheses())
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
                        var SingleObjectArray = false ? SingleArray.ElementTypeName == "object" : false;
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

                                using (Parentheses())
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
                            using (Parentheses())
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

            #region RemoteEvents
            using (DefineType(
                    new TypeInfo
                    {
                        IsSealed = true,
                        Name = "RemoteEvents",
                        Fields = new[]
                            {
                                RemoteEvents_DispatchTable,
                                RemoteEvents_DispatchTableDelegates
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

                    using (Parentheses())
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

                #region events
                foreach (var v in r.MethodDefinitions)
                {
                    #region ~Arguments
                    using (DefineType(
                         new TypeInfo
                         {
                             IsSealed = true,
                             Name = v.Name + "Arguments",
                             Fields = v.ParametersInfo.Parameters.Select(i =>
                                 new FieldInfo { FieldName = i.Name, TypeName = i.TypeName }).ToArray()
                         }
                            ))
                    {
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

                    using (Parentheses())
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

                                        using (Parentheses())
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

                                                    using (Parentheses())
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


            }
            #endregion
        }

        

    }

}
