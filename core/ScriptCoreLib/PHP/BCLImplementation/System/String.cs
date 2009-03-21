using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[ScriptParameterByVal]
	[Script(Implements = typeof(global::System.String), InternalConstructor = true)]
	internal class __String
	{
		public __String(char c, int count)
		{
		}

		public static string InternalConstructor(char c, int count)
		{
			var x = Native.API.chr(c);
			var y = "";

			for (int i = 0; i < count; i++)
			{
				y += x;
			}

			return y;
		}

		public class API
		{
			#region int strrpos ( string haystack, string needle [, int offset] )

			/// <summary>
			/// Returns the numeric position of the last occurrence of needle in the haystack string. Note that the needle in this case can only be a single character in PHP 4. If a string is passed as the needle, then only the first character of that string will be used. 
			/// </summary>
			/// <param name="_haystack">string haystack</param>
			/// <param name="_needle">string needle</param>
			[Script(IsNative = true)]
			public static int strrpos(object _haystack, string _needle) { return default(int); }

			#endregion


			#region int strpos ( string haystack, mixed needle [, int offset] )

			/// <summary>
			/// Returns the numeric position of the first occurrence of needle in the haystack string. Unlike the strrpos(), this function can take a full string as the needle parameter and the entire string will be used. 
			/// </summary>
			/// <param name="_haystack">string haystack</param>
			/// <param name="_needle">mixed needle</param>
			[Script(IsNative = true)]
			public static int strpos(__String _haystack, object _needle) { return default(int); }

			#endregion

			[Script(IsNative = true)]
			public static int strpos(__String _haystack, object _needle, int start) { return default(int); }

			
			#region mixed str_replace ( mixed search, mixed replace, mixed subject [, int &count] )

			/// <summary>  
			/// This function returns a string or an array with all occurrences of search in subject replaced with the given replace value. If you don't need fancy replacing rules (like regular expressions), you should always use this function instead of ereg_replace() or preg_replace().   
			/// </summary>  
			/// <param name="_search">mixed search</param>  
			/// <param name="_replace">mixed replace</param>  
			/// <param name="_subject">mixed subject</param>  
			[Script(IsNative = true)]
			public static __String str_replace(object _search, object _replace, object _subject) { return default(__String); }

			#endregion


			#region string substr ( string string, int start , int length )

			/// <summary>  
			/// substr() returns the portion of string specified by the start and length parameters.   
			/// </summary>  
			/// <param name="_string">string string</param>  
			/// <param name="_start">int start</param>  
			/// <param name="_length">int length</param>  
			[Script(IsNative = true)]
			public static __String substr(__String _string, int _start, int _length) { return default(__String); }

			#endregion

			#region string substr ( string string, int start )

			/// <summary>  
			/// substr() returns the portion of string specified by the start and length parameters.   
			/// </summary>  
			/// <param name="_string">string string</param>  
			/// <param name="_start">int start</param>  
			[Script(IsNative = true)]
			public static __String substr(__String _string, int _start) { return default(__String); }

			#endregion

			#region int strlen ( string string )

			/// <summary>  
			/// Returns the length of the given string.   
			/// </summary>  
			/// <param name="_string">string string</param>  
			[Script(IsNative = true)]
			public static int strlen(__String _string) { return default(int); }

			#endregion

			#region string trim ( string str [, string charlist] )

			/// <summary>  
			/// This function returns a string with whitespace stripped from the beginning and end of str  
			/// </summary>  
			/// <param name="_str">string str</param>  
			[Script(IsNative = true)]
			public static __String trim(__String _str) { return default(__String); }

			#endregion

			#region string strtoupper ( string string )

			/// <summary>  
			/// Returns string with all alphabetic characters converted to uppercase.   
			/// </summary>  
			/// <param name="_string">string string</param>  
			[Script(IsNative = true)]
			public static __String strtoupper(__String _string) { return default(__String); }

			#endregion

			#region string strtolower ( string str )

			/// <summary>  
			/// Returns string with all alphabetic characters converted to lowercase.   
			/// </summary>  
			/// <param name="_str">string str</param>  
			[Script(IsNative = true)]
			public static __String strtolower(__String _str) { return default(__String); }

			#endregion

			#region int strcmp ( string str1, string str2 )

			/// <summary>  
			/// Returns &lt; 0 if str1 is less than str2; &gt; 0 if str1 is greater than str2, and 0 if they are equal.   
			/// </summary>  
			/// <param name="_str1">string str1</param>  
			/// <param name="_str2">string str2</param>  
			[Script(IsNative = true)]
			public static int strcmp(__String _str1, __String _str2) { return default(int); }

			#endregion

		}

		#region substr
		[Script(DefineAsStatic = true)]
		public __String Substring(int a0)
		{
			return API.substr(this, a0);
		}

		[Script(DefineAsStatic = true)]
		public __String Substring(int a0, int a1)
		{
			return API.substr(this, a0, a1);
		}

		[Script(DefineAsStatic = true)]
		public bool EndsWith(__String a0)
		{
			return this.Substring(this.Length - a0.Length) == a0;
		}

		[Script(DefineAsStatic = true)]
		public bool StartsWith(__String a0)
		{
			return this.Substring(0, a0.Length) == a0;
		}

		[Script(DefineAsStatic = true)]
		public string[] Split(params char[] a0)
		{
			string e = Native.API.chr((int)a0[0]);

			return Native.API.explode(e, this);
		}

		#endregion



		[Script(DefineAsStatic = true)]
		public __String Replace(__String a0, __String a1)
		{
			return API.str_replace(a0, a1, this);
		}

		[Script(DefineAsStatic = true)]
		public __String Trim()
		{
			return API.trim(this);
		}

		[Script(OptimizedCode = @"$_0 = strpos({arg0}, {arg1}, {arg2}); return $_0 === false ? -1 : $_0;", UseCompilerConstants = true)]
		public static int API_strpos(__String _haystack, object _needle, int start) { return default(int); }


		[Script(DefineAsStatic = true)]
		public int IndexOf(string e)
		{
			return API_strpos(this, e, 0);
		}

		[Script(DefineAsStatic = true)]
		public int IndexOf(string e, int start)
		{
			return API_strpos(this, e, start);
		}

		[Script(DefineAsStatic = true)]
		public int LastIndexOf(string e)
		{
			return API.strrpos(this, e);
		}

		[Script(DefineAsStatic = true)]
		public __String ToLower()
		{
			return API.strtolower(this);
		}

		[Script(DefineAsStatic = true)]
		public __String ToUpper()
		{
			return API.strtoupper(this);
		}

		[Script(DefineAsStatic = true)]
		public int CompareTo(__String e)
		{
			return API.strcmp(this, e);

		}

		public int Length
		{
			[Script(DefineAsStatic = true)]
			get
			{
				return API.strlen(this);
			}
		}

		[Script(DefineAsStatic = true)]
		public char get_Chars(int i)
		{
			return Native.API.ord(InternalCharAt(this, i));
		}

		[Script(OptimizedCode = "return $e{$i};")]
		static internal string InternalCharAt(__String e, int i)
		{
			return default(string);
		}

		[Script(DefineAsStatic = true)]
		public bool Contains(string a)
		{
			return IndexOf(a) > -1;
		}

		#region Concat
		[Script(OptimizedCode = "return implode($a0, '');")]
		public static __String Concat(string[] a0)
		{
			return default(__String);
		}

		[Script(OptimizedCode = "return implode($a0, '');")]
		public static __String Concat(object[] a0)
		{
			return default(__String);
		}

		[Script(OptimizedCode = "return {arg0}.'';",
			UseCompilerConstants = true)]
		public static __String Concat(object a0)
		{
			return default(__String);
		}

		[Script(OptimizedCode = "return $a0.$a1;")]
		public static __String Concat(object a0, object a1)
		{
			return default(__String);
		}

		[Script(OptimizedCode = "return $a0.$a1.$a2;")]
		public static __String Concat(object a0, object a1, object a2)
		{
			return default(__String);
		}

		[Script(OptimizedCode = "return $a0.$a1;")]
		public static __String Concat(__String a0, __String a1)
		{
			return default(__String);
		}

		[Script(OptimizedCode = "return $a0.$a1.$a2;")]
		public static __String Concat(__String a0, __String a1, __String a2)
		{
			return default(__String);
		}

		[Script(OptimizedCode = "return $a0.$a1.$a2.$a3;")]
		public static __String Concat(__String a0, __String a1, __String a2, __String a3)
		{
			return default(__String);
		}
		#endregion

		#region equal
		[Script(OptimizedCode = "return $a == $b;")]
		public static bool operator ==(__String a, __String b)
		{
			return default(bool);
		}

		[Script(DefineAsStatic = true)]
		public override bool Equals(object obj)
		{
			return this == (__String)obj;
		}

		[Script(OptimizedCode = "return $a != $b;")]
		public static bool operator !=(__String a, __String b)
		{
			return default(bool);
		}

		[Script(DefineAsStatic = true)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion

		[Script(DefineAsStatic = true)]
		public string PadLeft(int total, char c)
		{
			string v = (string)(object)this;

			while (v.Length < total)
				v = InternalConstructor(c, 1) + v;

			return v;
		}

		public static bool IsNullOrEmpty(string e)
		{
			if (e == null)
				return true;

			if (e == "")
				return true;

			return false;
		}
	}
}
