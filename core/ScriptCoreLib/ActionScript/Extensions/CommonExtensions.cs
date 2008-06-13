﻿using System;
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

namespace ScriptCoreLib.ActionScript.Extensions
{
    [Script]
    public static class CommonExtensions
    {
        [Script]
        class IFill_Dispose : IDisposable
        {
            public IFill e;
            public Graphics target;

            public void Dispose()
            {
                e.end(target);
            }

        }

        public static IDisposable Fill(this IFill e, Graphics target, Rectangle r)
        {
            e.begin(target, r);

            return new IFill_Dispose
            {
                e = e,
                target = target
            };
        }

        [Script(OptimizedCode = "return new c();")]
        public static object CreateType(this Class c)
        {
            return default(object);
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
    }
}
