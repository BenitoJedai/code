using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/string.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/String.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\String.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\String.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\String.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\String.cs

    // X:\opensource\github\WootzJs\WootzJs.Runtime\String.cs
    // haha. Purpose: Your favorite String class.

    [Script(
        Implements = typeof(global::System.String),
        ImplementationType = typeof(global::java.lang.String),
        InternalConstructor = true

        )]
    internal class __String
    {
        static public string Join(string a0, string[] a1)
        {
            //20140514
            // X:\jsc.svn\examples\java\test\JVMCLRStringJoin\JVMCLRStringJoin\Program.cs
            // android/java does not have it. need to implement it!

            var w = new StringBuilder();

            for (int i = 0; i < a1.Length; i++)
            {
                if (i > 0)
                    w.Append(a0);

                w.Append(a1[i]);
            }

            return w.ToString();
        }

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
            string p = new string(new[] { paddingChar });

            while (u.Length < totalWidth)
                u = p + u;

            return u;
        }


        // added 20140831
        // tested by 
        // X:\jsc.svn\examples\java\hybrid\JVMCLRRSACryptoServiceProvider\JVMCLRRSACryptoServiceProvider\Program.cs

        [Script(DefineAsStatic = true)]
        public string PadRight(int totalWidth)
        {
            return PadRight(totalWidth, ' ');
        }

        [Script(DefineAsStatic = true)]
        public string PadRight(int totalWidth, char paddingChar)
        {
            string u = (string)(object)this;
            string p = new string(new[] { paddingChar });

            while (u.Length < totalWidth)
                u = u + p;

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

        // no need to support non java5? its 2014!
#if JAVA5

        [Script(ExternalTarget = "replace")]
#else
        [Script(DefineAsStatic = true)]
#endif
        public string Replace(string what, string with)
        {
            // tested by
            // X:\jsc.svn\core\ScriptCoreLibJava\Extensions\BCLImplementationExtensions.cs

            //Console.WriteLine("enter Replace");
            //Console.WriteLine("what: " + what);
            //Console.WriteLine("with: " + with);

            //return Convert.ReplaceString((string)(object)this, a, b);
            var whom = (string)(object)this;

            //Console.WriteLine("whom: " + whom);

            int j = -1;
            int i = whom.IndexOf(what);

            if (i == -1)
                return whom;

            var output = "";


            //enter Replace
            //what: .
            //with: /
            //whom: ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices_Func
            //i: 17
            //output: ScriptCoreLibJava/
            //i: 35
            //java.lang.StringIndexOutOfBoundsException: String index out of range: -1

            while (i > -1)
            {
                //Console.WriteLine("i: " + i);

                if (j < 0)
                {
                    output += whom.Substring(0, i) + with;
                    //Console.WriteLine("output: " + output);
                }
                else
                {
                    //Console.WriteLine("j: " + j);

                    var startIndex = j + what.Length;
                    var length = i - j - what.Length;

                    //Console.WriteLine("startIndex: " + startIndex);
                    //Console.WriteLine("length: " + length);

                    output += whom.Substring(startIndex, length) + with;
                    //Console.WriteLine("output: " + output);
                }

                j = i;
                i = whom.IndexOf(what, i + what.Length);
            }

            //Console.WriteLine("j: " + j);
            output += whom.Substring(j + what.Length);

            return output;
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
            if ((object)e == null)
                return null == (object)f;

            return InternalEquals(e, f);
        }

        [Script(ExternalTarget = "equals", DefineAsInstance = true)]
        public static bool InternalEquals(string e, string f)
        {
            return default(bool);
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

        public static bool operator ==(__String a, __String b)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140111-iquery

            return Equals((string)(object)a, (string)(object)b);
        }


        public static bool operator !=(__String a, __String b)
        {
            return !Equals((string)(object)a, (string)(object)b);
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
                if (v != null)
                    b.append(v);
            }

            return b.ToString();
        }


        public static string Concat(params string[] e)
        {
            var b = new global::java.lang.StringBuffer();

            foreach (string v in e)
            {
                if (!string.IsNullOrEmpty(v))
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

        //[Script(
        //    StringConcatOperator = "+")]
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



            return __String.Concat(
                 e: new object[] { a, b }
          );
        }

        //[Script(
        //    StringConcatOperator = "+"
        //    )]
        public static string Concat(string a, string b)
        {
            // X:\jsc.svn\examples\java\CLRJVMNullString\CLRJVMNullString\Program.cs

            return __String.Concat(
                e: new string[] { a, b }
            );
        }



        //[Script(
        //    StringConcatOperator = "+"
        //    )]
        public static string Concat(string a, string b, string c)
        {
            return __String.Concat(
                 e: new string[] { a, b, c }
            );
        }

        //[Script(
        //    StringConcatOperator = "+")]
        public static string Concat(string a, string b, string c, string d)
        {
            return __String.Concat(
                    e: new string[] { a, b, c, d }
             );
        }

        //[Script(
        //    StringConcatOperator = "+")]
        public static string Concat(object a, object b, object c)
        {
            return __String.Concat(
                         e: new object[] { a, b, c }
                  );
        }


        [Script(DefineAsStatic = true)]
        public char[] ToCharArray()
        {
            var x = (string)(object)this;
            var a = new char[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                a[i] = x[i];
            }
            return a;
        }



		//X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs

		// x:\jsc.svn\examples\javascript\webcamavatarsexperiment\webcamavatarsexperiment\application.cs


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


		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150323
		// x:\jsc.svn\examples\javascript\test\test46anonymoustypetostring\test46anonymoustypetostring\class1.cs
		// 4.6	
		public static string Format(IFormatProvider provider, string format, object[] args)
		{
			// called by anonymous type tostring
			return Format(format, args);
		}

		//script: error JSC1000: No implementation found for this native method, please implement[static System.String.Format(System.String, System.Object, System.Object, System.Object)]
		public static string Format(string format, object args0, object args1, object args2)
		{
			// X:\jsc.svn\examples\javascript\WebGL\collada\WebGLRah66Comanche\WebGLRah66Comanche\Library\ZeProperties.cs
			// called by anonymous type tostring
			return Format(format, new[] { args0, args1, args2 });
		}
		#endregion


	}

}
