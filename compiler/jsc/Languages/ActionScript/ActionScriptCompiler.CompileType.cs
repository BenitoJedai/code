﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;
using System.IO;

namespace jsc.Languages.ActionScript
{
	partial class ActionScriptCompiler
	{
		public Action CompileType_WriteAdditionalMembers;

		public override bool CompileType(Type z)
		{
			if (IsNativeType(z))
				return false;

			if (z.Name.Contains("<PrivateImplementationDetails>") || (z.DeclaringType != null && z.DeclaringType.Name.Contains("<PrivateImplementationDetails>")))
				return false;

			WriteCommentLine(Path.GetFileName(z.Assembly.Location));


			CompileType_WriteAdditionalMembers = delegate { };

			WriteIdent();
			WriteKeywordSpace(Keywords._package);
			Write(NamespaceFixup(z.Namespace, z));
			WriteLine();

			using (CreateScope())
			{
				WriteImportTypes(z);

				WriteLine();

				var za = ScriptAttribute.Of(z, true);

				if (ScriptAttribute.IsAnonymousType(z))
					za = new ScriptAttribute();

				var z_Implements = za.Implements;
				var z_NonPrimitiveValueType = z_Implements != null && z_Implements.IsValueType && !z_Implements.IsPrimitive;


				#region type summary
				var u = GetXMLNode(z);

				if (u != null)
					WriteBlockComment(u["summary"].InnerText);
				#endregion

				if (za.IsDebugCode)
				{
					WriteIdent();
					WriteCommentLine("[Script(IsDebugCode = true)]");
				}
				WriteCustomAttributes(z);
				WriteTypeSignature(z, za);

				var cctor = default(Action);

				using (CreateScope())
				{
					if (z.IsDelegate())
					{
						this.WriteDelegate(z, za);
					}
					else
					{
						WriteTypeFields(z, za);
						WriteLine();

						var ctor = default(ConstructorMergeInfo);

						if (ScriptAttribute.IsAnonymousType(z))
						{
							#region IsAnonymousType
							WriteTypeInstanceConstructors(z);
							WriteLine();

							foreach (var p in z.GetProperties())
							{
								var GetMethod = p.GetGetMethod();

								WriteMethodSignature(GetMethod, false);
								WriteMethodBody(GetMethod);

							}


							var ToString = z.GetMethod("ToString");

							WriteMethodSignature(ToString, false);
							WriteMethodBody(ToString);
							#endregion

						}
						else
						{
							if (za.ImplementationType == null)
							{
								// there is another type that needs to be created

								ctor = WriteTypeInstanceConstructorsAndGetPrimary(z);
								WriteLine();

								WriteTypeInstanceMethods(z, za);
								WriteLine();

								WritePropertiesAsMethods(z);
							}
							else
							{
								WriteIdent();
								WriteCommentLine("this class is just extending another class via static members");
							}
						}

						WriteTypeStaticMethods(z, za);
						WriteLine();

						cctor = WriteTypeStaticConstructor(z, za);
						WriteLine();

						if (!z.IsInterface)
						{
							WriteInterfaceMappingMethods(z);
						}

						if (!ScriptAttribute.IsAnonymousType(z))
							WriteVirtualMethodOverrides(z);

						if (ctor != null)
							if (z_NonPrimitiveValueType)
							{
								// define ctor as methods
								WriteIdent();
								WriteCommentLine("NonPrimitiveValueType");
								WriteMethodSignature(ctor.Primary, false, WriteMethodSignatureMode.ValueTypeConstructorAlias, ctor.Values, i => ctor.CustomVariableInitialization += i, null);
								WriteMethodBody(ctor.Primary, null, ctor.CustomVariableInitialization);

							}
					}

					CompileType_WriteAdditionalMembers();

				}

				if (cctor != null)
					cctor();

			}

			return true;
		}

		private void WritePropertiesAsMethods(Type z)
		{
			// In IL one can create a delegate from property set or get methods!
			// so we are doing it the wrong way
			// instead of first only defining the methods and then properties
			// we are defining properties and then methods... funny :)

			this.WriteIdent();
			this.WriteCommentLine("properties as methods");

			foreach (var item in z.GetProperties(BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic))
			{
				this.WriteLine();

				var GetMethod = item.GetGetMethod();
				if (GetMethod != null)
				{
					WriteMethodSignature(GetMethod, false, WriteMethodSignatureMode.DeclaringAsMethod);
					using (this.CreateScope())
					{
						WriteIdent();
						WriteKeywordSpace(Keywords._return);
						WriteKeyword(Keywords._this);
						Write(".");
						Write(item.Name);
						Write(";");
						WriteLine();
					}
				}

				this.WriteLine();


				var SetMethod = item.GetSetMethod();
				if (SetMethod != null)
				{
					WriteMethodSignature(SetMethod, false, WriteMethodSignatureMode.DeclaringAsMethod);
					using (this.CreateScope())
					{
						WriteIdent();
						WriteKeyword(Keywords._this);
						Write(".");
						Write(item.Name);
						WriteAssignment();
						WriteDecoratedMethodParameter(SetMethod.GetParameters().Single());
						Write(";");
						WriteLine();
					}
				}

			}
		}

	}
}
