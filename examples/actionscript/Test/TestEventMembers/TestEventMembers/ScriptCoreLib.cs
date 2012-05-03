﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;



namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    // may implement System.Core
    // should be defined in ScriptCoreLib.Query but
    // this assembly needs to use them

    [Script(Implements = typeof(global::System.Action))]
    internal delegate void __Action();

}





namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Delegate))]
    internal class __Delegate
    {
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Target)]
        public object _Target;

        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Method)]
        public global::System.IntPtr _Method;

        public __Delegate(object e, global::System.IntPtr p)
        {
            _Target = e;
            _Method = p;
        }


        Function _FunctionPointer;

        public Function FunctionPointer
        {
            get
            {
                if (_FunctionPointer == null)
                {
                    var method = ToIntPtr(_Method);

                    if (method.FunctionToken != null)
                    {
                        _FunctionPointer = method.FunctionToken;
                    }
                    else
                    {
                        _FunctionPointer = GetFunctionPointer(_Target, method.StringToken);
                    }
                }

                return _FunctionPointer;
            }
        }

        [Script(OptimizedCode = "return o;")]
        private static __IntPtr ToIntPtr(global::System.IntPtr o)
        {
            return default(__IntPtr);
        }

        [Script(OptimizedCode = "return o[n];")]
        private static Function GetFunctionPointer(object o, string n)
        {
            return default(Function);
        }





        public static __Delegate Combine(__Delegate a, __Delegate b)
        {
            if (a == null)
            {
                return b;
            }
            if (b == null)
            {
                return a;
            }

            return a.CombineImpl(b);
        }

        protected virtual __Delegate CombineImpl(__Delegate d)
        {
            return null;
            //throw new global::System.Exception("use MulticastDelegate instead");
        }

        public static __Delegate Remove(__Delegate source, __Delegate value)
        {
            if (source == null)
            {
                return null;
            }
            if (value == null)
            {
                return source;
            }
            return source.RemoveImpl(value);
        }

        protected virtual __Delegate RemoveImpl(__Delegate d)
        {
            return null;
            //thrvow new global::System.Exception("use MulticastDelegate instead");
        }
    }
}


namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.MulticastDelegate))]
    internal class __MulticastDelegate : __Delegate
    {
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.List)]
        protected Array list = new Array();


        public __MulticastDelegate(object e, global::System.IntPtr p)
            : base(e, p)
        {
            list.push(this);
        }



        protected override __Delegate CombineImpl(__Delegate d)
        {
            list.push(d);

            return this;
        }

        protected override __Delegate RemoveImpl(__Delegate d)
        {
            var j = -1;
            var a = ((__Delegate[])(object)list);


            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == d)
                {
                    j = i;
                    break;
                }
            }

            if (j > -1)
                list.splice(j, 1);

            if (list.length == 0)
                return null;

            return this;
        }
    }
}


namespace ScriptCoreLib.ActionScript
{
	// http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/Array.html
	[Script(IsNative = true, IsArray = true)]
	public class Array
	{
		#region Constants
		/// <summary>
		/// [static] Specifies case-insensitive sorting for the Array class sorting methods.
		/// </summary>
		public static readonly uint CASEINSENSITIVE = 1;

		/// <summary>
		/// [static] Specifies descending sorting for the Array class sorting methods.
		/// </summary>
		public static readonly uint DESCENDING = 2;

		/// <summary>
		/// [static] Specifies numeric (instead of character-string) sorting for the Array class sorting methods.
		/// </summary>
		public static readonly uint NUMERIC = 16;

		/// <summary>
		/// [static] Specifies that a sort returns an array that consists of array indices.
		/// </summary>
		public static readonly uint RETURNINDEXEDARRAY = 8;

		/// <summary>
		/// [static] Specifies the unique sorting requirement for the Array class sorting methods.
		/// </summary>
		public static readonly uint UNIQUESORT = 4;

		#endregion


		#region Properties
		/// <summary>
		/// A non-negative integer specifying the number of elements in the array.
		/// </summary>
		public uint length { get; set; }

		#endregion


		#region Methods
		/// <summary>
		/// Concatenates the elements specified in the parameters with the elements in an array and creates a new array.
		/// </summary>
		public Array concat(/* params */ object args)
		{
			return default(Array);
		}

		/// <summary>
		/// Concatenates the elements specified in the parameters with the elements in an array and creates a new array.
		/// </summary>
		public Array concat()
		{
			return default(Array);
		}

		/// <summary>
		/// Executes a test function on each item in the array until an item is reached that returns false for the specified function.
		/// </summary>
		public bool every(Function callback, object thisObject)
		{
			return default(bool);
		}

		/// <summary>
		/// Executes a test function on each item in the array until an item is reached that returns false for the specified function.
		/// </summary>
		public bool every(Function callback)
		{
			return default(bool);
		}

		/// <summary>
		/// Executes a test function on each item in the array and constructs a new array for all items that return true for the specified function.
		/// </summary>
		public Array filter(Function callback, object thisObject)
		{
			return default(Array);
		}

		/// <summary>
		/// Executes a test function on each item in the array and constructs a new array for all items that return true for the specified function.
		/// </summary>
		public Array filter(Function callback)
		{
			return default(Array);
		}

		/// <summary>
		/// Executes a function on each item in the array.
		/// </summary>
		public void forEach(Function callback, object thisObject)
		{
		}

		/// <summary>
		/// Executes a function on each item in the array.
		/// </summary>
		public void forEach(Function callback)
		{
		}

		/// <summary>
		/// Searches for an item in an array by using strict equality (===) and returns the index position of the item.
		/// </summary>
		public int indexOf(object searchElement, int fromIndex)
		{
			return default(int);
		}

		/// <summary>
		/// Searches for an item in an array by using strict equality (===) and returns the index position of the item.
		/// </summary>
		public int indexOf(object searchElement)
		{
			return default(int);
		}

		/// <summary>
		/// Converts the elements in an array to strings, inserts the specified separator between the elements, concatenates them, and returns the resulting string.
		/// </summary>
		public string join(object sep)
		{
			return default(string);
		}

		/// <summary>
		/// Searches for an item in an array, working backward from the last item, and returns the index position of the matching item using strict equality (===).
		/// </summary>
		public int lastIndexOf(object searchElement, int fromIndex)
		{
			return default(int);
		}

		/// <summary>
		/// Searches for an item in an array, working backward from the last item, and returns the index position of the matching item using strict equality (===).
		/// </summary>
		public int lastIndexOf(object searchElement)
		{
			return default(int);
		}

		/// <summary>
		/// Executes a function on each item in an array, and constructs a new array of items corresponding to the results of the function on each item in the original array.
		/// </summary>
		public Array map(Function callback, object thisObject)
		{
			return default(Array);
		}

		/// <summary>
		/// Executes a function on each item in an array, and constructs a new array of items corresponding to the results of the function on each item in the original array.
		/// </summary>
		public Array map(Function callback)
		{
			return default(Array);
		}

		/// <summary>
		/// Removes the last element from an array and returns the value of that element.
		/// </summary>
		public object pop()
		{
			return default(object);
		}

		/// <summary>
		/// Adds one or more elements to the end of an array and returns the new length of the array.
		/// </summary>
		public uint push(/* params */ object args)
		{
			return default(uint);
		}

		/// <summary>
		/// Adds one or more elements to the end of an array and returns the new length of the array.
		/// </summary>
		public uint push()
		{
			return default(uint);
		}

		/// <summary>
		/// Reverses the array in place.
		/// </summary>
		public Array reverse()
		{
			return default(Array);
		}

		/// <summary>
		/// Removes the first element from an array and returns that element.
		/// </summary>
		public object shift()
		{
			return default(object);
		}

		/// <summary>
		/// Returns a new array that consists of a range of elements from the original array, without modifying the original array.
		/// </summary>
		public Array slice(int startIndex, int endIndex)
		{
			return default(Array);
		}

		/// <summary>
		/// Returns a new array that consists of a range of elements from the original array, without modifying the original array.
		/// </summary>
		public Array slice(int startIndex)
		{
			return default(Array);
		}

		/// <summary>
		/// Returns a new array that consists of a range of elements from the original array, without modifying the original array.
		/// </summary>
		public Array slice()
		{
			return default(Array);
		}

		/// <summary>
		/// Executes a test function on each item in the array until an item is reached that returns true.
		/// </summary>
		public bool some(Function callback, object thisObject)
		{
			return default(bool);
		}

		/// <summary>
		/// Executes a test function on each item in the array until an item is reached that returns true.
		/// </summary>
		public bool some(Function callback)
		{
			return default(bool);
		}

		/// <summary>
		/// Sorts the elements in an array.
		/// </summary>
		public Array sort(/* params */ object args)
		{
			return default(Array);
		}

		/// <summary>
		/// Sorts the elements in an array.
		/// </summary>
		public Array sort()
		{
			return default(Array);
		}

		/// <summary>
		/// Sorts the elements in an array according to one or more fields in the array.
		/// </summary>
		public Array sortOn(object fieldName, object options)
		{
			return default(Array);
		}

		/// <summary>
		/// Sorts the elements in an array according to one or more fields in the array.
		/// </summary>
		public Array sortOn(object fieldName)
		{
			return default(Array);
		}

		/// <summary>
		/// Adds elements to and removes elements from an array.
		/// </summary>
		public Array splice(int startIndex, uint deleteCount, /* params */ object values)
		{
			return default(Array);
		}

		/// <summary>
		/// Adds elements to and removes elements from an array.
		/// </summary>
		public Array splice(int startIndex, uint deleteCount)
		{
			return default(Array);
		}

		/// <summary>
		/// Returns a string that represents the elements in the specified array.
		/// </summary>
		public string toLocaleString()
		{
			return default(string);
		}

		/// <summary>
		/// Adds one or more elements to the beginning of an array and returns the new length of the array.
		/// </summary>
		public uint unshift(/* params */ object args)
		{
			return default(uint);
		}

		/// <summary>
		/// Adds one or more elements to the beginning of an array and returns the new length of the array.
		/// </summary>
		public uint unshift()
		{
			return default(uint);
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Lets you create an array of the specified number of elements.
		/// </summary>
		public Array(int numElements)
		{
		}

		/// <summary>
		/// Lets you create an array of the specified number of elements.
		/// </summary>
		public Array()
		{
		}

		/// <summary>
		/// Lets you create an array that contains the specified elements.
		/// </summary>
		public Array(/* params */ object values)
		{
		}


		#endregion

	}
}


namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/Function.html
    [Script(IsNative = true)]
    public class Function
    {

        /// <summary>
        /// Specifies the value of thisObject to be used within any function that ActionScript calls.
        /// </summary>
        /// <param name="thisObject"></param>
        /// <param name="argArray"></param>
        /// <returns></returns>
        public object apply(object thisObject, Array argArray)
        {
            return default(object);
        }


    }
}


namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.IntPtr))]
    internal class __IntPtr
    {
        public string StringToken;
        public Function FunctionToken;
        //public Class ClassToken;



        public static explicit operator __IntPtr(string _Token)
        {
            return new __IntPtr { StringToken = _Token };
        }

        public static explicit operator __IntPtr(Function _Token)
        {
            return new __IntPtr { FunctionToken = _Token };
        }

        public static explicit operator string(__IntPtr _ptr)
        {
            return _ptr.StringToken;
        }

        public static explicit operator Function(__IntPtr _ptr)
        {
            return _ptr.FunctionToken;
        }





    }
}

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/201/langref/flash/display/DisplayObjectContainer.html
    [Script(IsNative = true)]
    public class DisplayObjectContainer : InteractiveObject
    { }
}

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/display/Stage.html
    [Script(IsNative = true)]
    public class Stage : DisplayObjectContainer
    {
        /// <summary>
        /// Dispatched when the Stage object enters, or leaves, full-screen mode.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action fullScreen;

    }
}


namespace ScriptCoreLib.ActionScript.Extensions.flash.display
{
    // if a type implements a type that is set to be native, then only implementation
    // which is marked with NotImplementedHere applies


    [Script(Implements = typeof(Stage))]
    internal static class __Stage
    {
        #region fullScreen
        public static void add_fullScreen(Stage that, Action value)
        {
        }

        public static void remove_fullScreen(Stage that, Action value)
        {
        }
        #endregion
    }
}