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

			Action<string> WriteKeywordSpace =
				text =>
				{
					WriteBlue(text);
					WriteSpace();
				};

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


			Action WriteEquals =
				delegate
				{
					WriteSpace();
					Write("==");
					WriteSpace();
				};

			Action WriteInequals =
				delegate
				{
					WriteSpace();
					Write("!=");
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
					   //using (Conditional("!NoAttributes"))
					   WriteAttributeLine("Script");

					   WriteAttributeLine("CompilerGenerated");
				   }

				   WriteIdent();
				   WriteBlue("public");
				   WriteSpace();

				   if (i.IsSealed)
				   {
					   WriteBlue("sealed");
					   WriteSpace();
				   }

				   if (i.IsAbstract)
				   {
					   WriteBlue("abstract");
					   WriteSpace();
				   }

				   WriteBlue("partial");
				   WriteSpace();

				   if (i.IsInterface)
				   {
					   WriteBlue("interface");
				   }
				   else
				   {
					   WriteBlue("class");

				   }

				   WriteSpace();


				   WriteCyan(i.Name);

				   #region base types
				   var BaseTypeNames = new string[] { }.AsEnumerable();

				   if (i.BaseTypeName != null)
					   BaseTypeNames = BaseTypeNames.Concat(new[] { i.BaseTypeName });

				   if (i.BaseTypeNames != null)
					   BaseTypeNames = BaseTypeNames.Concat(i.BaseTypeNames);

				   var BaseTypeNamesArray = BaseTypeNames.Where(j => !string.IsNullOrEmpty(j)).ToArray();

				   for (int ii = 0; ii < BaseTypeNamesArray.Length; ii++)
				   {
					   if (ii == 0)
					   {
						   WriteSpace();
						   Write(":");
					   }
					   else
					   {
						   Write(",");
					   }

					   WriteSpace();
					   WriteCyan(BaseTypeNamesArray[ii]);
				   }
				   #endregion


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

							   if (f.IsProperty)
							   {
								   WriteSpace();
								   using (InlineCodeBlock())
								   {
									   WriteBlue("get");
									   Write(";");
									   WriteSpace();

									   WriteBlue("set");
									   Write(";");
								   }
							   }
							   else
							   {
								   if (!string.IsNullOrEmpty(f.DefaultValue))
								   {
									   WriteAssignment();
									   Write(f.DefaultValue);
								   }

								   Write(";");
							   }
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
					"System.Runtime.CompilerServices",
					"ScriptCoreLib.Shared.Nonoba"
				);

				UsingNamespace("ScriptCoreLib");

				WriteLine();

			}

			using (DefineType(Domain))
			{
				var MessagesEnumName = "Messages";

				#region Messages
				using (Region(MessagesEnumName))
				{
					//using (Conditional("!NoAttributes"))
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

				var WithUserArguments_user = new FieldInfo { FieldName = "user", TypeName = "int" };




				Func<ProxyProvider.MethodParametersInfo.ParamInfo, bool> IsUserParameter =
					i => i.Name == WithUserArguments_user.FieldName && i.TypeName == WithUserArguments_user.TypeName;

				var IsNotUserParameter = IsUserParameter.AsNegative();

				Func<ProxyProvider.MethodDefinition, bool> IsUserArguments =
					v => v.Name.StartsWith("User") && v.ParametersInfo.Parameters.Any(IsUserParameter);




				//public partial interface IEventsFromUserToOthers
				//{
				//    event Action<RemoteEvents.TeleportToArguments> TeleportTo;
				//}

				//public partial interface IMessagesFromUserToOthers
				//{
				//    void UserTeleportTo(int user, int x, int y);
				//}

				var MessagesToOthers = from MessageWithoutUser in r.MethodDefinitions
									   where !MessageWithoutUser.Name.StartsWith("User")
									   let MessageWithUserFilter = r.MethodDefinitions.Where(j => j.Name == "User" + MessageWithoutUser.Name)
									   let MessageWithUser = MessageWithUserFilter.FirstOrDefault(IsUserArguments)
									   where MessageWithUser != null
									   select new
									   {
										   MessageWithoutUser,
										   MessageWithUser
									   };

				var MessagesToOthersArray = MessagesToOthers.ToArray();




				var IMessages = new TypeInfo
				{
					IsInterface = true,

					Name = "IMessages"
				};

				var IEvents = new TypeInfo
				{
					IsInterface = true,

					Name = "IEvents"
				};


				// client -> server -> other clients
				// DoMyProcedure(...)
				// UserDoMyProcedure(..., int user, ...)
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

				var WithUserArgumentsRouter_RemoveDelegates = "RemoveDelegates";
				var WithUserArgumentsRouter_CombineDelegates = "CombineDelegates";
				var WithUserArgumentsRouter_MulticastTarget = new FieldInfo { FieldName = "Target", TypeName = IMessages.Name };
				var WithUserArgumentsRouter_Multicast =
					new TypeInfo
					{
						IsSealed = true,
						Name = "WithUserArgumentsRouter_Broadcast",
						BaseTypeName = WithUserArguments.Name,
						Fields = new[]
                                     {
                                         WithUserArgumentsRouter_MulticastTarget
                                     }.ToArray()
					};

				var WithUserArgumentsRouter_SinglecastViewTarget = new FieldInfo { FieldName = "Target", TypeName = IMessages.Name };
				var WithUserArgumentsRouter_SinglecastView =
					new TypeInfo
					{
						IsSealed = true,
						Name = "WithUserArgumentsRouter_SinglecastView",
						BaseTypeName = WithUserArguments.Name,
						Fields = new[]
                                     {
                                         WithUserArgumentsRouter_SinglecastViewTarget
                                     }.ToArray()
					};

				var WithUserArgumentsRouter_SinglecastTarget = new FieldInfo { FieldName = "Target", TypeName = "System.Converter<int, IMessages>" };
				var WithUserArgumentsRouter_Singlecast =
					new TypeInfo
					{
						IsSealed = true,
						Name = "WithUserArgumentsRouter_Singlecast",
						BaseTypeName = WithUserArguments.Name,
						Fields = new[]
						 {
							 WithUserArgumentsRouter_SinglecastTarget
						 }.ToArray()
					};


				var RemoteEvents_DispatchTable = new FieldInfo { FieldName = "DispatchTable", TypeName = "Dictionary<" + MessagesEnumName + ", Action<IDispatchHelper>>", IsPrivate = true, IsReadOnly = true };
				var RemoteEvents_DispatchTableDelegates = new FieldInfo { FieldName = "DispatchTableDelegates", TypeName = "Dictionary<" + MessagesEnumName + ", Converter<object, Delegate>>", IsPrivate = true, IsReadOnly = true };
				var RemoteEvents_BroadcastRouter = new FieldInfo
				{
					FieldName = "_BroadcastRouter",
					TypeName = WithUserArgumentsRouter_Multicast.Name,
					IsPrivate = true,
					AccessedThroughProperty = "BroadcastRouter"
				};

				var RemoteEvents_SinglecastRouter = new FieldInfo
				{
					FieldName = "_SinglecastRouter",
					TypeName = WithUserArgumentsRouter_Singlecast.Name,
					IsPrivate = true,
					AccessedThroughProperty = "SinglecastRouter"
				};

				var RemoteEvents =
					new TypeInfo
					{
						IsSealed = true,
						Name = "RemoteEvents",
						BaseTypeNames = new[] { IEvents.Name/*, IPairedEvents.WithoutUser.Name, IPairedEvents.WithUser.Name*/ },
						Fields = new[]
                                {
                                    RemoteEvents_DispatchTable,
                                    RemoteEvents_DispatchTableDelegates,
                                    RemoteEvents_BroadcastRouter,
									RemoteEvents_SinglecastRouter
                                }
					};

				var RemoteMessages_Send = new FieldInfo { FieldName = "Send", TypeName = "Action<SendArguments>" };
				var RemoteMessages_VirtualTargets = new FieldInfo { FieldName = "VirtualTargets", TypeName = "Func<IEnumerable<IMessages>>" };
				var RemoteMessages =
					 new TypeInfo
						{
							IsSealed = true,
							Name = "RemoteMessages",
							BaseTypeNames = new[] { IMessages.Name/*, IPairedMessages.WithoutUser.Name, IPairedMessages.WithUser.Name*/ },
							Fields = new[]
							{
								RemoteMessages_Send,
								RemoteMessages_VirtualTargets
							}
						};

				using (DefineType(IMessages))
				{
				}

				using (DefineType(IEvents))
				{
					foreach (var v in r.MethodDefinitions)
					{
						using (IndentLine())
						{
							WriteBlue("event");
							WriteSpace();
							WriteCyan("Action<" + RemoteEvents.Name + "." + v.Name + "Arguments>");
							WriteSpace();
							Write(v.Name);
							Write(";");
						}
					}
				}





				WriteLine();

				#region RemoteMessages
				using (DefineType(RemoteMessages))
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

						#region signature
						using (IndentLine())
						{

							WriteBlue("public");
							WriteSpace();

							WriteBlue("void");
							WriteSpace();


							Write(v.Name);

							using (Parenthesis())
								v.ParametersInfo.Parameters.ForEach(
								 (p, k) =>
								 {
									 if (k > 0)
									 {
										 Write(",");
										 WriteSpace();
									 }

									 WriteVariableDefinition(p.TypeName, p.Name);
								 }
							   );

						}
						#endregion

						//{
						//    Send(new SendArguments { i = Messages.TeleportTo, args = new object[] { x, y } });
						//}

						using (CodeBlock())
						{
							#region Send if availible
							using (IndentLine())
							{
								WriteKeywordSpace("if");
								using (Parenthesis())
								{
									WriteBlue("this");
									Write(".");
									Write(RemoteMessages_Send.FieldName);
									WriteInequals();
									WriteBlue("null");
								}
							}
							using (CodeBlock())
							{

								#region Send(...)
								var SingleArray = v.ParametersInfo.SingleArrayParameter;
								var SingleObjectArray = SingleArray != null ? SingleArray.ElementTypeName == "object" : false;

								var SignleArrayConverted = SingleArray != null;

								// we should not try to convert (object[])
								SignleArrayConverted &= !SingleObjectArray;

								if (v.ParametersInfo.Parameters.Length > 1)
									if (SingleArray == null)
									{

										var AllPriorParametersAreNotArrays = true;

										for (int i = 0; i < v.ParametersInfo.Parameters.Length - 1; i++)
										{
											if (v.ParametersInfo.Parameters[i].IsArray)
												AllPriorParametersAreNotArrays = false;
										}

										if (AllPriorParametersAreNotArrays)
										{
											if (v.ParametersInfo.Parameters.Last().IsArray)
											{
												SingleArray = v.ParametersInfo.Parameters.Last();
												SignleArrayConverted = true;
											}
										}
									}

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
										WriteSpace();
										Write("+");
										WriteSpace();
										Write("" + (v.ParametersInfo.Parameters.Length - 1));
										Write("]");
										Write(";");
									}

									for (int i = 0; i < v.ParametersInfo.Parameters.Length - 1; i++)
									{
										using (IndentLine())
										{
											Write("args");
											Write("[");
											Write("" + i);
											Write("]");
											WriteAssignment();
											Write(v.ParametersInfo.Parameters[i].Name);
											Write(";");
										}
									}

									using (IndentLine())
									{
										WriteStaticMethodName("Array", "Copy");

										using (Parenthesis())
										{
											Write(SingleArray.Name);

											Write(",");
											WriteSpace();
											Write("0");

											Write(",");
											WriteSpace();
											Write("args");

											Write(",");
											WriteSpace();
											Write("" + (v.ParametersInfo.Parameters.Length - 1));

											Write(",");
											WriteSpace();
											WriteInstanceMethodName(SingleArray.Name, "Length");
										}

										Write(";");
									}
								}

								using (IndentLine())
								{
									WriteBlack(RemoteMessages_Send.FieldName);
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
												// the only parameter is object[]
												Write(SingleArray.Name);
											}
											else
											{
												// all parameters are non arrays

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
								#endregion

							}
							#endregion

							// if there are other targets, invoke them too
							using (IndentLine())
							{
								WriteKeywordSpace("if");
								using (Parenthesis())
								{
									WriteBlue("this");
									Write(".");
									Write(RemoteMessages_VirtualTargets.FieldName);
									WriteInequals();
									WriteBlue("null");
								}
							}
							using (CodeBlock())
							{
								const string Local_Target__ = "Target__";
								using (IndentLine())
								{
									WriteKeywordSpace("foreach");
									using (Parenthesis())
									{
										WriteKeywordSpace("var");
										Write(Local_Target__);
										WriteSpace();
										WriteKeywordSpace("in");
										WriteBlue("this");
										Write(".");
										Write(RemoteMessages_VirtualTargets.FieldName);
										Write("()");
									}
								}
								using (CodeBlock())
								using (IndentLine())
								{
									Write(Local_Target__);
									Write(".");
									Write(v.Name);
									using (Parenthesis())
									{
										v.ParametersInfo.Parameters.ForEach(
											(arg, index) =>
											{
												if (index > 0)
													Write(", ");

												Write(arg.Name);
											}
										);
									};
									Write(";");
								}
							}
						}
					}
				}
				#endregion


				WriteLine();










				#region RemoteEvents
				using (DefineType(RemoteEvents))
				{
					var KnownConverters = new Dictionary<string, string>
                    {
                        { "int", "GetInt32" },
                        { "double", "GetDouble" },
                        { "string", "GetString" },
                        
                        { "int[]", "GetInt32Array" },
                        { "double[]", "GetDoubleArray" },
                        { "string[]", "GetStringArray" },

						//{ "object[]", "GetArray" },

						{ "byte[]", "GetMemoryStream" },

                    };



					#region DispatchHelper
					using (DefineType(
							new TypeInfo
							{
								IsSealed = false,
								Name = "DispatchHelper",
								Fields =
									KnownConverters.AsEnumerable().Select(
										i => new FieldInfo { FieldName = i.Value, TypeName = "Converter<uint, " + i.Key + ">", IsProperty = true }
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
							WriteVariableDefinition("IDispatchHelper", "h");

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




					#region WithUserArgumentsRouter_Multicast
					using (DefineType(WithUserArgumentsRouter_Multicast))
					{
						#region Automatic Event Routing
						WriteLine();
						using (Region("Automatic Event Routing"))
						{
							#region CombineDelegates
							using (IndentLine())
							{
								WriteKeywordSpace("public");
								WriteKeywordSpace("void");

								Write(WithUserArgumentsRouter_CombineDelegates);

								using (Parenthesis())
								{
									// remove User prefix
									WriteVariableDefinition(IEvents.Name, "value");
								}
							}

							Action<ProxyProvider.MethodDefinition, Action> WriteEventRouting =
								(Method, WriteOperation) =>
								{
									// some user events really do not have their non-user counterpart

									var NonUserEventName = Method.Name.Substring(4);

									using (IndentLine())
									{
										WriteBlue("value");
										Write(".");
										Write(NonUserEventName);
										WriteSpace();
										WriteOperation();
										WriteSpace();

										Write("this");
										Write(".");
										Write(Method.Name);
										Write(";");
									}

								};

							using (CodeBlock())
							{
								//WriteEventRouting.FixLastParam(() => Write("+="));


								foreach (var v in MessagesToOthersArray)
								{
									WriteEventRouting(v.MessageWithUser, () => Write("+="));
								}
							}
							#endregion


							WriteLine();

							#region RemoveDelegates
							using (IndentLine())
							{
								WriteBlue("public");
								WriteSpace();

								WriteBlue("void");
								WriteSpace();

								Write(WithUserArgumentsRouter_RemoveDelegates);

								using (Parenthesis())
								{
									// remove User prefix
									WriteVariableDefinition(IEvents.Name, "value");
								}


							}

							using (CodeBlock())
							{
								foreach (var v in MessagesToOthersArray)
								{
									WriteEventRouting(v.MessageWithUser, () => Write("-="));
								}
							}
							#endregion

						}
						WriteLine();
						#endregion

						#region Routing
						using (Region("Routing"))
						{
							foreach (var _v in MessagesToOthersArray)
							{
								var v = _v.MessageWithUser;

								//public void UserTeleportTo(TeleportToArguments e)
								//{
								//    Target.UserTeleportTo(this.user, e.x, e.y);
								//}

								using (IndentLine())
								{
									WriteKeywordSpace("public");
									WriteKeywordSpace("void");

									Write(v.Name);

									using (Parenthesis())
									{
										// remove User prefix
										WriteVariableDefinition(_v.MessageWithoutUser.Name + "Arguments", "e");
									}
								}

								using (CodeBlock())
								using (IndentLine())
								{
									//WriteBlue("return");
									//WriteSpace();

									Write(WithUserArgumentsRouter_MulticastTarget.FieldName);
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
						#endregion


					}
					#endregion

					#region WithUserArgumentsRouter_SinglecastView
					using (DefineType(WithUserArgumentsRouter_SinglecastView))
					{
						#region Routing
						using (Region("Routing"))
						{
							foreach (var v in r.MethodDefinitions.Where(IsUserArguments))
							{
								//public void UserPlayerAdvertise(UserPlayerAdvertiseArguments e)
								//{
								//    this.Target(e.user).UserPlayerAdvertise(this.user, e.name);
								//}
								const string Local_Arguments = "e";

								using (IndentLine())
								{
									WriteKeywordSpace("public");
									WriteKeywordSpace("void");

									Write(v.Name);

									using (Parenthesis())
									{
										for (int i = 1; i < v.ParametersInfo.Parameters.Length; i++)
										{
											if (i > 1)
												Write(", ");
											var p = v.ParametersInfo.Parameters[i];

											WriteVariableDefinition(p.TypeName, p.Name);
										}
									}
								}

								using (CodeBlock())
								{
									using (IndentLine())
									{
										WriteBlue("this");
										Write(".");
										Write(WithUserArgumentsRouter_SinglecastTarget.FieldName);
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
													Write(".");

												}

												Write(p.Name);
											}
										}
										Write(";");
									}
								}

								using (IndentLine())
								{
									WriteKeywordSpace("public");
									WriteKeywordSpace("void");

									Write(v.Name);

									using (Parenthesis())
									{
										WriteVariableDefinition(v.Name + "Arguments", Local_Arguments);
									}
								}

								using (CodeBlock())
								{
									using (IndentLine())
									{
										WriteBlue("this");
										Write(".");
										Write(WithUserArgumentsRouter_SinglecastTarget.FieldName);
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
								}
							}
						}
						#endregion


					}
					#endregion

					#region WithUserArgumentsRouter_Singlecast
					using (DefineType(WithUserArgumentsRouter_Singlecast))
					{
						#region Automatic Event Routing
						WriteLine();
						using (Region("Automatic Event Routing"))
						{
							#region CombineDelegates
							using (IndentLine())
							{
								WriteKeywordSpace("public");
								WriteKeywordSpace("void");

								Write(WithUserArgumentsRouter_CombineDelegates);

								using (Parenthesis())
								{
									// remove User prefix
									WriteVariableDefinition(IEvents.Name, "value");
								}
							}

							Action<ProxyProvider.MethodDefinition, Action> WriteEventRouting =
								(Method, WriteOperation) =>
								{
									// some user events really do not have their non-user counterpart

									using (IndentLine())
									{
										WriteBlue("value");
										Write(".");
										Write(Method.Name);
										WriteSpace();
										WriteOperation();
										WriteSpace();

										Write("this");
										Write(".");
										Write(Method.Name);
										Write(";");
									}

								};

							using (CodeBlock())
							{
								foreach (var v in r.MethodDefinitions.Where(IsUserArguments))
								{
									WriteEventRouting(v, () => Write("+="));
								}
							}
							#endregion


							WriteLine();

							#region RemoveDelegates
							using (IndentLine())
							{
								WriteBlue("public");
								WriteSpace();

								WriteBlue("void");
								WriteSpace();

								Write(WithUserArgumentsRouter_RemoveDelegates);

								using (Parenthesis())
								{
									WriteVariableDefinition(IEvents.Name, "value");
								}


							}

							using (CodeBlock())
							{
								foreach (var v in r.MethodDefinitions.Where(IsUserArguments))
								{
									WriteEventRouting(v, () => Write("-="));
								}
							}
							#endregion

						}
						WriteLine();
						#endregion

						#region Routing
						using (Region("Routing"))
						{
							foreach (var v in r.MethodDefinitions.Where(IsUserArguments))
							{
								//public void UserPlayerAdvertise(UserPlayerAdvertiseArguments e)
								//{
								//    this.Target(e.user).UserPlayerAdvertise(this.user, e.name);
								//}
								const string Local_Arguments = "e";


								using (IndentLine())
								{
									WriteKeywordSpace("public");
									WriteKeywordSpace("void");

									Write(v.Name);

									using (Parenthesis())
									{
										WriteVariableDefinition(v.Name + "Arguments", Local_Arguments);
									}
								}

								using (CodeBlock())
								{
									const string Local_target = "_target";

									using (IndentLine())
									{
										WriteBlue("var");
										WriteSpace();
										Write(Local_target);
										WriteAssignment();
										WriteBlue("this");
										Write(".");
										Write(WithUserArgumentsRouter_SinglecastTarget.FieldName);
										using (Parenthesis())
										{
											Write(Local_Arguments);
											Write(".");
											Write("user");
										}
										Write(";");
									}

									using (IndentLine())
									{
										WriteKeywordSpace("if");

										using (Parenthesis())
										{
											Write(Local_target);
											WriteEquals();
											WriteBlue("null");
										}

										WriteSpace();

										WriteBlue("return");
										Write(";");
									}

									using (IndentLine())
									{
										Write(Local_target);
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
								}
							}
						}
						#endregion

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
							WriteCyan("Dictionary<" + MessagesEnumName + ", Action<IDispatchHelper>>");
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
													v.ParametersInfo.Parameters.ForEach(
														(p, k) =>
														{
															if (k > 0)
															{
																Write(",");
																WriteSpace();
															}


															Write(p.Name);
															WriteAssignment();
															Write("e");
															Write(".");



															Write(KnownConverters[p.TypeName]);

															using (Parenthesis())
																Write("" + k);

														}
													 );
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


					#region BroadcastRouter { get; set; }
					using (IndentLine())
					{
						WriteBlue("public");
						WriteSpace();
						WriteVariableDefinition(
							RemoteEvents_BroadcastRouter.TypeName,
							RemoteEvents_BroadcastRouter.AccessedThroughProperty
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
							Write(RemoteEvents_BroadcastRouter.FieldName);
							Write(";");
						}
						#endregion

						#region set
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
									Write(RemoteEvents_BroadcastRouter.FieldName);
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

								using (IndentLine())
								{
									Write(RemoteEvents_BroadcastRouter.FieldName);
									Write(".");
									Write(WithUserArgumentsRouter_RemoveDelegates);

									using (Parenthesis())
									{
										WriteBlue("this");
									}
									Write(";");
								}


							}
							#endregion

							using (IndentLine())
							{
								Write(RemoteEvents_BroadcastRouter.FieldName);
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
									Write(RemoteEvents_BroadcastRouter.FieldName);
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

								using (IndentLine())
								{
									Write(RemoteEvents_BroadcastRouter.FieldName);
									Write(".");
									Write(WithUserArgumentsRouter_CombineDelegates);

									using (Parenthesis())
									{
										WriteBlue("this");
									}
									Write(";");
								}


							}
							#endregion


						}

						#endregion

					}
					#endregion

					#region SinglecastRouter { get; set; }
					using (IndentLine())
					{
						WriteBlue("public");
						WriteSpace();
						WriteVariableDefinition(
							RemoteEvents_SinglecastRouter.TypeName,
							RemoteEvents_SinglecastRouter.AccessedThroughProperty
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
							Write(RemoteEvents_SinglecastRouter.FieldName);
							Write(";");
						}
						#endregion

						#region set
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
									Write(RemoteEvents_SinglecastRouter.FieldName);
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

								using (IndentLine())
								{
									Write(RemoteEvents_SinglecastRouter.FieldName);
									Write(".");
									Write(WithUserArgumentsRouter_RemoveDelegates);

									using (Parenthesis())
									{
										WriteBlue("this");
									}
									Write(";");
								}


							}
							#endregion

							using (IndentLine())
							{
								Write(RemoteEvents_SinglecastRouter.FieldName);
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
									Write(RemoteEvents_SinglecastRouter.FieldName);
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

								using (IndentLine())
								{
									Write(RemoteEvents_SinglecastRouter.FieldName);
									Write(".");
									Write(WithUserArgumentsRouter_CombineDelegates);

									using (Parenthesis())
									{
										WriteBlue("this");
									}
									Write(";");
								}


							}
							#endregion


						}

						#endregion

					}
					#endregion
				}
				#endregion


				#region bridge

				const string Bridge_VirtualLatencyDefaultImplemenetation = "VirtualLatencyDefaultImplemenetation";
				const string Bridge_VirtualLatency = "VirtualLatency";
				const string Bridge = "Bridge";

				using (DefineType(
					new TypeInfo
					{
						Name = Bridge,
						Fields = new[]
						{
							new FieldInfo { TypeName = "Action<Action>", FieldName = Bridge_VirtualLatency }
						},
						BaseTypeNames = RemoteEvents.BaseTypeNames.Concat(RemoteMessages.BaseTypeNames).ToArray()
					}))
				{
					#region ctor
					using (IndentLine())
					{
						WriteBlue("public");
						WriteSpace();
						Write(Bridge);

						using (Parenthesis())
						{
						}

					}

					using (CodeBlock())
					{
						using (IndentLine())
						{
							WriteBlue("this");
							Write(".");
							Write(Bridge_VirtualLatency);
							WriteAssignment();
							Write(Bridge_VirtualLatencyDefaultImplemenetation);
							Write(";");
						}
					}
					#endregion

					#region Bridge_VirtualLatencyDefaultImplemenetation
					using (IndentLine())
					{
						WriteBlue("public");
						WriteSpace();
						WriteBlue("void");
						WriteSpace();
						Write(Bridge_VirtualLatencyDefaultImplemenetation);

						using (Parenthesis())
						{
							WriteVariableDefinition("Action", "e");
						}

					}

					using (CodeBlock())
					{
						using (IndentLine())
						{
							Write("e");
							using (Parenthesis())
							{
							}
							Write(";");
						}
					}
					#endregion

					foreach (var v in r.MethodDefinitions)
					{
						using (IndentLine())
						{
							WriteBlue("public");
							WriteSpace();
							WriteBlue("event");
							WriteSpace();
							WriteVariableDefinition("Action<" + RemoteEvents.Name + "." + v.Name + "Arguments" + ">", v.Name);
							Write(";");
						}

						#region WriteExplicitImplementation
						Action<TypeInfo, Action> WriteExplicitImplementation =
							(DeclaringType, Code) =>
							{
								using (IndentLine())
								{

									WriteBlue("void");
									WriteSpace();

									WriteCyan(DeclaringType.Name);
									Write(".");
									Write(v.Name);

									using (Parenthesis())
										v.ParametersInfo.Parameters.ForEach(
										 (p, k) =>
										 {
											 if (k > 0)
											 {
												 Write(",");
												 WriteSpace();
											 }

											 WriteVariableDefinition(p.TypeName, p.Name);
										 }
									   );


								}
								using (CodeBlock())
								{
									Code();
								}
							};
						#endregion


						WriteExplicitImplementation(IMessages,
							delegate
							{
								using (IndentLine())
								{
									WriteBlue("if");

									using (Parenthesis())
									{
										Write(v.Name);
										WriteSpace();
										Write("==");
										WriteSpace();
										WriteBlue("null");
									}

									WriteSpace();
									WriteBlue("return");
									Write(";");
								}

								#region var v;
								using (IndentLine())
								{
									WriteBlue("var");
									WriteSpace();
									Write("v");
									WriteAssignment();

									WriteBlue("new");
									WriteSpace();
									WriteCyan(RemoteEvents.Name);
									Write(".");
									WriteCyan(v.Name + "Arguments");
									WriteSpace();

									using (InlineCodeBlock())
									{
										v.ParametersInfo.Parameters.ForEach(
											(p, k) =>
											{
												if (k > 0)
												{
													Write(",");
													WriteSpace();
												}


												Write(p.Name);
												WriteAssignment();
												Write(p.Name);


											}
										);

									}

									Write(";");
								}
								#endregion

								using (IndentLine())
								{
									WriteBlue("this");
									Write(".");
									Write(Bridge_VirtualLatency);
									using (Parenthesis())
									{
										Write("() => ");

										WriteBlue("this");
										Write(".");
										Write(v.Name);

										using (Parenthesis())
										{
											Write("v");
										}
									}

									Write(";");

								}
							}
						);

						#region RedirectToIMessages
						Action RedirectToIMessages =
							delegate
							{
								using (IndentLine())
								{
									using (Parenthesis())
									{
										using (Parenthesis())
											WriteCyan(IMessages.Name);
										WriteBlue("this");
									}
									Write(".");
									Write(v.Name);
									using (Parenthesis())
										v.ParametersInfo.Parameters.ForEach(
										 (p, k) =>
										 {
											 if (k > 0)
											 {
												 Write(",");
												 WriteSpace();
											 }

											 Write(p.Name);
										 }
									   );

									Write(";");
								}

							};
						#endregion

						//if (MessagesToOthersArray.Any(i => i.MessageWithUser.Name == v.Name))
						//    WriteExplicitImplementation(IPairedMessages.WithUser, RedirectToIMessages);

						//if (MessagesToOthersArray.Any(i => i.MessageWithoutUser.Name == v.Name))
						//    WriteExplicitImplementation(IPairedMessages.WithoutUser, RedirectToIMessages);

						WriteLine();
					}
				}

				#endregion
			}

		}



	}

}
