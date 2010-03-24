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
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using jsc.meta.Commands.Reference.ReferenceUltraSource.Templates;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	partial class ReferenceUltraSource
	{
		[Description("Convert assemblies .xml into Downloadable Asset.")]
		public class DefineXDocuments
		{
			public string DefaultNamespace;
			public XElement BodyElement;
			public RewriteToAssembly r;
			public Func<string, FileInfo> GetLocalResource;


			public void Define()
			{

				var Items = Enumerable.ToArray(
					from a in this.BodyElement.XPathSelectElements("//a")

					let href = a.Attribute("href")
					where href != null


					let LinkSource = href.Value
					where LinkSource.EndsWith(".xml")

					let LinkTitleAttribute = a.Attribute("title")

					let LinkName = LinkSource.SkipUntilLastIfAny("/").TakeUntilLastIfAny(".")

					let LinkTitle = LinkTitleAttribute == null ? LinkName : LinkTitleAttribute.Value

					let LocalResource = GetLocalResource(LinkSource)


					select new { LinkSource, LinkTitle, LocalResource }
				);


				foreach (var item in Items)
				{
					var AssetPath = "assets/" + DefaultNamespace + "/" + item.LinkSource;
					var Bytes = File.ReadAllBytes(item.LocalResource.FullName);
					var Title = item.LinkTitle;

					DefineNamedXDocument(AssetPath, Title, Bytes);
				}
			}



			public void DefineNamedXDocument(string AssetPath, string Title, byte[] Bytes)
			{
				var TypeFullName = DefaultNamespace + ".Data." + Title;

				DefineNamedXDocument(AssetPath, r, TypeFullName, false, Bytes);
			}

			public static MethodInfo DefineNamedXDocument(string AssetPath, RewriteToAssembly r, string TypeFullName, bool IsInternal, XDocument Bytes)
			{
				return DefineNamedXDocument(AssetPath, r, TypeFullName, false,
					Encoding.UTF8.GetBytes(Bytes.ToString())
				);
			}

			public static MethodInfo DefineNamedXDocument(string AssetPath, RewriteToAssembly r, string TypeFullName, bool IsInternal, byte[] Bytes)
			{
				var __Create = default(MethodInfo);
				r.RewriteArguments.ScriptResourceWriter.Add(AssetPath, Bytes);


				var TemplateType = typeof(NamedXDocument);

				using (r.RewriteArguments.context.ToTransientTransaction())
				{
					r.AtILOverride +=
						(m, il_a) =>
						{
							if (m.DeclaringType != TemplateType)
								return;

							il_a[OpCodes.Ldstr] =
								(e) =>
								{
									if (e.i.TargetLiteral != NamedXDocument.DefaultSource)
									{
										e.Default();
										return;
									}

									e.il.Emit(OpCodes.Ldstr, AssetPath);
									return;
								};
						};



					r.RewriteArguments.context.TypeRenameCache[TemplateType] = TypeFullName;



					//var MyType = r.RewriteArguments.context.TypeCache[TemplateType];

					__Create = r.RewriteArguments.context.MethodCache[
						((Action<Action<XElement>>)NamedXDocument.CreateAsElement).Method
					];

					TemplateType = null;

				}

				return __Create;
			}


		}


	}

	namespace Templates
	{
		public class NamedXDocument
		{
			internal const string DefaultSource = "DefaultSource";

			// do we emit any new members?
			// maybe some metadata?

			public XDocument Document { get; set; }

			public object Tag { get; set; }


			// this is explicit to javascript in this implementation
			public static void CreateAsElement(Action<XElement> done)
			{
				Create(
					 r =>
					 {
						 done(r.Document.Root);
					 }
				);
			}

			public static void Create(Action<NamedXDocument> done)
			{
				DefaultSource.DownloadToXDocument(
					Document =>
					{
						var x = new NamedXDocument
						{
							Document = Document,
						};


						done(x);
					}
				);
			}


		}


	}


}
