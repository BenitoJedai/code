using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.utils
{

    /// <summary>
    /// Package-Level methods for flash.utils
    /// </summary>
    [ScriptImportsType("flash.utils.describeType")]
    [ScriptImportsType("flash.utils.getDefinitionByName")]
    [ScriptImportsType("flash.utils.getTimer")]
    [ScriptImportsType("flash.utils.clearInterval")]
    [ScriptImportsType("flash.utils.clearTimeout")]
    [ScriptImportsType("flash.utils.escapeMultiByte")]
    [ScriptImportsType("flash.utils.getQualifiedClassName")]
    [ScriptImportsType("flash.utils.getQualifiedSuperclassName")]
    [ScriptImportsType("flash.utils.setInterval")]
    [ScriptImportsType("flash.utils.setTimeout")]
    [ScriptImportsType("flash.utils.unescapeMultiByte")]
    [Script]
    public class Globals
    {
        // http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#describeType()
        [Script(OptimizedCode = "return flash.utils.describeType(e);")]
        internal static XML describeType(object e)
        {
            return default(XML);
        }

        // http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#getDefinitionByName()
        [Script(OptimizedCode = "return flash.utils.getDefinitionByName(e);")]
        public static object getDefinitionByName(string e)
        {
            return default(object);
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#getTimer()
        [Script(OptimizedCode = "return flash.utils.getTimer();")]
        public static int getTimer()
        {
            return default(int);
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#clearInterval()
        [Script(OptimizedCode = "return flash.utils.clearInterval(id);")]
        public static void clearInterval(uint id)
        {
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#setInterval()
        [Script(OptimizedCode = "return flash.utils.setInterval(closure,delay);")]
        public static uint setInterval(Function closure, double delay)
        {
            return default(uint);
        }


        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#setInterval()
        [Script(OptimizedCode = "return flash.utils.setInterval(closure,delay,arg0);")]
        public static uint setInterval(Function closure, double delay, object arg0)
        {
            return default(uint);
        }


        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#setInterval()
        [Script(OptimizedCode = "return flash.utils.setInterval(closure,delay,arg0,arg1);")]
        public static uint setInterval(Function closure, double delay, object arg0, object arg1)
        {
            return default(uint);
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#setInterval()
        [Script(OptimizedCode = "return flash.utils.setInterval(closure,delay,arg0,arg1,arg2);")]
        public static uint setInterval(Function closure, double delay, object arg0, object arg1, object arg2)
        {
            return default(uint);
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#setInterval()
        [Script(OptimizedCode = "return flash.utils.setInterval(closure,delay,arg0,arg1,arg2,arg3);")]
        public static uint setInterval(Function closure, double delay, object arg0, object arg1, object arg2, object arg3)
        {
            return default(uint);
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#setTimeout()
        [Script(OptimizedCode = "return flash.utils.setTimeout(closure,delay);")]
        public static uint setTimeout(Function closure, double delay)
        {
            return default(uint);
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#setTimeout()
        [Script(OptimizedCode = "return flash.utils.setTimeout(closure,delay,arg0);")]
        public static uint setTimeout(Function closure, double delay, object arg0)
        {
            return default(uint);
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#setTimeout()
        [Script(OptimizedCode = "return flash.utils.setTimeout(closure,delay,arg0,arg1);")]
        public static uint setTimeout(Function closure, double delay, object arg0, object arg1)
        {
            return default(uint);
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#setTimeout()
        [Script(OptimizedCode = "return flash.utils.setTimeout(closure,delay,arg0,arg1,arg2);")]
        public static uint setTimeout(Function closure, double delay, object arg0, object arg1, object arg2)
        {
            return default(uint);
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#setTimeout()
        [Script(OptimizedCode = "return flash.utils.setTimeout(closure,delay,arg0,arg1,arg2,arg3);")]
        public static uint setTimeout(Function closure, double delay, object arg0, object arg1, object arg2, object arg3)
        {
            return default(uint);
        }


        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#clearTimeout()
        [Script(OptimizedCode = "return flash.utils.clearTimeout(id);")]
        public static void clearTimeout(uint id)
        {
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#escapeMultiByte()
        [Script(OptimizedCode = "return flash.utils.escapeMultiByte(value);")]
        public static string escapeMultiByte(string value)
        {
            return default(string);
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#unescapeMultiByte()
        [Script(OptimizedCode = "return flash.utils.unescapeMultiByte(value);")]
        public static string unescapeMultiByte(string value)
        {
            return default(string);
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#getQualifiedClassName()
        [Script(OptimizedCode = "return flash.utils.getQualifiedClassName(e);")]
        public static string getQualifiedClassName(object e)
        {
            return default(string);
        }

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/utils/package.html#getQualifiedSuperclassName()
        [Script(OptimizedCode = "return flash.utils.getQualifiedSuperclassName(e);")]
        public static string getQualifiedSuperclassName(object e)
        {
            return default(string);
        }

    }
}
