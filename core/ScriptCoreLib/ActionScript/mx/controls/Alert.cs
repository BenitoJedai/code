using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.mx.controls
{
    // http://livedocs.adobe.com/flex/201/langref/mx/controls/Alert.html
    [Script(IsNative = true)]
    public class Alert
    {
        // show(text:String = "", title:String = "", flags:uint = 0x4, parent:Sprite = null, closeHandler:Function = null, iconClass:Class = null, defaultButtonFlag:uint = 0x4):Alert

        /// <summary>
        /// Static method that pops up the Alert control.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static public Alert show(string text)
        {
            return default(Alert);
        }


    }
}
