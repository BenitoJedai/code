using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using javax.common.runtime;
using System;

namespace ScriptCoreLibJava.BCLImplementation.System
{

	[Script(
		Implements = typeof(global::System.String),
		ImplementationType = typeof(global::java.lang.String),
		InternalConstructor = true

		)]
	internal class __String
	{
		public static bool IsNullOrEmpty(string e)
		{
			if (e == null)
				return true;

			if (e == "")
				return true;

			return false;
		}

		public __String(char[] c)
		{
		}

		public static string InternalConstructor(char[] c)
		{
			var w = new java.lang.String(c);

			return (string)(object)w;
		}


		public __String(char c, int count)
		{
		}

		public static string InternalConstructor(char c, int count)
		{
			var w = new StringBuilder();

			for (int i = 0; i < count; i++)
			{
				w.Append(c);
			}

			return w.ToString();
		}

		[Script(ExternalTarget = "charAt")]
		public char get_Chars(int i)
		{
			return default(char);
		}

		[Script(ExternalTarget = "trim")]
		public string Trim()
		{
			return default(string);
		}

		[Script(DefineAsStatic = true)]
		public string PadLeft(int totalWidth)
		{
			return PadLeft(totalWidth, ' ');
		}

		[Script(DefineAsStatic = true)]
		public string PadLeft(int totalWidth, char paddingChar)
		{
			string u = (string)(object)this;
			string p = __Convert.ToString(paddingChar);

			while (u.Length < totalWidth)
				u = p + u;

			return u;
		}


		[Script(ExternalTarget = "substring")]
		public string Substring(int start)
		{
			return default(string);
		}

		[Script(DefineAsStatic = true)]
		public string Substring(int start, int len)
		{
			global::java.lang.String s = (global::java.lang.String)(object)this;





			return s.substring(start, start + len);
		}

#if JAVA5

        [Script(ExternalTarget = "replace")]
#else
		[Script(DefineAsStatic = true)]
#endif
		public string Replace(string what, string with)
		{
			//return Convert.ReplaceString((string)(object)this, a, b);
			var whom = (string)(object)this;

			int j = -1;
			int i = whom.IndexOf(what);

			if (i == -1)
				return whom;

			var b = "";




			while (i > -1)
			{
				if (j < 0)
					b += whom.Substring(0, i) + with;
				else
					b += whom.Substring(j + what.Length, i - j - what.Length) + with;

				j = i;
				i = whom.IndexOf(what, i + what.Length);
			}

			b += whom.Substring(j + what.Length);

			return b;
		}

		[Script(ExternalTarget = "replace")]
		public string Replace(char a, char b)
		{
			return default(string);
		}

		[Script(DefineAsStatic = true)]
		public bool Contains(string a)
		{
			return IndexOf(a) > -1;
		}


		[Script(ExternalTarget = "indexOf")]
		public int IndexOf(string str)
		{
			return default(int);
		}


		[Script(ExternalTarget = "indexOf")]
		public int IndexOf(string str, int pos)
		{
			return default(int);
		}

		[Script(ExternalTarget = "indexOf")]
		public int IndexOf(char str)
		{
			return default(int);
		}

		[Script(ExternalTarget = "indexOf")]
		public int IndexOf(char str, int pos)
		{
			return default(int);
		}

		[Script(ExternalTarget = "lastIndexOf")]
		public int LastIndexOf(string str)
		{
			return default(int);
		}

		[Script(ExternalTarget = "lastIndexOf")]
		public int LastIndexOf(char str)
		{
			return default(int);
		}

		[Script(DefineAsStatic = true)]
		public string[] Split(char[] e)
		{
			var a = (string)(object)this;

			if (null == a)
				throw new InvalidOperationException();

			return SplitStringByChar(a, e[0]);
		}



		public static string[] SplitStringByChar(string e, char p)
		{
			if (null == e)
				throw new InvalidOperationException();

			var a = new java.util.ArrayList();

			int i = -1;
			bool b = true;

			while (b)
			{
				int j = e.IndexOf(p, i + 1);

				if (j == -1)
				{
					a.add(e.Substring(i + 1));
					b = false;
				}
				else
				{
					a.add(e.Substring(i + 1, j - i - 1));
					i = j;
				}


			}

			return (string[])a.toArray(new string[a.size()]);
		}

		[Script(DefineAsStatic = true)]
		public string[] Split(string[] e, global::System.StringSplitOptions o)
		{
			var a = new global::System.Collections.ArrayList();
			var x = (string)(object)this;

			var i = x.IndexOf(e[0]);

			if (i < 0)
			{
				// all in
				a.Add(x);
			}
			else
			{
				var j = 0;

				while (i >= 0)
				{
					a.Add(x.Substring(j, i - j));
					j = i + e[0].Length;
					i = x.IndexOf(e[0], j);
				}

				a.Add(x.Substring(j));

			}


			return (string[])a.ToArray(typeof(string));
		}

		[Script(ExternalTarget = "startsWith")]
		public bool StartsWith(string prefix)
		{
			return default(bool);
		}

		[Script(ExternalTarget = "endsWith")]
		public bool EndsWith(string suffix)
		{
			return default(bool);
		}

		public static bool Equals(string e, string f)
		{
			if (e == null)
				return null == f;

			return e == f;
		}

		[Script(ExternalTarget = "equals")]
		public bool Equals(string e)
		{
			return default(bool);
		}

		[Script(ExternalTarget = "compareTo")]
		public int CompareTo(string e)
		{
			return default(int);
		}

		[Script(ExternalTarget = "toUpperCase")]
		public string ToUpper()
		{
			return default(string);
		}

		[Script(ExternalTarget = "toLowerCase")]
		public string ToLower()
		{
			return default(string);
		}

		[Script(ExternalTarget = "equals", DefineAsInstance = true)]
		public static bool operator ==(__String a, __String b)
		{
			return default(bool);
		}


		public static bool operator !=(__String a, __String b)
		{
			return !(a == b);
		}

		public int Length
		{
			[Script(ExternalTarget = "length")]
			get
			{
				return default(int);
			}
		}


		public static string Concat(params object[] e)
		{
			var b = new global::java.lang.StringBuffer();

			foreach (object v in e)
			{
				b.append(v);
			}

			return b.ToString();
		}


		public static string Concat(params string[] e)
		{
			var b = new global::java.lang.StringBuffer();

			foreach (object v in e)
			{
				b.append(v);
			}

			return b.ToString();
		}



		//[Script(DefineAsStatic = true)]
		public static string Concat(object a)
		{
			if (a == null)
			{

				return null;
			}




			return a.ToString();
		}

		[Script(
			StringConcatOperator = "+")]
		public static string Concat(object a, object b)
		{
			if (a == null)
			{
				if (b == null)
					return null;

				return b.ToString();
			}
			else
			{
				if (b == null)
					return a.ToString();
			}



			return a.ToString() + b.ToString();
		}

		[Script(
			StringConcatOperator = "+"
			)]
		public static string Concat(string a, string b)
		{
			return default(string);
		}

		[Script(
			StringConcatOperator = "+"
			)]
		public static string Concat(string a, string b, string c)
		{
			return default(string);
		}

		[Script(
			StringConcatOperator = "+")]
		public static string Concat(string a, string b, string c, string d)
		{
			return default(string);
		}

		[Script(
			StringConcatOperator = "+")]
		public static string Concat(object a, object b, object c)
		{
			return default(string);
		}
	}

}
