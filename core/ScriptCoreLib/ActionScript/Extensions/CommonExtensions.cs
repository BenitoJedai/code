using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.BCLImplementation.System;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.mx.core;

namespace ScriptCoreLib.ActionScript.Extensions
{
    [Script]
    public static class CommonExtensions
    {
        [Script(OptimizedCode = "return new c();")]
        public static object CreateType(this Class c)
        {
            return default(object);
        }

        public static SoundAsset ToSoundAsset(this Class c)
        {
            return (SoundAsset)c.CreateType();
        }

        public static BitmapAsset ToBitmapAsset(this Class c)
        {
            return (BitmapAsset)c.CreateType();
        }

        public static void CombineDelegate<T>(EventDispatcher _this, Action<T> value, string name)
            where T : Event
        {
            _this.addEventListener(name, value.ToFunction(), false, 0, false);
        }

        public static void RemoveDelegate<T>(EventDispatcher _this, Action<T> value, string name)
            where T : Event
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
