using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using System.Xml.XPath;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.meta.Library.Templates;
using jsc.meta.Library.Templates.Avalon;
using jsc.meta.Library.Templates.JavaScript;
using jsc.Script;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Ultra.Library.Extensions;
using jsc.meta.Library.Templates.JavaScript.Named;
using jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins;
using ScriptCoreLib.Extensions;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	partial class ReferenceUltraSource
	{
		public class TypeVariationsTuple
		{
			public Type Type;
			public string Source;
		}

		public class TypeVariations
		{
			public string FromAssetsSource;
			public Type FromAssets;

			public string FromWebSource;
			public Type FromWeb;
			public bool FromWebNotAvailable;

			public string FromBase64Source;
			public Type FromBase64;
			public bool FromBase64NotAvailable;

			public void AssingDefaults()
			{
				var Variations = this;
				if (Variations.FromBase64 == null)
				{
					Variations.FromBase64NotAvailable = true;
					Variations.FromBase64 = Variations.FromAssets;
					Variations.FromBase64Source = Variations.FromAssetsSource;
				}

				if (Variations.FromWeb == null)
				{
					Variations.FromWebNotAvailable = true;
					Variations.FromWeb = Variations.FromAssets;
					Variations.FromWebSource = Variations.FromAssetsSource;
				}
			}
		}

		public class DefineNamedElements
		{
			public string DefaultNamespace;
			public RewriteToAssembly.AssemblyRewriteArguments a;
			public XElement BodyElement;
			public RewriteToAssembly r;
			public Func<string, FileInfo> GetLocalResource;


			public Dictionary<string, TypeVariations> TypeVariations;
			public Dictionary<string, TypeVariations> RemotingTypeVariations;

			public string PageName;
			public Dictionary<string, Type> ElementTypes;


			public void Define()
			{
				if (GetLocalResource == null)
					GetLocalResource = e => null;


				var ElementsWithSource = new [] 
				{
					BodyElement.XPathSelectElements(".//img[@src]"),
					BodyElement.XPathSelectElements(".//audio[@src]")
				}.SelectMany(k => k).ToArray();

				foreach (var CurrentElement in ElementsWithSource)
				{
					var ElementType = ElementTypes[CurrentElement.Name.LocalName];
					
					var Namespace = default(string);

					if (ElementType == typeof(IHTMLImage))
					{
						Namespace = "Images";
					}

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

					var name0 = name1.Replace("\\", "/").SkipUntilLastIfAny("/").TakeUntilLastIfAny("?").TakeUntilLastIfAny(".");
					var name = CompilerBase.GetSafeLiteral(name0, null);


					// should we add suffix? Use argument?
					//name += "Image";

					// if its not from the web, only in our project then this type cannot be made available can it.

					var src_value = src.Value.TakeUntilIfAny("?");
					if (src_value.StartsWith("//"))
						src_value = "http:" + src_value;

					if (TypeVariations.ContainsKey(src_value))
						continue;

					var LocalResource = GetLocalResource(src_value);
					var __WebClient = default(WebClient);

					var Resource = default(byte[]);


					if (LocalResource == null)
					{
						if (src_value.StartsWith("http://") || src_value.StartsWith("https://"))
						{
							Console.WriteLine("Downloading: " + src_value);
							Resource = (__WebClient = new WebClient()).DownloadData(new Uri(src_value));
						}
						else
						{
							throw new InvalidOperationException(
								"Referenced asset not found in the project and not linked to http:// uri."
							);
						}
					}
					else
						Resource = File.ReadAllBytes(LocalResource.FullName);


					var Extension = default(string);

					if (__WebClient == null)
						Extension = "." + src.Value.SkipUntilLastIfAny(".");
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
					var RemotingVariations = new TypeVariations();


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
							Variations.FromWebSource = src.Value; 
							DefineNamedElement(ElementType, Namespace, name, Variations, RemotingVariations, src.Value, "FromWeb");
						}

						//var AssetPath = "assets/" + DefaultNamespace + "/UltraSource/FromAssets/" + name + Extension;
						// Long paths are not good. ASP.NET will fault.
						Variations.FromAssetsSource = "assets/" + DefaultNamespace + "/" + name + Extension;

						a.ScriptResourceWriter.Add(Variations.FromAssetsSource, Resource);



						DefineNamedElement(ElementType, Namespace, name, Variations, RemotingVariations, Variations.FromAssetsSource, "FromAssets");

						if (ElementType == typeof(IHTMLImage))
						{
							DefineAvalonNamedImage(a, r,
								DefaultNamespace + ".Avalon.Images." + name, Variations.FromAssetsSource, null
							);
						}

						// lets define 

						// src="data:image/gif;base64,R0lGODlhDwAPAKECAAAAzMzM

						#region ResourceBase64
						if (ElementType == typeof(IHTMLImage))
						{

							var ResourceBase64 = Convert.ToBase64String(Resource);

							Variations.FromBase64Source = "data:" + EmbedMimeTypes.Resolve(Extension) + ";base64," + ResourceBase64;
							

							{
								var Base64Lookup = a.Module.DefineType(DefaultNamespace + ".Data.Base64Lookup._" + Resource.ToMD5Bytes().ToHexString(), TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Abstract, null);


								var m = Base64Lookup.DefineMethod("GetSource", MethodAttributes.Public | MethodAttributes.Static, typeof(string), null);

								m.GetILGenerator().Apply(
									il =>
									{
										il.Emit(OpCodes.Ldstr, Variations.FromBase64Source);
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

								RemotingVariations.FromBase64 = DefineRemotingNamedImage(a, r,
									DefaultNamespace + ".HTML.Images.FromBase64.Remoting." + name,
									null,
									m
								);
							}

						}
						#endregion


						Variations.AssingDefaults();
						RemotingVariations.AssingDefaults();

						TypeVariations.Add(src_value, Variations);
						RemotingTypeVariations.Add(src_value, RemotingVariations);



						TemplateType = null;

					}
				}
			}

			private void DefineNamedElement(Type ElementType, string Namespace, string name,
				TypeVariations Variations,
				TypeVariations RemotingVariations, 
				
				string AssetPath, string VariationName)
			{
				if (ElementType == typeof(IHTMLImage))
				{
					Variations.FromAssets = DefineNamedImage(a, r,
						DefaultNamespace + ".HTML." + Namespace + "." + VariationName + "." + name,
						AssetPath,
						null
					);

					// From Assets - would that be from the web server assets?

					//RemotingVariations.FromAssets = DefineRemotingNamedImage(a, r,
					//    DefaultNamespace + ".HTML." + Namespace + "." + VariationName + ".Remoting." + name,
					//    AssetPath,
					//    null
					//);
				}
				else
				{
					// sure it's audio?
					Variations.FromAssets = DefineNamedAudio(a, r,
						DefaultNamespace + ".HTML." + Namespace + "." + VariationName + "." + name,
						AssetPath
					);
				}
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

		public static Type DefineAvalonNamedImage(
			RewriteToAssembly.AssemblyRewriteArguments a,
			RewriteToAssembly r,
			string ImageFullName,
			string ImageSource,
			MethodInfo get_ImageSource
			)
		{
			var TemplateType = typeof(AvalonNamedImage);

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
								if (e.i.TargetLiteral != AvalonNamedImage._src)
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
