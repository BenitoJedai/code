using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Shared.Lambda;

namespace ScriptCoreLib.ActionScript.Extensions
{
    [Script]
    public static class QueryExtensions
    {
        public static IEnumerable<DisplayObject> Children(this DisplayObjectContainer c)
        {
            return Enumerable.Range(0, c.numChildren).Select(i => c.getChildAt(i));

        }

        static void ToDictionaryHandler<T>(ZipFileEntry.Cookie<T>[] source, Action<Dictionary<string, T>> handler)
        {
            var dict = source.ToDictionary(k => k.Entry.FileName, v => v.Value);

            handler(dict);
        }

        public static IEnumerable<ZipFileEntry> ToBitmapArray(this IEnumerable<ZipFileEntry> e, Action<Bitmap[]> handler)
        {
            return e.ToArray(CommonExtensions.LoadBytes, handler);
        }

        static void ToArrayHandler<T>(ZipFileEntry.Cookie<T>[] source, Action<T[]> handler)
        {
            var a = source.Select(i => i.Value).ToArray();

            handler(a);
        }


        public static IEnumerable<ZipFileEntry> ToArray<T>(this IEnumerable<ZipFileEntry> e, Action<ByteArray, Action<T>> factory, Action<T[]> handler)
        {
            Action<ZipFileEntry.Cookie<T>[]> ToArray = a => ToArrayHandler(a, handler);


            return e.ToCookies(
               (ZipFileEntry z, Action<T> h) => factory(z.Bytes, h), ToArray
            );
        }

        public static IEnumerable<ZipFileEntry> ToBitmapDictionary(this IEnumerable<ZipFileEntry> e, Action<Dictionary<string, Bitmap>> handler)
        {
            return e.ToDictionary(
              CommonExtensions.LoadBytes, handler);
        }

        public static IEnumerable<ZipFileEntry> ToDictionary<T>(this IEnumerable<ZipFileEntry> e, Action<ByteArray, Action<T>> factory, Action<Dictionary<string, T>> handler)
        {
            Action<ZipFileEntry.Cookie<T>[]> ToDictionary = a => ToDictionaryHandler(a, handler);


            return e.ToCookies(
                (ZipFileEntry z, Action<T> h) => factory(z.Bytes, h), ToDictionary
            );
        }

        public static IEnumerable<ZipFileEntry> ToCookies<T>(this IEnumerable<ZipFileEntry> e, Action<ByteArray, Action<T>> factory, Action<ZipFileEntry.Cookie<T>[]> handler)
        {
            return e.ToCookies(
                (ZipFileEntry z, Action<T> h) => factory(z.Bytes, h), handler
            );
        }

        public static IEnumerable<ZipFileEntry> ToCookies<T>(this IEnumerable<ZipFileEntry> e, Action<ZipFileEntry, Action<T>> factory, Action<ZipFileEntry.Cookie<T>[]> handler)
        {
            var f = e.ToArray();
            var a = new ZipFileEntry.Cookie<T>[f.Length];
            var i = 0;
            var u = 0;

            foreach (var v in f)
            {
                var k = i;

                factory(v,
                    x =>
                    {
                        a[k] = new ZipFileEntry.Cookie<T>
                        {
                            Entry = f[k],
                            Value = x
                        };

                        u++;

                        if (u == f.Length)
                            handler(a);
                    }
                );

                i++;
            }

            return e;
        }

    }
}
