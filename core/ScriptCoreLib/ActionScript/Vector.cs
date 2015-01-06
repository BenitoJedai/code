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
    [Script(IsNative = true)]
    public class Vector<T>
    {
        // is AIR to support other generics in 2015?

        public Vector()
        {

        }

        public Vector(uint length)
            : this()
        {

        }

        public Vector(uint length, bool @fixed)
            : this(length)
        {

        }

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
        public uint push(T arg0)
        {
            return default(uint);
        }

        public uint push(T arg0, T arg1)
        {
            return default(uint);
        }

        public uint push(T arg0, T arg1, T arg2)
        {
            return default(uint);
        }

        public uint push(T arg0, T arg1, T arg2, T arg3)
        {
            return default(uint);
        }


        public uint push(T arg0, T arg1, T arg2, T arg3, T arg4)
        {
            return default(uint);
        }

        public uint push(T arg0, T arg1, T arg2, T arg3, T arg4, T arg5)
        {
            return default(uint);
        }

        public uint push(T arg0, T arg1, T arg2, T arg3, T arg4, T arg5, T arg6)
        {
            return default(uint);
        }

        public uint push(T arg0, T arg1, T arg2, T arg3, T arg4, T arg5, T arg6, T arg7)
        {
            return default(uint);
        }

        public uint push(T arg0, T arg1, T arg2, T arg3, T arg4, T arg5, T arg6, T arg7, T arg8)
        {
            return default(uint);
        }

        public uint push(T arg0, T arg1, T arg2, T arg3, T arg4, T arg5, T arg6, T arg7, T arg8, T arg9)
        {
            return default(uint);
        }



        public static implicit operator Vector<T>(T[] e)
        {
            return default(Vector<T>);
        }

        public T this[int i]
        {
            get
            {
                return default(T);
            }
            set
            {

            }
        }

        /// <summary>
        /// Concatenates the Vectors specified in the parameters list with the elements in this Vector and creates a new Vector.
        /// </summary>
        /// <param name="arg1"></param>
        /// <returns></returns>
        public Vector<T> concat(object arg1)
        {
            return default(Vector<T>);
        }


        /// <summary>
        /// Executes a test function on each item in the Vector until an item is reached that returns false for the specified function.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="thisObject"></param>
        /// <returns></returns>
        public bool every(Function callback, Object thisObject = null)
        {
            return default(bool);
        }

        /// <summary>
        /// Executes a test function on each item in the Vector and returns a new Vector containing all items that return true for the specified function.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="thisObject"></param>
        /// <returns></returns>
        public Vector<T> filter(Function callback, Object thisObject = null)
        {
            return default(Vector<T>);
        }



        /// <summary>
        /// Executes a function on each item in the Vector.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="thisObject"></param>
        /// <returns></returns>
        public void forEach(Function callback, Object thisObject = null)
        {
        }


        /// <summary>
        /// Searches for an item in the Vector and returns the index position of the item.
        /// </summary>
        /// <param name="searchElement"></param>
        /// <param name="fromIndex"></param>
        /// <returns></returns>
        public int indexOf(T searchElement, int fromIndex = 0)
        {
            return default(int);
        }

        /// <summary>
        /// Converts the elements in the Vector to strings, inserts the specified separator between the elements, concatenates them, and returns the resulting string.
        /// </summary>
        /// <param name="sep"></param>
        /// <returns></returns>
        public string join(string sep = ",")
        {
            return default(string);
        }


        /// <summary>
        ///Searches for an item in the Vector, working backward from the specified index position, and returns the index position of the matching item.
        /// 
        /// </summary>
        /// <param name="searchElement"></param>
        /// <param name="fromIndex"></param>
        /// <returns></returns>
        public int lastIndexOf(T searchElement, int fromIndex = 0x7fffffff)
        {
            return default(int);
        }


        /// <summary>
        /// Executes a function on each item in the Vector, and returns a new Vector of items corresponding to the results of calling the function on each item in this Vector.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="thisObject"></param>
        /// <returns></returns>
        public Vector<T> map(Function callback, Object thisObject = null)
        {
            return default(Vector<T>);
        }


        /// <summary>
        /// Reverses the order of the elements in the Vector.
        /// </summary>
        /// <returns></returns>
        public Vector<T> reverse()
        {
            return default(Vector<T>);
        }

        /// <summary>
        /// Removes the first element from the Vector and returns that element.
        /// </summary>
        /// <returns></returns>
        public T shift()
        {
            return default(T);
        }

        /// <summary>
        /// Returns a new Vector that consists of a range of elements from the original Vector, without modifying the original Vector.
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public Vector<T> slice(int startIndex = 0, int endIndex = 16777215)
        {
            return default(Vector<T>);
        }

        /// <summary>
        /// Executes a test function on each item in the Vector until an item is reached that returns true.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="thisObject"></param>
        /// <returns></returns>
        public bool some(Function callback, Object thisObject = null)
        {
            return default(bool);
        }

        /// <summary>
        /// Sorts the elements in the Vector object, and also returns a sorted Vector object.
        /// </summary>
        /// <param name="sortBehavior"></param>
        /// <returns></returns>
        public Vector<T> sort(object sortBehavior)
        {
            return default(Vector<T>);
        }

        /// <summary>
        ///         Adds elements to and removes elements from the Vector.

        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public Vector<T> splice(int startIndex, uint deleteCount = 4294967295)
        {
            return default(Vector<T>);
        }

        public Vector<T> splice(int startIndex, uint deleteCount = 4294967295, object item1 = null)
        {
            return default(Vector<T>);
        }

        public Vector<T> splice(int startIndex, uint deleteCount = 4294967295, object item1 = null, object item2 = null)
        {
            return default(Vector<T>);
        }

        public Vector<T> splice(int startIndex, uint deleteCount = 4294967295, object item1 = null, object item2 = null, object item3 = null)
        {
            return default(Vector<T>);
        }


        public Vector<T> splice(int startIndex, uint deleteCount = 4294967295, object item1 = null, object item2 = null, object item3 = null, object item4 = null)
        {
            return default(Vector<T>);
        }


        public Vector<T> splice(int startIndex, uint deleteCount = 4294967295, object item1 = null, object item2 = null, object item3 = null, object item4 = null, object item5 = null)
        {
            return default(Vector<T>);
        }

        /// <summary>
        /// Returns a string that represents the elements in the specified Vector.
        /// </summary>
        /// <returns></returns>
        public string toLocaleString()
        {
            return default(string);
        }

        /// <summary>
        ///  Returns a string that represents the elements in the Vector.
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            return default(string);
        }


        /// <summary>
        /// Adds one or more elements to the beginning of the Vector and returns the new length of the Vector.
        /// </summary>
        /// <param name="arg1"></param>
        /// <returns></returns>
        public uint unshift()
        {
            return default(uint);
        }

        public uint unshift(object arg1)
        {
            return default(uint);
        }

        public uint unshift(object arg1, object arg2)
        {
            return default(uint);
        }

        public uint unshift(object arg1, object arg2, object arg3)
        {
            return default(uint);
        }

        public uint unshift(object arg1, object arg2, object arg3, object arg4)
        {
            return default(uint);
        }

        public uint unshift(object arg1, object arg2, object arg3, object arg4, object arg5)
        {
            return default(uint);
        }



    }

}
