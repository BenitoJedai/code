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
using ScriptCoreLib.Ultra.Documentation;
using ScriptCoreLib.Ultra.Library.Extensions;
namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	partial class ReferenceUltraSource
	{
		[Description("Convert assemblies (.dll, .xml) from zip files into documentation.")]
		public class DefineDocumentation
		{
			public ReferenceUltraSource Context;

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

					// we may link to a file which we do not explicitly have in the project
					where res != null

					let zip = res.ToZIPFile()
					from AssemblyEntry in zip.Entries
					let IsApplication = AssemblyEntry.FileName.ToLower().EndsWith(".exe")
					let IsLibrary = AssemblyEntry.FileName.ToLower().EndsWith(".dll")
					where IsApplication || IsLibrary

					let AssemblyDocumentationFile = AssemblyEntry.FileName.TakeUntilLastIfAny(".") + ".xml"
					let AssemblyDocumentation = zip.Entries.FirstOrDefault(k => k.FileName.ToLower() == AssemblyDocumentationFile.ToLower())

					let TryParse = new Func<XDocument>(
						() =>
						{
							if (AssemblyDocumentation != null)

								try
								{
									return XDocument.Parse(AssemblyDocumentation.Text);
								}
								catch
								{
									// Visual Basic generates odd xml?
								}
							return null;
						}
					)

					let DocumentationOrDefault = TryParse()

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

				// rationale:
				// Creating one too many types is a problem.
				// It is slow to compile and it creates a big downloadable.

				// http://www.google.com/search?sourceid=chrome&ie=UTF-8&q=define:+compilation
				// something that is compiled (as into a single book or file) 
				var CompilationBuilder = this.r.RewriteArguments.Module.DefineType(
					DefaultNamespace + ".Documentation.Compilation", TypeAttributes.Public,
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
						DefaultNamespace + ".Documentation.Compilation" + Archive.Key + "Archive",
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
							DefaultNamespace + ".Documentation." + Archive.Key + "." + Assembly.Key + ".CompilationAssembly",
							TypeAttributes.NotPublic,
							typeof(CompilationAssemblyBase)
						);
						var AssemblyBuilder_ctor = AssemblyBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);

						var First = Assembly.First();

						var LoadImplementation = DefineXDocuments.DefineNamedXDocument(
							"assets/" + this.DefaultNamespace + "/Data/" + AssemblyNameHint + ".xml",
							r,
							null,
							DefaultNamespace + ".Documentation." + Archive.Key + "." + Assembly.Key + ".CompilationAssemblyData",
							true,
							new XDocument(
								new XElement(CompilationXNames.Assembly,
										from SourceType in First.Assembly.GetTypes()

										let Summary = from Documentation in new[] { First.DocumentationOrDefault }
													  where Documentation != null
													  from members in Documentation.Root.Elements("members")
													  from member in members.Elements("member")
													  let name = member.Attribute("name")
													  where name != null
													  where name.Value == "T:" + SourceType.FullName
													  select member.Element("summary").Value

										select new XElement(CompilationType.__Element,

											new[] {
												new XElement(CompilationXNames.Summary,  Summary.FirstOrDefault() ?? ""),
												new XElement(CompilationType.__FullName, SourceType.FullName),
												new XElement(CompilationType.__MetadataToken, SourceType.MetadataToken),
												new XElement(CompilationType.__IsInterface, SourceType.IsInterface),
												new XElement(CompilationType.__DeclaringType, SourceType.DeclaringType == null ? 0 : SourceType.DeclaringType.MetadataToken),
											}.Concat(
												from SourceMethod in SourceType.GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
												select new XElement(CompilationMethod.__Element,

													new[] 
													{
														new XElement(CompilationMethod.__Name, SourceMethod.Name),
														new XElement(CompilationXNames.MetadataToken, SourceMethod.MetadataToken)
													}.Concat(
														from SourceParameter in SourceMethod.GetParameters()
														select
															new XElement(CompilationMethodParameter.__Element,
																new XElement(CompilationMethodParameter.__Name, SourceParameter.Name)
															)
													)
												)
											).Concat(
													from SourceMethod in SourceType.GetConstructors(BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
													select new XElement(CompilationConstructor.__Element,
													new XElement[] 
													{
														new XElement(CompilationXNames.MetadataToken, SourceMethod.MetadataToken)

													}.Concat(
															from SourceParameter in SourceMethod.GetParameters()
															select
																new XElement(CompilationMethodParameter.__Element,
																	new XElement(CompilationMethodParameter.__Name, SourceParameter.Name)
																)
														)
													)
											).Concat(
												from SourceMethod in SourceType.GetFields(BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
												select new XElement(CompilationField._Field,
													new XElement(CompilationField.__Name, SourceMethod.Name)
												)
											).Concat(
												from SourceNestedType in SourceType.GetNestedTypes()
												select new XElement(CompilationType.__NestedType, SourceNestedType.MetadataToken)
											).Concat(
												from SourceProperty in SourceType.GetProperties(BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
												select new XElement(CompilationProperty.__Element,

													new[] { 
														new XElement(CompilationProperty.__Name, SourceProperty.Name)
													}.Concat(
														from SourcePropertyMethod in new[] {
															new { Name = CompilationXNames.Set, Method = SourceProperty.GetSetMethod() },
															new { Name = CompilationXNames.Get, Method = SourceProperty.GetGetMethod() }
														}
														where SourcePropertyMethod.Method != null
														select new XElement(SourcePropertyMethod.Name, SourcePropertyMethod.Method.MetadataToken)
													)
												)
											).Concat(
												from SourceEvent in SourceType.GetEvents(BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
												select new XElement(CompilationEvent.__Element,
													new XElement(CompilationEvent.__Name, SourceEvent.Name),
													new XElement(CompilationXNames.Add, SourceEvent.GetAddMethod().MetadataToken),
													new XElement(CompilationXNames.Remove, SourceEvent.GetRemoveMethod().MetadataToken)
												)
											).Concat(
												from ElementType in ReferenceJavaScriptDocument.GetElementTypes(new[] { SourceType })
												select new XElement(CompilationType.__HTMLElement, ElementType.Key)
											).Concat(
												from SourceType__ in new[] { SourceType }
												where SourceType__.IsGenericTypeDefinition
												from GenericArgument in SourceType__.GetGenericArguments()
												select new XElement(CompilationType.__GenericArgument, GenericArgument)
											)


										)
								)
							),
							true

						);


						{
							var il = AssemblyBuilder_ctor.GetILGenerator();

							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Call, typeof(CompilationAssemblyBase).GetConstructors().Single());

							il.EmitStoreFields(
								this.r.RewriteArguments.context.PropertyCache[
									typeof(CompilationAssemblyBase).GetProperties(
										BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
									)
								],
								new
								{
									First.Assembly.GetName().Name,
									First.Assembly.ManifestModule.MetadataToken,
									LoadImplementation
								}
							);
						}

						// if we have multiple copies in the archive we select the first





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
