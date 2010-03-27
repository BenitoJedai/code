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

namespace jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins
{
	[Description("Convert images into spritesheet items.")]
	public class SpriteSheet
	{
		/* usage:
		     <script type="UltraSource.SpriteSheet" for='SpriteSheet' ></script>
   

		 */
		public string DefaultNamespace;
		public XElement BodyElement;
		public RewriteToAssembly r;
		public Func<string, FileInfo> GetLocalResource;

		static void DrawPixel(WriteableBitmap w, int X, int Y)
		{
			int column = (int)X;
			int row = (int)Y;

			// Reserve the back buffer for updates.
			w.Lock();

			unsafe
			{
				// Get a pointer to the back buffer.
				int pBackBuffer = (int)w.BackBuffer;

				// Find the address of the pixel to draw.
				pBackBuffer += row * w.BackBufferStride;
				pBackBuffer += column * 4;

				// Compute the pixel's color.
				int color_data = 255 << 16; // R
				color_data |= 128 << 8;   // G
				color_data |= 255 << 0;   // B

				// Assign the color data to the pixel.
				*((int*)pBackBuffer) = color_data;
			}

			// Specify the area of the bitmap that changed.
			w.AddDirtyRect(new Int32Rect(column, row, 1, 1));

			// Release the back buffer and make it available for display.
			w.Unlock();
		}

		static void ErasePixel(WriteableBitmap w, int X, int Y)
		{
			byte[] ColorData = { 0xFF, 0, 0, 0xFF }; // B G R A

			var rect = new Int32Rect(
					(int)(X),
					(int)(Y),
					1,
					1);

			w.WritePixels(rect, ColorData, 4, 0);
		}

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

					let SpriteLeft = Convert.ToInt32(SpriteStyle.Single(k => k.key == "left").value.TakeUntilIfAny("px").Trim())
					let SpriteTop = Convert.ToInt32(SpriteStyle.Single(k => k.key == "top").value.TakeUntilIfAny("px").Trim())

					let CroppedBitmap = new CroppedBitmap(BackgroundBitmap, new Int32Rect(SpriteLeft, SpriteTop, SpriteBitmap.PixelWidth, SpriteBitmap.PixelHeight))


					select new
					{
						SpriteLeft,
						SpriteTop,
						SpriteSource,
						SpriteId,
						SpriteBitmap,
						CroppedBitmap,
						BackgroundSource,
						BackgroundBitmap,
					};

			foreach (var item in q)
			{
				var Bitmap = item.SpriteBitmap;

				// http://www.vistax64.com/avalon/103701-using-wpf-generate-image.html
				var rtb = new WriteableBitmap(
					item.SpriteBitmap.PixelWidth,
					item.SpriteBitmap.PixelHeight,
					96,
					96,
					PixelFormats.Pbgra32,
					null
				);




				var CroppedStride = (item.CroppedBitmap.PixelWidth * item.CroppedBitmap.Format.BitsPerPixel + 7) / 8;
				var CroppedPixels = new byte[CroppedStride * item.CroppedBitmap.PixelHeight];

				item.CroppedBitmap.CopyPixels(CroppedPixels, CroppedStride, 0);

				var SpriteStride = (item.SpriteBitmap.PixelWidth * item.SpriteBitmap.Format.BitsPerPixel + 7) / 8;
				var SpritePixels = new byte[SpriteStride * item.SpriteBitmap.PixelHeight];

				item.SpriteBitmap.CopyPixels(SpritePixels, SpriteStride, 0);

				for (int i = 3; i < SpritePixels.Length; i += 4)
				{
					if (SpritePixels[i] == 0)
						CroppedPixels[i] = 0;
				}

				rtb.WritePixels(new Int32Rect(0, 0, item.CroppedBitmap.PixelWidth, item.CroppedBitmap.PixelHeight)
					, CroppedPixels, CroppedStride, 0);

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
