using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Windows.Media.Imaging;
using System.Reflection;
using jsc.meta.Commands.Rewrite;
using System.Windows.Media;
using System.IO;
using System.Windows;
using jsc.meta.Library.Templates.Avalon;
using System.Windows.Controls;
using jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins.Templates;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource.Plugins
{
    [Description("Square images will be split half and two triangles shall be provided for MatrixTransform.")]
    public class AffineTriangles
    {
        public string DefaultNamespace;
        public TypeBuilder DeclaringType;
        public BitmapSource Bitmap;
        public RewriteToAssembly r;

        public void Define()
        {


            if (Bitmap.Format.BitsPerPixel != 32)
                return;

            var PixelWidth = Bitmap.PixelWidth;

            // what if we are reading from a different BP
            var BitmapStride = (PixelWidth * Bitmap.Format.BitsPerPixel + 7) / 8;

            var BitmapPixels_Original = new byte[BitmapStride * Bitmap.PixelHeight];

            var Pixels1 = new byte[BitmapStride * Bitmap.PixelHeight];
            var Pixels2 = new byte[BitmapStride * Bitmap.PixelHeight];

            Bitmap.CopyPixels(BitmapPixels_Original, BitmapStride, 0);

            // 1. Apply triangle mask

            for (int iy = 0; iy < PixelWidth; iy++)
                for (int ix = 0; ix < PixelWidth; ix++)
                {
                    var PixelDestination = BitmapStride * iy + ix * 4;
                    var PixelSource = BitmapStride * ((PixelWidth - 1) - iy) + ((PixelWidth - 1) - ix) * 4;

                    if (iy <= (PixelWidth - ix))
                    {
    
                        Pixels1[PixelDestination + 0] = BitmapPixels_Original[PixelDestination + 0];
                        Pixels1[PixelDestination + 1] = BitmapPixels_Original[PixelDestination + 1];
                        Pixels1[PixelDestination + 2] = BitmapPixels_Original[PixelDestination + 2];
                        Pixels1[PixelDestination + 3] = BitmapPixels_Original[PixelDestination + 3];

                    }

                    if (iy < (PixelWidth - ix))
                    {
                        Pixels2[PixelDestination + 0] = BitmapPixels_Original[PixelSource + 0];
                        Pixels2[PixelDestination + 1] = BitmapPixels_Original[PixelSource + 1];
                        Pixels2[PixelDestination + 2] = BitmapPixels_Original[PixelSource + 2];
                        Pixels2[PixelDestination + 3] = BitmapPixels_Original[PixelSource + 3];
                    }
                }

            Action<string, byte[]> AddAsset =
                (AssetPath, Pixels) =>
                {
                    // http://www.vistax64.com/avalon/103701-using-wpf-generate-image.html
                    var PixelsBitmap = new WriteableBitmap(
                        Bitmap.PixelWidth,
                        Bitmap.PixelHeight,
                        96,
                        96,
                        PixelFormats.Bgra32,
                        null
                    );

                    PixelsBitmap.WritePixels(
                        new Int32Rect(0, 0, Bitmap.PixelWidth, Bitmap.PixelHeight)
                        , Pixels, BitmapStride, 0
                    );

                    var Resource = new MemoryStream();
                    var png = new PngBitmapEncoder();

                    
                    png.Frames.Add(BitmapFrame.Create(PixelsBitmap));
                    png.Save(Resource);


                    
                    r.RewriteArguments.ScriptResourceWriter.Add(AssetPath, Resource.ToArray());
                };

            var AssetPath1 = "assets/" + DefaultNamespace + "/AffineTriangles/1/" + DeclaringType.Name + ".png";
            AddAsset(AssetPath1, Pixels1);

            var AssetPath2 = "assets/" + DefaultNamespace + "/AffineTriangles/2/" + DeclaringType.Name + ".png";
            AddAsset(AssetPath2, Pixels2);

            DefineNestedType("AffineTriangle1", AssetPath1, PixelWidth);
            DefineNestedType("AffineTriangle2", AssetPath2, PixelWidth);




        }

        void DefineNestedType(string Name, string AssetPath, double PixelWidth)
        {
            var TemplateType = typeof(NestedAffineTriangleImage);


            using (r.RewriteArguments.context.ToTransientTransaction())
            {
                r.AtILOverride +=
                    (m, il_a) =>
                    {
                        if (m.DeclaringType != TemplateType)
                            return;

                        il_a[OpCodes.Ldstr] = e => e.il.Emit(OpCodes.Ldstr, AssetPath);
                        il_a[OpCodes.Ldc_R8] = e => e.il.Emit(OpCodes.Ldc_R8, PixelWidth);
                    };

                r.RewriteArguments.context.OverrideDeclaringType[TemplateType] = DeclaringType;
                r.RewriteArguments.context.TypeRenameCache[TemplateType] = Name;


                var MyType = r.RewriteArguments.context.TypeCache[TemplateType];

                TemplateType = null;
            }
        }
    }

    namespace Templates
    {
        public class NestedAffineTriangleImage : Image
        {
            public NestedAffineTriangleImage()
            {
                var Source = global::ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions.ToSource("");

                this.Source = Source;

                this.Width = 0;
                this.Height = 0;
            }
        }
    }
}
