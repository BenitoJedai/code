using System;
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
using jsc.meta.Library.Templates.JavaScript;
using jsc.meta.Tools;
using jsc.Script;
using Microsoft.CSharp;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Windows.Media.Imaging;

namespace jsc.meta.Commands.Reference
{

	partial class ReferenceJavaScriptDocument
	{
		public class TypeVariations
		{
			public Type FromWeb;
			public Type FromBase64;
			public Type FromAssets;
		}

		private static void DefineNamedImages(
			string DefaultNamespace,
			RewriteToAssembly.AssemblyRewriteArguments a,
			XElement BodyElement,
			RewriteToAssembly r,
			Func<string, FileInfo> GetLocalResource,

			Action<string, TypeVariations> AddTypeVariations

			)
		{
			var Images_value = BodyElement.XPathSelectElements("//img").ToArray();
			foreach (var CurrentElement in Images_value)
			{
				var src = CurrentElement.Attribute("src");
				var alt = CurrentElement.Attribute("alt");

				var name0 = (alt ?? src).Value.Replace("\\", "/").SkipUntilIfAny("/").TakeUntilIfAny(".");
				var name = CompilerBase.GetSafeLiteral(name0, null);

				// should we add suffix? Use argument?
				//name += "Image";

				// if its not from the web, only in our project then this type cannot be made available can it.

				var LocalResource = GetLocalResource(src.Value);
				var Resource = LocalResource == null ? new WebClient().DownloadData(src.Value) : File.ReadAllBytes(LocalResource.FullName);
				var Extension = src.Value.Substring(src.Value.LastIndexOf("."));
				var Bitmap = ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions.ToImageSource(Extension, new MemoryStream(Resource));


				var Variations = new TypeVariations();


				using (a.context.ToTransientTransaction())
				{
					var TemplateType = typeof(NamedImageInformation);

					r.ILOverride +=
						(m, il_a) =>
						{
							if (m.DeclaringType != TemplateType)
								return;

							il_a.BeforeInstructions =
								e =>
								{
									if (e.SourceMethod == ((Func<int>)NamedImageInformation.GetImageDefaultHeight).Method)
									{
										e.il.Emit(OpCodes.Ldc_I4, (int)Bitmap.Height);
										e.il.Emit(OpCodes.Ret);
										e.Complete();
										return;
									}

									if (e.SourceMethod == ((Func<int>)NamedImageInformation.GetImageDefaultWidth).Method)
									{
										e.il.Emit(OpCodes.Ldc_I4, (int)Bitmap.Width);
										e.il.Emit(OpCodes.Ret);
										e.Complete();
										return;
									}

									if (e.SourceMethod == ((Func<int>)NamedImageInformation.GetImageFileSize).Method)
									{
										e.il.Emit(OpCodes.Ldc_I4, (int)Resource.Length);
										e.il.Emit(OpCodes.Ret);
										e.Complete();
										return;
									}
								};

						};


					a.context.TypeRenameCache.Resolve +=
						SourceType =>
						{
							if (SourceType != TemplateType)
								return;

							a.context.TypeRenameCache[SourceType] =
								DefaultNamespace + ".Data.Images." + name;
						};

					var MyType = a.context.TypeCache[TemplateType];

					if (LocalResource == null)
					{
						Variations.FromWeb = DefineNamedImage(a, r,
							DefaultNamespace + ".HTML.Images.FromWeb." + name,
							src.Value,
							null,
							Bitmap
						);
					}


					Variations.FromAssets = DefineNamedImage(a, r,
						DefaultNamespace + ".HTML.Images.FromAssets." + name,
						"assets/namespace/" + src.Value,
						null,
							Bitmap
					);

					// src="data:image/gif;base64,R0lGODlhDwAPAKECAAAAzMzM


					var ResourceBase64 = Convert.ToBase64String(Resource);

					var Source = "data:" + EmbedMimeTypes.Resolve(Extension) + ";base64," + ResourceBase64;

					{
						var Base64Lookup = a.Module.DefineType(DefaultNamespace + ".Data.Base64Lookup." + Resource.ToMD5Bytes().ToHexString(), TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Abstract, null);


						var m = Base64Lookup.DefineMethod("GetSource", MethodAttributes.Public | MethodAttributes.Static, typeof(string), null);

						m.GetILGenerator().Apply(
							il =>
							{
								il.Emit(OpCodes.Ldstr, Source);
								il.Emit(OpCodes.Ret);
							}
						);


						//Base64LookupCache[new Uri(uri)] = m;

						Base64Lookup.CreateType();



						Variations.FromBase64 = DefineNamedImage(a, r,
							DefaultNamespace + ".HTML.Images.FromBase64." + name,
							null,
							m,
							Bitmap
						);
					}


					AddTypeVariations(src.Value, Variations);



					TemplateType = null;

				}
			}
		}

		private static Type DefineNamedImage(
			RewriteToAssembly.AssemblyRewriteArguments a,
			RewriteToAssembly r,
			string ImageFullName,
			string ImageSource,
			MethodInfo get_ImageSource,
			BitmapSource Bitmap)
		{
			var TemplateType = typeof(NamedImage);

			using (a.context.ToTransientTransaction())
			{
				r.ILOverride +=
					(m, il_a) =>
					{
						if (m.DeclaringType != TemplateType)
							return;

						il_a[OpCodes.Ldstr] =
							(e) =>
							{
								if (e.i.TargetLiteral != NamedImage.IHTMLImage_src)
								{
									e.Default();
									return;
								}

								if (ImageSource != null)
								{
									e.il.Emit(OpCodes.Ldstr, ImageSource);
									return;
								}

								e.il.Emit(OpCodes.Call, get_ImageSource);
							};
					};

				a.context.TypeRenameCache.Resolve +=
					SourceType =>
					{
						if (SourceType != TemplateType)
							return;

						a.context.TypeRenameCache[SourceType] =
							ImageFullName;
					};

				var MyType = a.context.TypeCache[TemplateType];

				TemplateType = null;

				return MyType;
			}
		}



	}
}
