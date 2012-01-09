using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Media.Imaging;

namespace WebGLSpadeSpy.StudyViaAvalon
{
    using uint8 = Byte;
    using uint32 = UInt32;

    public unsafe class ApplicationCanvas : Canvas
    {
        unsafe delegate void __load_map(uint8* v, int len);

        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            r.Fill = Brushes.Red;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            var vxl = (@"Z:\media\Program Files\Ace of Spades\vxl\normandie.vxl");
            //var vxl = (@"Z:\media\Program Files\Ace of Spades\vxl\urbantankfightfinal.vxl");

            var _geom = new int[512, 512, 64];
            var _color = new uint[512, 512, 64];

            Action<int, int, int, int> setgeom =
                (x, y, z, k) =>
                {
                    _geom[x, y, z] = k;
                };

            Action<int, int, int, uint> setcolor =
                (x, y, z, k) =>
                {
                    _color[x, y, z] = k;
                };

            // http://silverspaceship.com/aosmap/aos_file_format.html
            #region load_map
            __load_map load_map = (v, len) =>
            {
                uint8* @base = v;
                int x, y, z;
                for (y = 0; y < 512; ++y)
                {
                    for (x = 0; x < 512; ++x)
                    {
                        for (z = 0; z < 64; ++z)
                        {
                            setgeom(x, y, z, 1);
                        }
                        z = 0;
                        for (; ; )
                        {
                            uint32* color;
                            int i;
                            int number_4byte_chunks = v[0];
                            int top_color_start = v[1];
                            int top_color_end = v[2]; // inclusive
                            int bottom_color_start;
                            int bottom_color_end; // exclusive
                            int len_top;
                            int len_bottom;

                            for (i = z; i < top_color_start; i++)
                                setgeom(x, y, i, 0);

                            color = (uint32*)(v + 4);
                            for (z = top_color_start; z <= top_color_end; z++)
                                setcolor(x, y, z, *color++);

                            len_bottom = top_color_end - top_color_start + 1;

                            // check for end of data marker
                            if (number_4byte_chunks == 0)
                            {
                                // infer ACTUAL number of 4-byte chunks from the length of the color data
                                v += 4 * (len_bottom + 1);
                                break;
                            }

                            // infer the number of bottom colors in next span from chunk length
                            len_top = (number_4byte_chunks - 1) - len_bottom;

                            // now skip the v pointer past the data to the beginning of the next span
                            v += v[0] * 4;

                            bottom_color_end = v[3]; // aka air start
                            bottom_color_start = bottom_color_end - len_top;

                            for (z = bottom_color_start; z < bottom_color_end; ++z)
                            {
                                setcolor(x, y, z, *color++);
                            }
                        }
                    }
                }


                System.Diagnostics.Debug.Assert(v - @base == len);
            };
            #endregion

            var bytes = System.IO.File.ReadAllBytes(vxl);

            fixed (byte* ptr = bytes)
            {
                load_map(ptr, bytes.Length);
            }

            #region heightmap
            var heightmap = new int[512, 512];
            var heightmap_min = 64;
            var heightmap_max = 0;
            var map_h = 512;
            var map_w = 512;

            for (var y = 0; y < map_h; ++y)
            {
                for (var x = 0; x < map_w; ++x)
                {
                    for (var z = 0; z < 64; ++z)
                    {
                        if (_geom[x, y, z] == 0)
                        {
                            heightmap[x, y] = z;
                        }
                    }


                    heightmap_min = Math.Min(heightmap_min, heightmap[x, y]);
                    heightmap_max = Math.Max(heightmap_max, heightmap[x, y]);

                }
            }
            #endregion

            double dpi = 72;
            int width = 512;
            int height = 512;
            byte[] pixelData = new byte[width * height * 4];

            for (int y = 0; y < height; ++y)
            {
                int yIndex = y * width;
                for (int x = 0; x < width; ++x)
                {
                    var z = heightmap[x, y];

                    z = 255 - (z - heightmap_min) * 255 / (heightmap_max - heightmap_min);

                    var i = (x + yIndex) * 4;


                    var color = _color[x, y, heightmap[x, y] + 1];

                    pixelData[i + 0] = (byte)((color >> (8 * 0)) & 0xFF);
                    pixelData[i + 1] = (byte)((color >> (8 * 1)) & 0xFF);
                    pixelData[i + 2] = (byte)((color >> (8 * 2)) & 0xFF);

                    pixelData[i + 3] = (byte)(0xFF);
                }
            }

            BitmapSource bmpSource = BitmapSource.Create(
                width, height,
                dpi, dpi,
                PixelFormats.Bgra32,
                null,
                pixelData,
                width * 4);

            new Image { Source = bmpSource }.AttachTo(this);
        }

    }
}
