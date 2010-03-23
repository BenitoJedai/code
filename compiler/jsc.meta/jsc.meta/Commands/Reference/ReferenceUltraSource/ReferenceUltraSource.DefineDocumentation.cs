using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using jsc.Languages;
using jsc.Languages.IL;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.Script;
using ScriptCoreLib.Archive.ZIP;
using ScriptCoreLib.Documentation;
using ScriptCoreLib.Ultra.Library.Extensions;
namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	partial class ReferenceUltraSource
	{
		[Description("Convert assemblies (.dll, .xml) from zip files into documentation.")]
		public class DefineDocumentation
		{
			public string DefaultNamespace;
			public XElement BodyElement;
			public RewriteToAssembly r;
			public Func<string, FileInfo> GetLocalResource;


			public void Define()
			{
				var DefaultNamespace = this.DefaultNamespace.TakeUntilLastIfAny(".Documentation");

				var Assemblies = Enumerable.ToArray(
					from a in this.BodyElement.XPathSelectElements("//a")

					let href = a.Attribute("href")
					where href != null


					let Archive = href.Value
					where Archive.EndsWith(".zip")

					let ArchiveTitleAttribute = a.Attribute("title")

					let ArchiveName = Archive.SkipUntilLastIfAny("/").TakeUntilLastIfAny(".")

					let ArchiveTitle = ArchiveTitleAttribute == null ? ArchiveName : ArchiveTitleAttribute.Value


					let res = GetLocalResource(Archive)
					let zip = res.ToZIPFile()
					from AssemblyEntry in zip.Entries
					let IsApplication = AssemblyEntry.FileName.ToLower().EndsWith(".exe")
					let IsLibrary = AssemblyEntry.FileName.ToLower().EndsWith(".dll")
					where IsApplication || IsLibrary

					let AssemblyDocumentationFile = AssemblyEntry.FileName.SkipUntilLastIfAny(".") + ".xml"
					let AssemblyDocumentation = zip.Entries.FirstOrDefault(k => k.FileName.ToLower() == AssemblyDocumentationFile)


					let DocumentationOrDefault = AssemblyDocumentation == null ? null : XDocument.Parse(AssemblyDocumentation.Text)

					let Assembly = Assembly.Load(AssemblyEntry.Bytes)

					let AssemblyName = Assembly.GetName().Name


					// Visual Studio generated assembly.
					where !AssemblyName.EndsWith(".vshost")

					// ? Chicken and egg. :) We would be documenting a previous version...
					where AssemblyName != this.DefaultNamespace


					//let AssemblyName = AssemblyEntry.FileName.SkipUntilLastIfAny("/").TakeUntilLastIfAny(".")

					select new { ArchiveTitle, AssemblyName, DocumentationOrDefault, Assembly }
				);

				// currently we only support one page referencing the compilation archives...
				if (!Assemblies.Any())
					return;

				// http://www.google.com/search?sourceid=chrome&ie=UTF-8&q=define:+compilation
				// something that is compiled (as into a single book or file) 
				var CompilationBuilder = this.r.RewriteArguments.Module.DefineType(
					this.DefaultNamespace + ".Documentation.Compilation", TypeAttributes.Public,
					typeof(CompilationBase)
				);

				var CompilationBuilder_ctor = CompilationBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);

				{
					var il = CompilationBuilder_ctor.GetILGenerator();

					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Call, typeof(CompilationBase).GetConstructors().Single());
				}

				var ArchiveBuilderRedirects = new List<Action>();

				// define a method like it over here and give me a duplicater

				#region default view
				foreach (var Archive in (from k in Assemblies group k by k.ArchiveTitle))
				{
					var ArchiveBuilder = this.r.RewriteArguments.Module.DefineType(
						this.DefaultNamespace + ".Documentation.Compilation" + Archive.Key + "Archive",
						TypeAttributes.NotPublic,
						typeof(CompilationArchiveBase)
					);

					var ArchiveBuilder_ctor = ArchiveBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);

					{
						var il = ArchiveBuilder_ctor.GetILGenerator();

						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Call, typeof(CompilationArchiveBase).GetConstructors().Single());

						il.EmitStoreFields(
							this.r.RewriteArguments.context.PropertyCache[
								typeof(CompilationArchiveBase).GetProperties(
									BindingFlags.Instance | BindingFlags.Public | BindingFlags.Instance
								)
							],
							new
							{
								Name = Archive.Key,
							}
						);
					}


					var AssemblyBuilderRedirects = new List<Action>();


					#region Archive
					foreach (var Assembly in (from k in Archive group k by k.AssemblyName))
					{
						var AssemblyNameHint = CompilerBase.GetSafeLiteral(Assembly.Key, null);

						var AssemblyBuilder = this.r.RewriteArguments.Module.DefineType(
							this.DefaultNamespace + ".Documentation." + Archive.Key + "." + Assembly.Key + ".CompilationAssembly",
							TypeAttributes.NotPublic,
							typeof(CompilationAssemblyBase)
						);
						var AssemblyBuilder_ctor = AssemblyBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);

						var First = Assembly.First();

						{
							var il = AssemblyBuilder_ctor.GetILGenerator();

							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Call, typeof(CompilationAssemblyBase).GetConstructors().Single());

							il.EmitStoreFields(
								this.r.RewriteArguments.context.PropertyCache[
									typeof(CompilationAssemblyBase).GetProperties(
										BindingFlags.Instance | BindingFlags.Public | BindingFlags.Instance
									)
								],
								new
								{
									First.Assembly.GetName().Name,
									First.Assembly.ManifestModule.MetadataToken
								}
							);
						}

						// if we have multiple copies in the archive we select the first

						var TypeBuilderRedirects = new List<Action>();

						foreach (var ExportedType in First.Assembly.GetExportedTypes())
						{
							var TypeBuilder = this.r.RewriteArguments.Module.DefineType(
								this.DefaultNamespace + ".Documentation." + Archive.Key + "." + Assembly.Key + "." + ExportedType.FullName.SkipUntilIfAny(Assembly.Key + "."),
								TypeAttributes.NotPublic,
								typeof(CompilationTypeBase)
							);

							var TypeBuilder_ctor = TypeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, null);

							{
								var il = TypeBuilder_ctor.GetILGenerator();

								il.Emit(OpCodes.Ldarg_0);
								il.Emit(OpCodes.Call, typeof(CompilationTypeBase).GetConstructors().Single());

								il.EmitStoreFields(
									this.r.RewriteArguments.context.PropertyCache[
										typeof(CompilationTypeBase).GetProperties(
											BindingFlags.Instance | BindingFlags.Public | BindingFlags.Instance
										)
									],
									new
									{
										ExportedType.FullName,
										ExportedType.MetadataToken
									}
								);

							}

							// do members here

							{
								var il = TypeBuilder_ctor.GetILGenerator();
								il.Emit(OpCodes.Ret);

							}

							TypeBuilder.CreateType();

							TypeBuilderRedirects.Add(
								delegate
								{
									this.r.RewriteArguments.context.MemberRenameCache[
										((Func<CompilationAssemblyBaseTemplate, CompilationTypeBase>)(k => k.Internal)).ToReferencedField()
									] = "Internal_" + TypeBuilder.FullName;

									this.r.RewriteArguments.context.MemberRenameCache[
										((Func<CompilationAssemblyBaseTemplate, Func<CompilationTypeBase>>)(k => k.Add)).ToReferencedMethod()
									] = "InternalAdd_" + TypeBuilder.FullName;

									this.r.RewriteArguments.context.TypeCache[typeof(CompilationAssemblyBaseTemplate)] = AssemblyBuilder;
									this.r.RewriteArguments.context.ConstructorCache[
										typeof(CompilationTypeBaseTemplate).GetConstructors().Single()
									] = TypeBuilder_ctor;

									this.r.RewriteArguments.context.TypeCache[typeof(CompilationAssemblyBaseTemplate)] = AssemblyBuilder;

								}
							);
						}

						DuplicateWriter(
							((Action<CompilationAssemblyBaseTemplate>)(k => k.InitializeTypes())).ToReferencedMethod(),
							AssemblyBuilder_ctor.GetILGenerator(),
							AssemblyBuilder,
							TypeBuilderRedirects
						);

						AssemblyBuilder_ctor.GetILGenerator().Emit(OpCodes.Ret);
						AssemblyBuilder.CreateType();

						AssemblyBuilderRedirects.Add(
							delegate
							{
								this.r.RewriteArguments.context.MemberRenameCache[
									((Func<CompilationArchiveBaseTemplate, CompilationAssemblyBase>)(k => k.InternalAssemblies1)).ToReferencedField()
								] = "Internal_" + AssemblyNameHint;

								this.r.RewriteArguments.context.MemberRenameCache[
									((Func<CompilationArchiveBaseTemplate, Func<CompilationAssemblyBase>>)(k => k.AddInternalAssemblies1)).ToReferencedMethod()
								] = "InternalAdd_" + AssemblyNameHint;

								this.r.RewriteArguments.context.TypeCache[typeof(CompilationAssemblyBaseTemplate)] = ArchiveBuilder;
								this.r.RewriteArguments.context.ConstructorCache[
									typeof(CompilationAssemblyBaseTemplate).GetConstructors().Single()
								] = AssemblyBuilder_ctor;

								this.r.RewriteArguments.context.TypeCache[typeof(CompilationArchiveBaseTemplate)] = ArchiveBuilder;

							}
						);

					}
					#endregion

					DuplicateWriter(
						((Action<CompilationArchiveBaseTemplate>)(k => k.InitializeAssemblies())).ToReferencedMethod(),
						ArchiveBuilder_ctor.GetILGenerator(),
						ArchiveBuilder,
						AssemblyBuilderRedirects
					);

					ArchiveBuilder_ctor.GetILGenerator().Emit(OpCodes.Ret);
					ArchiveBuilder.CreateType();

					ArchiveBuilderRedirects.Add(
						delegate
						{
							this.r.RewriteArguments.context.MemberRenameCache[
								((Func<CompilationBaseTemplate, CompilationArchiveBase>)(k => k.InternalArchive1)).ToReferencedField()
							] = "Internal_" + ArchiveBuilder.Name;

							this.r.RewriteArguments.context.MemberRenameCache[
								((Func<CompilationBaseTemplate, Func<CompilationArchiveBase>>)(k => k.AddArchive1)).ToReferencedMethod()
							] = "InternalAdd_" + ArchiveBuilder.Name;

							this.r.RewriteArguments.context.TypeCache[typeof(CompilationArchiveBaseTemplate)] = ArchiveBuilder;
							this.r.RewriteArguments.context.ConstructorCache[
								typeof(CompilationArchiveBaseTemplate).GetConstructors().Single()
							] = ArchiveBuilder_ctor;

							this.r.RewriteArguments.context.TypeCache[typeof(CompilationBaseTemplate)] = CompilationBuilder;

						}
					);
				}
				#endregion


				DuplicateWriter(
					((Action<CompilationBaseTemplate>)(k => k.InitializeArchives())).ToReferencedMethod(),
					CompilationBuilder_ctor.GetILGenerator(),
					CompilationBuilder,
					ArchiveBuilderRedirects
				);

				CompilationBuilder_ctor.GetILGenerator().Emit(OpCodes.Ret);
				CompilationBuilder.CreateType();

			}

			public void DuplicateWriter(MethodInfo Template, ILGenerator il, TypeBuilder DeclaringType, IEnumerable<Action> Redirections)
			{
				foreach (var item in Redirections)
					using (this.r.RewriteArguments.context.ToTransientTransaction())
					{
						var il_a = new ILTranslationExtensions.EmitToArguments
						{
							TranslateTargetType = this.r.RewriteArguments.context.TypeCache,
							TranslateTargetField = this.r.RewriteArguments.context.FieldCache,
							TranslateTargetMethod = this.r.RewriteArguments.context.MethodCache,
							TranslateTargetConstructor = this.r.RewriteArguments.context.ConstructorCache,
						};

						il_a[OpCodes.Ret] =
							delegate
							{

							};

						item();

						Template.EmitTo(il, il_a);
					}

			}

		}
	}


}
