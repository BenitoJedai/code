﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using jsc.Languages.IL;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.meta.Tools;
using jsc.Script;
using Microsoft.CSharp;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using jsc.Library;
using ScriptCoreLib.Ultra.Library.Extensions;
using jsc.meta.Library.Templates.JavaScript;
using System.Diagnostics;

namespace jsc.meta.Commands.Reference
{
	[Description("Injecting javascript into HTML has never been that easy!")]
	public partial class ReferenceJavaScriptDocument
	{


		public void Invoke()
		{
			if (this.AttachDebugger)
				Debugger.Launch();

			var csproj = XDocument.Load(ProjectFileName.FullName);
			var csproj_dirty = false;



			/*

<Project ToolsVersion="3.5" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
	<RootNamespace>AutoGeneratedReferences</RootNamespace>

  <ItemGroup>
	<Reference Include="System" />

  <ItemGroup>
	<None Include="Components\JohDoe.TextComponent" />
			*/

			XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
			var nsItemGroup = ns + "ItemGroup";
			var nsRootNamespace = ns + "RootNamespace";
			var nsPropertyGroup = ns + "PropertyGroup";
			var nsNone = ns + "None";
			var nsContent = ns + "Content";
			var nsDependentUpon = ns + "DependentUpon";
			var nsReference = ns + "Reference";
			var nsHintPath = ns + "HintPath";
			var nsAssemblyName = ns + "AssemblyName";

			var SourceAssemblyName = Enumerable.First(
				 from PropertyGroup in csproj.Root.Elements(nsPropertyGroup)
				 from AssemblyName in PropertyGroup.Elements(nsAssemblyName)
				 select AssemblyName.Value
			);

			var DefaultNamespace = Enumerable.First(
				 from PropertyGroup in csproj.Root.Elements(nsPropertyGroup)
				 from RootNamespace in PropertyGroup.Elements(nsRootNamespace)
				 select RootNamespace.Value
			);

			// bin is assumed to being ignored by svn
			// we need to stage it
			var Staging = this.ProjectFileName.Directory.CreateSubdirectory("bin/" + WebSource_HTML + ".staging");

			// fixme: no caching as of yet
			//var Cache = Staging.CreateSubdirectory("cache");

			#region AddReference
			Action<FileInfo, AssemblyName> AddReference =
				(AssemblyFile, Name) =>
				{

					/* add reference
<Reference Include="AutoGeneratedReferences.Components.JohDoe.TextComponent, Version=0.0.0.0, Culture=neutral, processorArchitecture=MSIL">
  <SpecificVersion>False</SpecificVersion>
  <HintPath>bin\staging\AutoGeneratedReferences.Components.JohDoe.TextComponent.dll</HintPath>
</Reference>
					*/

					var TargetHintPath = AssemblyFile.FullName.Substring(ProjectFileName.Directory.FullName.Length + 1);

					if (!Enumerable.Any(
						 from ItemGroup in csproj.Root.Elements(nsItemGroup)
						 from Reference in ItemGroup.Elements(nsReference)
						 from HintPath in Reference.Elements(nsHintPath)
						 where TargetHintPath == HintPath.Value
						 select new { HintPath, Reference, ItemGroup }
						))
					{
						var TargetItemGroup = Enumerable.First(
							from ItemGroup in csproj.Root.Elements(nsItemGroup)
							from Reference in ItemGroup.Elements(nsReference)
							select ItemGroup
						);

						TargetItemGroup.Add(
							new XElement(nsReference,
								new XAttribute("Include", Name.ToString()),
								new XElement(nsHintPath, TargetHintPath)
							)
						);

						csproj_dirty = true;

					}
				};
			#endregion


			var Targets =
			  from ItemGroup in csproj.Root.Elements(nsItemGroup)
			  from None in ItemGroup.Elements(nsNone).Concat(ItemGroup.Elements(nsContent))
			  let Include = None.Attribute("Include").Value
			  let Directory = Path.GetDirectoryName(Include)


			  where DirectoryNeedsConversion(Directory)

			  let TargetName = DefaultNamespace + "." + Directory.Replace("/", ".").Replace("\\", ".")
			  let Target = new FileInfo(Path.Combine(Staging.FullName, TargetName.Substring(DefaultNamespace.Length + 1) + ".dll"))

			  let File = new FileInfo(Path.Combine(ProjectFileName.Directory.FullName, Include))
			  group new { ItemGroup, None, Include, File, Directory, TargetName, Target } by Directory;




			var References = Enumerable.Distinct(
				from k in Targets
				from f in k
				// should we restrict us to single file or allow multiple files to
				// enable grouping?
				where
					f.File.Name == __References
				from r in File.ReadAllLines(f.File.FullName)
				where !string.IsNullOrEmpty(r)
				select r
			);

			var LocalSources = Enumerable.ToArray(
				from k in Targets
				from f in k
				where f.File.Name.EndsWith(".htm")
				select new SourceFile
				{
					Content = File.ReadAllText(f.File.FullName),
					Reference = f.File.FullName,
					GetLocalResource =
						n =>
						{
							var r = k.SingleOrDefault(kk => kk.File.Name == n);

							if (r == null) return null;

							return r.File;
						}
				}
			);

			// http://support.microsoft.com/kb/304655
			var Sources = DownloadWebSource(References).Concat(LocalSources).ToArray();

			{
				var Product = DefaultNamespace + "." + UltraSource;

				// at this time we are not actually merging anything...
				var r = default(RewriteToAssembly);

				r = new RewriteToAssembly
				{
					staging = Staging,
					product = Product,

					#region if we are going to inject code from jsc we need to copy it
					rename = new RewriteToAssembly.NamespaceRenameInstructions[] {
					    "jsc.meta->" +  DefaultNamespace,
					    "jsc->" +  DefaultNamespace,
					},

					//merge = new RewriteToAssembly.MergeInstruction[] {
					//    "jsc.meta",
					//    "jsc"
					//},
					#endregion

					PostRewrite =
						a =>
						{
							// at this point we are free to add any additional code here
							// maybe we should infer some cool classes?


							a.Assembly.DefineAttribute<ObfuscationAttribute>(
								new
								{
									Feature =
										this.IsMerge ? "merge" : "script"
								}
							);

							// Nested types do not play well with type erasure...

							//var Pages = a.Module.DefineType(DefaultNamespace + ".Pages", TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.Public);

							// http://www.w3schools.com/tags/ref_entities.asp
							// http://www.w3schools.com/HTML/html_entities.asp
							// http://stackoverflow.com/questions/281682/reference-to-undeclared-entity-exception-while-working-with-xml


							var TypeVariations = new Dictionary<string, TypeVariations>();


							foreach (var item in Sources)
							{
								// http://stackoverflow.com/questions/1039476/reference-to-undeclared-entity-nbsp-why
								// http://forums.asp.net/t/1219076.aspx

								// dirty fix..
								var content = item.Content;

								// http://blogs.pingpoet.com/overflow/archive/2005/07/20/6607.aspx
								// http://msdn.microsoft.com/en-us/library/bb356942.aspx
								// fixme: XmlReader + DTD

								// yet another fix
								const string doctype_ok = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">";
								const string doctype_vs = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">";

								if (content.StartsWith(doctype_vs))
									content = doctype_ok + content.Substring(doctype_vs.Length);

								content = HTMLEntities.Aggregate(content,
										 (i, k) => i.Replace(k.Key, k.Value)
								 );


								// really dirty fix...

								// wordpress, why are you making the day harder than it needs to be? :)
								content = content.Replace(" xmlns=\"http://www.w3.org/1999/xhtml\"", " ");
								content = content.Replace(" xmlns=\"http://www.google.com/ns/jotspot\" ", " ");

								// should we use html tidy?
								// is the google sites printing view public?
								// google sites, why aren't attributes qouted? :|
								content = content.Replace(" cellpadding=1 cellspacing=1>", " cellpadding='1' cellspacing='1'>");
								content = content.Replace(" target=_top>", " target='_top'>");

								// http://stackoverflow.com/questions/66837/when-is-a-cdata-section-necessary-within-a-script-tag
								content = content.Replace("<script type=\"text/javascript\">", "<script type=\"text/javascript\"><![CDATA[");
								content = content.Replace("<script>", "<script><![CDATA[");
								content = content.Replace("</script>", "]]></script>");
								
								//var reader = XmlReader.Create(new StringReader(content), new XmlReaderSettings { ProhibitDtd = false });
								//var xml = XDocument.Load(reader);
								////var nameTable = reader.NameTable;
								//var namespaceManager = new XmlNamespaceManager(nameTable);
								//namespaceManager.AddNamespace("", "http://www.w3.org/1999/xhtml");

								var xml = XDocument.Parse(content);
								// http://stackoverflow.com/questions/477962/how-to-create-xelement-with-default-namespace-for-children-without-using-xnamespa

								XNamespace xhtml = "http://www.w3.org/1999/xhtml";

								// For body and each class element
								var TitleElement = xml.XPathSelectElement("/html/head/title");
								var BodyElement = xml.XPathSelectElement("/html/body");

								var TitleValue = TitleElement == null || string.IsNullOrEmpty(TitleElement.Value) ?
									item.Reference.TakeUntilIfAny("?").SkipUntilLastIfAny("/").TakeUntilIfAny(".") :
									TitleElement.Value;

								// should we make CamelCaseing optional?
								var PageName = CompilerBase.GetSafeLiteral(TitleValue, null).ToCamelCase();

								// we need to make the title/page name
								// C# compatible :)

								// The web application could opt in for dynamic CMS updates... RSS ? :) Download HTML on the server and push updates?


								DefineNamedImages(DefaultNamespace, a, BodyElement, r, item.GetLocalResource, TypeVariations.Add);

								var VariationsForPages = new Dictionary<string, Func<string, Type>>
								{
									{"FromWeb", Source => TypeVariations[Source].FromWeb ?? TypeVariations[Source].FromAssets},
									{"FromAssets", Source => TypeVariations[Source].FromAssets},
									{"FromBase64", Source => TypeVariations[Source].FromBase64}
								};

								foreach (var CurrentVariationForPage in VariationsForPages)
								{
									DefinePageType(DefaultNamespace, a, content, BodyElement, PageName, CurrentVariationForPage.Key, CurrentVariationForPage.Value);


									var __id = BodyElement.XPathSelectElements("//*[@id]").Select(k => new { CurrentElement = k, id = k.Attribute("id").Value });

									foreach (var k in __id)
									{
										DefinePageType(DefaultNamespace, a, null, k.CurrentElement, "Controls.Named." + PageName + "_" + k.id, CurrentVariationForPage.Key, CurrentVariationForPage.Value);
									}

									var __class = BodyElement.XPathSelectElements("//*[@class]").Except(BodyElement.XPathSelectElements("//*[@id]")).Select(k => new { CurrentElement = k, @class = k.Attribute("class").Value }).Where(k => !k.@class.Contains(" ")).GroupBy(k => k.@class).Where(k => k.Count() == 1).Select(k => k.Single());

									foreach (var k in __class)
									{
										DefinePageType(DefaultNamespace, a, null, k.CurrentElement, "Controls.Anonymous." + PageName + "_" + k.@class, CurrentVariationForPage.Key, CurrentVariationForPage.Value);
									}
								}
							}


							//Pages.CreateType();
						}
				};

				r.Invoke();

				AddReference(r.Output, new AssemblyName(Product));
			}

			if (csproj_dirty)
				csproj.Save(this.ProjectFileName.FullName);
		}

		private static bool DirectoryNeedsConversion(string Directory)
		{
			if (Directory == WebSource_HTML || Directory.EndsWith("." + WebSource_HTML) || Directory.EndsWith("\\" + WebSource_HTML))
				return true;

			if (Directory == UltraSource || Directory.EndsWith("." + UltraSource) || Directory.EndsWith("\\" + UltraSource))
				return true;

			return false;
		}



		private void DefineInstanceImages(RewriteToAssembly.AssemblyRewriteArguments a, TypeBuilder Page, Dictionary<XElement, FieldBuilder> lookup)
		{
			#region References
			var References = Page.DefineProperty("Images", PropertyAttributes.None, typeof(IHTMLImage[]), null);

			var References_get = Page.DefineMethod("get_Images", MethodAttributes.Public, typeof(IHTMLImage[]), null);

			References.SetGetMethod(References_get);

			{
				var il = References_get.GetILGenerator();

				Func<IHTMLImage[]> Implementation1 = () => new IHTMLImage[] { };

				var il_a = new ILTranslationExtensions.EmitToArguments();

				var Images = lookup.Where(k => k.Value != null).Select((k, index) => new { k, index }).ToArray();


				il_a[OpCodes.Ldc_I4_0] =
					x =>
					{
						il.Emit(OpCodes.Ldc_I4, Images.Length);
					};

				il_a[OpCodes.Stloc_0] =
					x =>
					{
						il.Emit(OpCodes.Stloc_0);

						foreach (var item in Images)
						{
							il.Emit(OpCodes.Ldloc_0);
							il.Emit(OpCodes.Ldc_I4, item.index);
							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldfld, item.k.Value);
							il.Emit(OpCodes.Stelem_Ref);


							/*
							L_0007: ldloc.1 
							L_0008: ldc.i4.0 
							L_0009: ldstr ""
							L_000e: stelem.ref 
							 */
						}
					};



				Implementation1.Method.EmitTo(il, il_a);
			}
			#endregion

		}

		private void DefineStaticImages(RewriteToAssembly.AssemblyRewriteArguments a, TypeBuilder Page, XElement[] i)
		{
			if (i == null)
				throw new ArgumentNullException("i");
			//var Images = Page.DefineNestedType("Images", TypeAttributes.NestedPublic);

			var References_value = i
				.Where(k => k.Attribute("src") != null)
				.Select(k => k.Attribute("src").Value)
				.Where(k => !string.IsNullOrEmpty(k))
				.Distinct()
				.Select((k, index) => new { k, index })
				.ToArray();

			// we might want to return IHTMLImage references instead with or without id's...

			#region References
			var References = Page.DefineProperty("Images", PropertyAttributes.None, typeof(string[]), null);

			var References_get = Page.DefineMethod("get_Images", MethodAttributes.Public | MethodAttributes.Static, typeof(string[]), null);

			References.SetGetMethod(References_get);

			{
				var il = References_get.GetILGenerator();

				Func<string[]> Implementation1 = () => new string[] { };

				var il_a = new ILTranslationExtensions.EmitToArguments();



				il_a[OpCodes.Ldc_I4_0] =
					x =>
					{
						il.Emit(OpCodes.Ldc_I4, References_value.Length);
					};

				il_a[OpCodes.Stloc_0] =
					x =>
					{
						il.Emit(OpCodes.Stloc_0);

						foreach (var item in References_value)
						{
							il.Emit(OpCodes.Ldloc_0);
							il.Emit(OpCodes.Ldc_I4, item.index);
							il.Emit(OpCodes.Ldstr, item.k);
							il.Emit(OpCodes.Stelem_Ref);


							/*
							L_0007: ldloc.1 
							L_0008: ldc.i4.0 
							L_0009: ldstr ""
							L_000e: stelem.ref 
							 */
						}
					};



				Implementation1.Method.EmitTo(il, il_a);
			}
			#endregion

		}


		public class Counter
		{
			public int Value;
		}


		static class TemplateHolder
		{
			public static IHTMLElement Initialize(IHTMLElement e)
			{
				return null;
			}


			public static void Implementation()
			{
			}
		}


		public class SourceFile
		{
			public string Reference;
			public string Content;

			public Func<string, FileInfo> GetLocalResource;
		}

		private static IEnumerable<SourceFile> DownloadWebSource(IEnumerable<string> References)
		{
			foreach (var Reference in References)
			{
				Console.WriteLine("downloading: " + Reference);

				var c = (HttpWebRequest)HttpWebRequest.Create(Reference);

				// http://code.logos.com/blog/2009/06/using_if-modified-since_in_http_requests.html
				// http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.ifmodifiedsince.aspx
				// http://www.acmebinary.com/blog/archive/2006/09/05/252.aspx

				var r = (HttpWebResponse)c.GetResponse();

				try
				{
					if (r.StatusCode == HttpStatusCode.OK)
					{
						var Content = new StreamReader(r.GetResponseStream()).ReadToEnd();

						yield return new SourceFile { Content = Content, Reference = Reference };
					}

				}
				finally
				{
					r.Close();
				}

			}
		}

		// http://www.w3.org/TR/REC-html40/sgml/entities.html
		public static readonly Dictionary<string, string> HTMLEntities = new Dictionary<string, string>
							{
								{"&nbsp;", "&#160;"},
								{"&ndash;", "&#8211;"},
								{"&laquo;", "&#171;"},
								{"&raquo;", "&#187;"},
							};

		// this should be part of the ScriptCoreLib

		// todo: we should actually scan the html elements for InternalConstructos and infer the type names!

		public static readonly Dictionary<string, Type> ElementTypes = new Dictionary<string, Type>
							{
								{"a", typeof(IHTMLAnchor)},
								{"img", typeof(IHTMLImage)},
								{"textarea", typeof(IHTMLTextArea)},
								{"input", typeof(IHTMLInput)},
								{"button", typeof(IHTMLButton)},
								{"label", typeof(IHTMLLabel)},
								{"iframe", typeof(IHTMLIFrame)},
							};


	}
}
