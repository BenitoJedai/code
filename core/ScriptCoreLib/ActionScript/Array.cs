using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/Array.html
    [Script(IsNative=true, IsArray = true)]
    public class Array
    {
        /// <summary>
        /// A non-negative integer specifying the number of elements in the array.
        /// </summary>
        public uint length { get; set; }

        /// <summary>
        /// Adds one or more elements to the end of an array and returns the new length of the array.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public uint push(object e)
        {
            return default(uint);
        }

        /// <summary>
        /// Adds elements to and removes elements from an array.
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="deleteCount"></param>
        /// <param name="value"></param>
        /// <returns>An array containing the elements that were removed from the original array.</returns>
        public Array splice(int startIndex, int deleteCount, object value)
        {
            return default(Array);
        }

        /// <summary>
        /// Adds elements to and removes elements from an array.
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="deleteCount"></param>
        /// <returns>An array containing the elements that were removed from the original array.</returns>
        public Array splice(int startIndex, int deleteCount)
        {
            return default(Array);
        }
    }
}
