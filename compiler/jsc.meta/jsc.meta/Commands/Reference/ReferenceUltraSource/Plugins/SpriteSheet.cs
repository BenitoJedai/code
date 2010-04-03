using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using jsc.meta.Commands.Rewrite;
using System.ComponentModel;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using jsc.meta.Library.Templates;
using System.Reflection.Emit;
using System.Windows;
using ScriptCoreLib.Shared.Lambda;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins
{
	[Description("Convert images into spritesheet items.")]
	public class SpriteSheet
	{
		/* usage:
		     <script type="UltraSource.SpriteSheet"  ></script>
   

		 */
		public string DefaultNamespace;
		public XElement BodyElement;
		public RewriteToAssembly r;
		public Func<string, FileInfo> GetLocalResource;

		public void Define()
		{
			var a = r.RewriteArguments;
			var q = from s in BodyElement.XPathSelectElements("//script[@type='UltraSource.SpriteSheet']")

					let ContextStyle =
						from css in s.Parent.Attribute("style").Value.Split(';')
						let key = css.Split(':').First().Trim()
						let value = css.Split(':').Last().Trim()
						select new { key, value }


					let Images = BodyElement.XPathSelectElements("//img").Where(k => k.Parent == s.Parent)

					let BackgroundSource = GetLocalResource(ContextStyle.Single(k => k.key == "background-image").value.SkipUntilIfAny("(").TakeUntilIfAny(")"))
					let BackgroundBitmap = ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions.ToImageSource(BackgroundSource.Extension, new MemoryStream(File.ReadAllBytes(BackgroundSource.FullName)))

					from SpriteInfo in Images

					let SpriteId = SpriteInfo.Attribute("id").Value
					let SpriteSource = GetLocalResource(SpriteInfo.Attribute("src").Value)
					let SpriteBitmap = ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions.ToImageSource(SpriteSource.Extension, new MemoryStream(File.ReadAllBytes(SpriteSource.FullName)))



					let SpriteStyle =
						from css in SpriteInfo.Attribute("style").Value.Split(';')
						let key = css.Split(':').First().Trim()
						let value = css.Split(':').Last().Trim()
						select new { key, value }

					let SpriteStyleGetPixelOrDefault =
						new { key = "", value = "px" }
						.ToAnonymousContainer()
						.ToFunc(k => Convert.ToInt32(k.value.TakeUntilIfAny("px").Trim()))
						.FirstParameter((string key) => SpriteStyle.SingleOrDefault(k => k.key == key))

					let SpriteStyleType = new
					{
						left = SpriteStyleGetPixelOrDefault("left", 0),
						top = SpriteStyleGetPixelOrDefault("top", 0),
						width = SpriteStyleGetPixelOrDefault("width", (int)SpriteBitmap.Width),
						height = SpriteStyleGetPixelOrDefault("height", (int)SpriteBitmap.Height),
					}




					let CroppedBitmap = new CroppedBitmap(BackgroundBitmap,
						new Int32Rect(
							SpriteStyleType.left,
							SpriteStyleType.top,
							SpriteStyleType.width,
							SpriteStyleType.height
						)
					)



					select new
					{
						SpriteStyleType,
						SpriteSource,
						SpriteId,
						SpriteBitmap,
						CroppedBitmap,
						BackgroundSource,
						BackgroundBitmap,
					};

			foreach (var item in q)
			{

				// http://www.vistax64.com/avalon/103701-using-wpf-generate-image.html
				var rtb = new WriteableBitmap(
					item.CroppedBitmap.PixelWidth,
					item.CroppedBitmap.PixelHeight,
					96,
					96,
					PixelFormats.Pbgra32,
					null
				);


				if (item.CroppedBitmap.Format.BitsPerPixel != 32)
					throw new NotImplementedException();

				// what if we are reading from a different BP
				var CroppedStride = (item.CroppedBitmap.PixelWidth * item.CroppedBitmap.Format.BitsPerPixel + 7) / 8;
				var CroppedPixels = new byte[CroppedStride * item.CroppedBitmap.PixelHeight];

				item.CroppedBitmap.CopyPixels(CroppedPixels, CroppedStride, 0);


				var IsResized =
					item.SpriteBitmap.Width != item.CroppedBitmap.Width ||
					item.SpriteBitmap.Height != item.CroppedBitmap.Height;

				if (IsResized)
				{
					// ? our mask has been transformed, bail
				}
				else
				{
					var SpriteStride = (item.SpriteBitmap.PixelWidth * item.SpriteBitmap.Format.BitsPerPixel + 7) / 8;
					var SpritePixels = new byte[SpriteStride * item.SpriteBitmap.PixelHeight];

					item.SpriteBitmap.CopyPixels(SpritePixels, SpriteStride, 0);

					for (int i = 3; i < SpritePixels.Length; i += 4)
					{
						if (SpritePixels[i] == 0)
							CroppedPixels[i] = 0;
					}
				}

				rtb.WritePixels(
					new Int32Rect(0, 0, item.CroppedBitmap.PixelWidth, item.CroppedBitmap.PixelHeight)
					, CroppedPixels, CroppedStride, 0
				);

				var Resource = new MemoryStream();
				var png = new PngBitmapEncoder();

				png.Frames.Add(BitmapFrame.Create(rtb));
				png.Save(Resource);

				var AssetPath = "assets/" + DefaultNamespace + "/SpriteSheet/" + item.SpriteId + ".png";

				r.RewriteArguments.ScriptResourceWriter.Add(AssetPath, Resource.ToArray());



				using (a.context.ToTransientTransaction())
				{
					#region NamedImageInformation
					var TemplateType = typeof(NamedImageInformation);


					r.AtILOverride +=
						(m, il_a) =>
						{
							if (m.DeclaringType != TemplateType)
								return;

							il_a.BeforeInstructions =
								e =>
								{
									if (e.SourceMethod == ((Func<int>)NamedImageInformation.GetImageDefaultWidth).Method)
									{
										e.il.Emit(OpCodes.Ldc_I4, (int)item.SpriteStyleType.width);
										e.il.Emit(OpCodes.Ret);
										e.Complete();
										return;
									}

									if (e.SourceMethod == ((Func<int>)NamedImageInformation.GetImageDefaultHeight).Method)
									{
										e.il.Emit(OpCodes.Ldc_I4, (int)item.SpriteStyleType.height);
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


					#endregion

					a.context.TypeRenameCache[TemplateType] =
						DefaultNamespace + ".Data.Images.SpriteSheet." + item.SpriteId;

					var MyType = a.context.TypeCache[TemplateType];

					ReferenceUltraSource.DefineNamedImage(r.RewriteArguments, r,
						DefaultNamespace + ".HTML.Images.SpriteSheet.FromAssets." + item.SpriteId,
						AssetPath, null
					);

					ReferenceUltraSource.DefineAvalonNamedImage(
						a,
						r,
						DefaultNamespace + ".Avalon.Images.SpriteSheet." + item.SpriteId,
						AssetPath
						, null
					);
				}
			}
		}

	}
}
