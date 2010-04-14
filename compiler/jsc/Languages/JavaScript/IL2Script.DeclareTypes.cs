using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

using ScriptCoreLib;
using ScriptCoreLib.CSharp.Extensions;

using jsc.Script;
using jsc.Languages.JavaScript;

namespace jsc
{
	using ilbp = ILBlock.Prestatement;
	using ili = ILInstruction;
	using ilfsi = ILFlow.StackItem;
	using ScriptCoreLib.Tools;

	partial class IL2Script
	{
		public static void DeclareTypes(IdentWriter w, Type[] arg_types, bool debug, ScriptAttribute attribute, Assembly assembly, CompileSessionInfo sinfo)
		{
			Type[] types = SortTypes(w, arg_types);

			w.Ident++;

			if (attribute != null)
				if (attribute.IsCoreLib)
				{
					// declare file scoped inheritance class builder
					//w.WriteLine(new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("jsc.Languages.JavaScript.$ctor$.js")).ReadToEnd());
					w.WriteLine("jsc.Languages.JavaScript.$ctor$.js".GetResourceFileContent());

				}


			w.WriteVariableAssignment(
				assembly.ManifestModule.ModuleVersionId,
				new
				{
					//FullName = assembly.FullName,
					Name = new
					{
						assembly.GetName().Name,
						assembly.GetName().FullName,
					}
				}
			);



			var CompiledTypes = new List<LiteralString>();

			foreach (Type z in types)
			{
				Console.WriteLine(z.FullName);
				////w.WriteCommentLine(z.FullName);

				if (z.Name.Contains("<PrivateImplementationDetails>"))
					continue;

				if (z.IsValueType)
				{
					if (z.DeclaringType != null)
						if (z.DeclaringType.Name.Contains("<PrivateImplementationDetails>"))
							continue;
				}

				if (!z.IsClass)
					continue;


				ScriptAttribute sa = ScriptAttribute.Of(z);

				#region DelegateImplementationProvider
				if (z.BaseType == typeof(System.MulticastDelegate))
				{
					if (sa != null)
					{
						// we have a delegate with a script attribute
						// now we have to write the implementation for it

						jsc.Languages.JavaScript.legacy.DelegateImplementationProvider.Write(w, z);

					}

					continue;
				}
				#endregion

				#region compilergenerated

				//if (ScriptAttribute.IsCompilerGenerated(z))

				if (sa == null)
				{
					if (ScriptAttribute.IsAnonymousType(z))
					{
						w.WriteCommentLine("Anonymous type");
					}
					else
					{
						w.WriteCommentLine("Closure type for " + z.FullName);
					}

					w.Helper.DOMDefineNamedType(z);
					w.Helper.DefineAndAssignPrototype(z);


					DeclareFields(w, z.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public), z);
					DeclareMethods(w, z.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public));

					if (ScriptAttribute.IsAnonymousType(z))
					{
						WriteInstanceConstructor(w, z, z.GetConstructors().Single());
					}

					continue;
				}

				#endregion



				if (sa != null)
				{

					if (!sa.IsDebugCode && debug)
						continue;

					Task.Enabled = true;
					Task.WriteLine("type: [{0}]", z.FullName);

					sinfo.Options.RaiseProccessStatusChanged(z.FullName);

					//using (new Task("DeclareTypes", z.FullName))
					//{


					#region prototype
					//w.WriteCommentLine("prototype");

					bool IsStaticClass = z.IsAbstract && z.IsSealed;

					if (!sa.HasNoPrototype && !IsStaticClass)
					{

						Type __inherit_from = z.BaseType;

						if (__inherit_from == typeof(object) && sa.Implements == null)
						{
							__inherit_from = w.Session.ResolveImplementation(__inherit_from);
						}


						w.Helper.DOMDefineNamedType(z, __inherit_from);

						// attributes
						#region Attributes
						var Attributes =
							(
								from i in z.GetCustomAttributes(false)
								let s = ScriptAttribute.OfProvider(i.GetType())
								where s != null
								select i
							).ToArray();

						if (Attributes.Length > 0)
							w.WriteMemberAssignment(z.GUID,
								new
								{
									GetAttributes =
										(
											from i in Attributes
											select
												new
												{
													Type = (LiteralString)IdentWriter.GetGUID64(i.GetType().GUID),
													Value = i
												}
										).ToArray().ToFunctionReturnValue()
								}
							);
						#endregion

						w.Helper.DefineAndAssignPrototype(z);


						CompiledTypes.Add(IdentWriter.GetGUID64(z));


					}
					//w.WriteCommentLine("endprototype");
					#endregion


					#region instance fields
					//w.WriteCommentLine("fields");

					DeclareFields(w, z.GetFields(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public), z);


					//w.WriteCommentLine("endfields");
					#endregion

					#region instance constructors

					if (sa.HasNoPrototype || IsStaticClass)
					{
						// are we native?
					}
					else
					{
						//w.WriteCommentLine("DefineTypeInheritanceConstructor");

						w.Helper.DefineTypeInheritanceConstructor(z, z.BaseType);
					}

					//if (sa.Implements == null)
					{
						ConstructorInfo[] ci = z.GetConstructors(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

						foreach (ConstructorInfo zc in ci)
						{
							if (zc.IsStatic)
							{
								continue;
								//  IL2Script.EmitBody(w, zc);
							}
							else
							{
								if (sa.HasNoPrototype)
								{
									continue;
								}


								ScriptAttribute zsa = ScriptAttribute.Of(zc);

								//Task.Enabled = zsa != null && zsa.IsDebugCode;

								WriteInstanceConstructor(w, z, zc);


							}

							w.WriteLine();

						}
					}
					#endregion



					//w.WriteCommentLine("methods");
					MethodInfo[] mi = z.GetMethods(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

					DeclareMethods(w, mi);



					DeclareVirtualMethods(w, z);


					//}
				}



			}

			Console.WriteLine();

			w.WriteCommentLine("Are the references up to date?");
			w.WriteCommentLine("Are they imported in the dependency sort order?");

			var KnownReferences =
				from reference in ScriptCoreLib.SharedHelper.LoadReferencedAssemblies(assembly, false)
				where reference.GetCustomAttributes<ScriptTypeFilterAttribute>().Any(q => q.Type == ScriptType.JavaScript)
				let token = IdentWriter.GetGUID64(reference.ManifestModule.ModuleVersionId)
				select new { reference, token };

			foreach (var v in KnownReferences)
			{
				w.WriteCommentLine("reference " + v.reference.GetName().Name + " - " + v.token);

			}

			w.WriteMemberAssignment(
				assembly.ManifestModule.ModuleVersionId,
				new
				{
					Types = CompiledTypes.ToArray(),
					References = KnownReferences.Select(k => (LiteralString)k.token).ToArray()
					//ScriptCoreLib.SharedHelper.LoadReferencedAssemblies(assembly, false).
					//Select(i => (LiteralString)IdentWriter.GetGUID64(i.ManifestModule.ModuleVersionId)).
					//ToArray()
				}
			);

			w.Helper.WriteOptionalNewline();


			DeclareStaticConstructors(w, types, debug);

			w.Ident--;


		}

	}
}
