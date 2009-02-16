using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Script.PHP
{
	partial class PHPCompiler
    {
		public override void WriteTypeSignature(Type z, ScriptAttribute za)
		{
			WriteLine("// " + z.FullName);

			WriteIdent();



			if (z.IsInterface)
			{
				WriteKeywordSpace(Keywords._interface);
			}
			else
			{
				if (z.IsAbstract)
					WriteKeywordSpace(Keywords._abstract);

				WriteKeywordSpace(Keywords._class);
			}

			WriteDecoratedTypeName(z);

			if (!z.ToScriptAttributeOrDefault().InternalConstructor)
			{
				if (!z.IsInterface)
				{
					if (z.BaseType != typeof(object))
					{
						var BaseType = ResolveImplementation(z.BaseType) ?? z.BaseType;


						ScriptAttribute ba = ScriptAttribute.Of(BaseType, false);

						if (ba == null)
							Break("base class should be for scripting : " + BaseType.FullName);

						WriteSpace();
						WriteKeywordSpace(Keywords._extends);
						WriteDecoratedTypeName(BaseType);

					}
				}


				var Interfaces = z.GetInterfaces();

				for (int i = 0; i < Interfaces.Length; i++)
				{
					if (i == 0)
					{
						WriteSpace();

						if (z.IsInterface)
							WriteKeywordSpace(Keywords._extends);
						else
							WriteKeywordSpace(Keywords._implements);
					}
					else
					{
						Write(", ");
					}

					var BaseType = ResolveImplementation(Interfaces[i]) ?? Interfaces[i];
					WriteDecoratedTypeName(BaseType);
				}
			}

			WriteLine();
		}


    }
}
