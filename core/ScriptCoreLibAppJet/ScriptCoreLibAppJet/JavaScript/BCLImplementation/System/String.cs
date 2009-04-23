using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibAppJet.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.String), InternalConstructor = true)]
    internal class __String
    {
		//public __String(char c, int count)
		//{
		//}

		//public static string InternalConstructor(char c, int count)
		//{
		//    var w = new StringBuilder();

		//    for (int i = 0; i < count; i++)
		//    {
		//        w.Append(FromCharCode(c));
		//    }

		//    return w.ToString();
		//}

		//public static string Format(string format, object a)
		//{
		//    // fast solution 

		//    return format.Replace("{0}", "" + a);
		//}

		//public static string Format(string format, object a, object b)
		//{
		//    // fast solution 

		//    return format
		//        .Replace("{0}", "" + a)
		//        .Replace("{1}", "" + b);
		//}

		//public static string Format(string format, params object[] b)
		//{
		//    // fast solution 


		//    var x = format;

		//    for (int i = 0; i < b.Length; i++)
		//    {
		//        x = x.Replace("{" + i + "}", b[i].ToString());
		//    }

		//    return x;
		//}


		public static bool IsNullOrEmpty(string e)
		{
			if (e == null)
				return true;

			if (e == "")
				return true;

			return false;
		}

		// http://www.pageresource.com/jscript/jstring2.htm

		[Script(OptimizedCode = "return e.charCodeAt(o);")]
		public static int GetCharCodeAt(string e, int o)
		{
			return default(int);
		}

		[Script(OptimizedCode = "return String.fromCharCode(i);")]
		public static string FromCharCode(int i)
		{
			return default(string);
		}


		//[Script(DefineAsStatic = true)]
		//public int CompareTo(__String e)
		//{
		//    return Expando.Compare(this, e);

		//}

		[Script(OptimizedCode = "return e.charAt(i);")]
		static internal char InternalCharAt(__String e, int i)
		{
			return default(char);
		}



		[Script(OptimizedCode = "return e.length;")]
		static internal int InternalLength(__String e)
		{
			return default(int);
		}

		[Script(OptimizedCode = "return e.lastIndexOf(c);")]
		static internal int InternalLastIndexOf(__String e, object c)
		{
			return default(int);
		}

		[Script(OptimizedCode = "return e.indexOf(c);")]
		static internal int InternalIndexOf(__String e, object c)
		{
			return default(int);
		}

		[Script(OptimizedCode = "return e.indexOf(c, pos);")]
		static internal int InternalIndexOf(__String e, object c, int pos)
		{
			return default(int);
		}

		[Script(DefineAsStatic = true)]
		public int LastIndexOf(string c)
		{
			return InternalLastIndexOf(this, c);
		}

		[Script(DefineAsStatic = true)]
		public int IndexOf(char c)
		{
			return InternalIndexOf(this, c);
		}

		[Script(DefineAsStatic = true)]
		public int IndexOf(string c)
		{
			return InternalIndexOf(this, c);
		}

		[Script(DefineAsStatic = true)]
		public int IndexOf(string c, int pos)
		{
			return InternalIndexOf(this, c, pos);
		}

		public int Length
		{
			[Script(DefineAsStatic = true)]
			get
			{
				return InternalLength(this);
			}
		}

		[Script(DefineAsStatic = true)]
		public char get_Chars(int i)
		{
			return (char)GetCharCodeAt((string)(object)this, i);
		}

		[Script(DefineAsStatic = true)]
		public bool Contains(string a)
		{
			return InternalIndexOf(this, a) > -1;
		}

		//#region concat
		[Script(OptimizedCode = "return a0.join('');")]
		public static string Concat(string[] a0)
		{
			return default(string);
		}

		[Script(OptimizedCode = "return a0.join('');")]
		public static string Concat(object[] a0)
		{
			return default(string);
		}

		[Script(OptimizedCode = "return {arg0}+'';",
				UseCompilerConstants = true)]
		public static string Concat(object a0)
		{
			return default(string);
		}

		[Script(OptimizedCode = "return a0+a1")]
		public static string Concat(object a0, object a1)
		{
			return default(string);
		}

		[Script(OptimizedCode = "return a0+a1+a2")]
		public static string Concat(object a0, object a1, object a2)
		{
			return default(string);
		}

		[Script(OptimizedCode = "return a0+a1")]
		public static string Concat(string a0, string a1)
		{
			return default(string);
		}

		[Script(OptimizedCode = "return a0+a1+a2")]
		public static string Concat(string a0, string a1, string a2)
		{
			return default(string);
		}

		[Script(OptimizedCode = "return a0+a1+a2+a3")]
		public static string Concat(string a0, string a1, string a2, string a3)
		{
			return default(string);
		}

		//#endregion
		[Script(DefineAsStatic = true, OptimizedCode = "return a0.split(a1).join(a2)")]
		internal object InternalReplace(object a0, object a1, object a2)
		{
		    return default(object);
		}

		[Script(DefineAsStatic = true)]
		public __String Replace(__String a0, __String a1)
		{
		    return (__String)InternalReplace(this, a0, a1);
		}

		[Script(OptimizedCode = @"return a1.join(a0);")]
		static public __String Join(__String a0, __String[] a1)
		{
		    return default(__String);
		}

		[Script(NoDecoration = true)]
		internal __String toLowerCase()
		{
		    return default(__String);
		}

		[Script(NoDecoration = true)]
		internal __String toUpperCase()
		{
		    return default(__String);
		}

		[Script(DefineAsStatic = true)]
		public __String ToLower()
		{
		    return toLowerCase();
		}

		[Script(DefineAsStatic = true)]
		public __String ToUpper()
		{
		    return toUpperCase();
		}

		//[Script(DefineAsStatic = true)]
		//public __String Trim()
		//{
		//    if (this == null)
		//        return default(__String);

		//    return IRegExp.Trim.replace(this, "");
		//}

		//[Script(DefineAsStatic = true)]
		//public string PadRight(int total)
		//{

		//    return PadRight(total, ' ');
		//}

		//[Script(DefineAsStatic = true)]
		//public string PadLeft(int total)
		//{

		//    return PadLeft(total, ' ');
		//}

		//[Script(DefineAsStatic = true)]
		//public string PadRight(int total, char c)
		//{
		//    string v = (string)(object)this;

		//    while (v.Length < total)
		//        v += Convert.ToString(c);

		//    return v;
		//}

		//[Script(DefineAsStatic = true)]
		//public string PadLeft(int total, char c)
		//{
		//    string v = (string)(object)this;

		//    while (v.Length < total)
		//        v = Convert.ToString(c) + v;

		//    return v;
		//}
		//#region substr

		[Script(OptimizedCode = "return a0.substr(a1);")]
		internal static __String InternalSubstring(__String a0, int a1)
		{
			return default(__String);
		}

		[Script(OptimizedCode = "return a0.substr(a1, a2);")]
		internal static __String InternalSubstring(__String a0, int a1, int a2)
		{
			return default(__String);
		}

		[Script(DefineAsStatic = true)]
		public __String Substring(int a0)
		{
			return InternalSubstring(this, a0);
		}


		[Script(DefineAsStatic = true)]
		public __String Substring(int a0, int a1)
		{
			return InternalSubstring(this, a0, a1);
		}

		//[Script(DefineAsStatic = true)]
		//public string[] Split(params char[] e)
		//{
		//    return IArray<string>.Split((string)(object)(this), __String.FromCharCode(e[0]));
		//}

		//[Script(DefineAsStatic = true)]
		//public string[] Split(string[] e, StringSplitOptions o)
		//{
		//    if (e.Length != 1)
		//        throw new NotImplementedException();

		//    var x = IArray<string>.Split((string)(object)(this), e[0]);

		//    if (o == StringSplitOptions.None)
		//        return x;

		//    var a = new IArray<string>();

		//    foreach (var v in x.ToArray())
		//    {
		//        if (!string.IsNullOrEmpty(v))
		//            a.push(v);
		//    }

		//    return a.ToArray();
		//}

		[Script(DefineAsStatic = true)]
		public bool EndsWith(__String a0)
		{
			return InternalSubstring(this, this.Length - a0.Length) == a0;
		}

		[Script(DefineAsStatic = true)]
		public bool StartsWith(__String a0)
		{
			return InternalSubstring(this, 0, a0.Length) == a0;
		}

		//#endregion

        #region equal
        [Script(OptimizedCode = "return a == b")]
        public static bool operator ==(__String a, __String b)
        {
            return default(bool);
        }

        [Script(DefineAsStatic = true)]
        public override bool Equals(object obj)
        {
            return this == (__String)obj;
        }

        [Script(OptimizedCode = "return a != b")]
        public static bool operator !=(__String a, __String b)
        {
            return default(bool);
        }

        [Script(DefineAsStatic = true)]
        public override int GetHashCode()
        {
			return default(int);
			//return base.GetHashCode();
        }
        #endregion
    }


}

