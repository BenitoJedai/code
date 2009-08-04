using System;
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

	}
}
