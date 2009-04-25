﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.ActionScript
{
	partial class ActionScriptCompiler
	{
		public void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv, bool favorPrimitives, bool favorTargetType, bool UseFullyQualifiedName)
		{
			WriteDecoratedTypeNameOrImplementationTypeName(timpv, favorPrimitives, favorTargetType, UseFullyQualifiedName, WriteDecoratedTypeNameOrImplementationTypeNameMode.Default);
		}

		public enum WriteDecoratedTypeNameOrImplementationTypeNameMode
		{
			Default,
			IgnoreImplementationType
		}

		public void WriteDecoratedTypeNameOrImplementationTypeName(Type timpv, bool favorPrimitives, bool favorTargetType, bool UseFullyQualifiedName, WriteDecoratedTypeNameOrImplementationTypeNameMode Mode)
		{

			if (timpv.IsGenericParameter)
			{
				Write("*");
				return;
			}



			//[Script(Implements = typeof(global::System.Boolean),
			//    ImplementationType=typeof(java.lang.Integer))]

			if (NativeTypes.ContainsKey(timpv))
			{
				// write native
				Write(NativeTypes[timpv]);
				return;
			}

			if (timpv.IsEnum)
			{
				WriteSafeLiteral(GetDecoratedTypeName(timpv, true));

				return;
			}

			var iType = MySession.ResolveImplementation(timpv);

			if (iType != null)
			{
				if (favorTargetType)
				{
					var s = iType.ToScriptAttribute();

					if (s.ImplementationType != null)
						iType = s.ImplementationType;
				}
			}



			Action<Type> WriteTypeName =
				t =>
				{
					// array of type has the same namespace the element type has, but in our case we need
					// Top Level.Array
					if (!t.IsArray)
					{
						var ns = NamespaceFixup(t.Namespace, t);

						if (UseFullyQualifiedName && !string.IsNullOrEmpty(ns))
						{
							Write(ns);
							Write(".");
						}

					}

					WriteSafeLiteral(GetDecoratedTypeName(t, true));


				};

			if (iType == null)
			{
				var s = timpv.ToScriptAttribute();

				if (!(Mode == WriteDecoratedTypeNameOrImplementationTypeNameMode.IgnoreImplementationType) && s != null && s.ImplementationType != null)
					WriteTypeName(s.ImplementationType);
				else
					WriteTypeName(timpv);

			}
			else
			{
				WriteTypeName(iType);
			}
		}



		public override void WriteDecoratedTypeName(Type context, Type subject)
		{
			// used by OpCodes.Newobj

			WriteDecoratedTypeNameOrImplementationTypeName(subject, false, false, IsFullyQualifiedNamesRequired(context, subject));

		}

		public void WriteDecoratedTypeName(Type context, Type subject, WriteDecoratedTypeNameOrImplementationTypeNameMode Mode)
		{
			WriteDecoratedTypeNameOrImplementationTypeName(subject, false, false, IsFullyQualifiedNamesRequired(context, subject), Mode);

		}
	}
}
