using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Markup;
using System.IO;
using System.Xml;

namespace ScriptCoreLib.Desktop.Avalon.Extensions
{
    public static class ControlExtensions
    {
        // see also: http://www.dreamincode.net/code/snippet4326.htm
        // see also: http://denisvuyka.wordpress.com/2007/12/03/wpf-diagramming-saving-you-canvas-to-image-xps-document-or-raw-xaml/
        // see also: http://blogs.msdn.com/b/jaimer/archive/2009/07/03/rendertargetbitmap-tips.aspx



        public static void RenderVisualTo(this Visual target, string filePath, double resolution = 96)
        {

            using (FileStream outStream = new FileStream(filePath, FileMode.Create))
            {
                PngBitmapEncoder enc = new PngBitmapEncoder();

                Rect xbounds = VisualTreeHelper.GetDescendantBounds(target);
                Rect bounds = VisualTreeHelper.GetDescendantBounds(target);

                if (target is Control)
                {
                    bounds.Width = (target as Control).ActualWidth;
                    bounds.Height = (target as Control).ActualHeight;
                }

                RenderTargetBitmap rtb = new RenderTargetBitmap(
                    (Int32)(bounds.Width * resolution / 96.0),
                    (Int32)(bounds.Height * resolution / 96.0),
                    resolution,
                    resolution, PixelFormats.Pbgra32
                );

                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(target);
                    dc.DrawRectangle(
                        vb, 
                        null,
                        new Rect(new Point(), xbounds.Size));
                }
                rtb.Render(dv);

                BitmapFrame bitmapFrame = BitmapFrame.Create(rtb);
                enc.Frames.Add(bitmapFrame);
                enc.Save(outStream);
            }
        }
    }
}
