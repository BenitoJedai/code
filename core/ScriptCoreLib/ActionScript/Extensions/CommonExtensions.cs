using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.BCLImplementation.System;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.mx.graphics;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.utils;
using System.IO;
using ScriptCoreLib.ActionScript.BCLImplementation.System.IO;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.text;
using System.Diagnostics;

namespace ScriptCoreLib.ActionScript.Extensions
{

    [ScriptImportsType("flash.net.navigateToURL")]
    [Script]
    public static class CommonExtensions
    {
        // http://livedocs.adobe.com/flex/2/langref/flash/net/package.html#navigateToURL()

        [Script(OptimizedCode = "return flash.net.navigateToURL(r, window);")]
        public static void NavigateTo(this URLRequest r, string window)
        {

        }


        //public static IEnumerable<T> AsEnumerable<T>(this Vector<T> e)
        //{
        //    return Enumerable.Range(0, (int)e.length).Select(x => e[x]);
        //}

        public static void InvokeWhenStageIsReady(this DisplayObject o, Action a)
        {
            if (o.stage == null)
                o.addedToStage +=
                    delegate
                    {
                        a();
                    };
            else
                a();
        }

        public static byte[] ToArray(this ByteArray e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            var p = e.position;

            e.position = 0;

            var a = new byte[e.length];

            for (int i = 0; i < e.length; i++)
            {
                a[i] = (byte)(((byte)e.readByte()) & 0xff);
            }

            e.position = p;

            return a;
        }
        public static MemoryStream ToMemoryStream(this ByteArray e)
        {

            return (MemoryStream)(object)new __MemoryStream { InternalBuffer = e };
        }

        public static ByteArray ToByteArray(this Stream s)
        {
            var m = s as MemoryStream;

            if (m == null)
                throw new NotSupportedException();

            var a = m.ToByteArray();

            a.endian = Endian.LITTLE_ENDIAN;

            return a;
        }

        public static ByteArray ToByteArray(this MemoryStream m)
        {
            var x = (__MemoryStream)(object)m;

            return x.InternalBuffer;
        }


        public static void OrphanizeChildren(this DisplayObjectContainer e)
        {
            while (e.numChildren > 0)
                e.getChildAt(0).Orphanize();
        }

        public static void Orphanize(this DisplayObject e)
        {
            if (e.parent != null)
            {
                var AsLoader = e.parent as Loader;

                if (AsLoader != null)
                    return;

                e.parent.removeChild(e);
            }

        }




        public static T MoveTo<T>(this T e, Point i) where T : DisplayObject
        {
            return e.MoveTo(i.x, i.y);
        }

        public static T MoveTo<T>(this T e, DisplayObject i) where T : DisplayObject
        {
            return e.MoveTo(i.x, i.y);
        }

        public static T MoveTo<T>(this T e, double x, double y) where T : DisplayObject
        {
            e.x = x;
            e.y = y;

            return e;
        }

        public static Point ToPoint(this DisplayObject e)
        {
            return new Point { x = e.x, y = e.y };
        }



        //[Obsolete("Breaking API changes in flex4", true)]
        //public static IDisposable Fill(this IFill e, Graphics target, Rectangle r)


        [Script(OptimizedCode = "return new c();")]
        public static object CreateType(this Class c)
        {
            return default(object);
        }

        public static Class ToClassToken(this Type e)
        {
            __Type c = e;

            return (Class)__Type.getDefinitionByName(c.InternalFullName);
        }

        public static Class ToClassToken(this IntPtr e)
        {
            var x = e;
            var z = (__IntPtr)(object)x;

            return z.ClassToken;
        }

        public static XML ToXMLAsset(this Class c)
        {
            return new XML(c.ToStringAsset());
        }

        public static string ToStringAsset(this Class c)
        {
            var a = c.ToByteArrayAsset();

            return a.readUTFBytes(a.length);
        }


        public static ZipFileEntry[] ToFiles(this Class c)
        {
            var bytes = c.ToByteArrayAsset();
            bytes.endian = Endian.LITTLE_ENDIAN;
            return ZipFileEntry.Parse(bytes);
        }

        public static void LoadBytes<T>(this ByteArray e, Action<T> done) where T : DisplayObject
        {
            var loader = new Loader();

            loader.contentLoaderInfo.complete +=
                delegate
                {
                    done((T)loader.content);
                };

            e.position = 0;

            loader.loadBytes(e
                , new LoaderContext(false, ApplicationDomain.currentDomain, null)
            );

        }

        public static ByteArrayAsset ToByteArrayAsset(this Class c)
        {
            return (ByteArrayAsset)c.CreateType();
        }

        public static FontAsset ToFontAsset(this Class c)
        {
            return (FontAsset)c.CreateType();
        }

        public static SoundAsset ToSoundAsset(this Class c)
        {
            if (c == null) return null;

            return (SoundAsset)c.CreateType();
        }



        public static BitmapAsset ToBitmapAsset(this Class c)
        {
            if (c == null) return null;

            return (BitmapAsset)c.CreateType();
        }

        public static Sprite ToSprite(this Class c)
        {
            if (c == null) return null;

            return (Sprite)c.CreateType();
        }

        public static void CombineDelegate<T>(EventDispatcher _this, Action<T> value, string name)
        // where T : Event
        {
            _this.addEventListener(name, value.ToFunction(), false, 0, false);
        }

        public static void RemoveDelegate<T>(EventDispatcher _this, Action<T> value, string name)
        // where T : Event
        {
            _this.removeEventListener(name, value.ToFunction(), false);
        }


        public static Function ToFunction(this Delegate e)
        {
            return ((__Delegate)(object)e).FunctionPointer;
        }

        public static Stage SetFullscreen(this Stage s, bool value)
        {
            if (value)
                s.displayState = StageDisplayState.FULL_SCREEN;
            else
                s.displayState = StageDisplayState.NORMAL;

            return s;
        }

        public static T AttachToBefore<T>(this T e, DisplayObject x) where T : DisplayObject
        {
            var c = x.parent;

            c.addChildAt(e, c.getChildIndex(x) - 1);

            return e;
        }


        // 20141123
        // we now know the stage
        public static T AttachToSprite<T>(this T e) where T : DisplayObject
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/2014
            // X:\jsc.svn\examples\actionscript\air\AIRThreadedSoundAsync\AIRThreadedSoundAsync\ApplicationSprite.cs

            e.AttachTo(
                __Thread.InternalPrimordialSprite
            );


            //c.addChild(e);

            return e;
        }


        public static T AttachTo<T>(this T e, DisplayObjectContainer c) where T : DisplayObject
        {
            c.addChild(e);

            return e;
        }

        public static T[] AttachTo<T>(this T[] e, DisplayObjectContainer c) where T : DisplayObject
        {
            foreach (var i in e)
                i.AttachTo(c);

            return e;
        }





        public static TextField AsConsole(this TextField x)
        {
            var sw = Stopwatch.StartNew();

            // X:\jsc.svn\examples\actionscript\air\AIRThreadedSoundAsync\AIRThreadedSoundAsync\ApplicationSprite.cs

            var history = new List<string>();

            var w = new __Console.__OutWriter
            {
                AtWriteLine = z =>
                {
                    history.Add(sw.ElapsedMilliseconds + "ms " + z);


                    if (history.Count > 16)
                        history.RemoveAt(0);


                    // System.String for System.String Join(System.String, System.String[]) used at
                    x.text = string.Join(
                        //Environment.NewLine, 
                        "\n",

                        history.ToArray());
                }
            };

            Console.SetOut(w);

            return x;
        }
    }
}
