﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/2/langref/flash/display/DisplayObjectContainer.html
    [Script(IsNative = true)]
    public class DisplayObjectContainer
    {
        public T addChild<T>(T child)  where T : DisplayObject
        {
            return default(T);
        }
    }
}
