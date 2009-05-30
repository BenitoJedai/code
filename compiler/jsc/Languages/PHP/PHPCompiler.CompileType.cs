using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

using jsc.CodeModel;

using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Script.PHP
{

	partial class PHPCompiler
	{

		public override bool CompileType(Type z)
		{
			try
			{


				if (z.IsEnum)
					return false;

				if (z.IsValueType)
				{
					if (z.Name.Contains("PrivateImplementationDetails"))
						return false;

					if (z.DeclaringType != null)
						if (z.DeclaringType.Name.Contains("PrivateImplementationDetails"))
							return false;

					Break("ValueType not supported : " + z.FullName + ", " + z.Name);
				}

				var za = z.ToScriptAttributeOrDefault();

				if (z.BaseType == typeof(global::System.MulticastDelegate))
				{
					jsc.Languages.PHP.DelegateImplementationProvider.Write(this, z);

					return true;
				}


				if (z.BaseType != typeof(object) && z.BaseType != null)
				{
					WriteImport(TypeInfoOf(ResolveImplementation(z.BaseType) ?? z.BaseType));
				}


				//Console.WriteLine(z.FullName);



				if (!z.ToScriptAttributeOrDefault().InternalConstructor)
				{
					WriteTypeSignature(z, za);

					using (CreateScope())
					{

						WriteTypeFields(z, za);
						WriteTypeInstanceConstructors(z);
						WriteTypeInstanceMethods(z, za);

						//WriteTypeVirtualMethods(z, za);

						if (!z.IsInterface)
						{
							WriteInterfaceMappingMethods(z);
						}

						WriteVirtualMethodOverrides(z);


					}
				}

				WriteLine();


				WriteTypeStaticMethods(z, za);

				WriteLine();


				WriteTypeStaticConstructor(z, true);

				WriteLine();
			}
			catch (Exception ex)
			{
				Break("internal error while compiling type " + z.FullName + "; " + ex.Message);
			}


			return true;
		}

	}
}
