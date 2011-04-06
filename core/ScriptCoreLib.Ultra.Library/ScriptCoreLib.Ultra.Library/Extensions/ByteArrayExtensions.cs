using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Extensions
{
    public static class ByteArrayExtensions
    {
        public static byte[] Replace(this byte[] e, byte[][] source, byte[][] target)
        {
            var m = new MemoryStream();
            var length = e.Length;

            Func<int, int, bool> IsMatchAtIndex = (i, y) =>
            {
                var j = false;
                // do we even fit in the stream?
                if (e.Length >= i + source[y].Length)
                {
                    j = true;

                    for (int x = 0; x < source[y].Length; x++)
                    {
                        if (source[y][x] != e[i + x])
                        {
                            j = false;
                            break;
                        }
                    }

                }
                return j;
            };

            Func<int, int> GetMatchAtIndex = i =>
            {
                var j = -1;
                for (int y = 0; y < source.Length; y++)
                {
                    if (IsMatchAtIndex(i, y))
                    {
                        j = y;
                        break;
                    }
                }

                return j;
            };

            for (int i = 0; i < length; i++)
            {
                // any matches?
                var j = GetMatchAtIndex(i);

                if (j < 0)
                {
                    // else continue
                    m.WriteByte(e[i]);
                }
                else
                {
                    m.Write(target[j], 0, target[j].Length);

                    // skip matched bytes
                    i += target[j].Length - 1;
                }
            }

            return m.ToArray();
        }
    }
}
