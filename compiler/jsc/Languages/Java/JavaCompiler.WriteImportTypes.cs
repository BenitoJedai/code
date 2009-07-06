
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Threading;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Languages.Java
{

	partial class JavaCompiler
	{

		public void WriteImportTypes(Type z)
		{
			// all field types, return types, parameter types, variable types, statics

			List<Type> t = GetImportTypes(z, true);
			List<string> imports = new List<string>();

			// why would we do that?
			//t.RemoveAll(delegate(Type x)
			//{
			//    return IsEmptyImplementationType(x);
			//});

			while (t.Count > 0)
			{
				Type p = t[0];

				// optimize me

				t.RemoveAll(
					delegate(Type x)
					{
						return x.GUID == z.GUID || x.GUID == p.GUID;
					}
				);



				ScriptAttribute a = ScriptAttribute.Of(p, false);

				string n = (p.Namespace == null) ? "" : p.Namespace + ".";



				if (a != null && a.Implements != null && a.ExternalTarget != null)
				{
					if (p != z)
					{

						imports.Add(NamespaceFixup(n + GetDecoratedTypeName(p, false, false, false), p));
					}

					imports.Add(NamespaceFixup(GetDecoratedTypeName(p, true, false, false), p));


				}
				else
				{

					imports.Add(NamespaceFixup(n + GetDecoratedTypeName(p, true, false, false), p));



				}



			}

			imports.RemoveAll(
				delegate(string x)
				{
					return System.Text.RegularExpressions.Regex.IsMatch(x, @"^java.lang.\w*$");
				}
				);

			imports.Sort(
			   delegate(string x, string y)
			   {
				   return x.CompareTo(y);
			   });


			foreach (var Namespace in imports)
			{
				// exclude onl
				//if (var.StartsWith("java.lang"))
				//    continue;



				WriteKeywordImport();
				Write(Namespace);
				WriteLine(";");

			}


		}





		private List<Type> GetImportTypes(Type t, bool bExcludeJavaLang)
		{


			List<Type> imp_types = new List<Type>();
			List<Type> imp = new List<Type>();

			Type[] tinterfaces = t.GetInterfaces();

			foreach (Type tinterface in tinterfaces)
				imp.Add(tinterface);

			Type bp = t.BaseType;

			while (bp != typeof(object) &&
					bp != null)
			{
				imp.Add(bp);
				bp = bp.BaseType;
			}

			foreach (FieldInfo v in this.GetAllFields(t))
			{
				imp.Add(v.FieldType);
			}

			foreach (MethodBase v in GetAllInstanceConstructors(t))
			{


				GetImportTypesFromMethod(t, imp, v);
			}


			foreach (MethodInfo mi in this.GetAllMethods(t))
			{
				imp.Add(mi.ReturnParameter.ParameterType);

				MethodBase v = mi;

				GetImportTypesFromMethod(t, imp, v);
			}

			while (imp.Count > 0)
			{
				Type p = imp[0];

				imp.RemoveAll(
					delegate(Type w)
					{
						if (w == null)
							return true;

						if (w.IsArray && p.IsArray)
						{
							return w.GetElementType().GUID == p.GetElementType().GUID;
						}

						return w.GUID == p.GUID;
					}
				);

				// exludeonly java lang classname
				//if (p.Namespace != null)
				//    if (bExcludeJavaLang && p.Namespace.StartsWith("java.lang")) 
				//        continue;

				// todo fix additional types handling

				while (p.IsArray)
				{
					p = p.GetElementType();

				}

				if (p.IsEnum)
				{
					// we are using the underlying system type
					continue;
				}

				if (p == typeof(object)) continue;
				if (p == typeof(void)) continue;
				if (p == typeof(string)) continue;
				if (p == typeof(int)) continue;
				if (p == typeof(uint)) continue;

				if (p == typeof(short)) continue;
				if (p == typeof(ushort)) continue;

				if (p == typeof(long)) continue;
				if (p == typeof(float)) continue;
				if (p == typeof(double)) continue;

				if (p == typeof(byte)) continue;
				if (p == typeof(sbyte)) continue;
				if (p == typeof(bool)) continue;
				if (p == typeof(char)) continue;

				// delegate closures wont have [Script]
				// but they will be defined within a type having one

				ScriptAttribute a = ScriptAttribute.Of(p, true);
				//var a = p.ToScriptAttribute();

				if (a == null)
				{
					Type p_impl = MySession.ResolveImplementation(p);

					if (p_impl == null)
					{
						if (ScriptAttribute.IsCompilerGenerated(p))
						{
							// pass thru..

							continue;
						}
						else
						{
							Break("class import: no implementation for " + p.FullName + " at " + t.FullName);
						}
					}

					p = p_impl;
					a = ScriptAttribute.Of(p, true);
				}


				imp_types.Add(p);


			}


			return imp_types;
		}

		private void GetImportTypesFromMethod(Type t, List<Type> imp, MethodBase v)
		{

			ScriptAttribute vs = ScriptAttribute.OfProvider(v);


			// DebugBreak(vs);

			if (vs != null && vs.DefineAsStatic)
				imp.Add(t);

			DebugBreak(vs);

			imp.AddRange(GetMethodExceptions(v));

			foreach (ParameterInfo p in v.GetParameters())
			{
				imp.Add(p.ParameterType);
			}

			if (v.IsAbstract)
				return;

			ILBlock b = new ILBlock(v);

			// do we have IL?
			if (b.Instructrions != null)
			{
				foreach (LocalVariableInfo l in v.GetMethodBody().LocalVariables)
				{
					imp.Add(l.LocalType);
				}


				foreach (ILInstruction i in b.Instructrions)
				{
					if (i == OpCodes.Ldtoken)
					{
						imp.Add(MySession.ResolveImplementation(typeof(RuntimeTypeHandle)));

						// A RuntimeHandle can be a fieldref/fielddef, a methodref/methoddef, or a typeref/typedef.
						var RuntimeTypeHandle_Type = i.TargetType;

						imp.Add(MySession.ResolveImplementation(RuntimeTypeHandle_Type) ?? RuntimeTypeHandle_Type);

						continue;
					}

					if (i == OpCodes.Ldvirtftn)
					{
						imp.Add(typeof(IntPtr));
						continue;
					}

					if (i == OpCodes.Ldftn)
					{
						imp.Add(typeof(IntPtr));

						if (i.TargetMethod != null)
							imp.Add(i.TargetMethod.DeclaringType);

						continue;
					}

					if (i.ReferencedMethod != null)
					{
						if (i.ReferencedMethod.DeclaringType == typeof(object))
							imp.Add(MySession.ResolveImplementation(typeof(object)));

						if (!IsTypeOfOperator(i.ReferencedMethod))
							if (i.ReferencedMethod.DeclaringType != typeof(object))
							{
								if (i.ReferencedMethod.DeclaringType == typeof(System.Runtime.CompilerServices.RuntimeHelpers))
									continue;

								// is that method special to us?
								if (i.TargetMethod != null)
									if (i.TargetMethod.DeclaringType.ToScriptAttribute() == null)
										if (StringToSByteArrayProvider.GetProvideImplementation(i.TargetMethod) != null)
											continue;

								MethodBase method = GetMethodImplementation(MySession, i);
								ScriptAttribute method_attribute = ScriptAttribute.OfProvider(method);


								if (method.IsConstructor || method.IsStatic || (method_attribute != null && method_attribute.DefineAsStatic))
								{
									imp.Add(method.DeclaringType);
									continue;
								}
							}
					}

					if (i == OpCodes.Box)
					{
						imp.Add(i.TargetType);
						continue;
					}

					if (i.TargetField != null)
					{
						if (i.TargetField.IsStatic)
						{
							imp.Add(i.TargetField.DeclaringType);
							continue;
						}
					}
				}
			}
		}

	}
}
