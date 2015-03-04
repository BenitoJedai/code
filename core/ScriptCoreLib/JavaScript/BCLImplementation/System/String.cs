using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using System.IO;
using System.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
	// http://referencesource.microsoft.com/#mscorlib/system/string.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/String.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System/String.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/String.cs

	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\String.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\String.cs
	// X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\String.cs
	// X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\String.cs

	// X:\opensource\github\WootzJs\WootzJs.Runtime\String.cs

	// haha. Purpose: Your favorite String class.

	[Script(Implements = typeof(global::System.String), InternalConstructor = true)]
	internal class __String
	{
		// https://github.com/dotnet/coreclr/blob/master/src/classlibnative/bcltype/stringnative.cpp
		// https://github.com/dotnet/coreclr/blob/master/src/classlibnative/bcltype/stringnative.h

		// X:\jsc.svn\examples\javascript\Test\Test46StringInterpolation\Test46StringInterpolation\Class1.cs

		// X:\jsc.svn\examples\javascript\test\Test46AnonymousTypeToString\Test46AnonymousTypeToString\Class1.cs
		// X:\jsc.svn\examples\javascript\Test\Test435AnonymousToString\Test435AnonymousToString\Class1.cs

		// why isnt there a byte* ctor?
		//  unsafe public extern String(sbyte *value);


		#region String(char c, int count)
		public __String(char c, int count)
		{
		}

		public static string InternalConstructor(char c, int count)
		{
			var w = new StringBuilder();

			for (int i = 0; i < count; i++)
			{
				w.Append(FromCharCode(c));
			}

			return w.ToString();
		}
		#endregion



		#region Format
		public static string Format(string format, object a)
		{
			// fast solution 

			return format.Replace("{0}", "" + a);
		}

		public static string Format(string format, object a, object b)
		{
			// fast solution 

			return format
				.Replace("{0}", "" + a)
				.Replace("{1}", "" + b);
		}

		public static string Format(string format, params object[] b)
		{
			// X:\jsc.svn\examples\javascript\test\Test46AnonymousTypeToString\Test46AnonymousTypeToString\Class1.cs

			// X:\jsc.svn\examples\javascript\Test\TestStringInterpolation\TestStringInterpolation\Application.cs

			// X:\jsc.svn\examples\actionscript\async\Test\TestTaskDelay\TestTaskDelay\ApplicationSprite.cs
			// X:\jsc.svn\examples\javascript\test\TestRoslynAnonymousType\TestRoslynAnonymousType\Class1.cs
			// fast solution 


			var x = format;

			for (int i = 0; i < b.Length; i++)
			{
				var value = b[i];

				// what about {0:x2}
				x = x.Replace("{" + i + "}", Convert.ToString(value));
			}

			return x;
		}
		#endregion



		public static bool IsNullOrWhiteSpace(string e)
		{
			if (e == null)
				return true;

			if (e.Trim() == "")
				return true;

			return false;
		}

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

		[Obsolete]
		[Script(OptimizedCode = "return String.fromCharCode(i);")]
		public static string FromCharCode(int i)
		{
			// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Text\UTF8Encoding.cs

			return default(string);
		}

		public static string __fromCharCode(byte[] bytes)
		{
			// http://stackoverflow.com/questions/20700393/urierror-malformed-uri-sequence
			// X:\jsc.svn\examples\vr\VRTurbanPhotosphere\VRTurbanPhotosphere\ApplicationWebService.cs
			//Console.WriteLine("enter __String.__fromCharCode " + new { bytes.Length });

			// X:\jsc.svn\examples\javascript\Test\TestUTF8GetStringPerformance\TestUTF8GetStringPerformance\Application.cs
			// UTF8.GetString { Length = 1989731 }

			//            String.fromCharCode(40, 41)
			//"()"

			var w = new StringBuilder();

			// return String.fromCharCode(i);


			// { ElapsedMilliseconds = 13, Length = 1000 }
			// { ElapsedMilliseconds = 21, Length = 16384 }
			// { ElapsedMilliseconds = 33, Length = 65536 }
			// GetString { Length = 131072 }
			//bytes = new byte[bytes.Length];

			// etString { Length = 131072 }
			//bytes = new byte[131072];

			//var a = (IArray<byte>)(object)bytes;
			//a.

			// { ElapsedMilliseconds = 20, Length = 65536 }

			var r = new MemoryStream(bytes);
			// https://code.google.com/p/chromium/issues/detail?id=56588
			var chunk = new byte[0x10000];

			var ok = true;
			var s = "";

			while (ok)
			{


				var len = r.Read(chunk, 0, (int)chunk.Length);

				if (len > 0)
				{
					var cm = new MemoryStream();
					cm.Write(chunk, 0, len);

					//Console.WriteLine("GetString chunk " + new { cm.Length });
					//Console.WriteLine("GetString  " + new { s.Length });


					var args = (object[])(object)cm.ToArray();
					// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Text\UTF8Encoding.cs

					// http://jsperf.com/string-fromcharcode-apply-vs-string-fromcharcode-using-
					//  message: "Maximum call stack size exceeded"
					var f = (IFunction)new IFunction("return String.fromCharCode;").apply(null);

					s += (string)f.apply(null, args);
				}
				else
				{
					ok = false;
				}
			}

			//Console.WriteLine("exit __String.__fromCharCode " + new { s });

			return s;
		}


		[Script(DefineAsStatic = true)]
		public int CompareTo(__String e)
		{
			return ScriptCoreLib.JavaScript.Runtime.Expando.Compare(this, e);

		}

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


		#region indexof

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
			return InternalIndexOf(this, new string(c, 1));
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
		#endregion

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
			// is this called by AsEnumerable correctly?
			// X:\jsc.svn\examples\javascript\css\CSSnthSelector\CSSnthSelector\Application.cs

			return (char)GetCharCodeAt((string)(object)this, i);
		}

		[Script(DefineAsStatic = true)]
		public char[] ToCharArray()
		{
			string v = (string)(object)this;

			var text_ToChar = new List<char>();

			foreach (var item in v)
			{

				text_ToChar.Add(item);
			}


			// tested by
			// X:\jsc.svn\examples\javascript\appengine\AppEngineImplicitDataRow\AppEngineImplicitDataRow\Application.cs

			return text_ToChar.ToArray();
		}



		[Script(DefineAsStatic = true)]
		public bool Contains(string a)
		{
			return InternalIndexOf(this, a) > -1;
		}

		#region concat
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

		[Script(OptimizedCode = "return [a0,a1].join('')")]
		public static string Concat(object a0, object a1)
		{
			// this fails with 'illegal access' in chrome app 2013-10-29
			// lets fix it.
			// whats the performance penalty here?

			if (a0 == null)
				if (a1 == null)
					return null;

			if (a0 == null)
				return a1.ToString();

			if (a1 == null)
				return a0.ToString();


			var s0 = a0.ToString();
			var s1 = a1.ToString();

			return s0 + s1;
		}

		[Script(OptimizedCode = "return [a0,a1,a2].join('')")]
		public static string Concat(object a0, object a1, object a2)
		{
			return default(string);
		}


		// X:\jsc.svn\examples\javascript\Test\TestNullStringConcat\TestNullStringConcat\Application.cs
		[Script(OptimizedCode = "return [a0,a1].join('');")]
		public static string Concat(string a0, string a1)
		{
			return default(string);
		}

		[Script(OptimizedCode = "return [a0,a1,a2].join('');")]
		public static string Concat(string a0, string a1, string a2)
		{
			return default(string);
		}

		[Script(OptimizedCode = "return [a0,a1,a2,a3].join('');")]
		public static string Concat(string a0, string a1, string a2, string a3)
		{
			return default(string);
		}

		#endregion




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


		public static string Join(string separator, IEnumerable<string> values)
		{
			// X:\jsc.svn\examples\javascript\forms\SQLiteConsoleExperiment\SQLiteConsoleExperiment\ApplicationControl.cs
			return Join(separator, Enumerable.ToArray(values));
		}

		[Script(OptimizedCode = @"return a1.join(a0);")]
		static public string Join(string a0, string[] a1)
		{
			// X:\jsc.svn\examples\java\test\JVMCLRStringJoin\JVMCLRStringJoin\Program.cs
			return default(string);
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

		[Script(DefineAsStatic = true)]
		public __String Trim()
		{
			if (this == null)
				return default(__String);

			return IRegExp.Trim.replace(this, "");
		}

		[Script(DefineAsStatic = true)]
		public string PadRight(int total)
		{

			return PadRight(total, ' ');
		}

		[Script(DefineAsStatic = true)]
		public string PadLeft(int total)
		{

			return PadLeft(total, ' ');
		}

		[Script(DefineAsStatic = true)]
		public string PadRight(int total, char c)
		{
			string v = (string)(object)this;

			while (v.Length < total)
				v += Convert.ToString(c);

			return v;
		}

		[Script(DefineAsStatic = true)]
		public string PadLeft(int total, char c)
		{
			string v = (string)(object)this;

			while (v.Length < total)
				v = Convert.ToString(c) + v;

			return v;
		}
		#region substr

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
		public __String Remove(int a0)
		{
			return InternalSubstring(this, 0, a0);
		}

		[Script(DefineAsStatic = true)]
		public __String Substring(int a0, int a1)
		{
			return InternalSubstring(this, a0, a1);
		}

		[Script(DefineAsStatic = true)]
		public string[] Split(params char[] e)
		{
			return IArray<string>.Split((string)(object)(this), __String.FromCharCode(e[0]));
		}

		[Script(DefineAsStatic = true)]
		public string[] Split(string[] e, StringSplitOptions o)
		{
			if (e.Length != 1)
				throw new NotImplementedException();

			var x = IArray<string>.Split((string)(object)(this), e[0]);

			if (o == StringSplitOptions.None)
				return x;

			var a = new IArray<string>();

			foreach (var v in x.ToArray())
			{
				if (!string.IsNullOrEmpty(v))
					a.push(v);
			}

			return a.ToArray();
		}

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

		#endregion

		#region equal
		[Script(OptimizedCode = "return a == b")]
		public static bool operator ==(__String a, __String b)
		{
			return default(bool);
		}

		public static bool Equals(string a, string b)
		{
			return a == b;
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
			return base.GetHashCode();
		}
		#endregion
	}


}

