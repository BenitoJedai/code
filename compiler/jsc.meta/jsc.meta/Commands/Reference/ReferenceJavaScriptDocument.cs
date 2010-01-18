﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.meta.Tools;
using Microsoft.CSharp;
using ScriptCoreLib;
using System.Reflection.Emit;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using jsc.Languages.IL;
using ScriptCoreLib.JavaScript.DOM;

namespace jsc.meta.Commands.Reference
{
	[Description("Injecting javascript into HTML has never been that easy!")]
	public class ReferenceJavaScriptDocument
	{
		// http://www.technospot.net/blogs/convert-html-to-javascript-dom-online-tool/

		// user drops an html file
		// ScriptCoreLib.JavaScript.DOM tree will be built

		// images should be downloaded and packaged as assets


		const string WebSource_HTML = "WebSource.HTML";

		const string __References = "references.txt";

		public FileInfo ProjectFileName;


		public void Invoke()
		{
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
			  where Directory == WebSource_HTML || Directory.EndsWith("." + WebSource_HTML)

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
				select r
			);

			var LocalSources = Enumerable.ToArray(
				from k in Targets
				from f in k
				where f.File.Name.EndsWith(".htm")
				select new SourceFile { Content = File.ReadAllText(f.File.FullName), Reference = f.File.FullName }
			);

			// http://support.microsoft.com/kb/304655
			var Sources = DownloadWebSource(References).Concat(LocalSources).ToArray();

			{
				var Product = DefaultNamespace + "." + WebSource_HTML;

				// at this time we are not actually merging anything...
				var r = new RewriteToAssembly
				{
					staging = Staging,
					product = Product,

					#region if we are going to inject code from jsc we need to copy it
					rename = new RewriteToAssembly.NamespaceRenameInstructions[] {
					    "jsc.meta->" +  DefaultNamespace,
					    "jsc->" +  DefaultNamespace,
					},

					merge = new RewriteToAssembly.MergeInstruction[] {
					    "jsc.meta",
					    "jsc"
					},
					#endregion

					PostRewrite =
						a =>
						{
							// at this point we are free to add any additional code here
							// maybe we should infer some cool classes?


							a.Assembly.DefineAttribute<ObfuscationAttribute>(
								new
								{
									Feature = "script",
								}
							);

							var Pages = a.Module.DefineType(DefaultNamespace + ".Pages", TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.Public);

							// http://www.w3schools.com/tags/ref_entities.asp
							// http://www.w3schools.com/HTML/html_entities.asp
							// http://stackoverflow.com/questions/281682/reference-to-undeclared-entity-exception-while-working-with-xml



							foreach (var item in Sources)
							{
								// http://stackoverflow.com/questions/1039476/reference-to-undeclared-entity-nbsp-why
								// http://forums.asp.net/t/1219076.aspx

								// dirty fix..
								var content = item.Content;

								// yet another fix
								const string doctype_ok = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">";
								const string doctype_vs = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">";

								if (content.StartsWith(doctype_vs))
									content = doctype_ok + content.Substring(doctype_vs.Length);

								content = HTMLEntities.Aggregate(content,
										 (i, k) => i.Replace(k.Key, k.Value)
								 );

								var xml = XDocument.Parse(content);

								var title = xml.XPathSelectElement("/html/head/title");
								var body = xml.XPathSelectElement("/html/body");

								var Page = Pages.DefineNestedType(title.Value, TypeAttributes.NestedPublic);

								// we should be returning DOM object instead?
								#region Page_HTML
								var Page_HTML = Page.DefineProperty("HTML", PropertyAttributes.None, typeof(string), null);

								var Page_HTML_get = Page.DefineMethod("get_HTML", MethodAttributes.Static | MethodAttributes.Public, typeof(string), null);

								{
									var il = Page_HTML_get.GetILGenerator();

									// http://stackoverflow.com/questions/3793/best-way-to-get-innerxml-of-an-xelement
									var body_innerXML = body.Nodes().Aggregate("", (b, node) => b += node.ToString());

									il.Emit(OpCodes.Ldstr, body_innerXML);
									il.Emit(OpCodes.Ret);
								}

								Page_HTML.SetGetMethod(Page_HTML_get);
								#endregion

								// http://www.exampledepot.com/egs/org.w3c.dom/xpath_GetElemByAttr.html
								var Elements = Page.DefineNestedType("StaticElements", TypeAttributes.NestedPublic);

								
								//foreach (var CurrentElement in body.XPathSelectElements("//img[@id]"))
								//{

								//}

								foreach (var CurrentElement in body.XPathSelectElements("//*[@id]"))
								{
									var id = CurrentElement.Attribute("id").Value;

									var e_Type = ElementTypes.ContainsKey(CurrentElement.Name.LocalName) ? ElementTypes[CurrentElement.Name.LocalName] : typeof(IHTMLElement);

									var e = Elements.DefineProperty(id, PropertyAttributes.None, e_Type, null);
									var get_e = Elements.DefineMethod("get_" + id, MethodAttributes.Static | MethodAttributes.Public, e_Type, null);

									Func<IHTMLElement> get_e_template =
										delegate
										{
											return Native.Document.getElementById("id");
										};

									{
										var il = get_e.GetILGenerator();

										var il_a = new ILTranslationExtensions.EmitToArguments();

										il_a[OpCodes.Ldstr] =
											x =>
											{
												x.il.Emit(OpCodes.Ldstr, id);
											};

										il_a[OpCodes.Ret] =
											x =>
											{
												x.il.Emit(OpCodes.Castclass, e_Type);
												x.il.Emit(OpCodes.Ret);
											};

										get_e_template.Method.EmitTo(il, il_a);


									}

									e.SetGetMethod(get_e);
								}

								Elements.CreateType();

								DefinePageConstructor(body, Page);

								Page.CreateType();
							}

							Pages.CreateType();
						}
				};

				r.Invoke();

				AddReference(r.Output, new AssemblyName(Product));
			}

			if (csproj_dirty)
				csproj.Save(this.ProjectFileName.FullName);
		}

		private static void DefinePageConstructor(XElement body, TypeBuilder Page)
		{
			var ctor = Page.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);
			var ElementType = typeof(IHTMLElement);

			var Container = Page.DefineField("_Container", ElementType, FieldAttributes.Private | FieldAttributes.InitOnly);

			var ElementProperty = Page.DefineProperty("Container", PropertyAttributes.None, ElementType, null);

			var get_ElementField = Page.DefineMethod("get_Container", MethodAttributes.Public, CallingConventions.Standard, ElementType, null);

			var get_ElementField_il = get_ElementField.GetILGenerator();

			get_ElementField_il.Emit(OpCodes.Ldarg_0);
			get_ElementField_il.Emit(OpCodes.Ldfld, Container);
			get_ElementField_il.Emit(OpCodes.Ret);

			ElementProperty.SetGetMethod(get_ElementField);


			var Counter = new Counter();

			{
				var il = ctor.GetILGenerator();

				DefinePageElement(body, Page, Counter, il, OpCodes.Ldnull);

				#region this.Container = loc0
				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldloc_0);
				il.Emit(OpCodes.Stfld, Container);
				#endregion

				il.Emit(OpCodes.Ret);
			}
		}

		public class Counter
		{
			public int Value;
		}

		private static void DefinePageElement(XElement body, TypeBuilder Page, Counter Counter, ILGenerator il, OpCode parent)
		{
			Action Implementation1 =
				delegate
				{
					var c = TemplateHolder.Initialize(null);
				};

			{
				var il_a = new ILTranslationExtensions.EmitToArguments();

				il_a[OpCodes.Call] = x =>
				{
					var Initialize = Page.DefineMethod(
						"Initialize_" + Counter.Value++,
						MethodAttributes.Private,
						typeof(IHTMLElement),
						new[] { typeof(IHTMLElement) }
					);

					DefinePageElement(body, Initialize.GetILGenerator(), Page, Counter);

					x.il.Emit(OpCodes.Ldarg_0);
					x.il.Emit(parent);
					x.il.Emit(OpCodes.Call, Initialize);
				};

				il_a[OpCodes.Ret] = x => { };
				il_a[OpCodes.Ldnull] = x => { };

				Implementation1.Method.EmitTo(il, il_a);
			}
		}

		private static void DefinePageElement(XElement body, ILGenerator il, TypeBuilder Page, Counter Counter)
		{

			Action Implementation2 =
				delegate
				{
					#region c.setAttribute("name", "value");
					Action<IHTMLElement> Implementation3 =
						c =>
						{
							// seems to work for .style too in browsers :)
							c.setAttribute("name", "value");
						};



					foreach (var item in body.Attributes())
					{
						if (item.Name.LocalName == "id")
						{
							var ElementType = ElementTypes.ContainsKey(body.Name.LocalName) ? ElementTypes[body.Name.LocalName] : typeof(IHTMLElement);
							var ElementField = Page.DefineField("_" + item.Value, ElementType, FieldAttributes.Private);

							il.Emit(OpCodes.Ldarg_0);
							il.Emit(OpCodes.Ldloc_0);
							il.Emit(OpCodes.Castclass, ElementType);
							il.Emit(OpCodes.Stfld, ElementField);

							var ElementProperty = Page.DefineProperty(item.Value, PropertyAttributes.None, ElementType, null);

							var get_ElementField = Page.DefineMethod("get_" + item.Value, MethodAttributes.Public, CallingConventions.Standard, ElementType, null);

							var get_ElementField_il = get_ElementField.GetILGenerator();

							get_ElementField_il.Emit(OpCodes.Ldarg_0);
							get_ElementField_il.Emit(OpCodes.Ldfld, ElementField);
							get_ElementField_il.Emit(OpCodes.Ret);

							ElementProperty.SetGetMethod(get_ElementField);
						}
						else
						{
							var il_a = new ILTranslationExtensions.EmitToArguments();

							il_a[OpCodes.Ret] = delegate { };
							il_a[OpCodes.Ldarg_0] = x => x.il.Emit(OpCodes.Ldloc_0);
							il_a[OpCodes.Ldstr] = x => x.il.Emit(OpCodes.Ldstr,
								x.i.TargetLiteral == "name" ? item.Name.LocalName : item.Value
							);

							Implementation3.Method.EmitTo(il, il_a);
						}
					}
					#endregion

					#region c.appendChild
					Action<IHTMLElement> Implementation4 =
						c =>
						{
							c.appendChild(new ITextNode("e"));
						};

					foreach (var item in body.Nodes())
					{
						if (item is XText)
						{
							var il_a = new ILTranslationExtensions.EmitToArguments();

							il_a[OpCodes.Ret] = delegate { };
							il_a[OpCodes.Ldarg_0] = x => x.il.Emit(OpCodes.Ldloc_0);
							il_a[OpCodes.Ldstr] = x => x.il.Emit(OpCodes.Ldstr, ((XText)item).Value);

							Implementation4.Method.EmitTo(il, il_a);
						}

						if (item is XElement)
						{
							DefinePageElement((XElement)item, Page, Counter, il, OpCodes.Ldloc_0);
						}
					}
					#endregion
				};

			{
				#region Implementation1
				Func<object, IHTMLElement, IHTMLElement> Implementation1 =
						(__this, parent) =>
						{
							var c = new IHTMLElement("" /* body.Name.LocalName */);

							//c.setAttribute("title", "hi");
							TemplateHolder.Implementation();



							if (parent != null)
								parent.appendChild(c);

							return c;
						};


				{
					var il_a = new ILTranslationExtensions.EmitToArguments();

					il_a[OpCodes.Call] = x =>
					{
						Action Implementation = TemplateHolder.Implementation;
						if (x.i.TargetMethod == Implementation.Method)
						{
							Implementation2();

							return;
						}

						il.Emit(OpCodes.Call, x.i.TargetMethod);
					};

					il_a[OpCodes.Ldarg_0] = x => x.il.Emit(OpCodes.Ldarg_1);
					il_a[OpCodes.Ldstr] = x => x.il.Emit(OpCodes.Ldstr, body.Name.LocalName);

					Implementation1.Method.EmitTo(il, il_a);
				}
				#endregion
			}
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

		public static readonly Dictionary<string, string> HTMLEntities = new Dictionary<string, string>
							{
								{"&nbsp;", "&#160;"},
								{"&ndash;", "&#8211;"}
							};

		public static readonly Dictionary<string, Type> ElementTypes = new Dictionary<string, Type>
							{
								{"a", typeof(IHTMLAnchor)},
								{"img", typeof(IHTMLImage)},
								{"textarea", typeof(IHTMLTextArea)},
								{"input", typeof(IHTMLInput)}
							};
	}
}
