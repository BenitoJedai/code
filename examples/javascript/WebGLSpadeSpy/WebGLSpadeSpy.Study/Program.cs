using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebGLSpadeSpy.Study
{
    using uint8 = Byte;
    using uint32 = UInt32;

    unsafe delegate void __load_map(uint8* v, int len);

    class Program
    {
        unsafe static void Main(string[] args)
        {
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

            #region print_map
            Action<string> print_map =
                vxl =>
                {
                    // Could not find a part of the path 'Z:\media\Program Files\Ace of Spades\vxl\normandie.vxl'.

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
                    var map_ystep = 8;
                    var map_xstep = 4;

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

                    var heightmap_delta = heightmap_max - heightmap_min;


                    for (var y = 0; y < map_h; y += map_ystep)
                    {
                        for (var x = 0; x < map_w; x += map_xstep)
                        {
                            var z = (
                                Enumerable.Range(0, map_ystep).SelectMany(
                                    yy => Enumerable.Range(0, map_xstep).Select(xx => new { xx, yy })
                                ).Sum(k => heightmap[x + k.xx, y + k.yy])) / (map_ystep * map_xstep);

                            z -= heightmap_min;

                            var zF = 0xF - ((z * 0xF) / (heightmap_delta));
                            var zFF = 0xFF - ((z * 0xFF) / (heightmap_delta));

                            if (zFF < (8 * 2))
                            {
                                Console.Write(" ");
                            }
                            else
                            {
                                if (zFF < (8 * 3))
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                else if (zFF < (8 * 4))
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                else if (zFF < (8 * 5))
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                else if (zFF < (8 * 6))
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else if (zFF < (8 * 7))
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                else if (zFF < (8 * 8))
                                    Console.ForegroundColor = ConsoleColor.White;
                                else
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                Console.Write(zF.ToString("x1"));
                            }
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                };
            #endregion


            print_map(@"X:\jsc.community\spade-builder\Maps\normandie.vxl");
            print_map(@"X:\jsc.community\spade-builder\Maps\island.vxl");
            //print_map(@"X:\jsc.community\spade-builder\Maps\urbantankfightfinal.vxl");
            //print_map(@"X:\jsc.community\spade-builder\Maps\forestriver0.vxl");

            Console.ReadKey(true);
        }
    }
}
