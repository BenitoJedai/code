using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using jsc.meta.Library;
using System.Reflection.Emit;
using System.Reflection;
using jsc.meta.Commands.Rewrite;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using System.Windows.Media.Imaging;
using jsc.meta.Library.Templates.JavaScript;
using ScriptCoreLib.Ultra.Library.Extensions;
using jsc.Script;
using System.Net;
using ScriptCoreLib.ActionScript;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	partial class ReferenceUltraSource
	{
		public class TypeVariations
		{
			public Type FromAssets;

			public Type FromWeb;
			public bool FromWebNotAvailable;

			public Type FromBase64;
			public bool FromBase64NotAvailable;
		}

		public class DefineNamedElements
		{
			public string DefaultNamespace;
			public RewriteToAssembly.AssemblyRewriteArguments a;
			public XElement BodyElement;
			public RewriteToAssembly r;
			public Func<string, FileInfo> GetLocalResource;


			public Dictionary<string, TypeVariations> TypeVariations;

			public string PageName;
			public Dictionary<string, Type> ElementTypes;


			public void Define()
			{
				if (GetLocalResource == null)
					GetLocalResource = e => null;


				var Images_value = BodyElement.XPathSelectElements("//img")
					.Concat(
					BodyElement.XPathSelectElements("//audio")
					)

				.ToArray();

				foreach (var CurrentElement in Images_value)
				{
					var ElementType = ElementTypes[CurrentElement.Name.LocalName];
					var Namespace = "Images";

					if (ElementType == typeof(IHTMLAudio))
						Namespace = "Audio";

					var src = CurrentElement.Attribute("src");
					var alt = CurrentElement.Attribute("alt");

					// it must be just a placeholder
					if (src == null || string.IsNullOrEmpty(src.Value))
						continue;

					// we are working with uri's here!
					// todo: we might need to infer file extension from http content type instead!

					var name1 = src.Value;

					if (alt != null && !string.IsNullOrEmpty(alt.Value))
						name1 = alt.Value.TakeUntilLastIfAny(".");

					var name0 = name1.Replace("\\", "/").SkipUntilLastIfAny("/").TakeUntilIfAny(".");
					var name = CompilerBase.GetSafeLiteral(name0, null);


					// should we add suffix? Use argument?
					//name += "Image";

					// if its not from the web, only in our project then this type cannot be made available can it.

					var src_value = src.Value;
					if (src_value.StartsWith("//"))
						src_value = "http:" + src_value;

					if (TypeVariations.ContainsKey(src_value))
						continue;

					var LocalResource = GetLocalResource(src_value);
					var __WebClient = default(WebClient);

					var Resource = default(byte[]);


					if (LocalResource == null)
					{
						Console.WriteLine("Downloading: " + src_value);
						Resource = (__WebClient = new WebClient()).DownloadData(new Uri(src_value));
					}
					else
						Resource = File.ReadAllBytes(LocalResource.FullName);


					var Extension = default(string);

					if (__WebClient == null)
						Extension = "." +src.Value.SkipUntilLastIfAny(".");
					else
					{
						var ContentType = __WebClient.ResponseHeaders[HttpResponseHeader.ContentType].TakeUntilIfAny(";");

						if (EmbedMimeTypes.lookup.ContainsValue(ContentType))
						{
							Extension = EmbedMimeTypes.lookup.FirstOrDefault(
								k => k.Value == ContentType
							).Key;
						}
						else
						{
							// if the server does not provide a meaningful content type we have to rely on alt attribute
							Extension = "." + alt.Value.SkipUntilLastIfAny(".");
						}
					}


					var Bitmap = default(BitmapSource);

					if (ElementType == typeof(IHTMLImage))
						Bitmap = ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions.ToImageSource(Extension, new MemoryStream(Resource));


					var Variations = new TypeVariations();


					using (a.context.ToTransientTransaction())
					{
						var TemplateType = typeof(NamedImageInformation);

						if (ElementType == typeof(IHTMLAudio))
							TemplateType = typeof(NamedAudioInformation);

						r.AtILOverride +=
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

										if (e.SourceMethod == ((Func<int>)NamedAudioInformation.GetAudioFileSize).Method)
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
									// ?
									DefaultNamespace + ".Data." + Namespace + "." + name;
							};

						var MyType = a.context.TypeCache[TemplateType];

						if (LocalResource == null)
						{
							DefineNamedElement(ElementType, Namespace, name, Variations, src.Value, "FromWeb");
						}

						var AssetPath = "assets/" + DefaultNamespace + "/UltraSource/FromAssets/" + name + Extension;

						a.ScriptResourceWriter.Add(AssetPath, Resource);


						DefineNamedElement(ElementType, Namespace, name, Variations, AssetPath, "FromAssets");

						// src="data:image/gif;base64,R0lGODlhDwAPAKECAAAAzMzM

						#region ResourceBase64
						if (ElementType == typeof(IHTMLImage))
						{

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
									m
								);
							}

						}
						#endregion


						if (Variations.FromBase64 == null)
						{
							Variations.FromBase64NotAvailable = true;
							Variations.FromBase64 = Variations.FromAssets;
						}

						if (Variations.FromWeb == null)
						{
							Variations.FromWebNotAvailable = true;
							Variations.FromWeb = Variations.FromAssets;
						}


						TypeVariations.Add(src_value, Variations);



						TemplateType = null;

					}
				}
			}

			private void DefineNamedElement(Type ElementType, string Namespace, string name, TypeVariations Variations, string AssetPath, string VariationName)
			{
				if (ElementType == typeof(IHTMLImage))
				{
					Variations.FromAssets = DefineNamedImage(a, r,
						DefaultNamespace + ".HTML." + Namespace + "." + VariationName + "." + name,
						AssetPath,
						null

					);
				}
				else
				{
					Variations.FromAssets = DefineNamedAudio(a, r,
						DefaultNamespace + ".HTML." + Namespace + "." + VariationName + "." + name,
						AssetPath
					);
				}
			}
		}

		private static Type DefineNamedImage(
			RewriteToAssembly.AssemblyRewriteArguments a,
			RewriteToAssembly r,
			string ImageFullName,
			string ImageSource,
			MethodInfo get_ImageSource)
		{
			var TemplateType = typeof(NamedImage);

			using (a.context.ToTransientTransaction())
			{
				r.AtILOverride +=
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


		private static Type DefineNamedAudio(
			RewriteToAssembly.AssemblyRewriteArguments a,
			RewriteToAssembly r,
			string ImageFullName,
			string ImageSource)
		{
			var TemplateType = typeof(NamedAudio);

			using (a.context.ToTransientTransaction())
			{
				r.AtILOverride +=
					(m, il_a) =>
					{
						if (m.DeclaringType != TemplateType)
							return;

						il_a[OpCodes.Ldstr] =
							(e) =>
							{
								if (e.i.TargetLiteral != NamedAudio.IHTMLAudio_src)
								{
									e.Default();
									return;
								}


								e.il.Emit(OpCodes.Ldstr, ImageSource);
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
