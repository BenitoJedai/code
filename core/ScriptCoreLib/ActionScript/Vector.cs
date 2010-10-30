using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://help.adobe.com/en_US/AS3LCR/Flash_10.0/Vector.html
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/Vector.html
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/package.html#Vector()
    // http://riaoo.com/?p=1852

    [GenericTypeDefinition]
    [DynamicType]
    [Script(IsNative = true, IsArray = true)]
    public class Vector<T>
    {
        /// <summary>
        /// Indicates whether the length property of the Vector can be changed.
        /// </summary>
        public bool @fixed { get; set; }

        /// <summary>
        /// The range of valid indices available in the Vector.
        /// </summary>
        public uint length { get; set; }

        /// <summary>
        /// Removes the last element from the Vector and returns that element.
        /// </summary>
        /// <returns></returns>
        public T pop()
        {
            return default(T);
        }

        /// <summary>
        /// Adds one or more elements to the end of the Vector and returns the new length of the Vector.
        /// </summary>
        /// <param name="a"></param>
        public void push(T a)
        {

        }
    }

}
