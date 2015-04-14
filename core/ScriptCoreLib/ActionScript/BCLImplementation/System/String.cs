using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/string.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/String.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/String.cs

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\String.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\String.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\String.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\String.cs

    // X:\opensource\github\WootzJs\WootzJs.Runtime\String.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/String.cs
    // https://github.com/sq/JSIL/blob/master/Proxies/String.cs
    // https://github.com/bridgedotnet/Bridge/blob/master/Bridge/System/String.cs

    // haha. Purpose: Your favorite String class.

    [Script(
        Implements = typeof(global::System.String),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.String),
        InternalConstructor = true
        )]
    internal class __String
    {
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

            // this will be too slow :)
			while (v.Length < total)
				v += Convert.ToString(c);

			return v;
		}

		[Script(DefineAsStatic = true)]
		public string PadLeft(int total, char c)
		{
			string v = (string)(object)this;

            // this will be too slow :)
            while (v.Length < total)
				v = Convert.ToString(c) + v;

			return v;
		}

        public __String(char[] c)
        {
        }

        public static string InternalConstructor(char[] c)
        {
            var w = new StringBuilder();

            for (int i = 0; i < c.Length; i++)
            {
                w.Append(FromCharCode(c[i]));
            }

            return w.ToString();
        }

        public __String(char c, int count)
        {
        }

        public static string InternalConstructor(char c, int count)
        {
            var w = new StringBuilder();
            var s = FromCharCode(c);

            for (int i = 0; i < count; i++)
            {
                w.Append(s);
            }

            return w.ToString();
        }



        [Script(DefineAsStatic = true, OptimizedCode = "return e.split(f).join(t);")]
        internal string InternalReplace(object e, object f, object t)
        {
            return default(string);
        }

        [Script(DefineAsStatic = true)]
        public string Replace(string a0, string a1)
        {
            return InternalReplace(this, a0, a1);
        }

        public static string Concat(object arg0)
        {
            if (arg0 == null)
            {
                return "";
            }
            return arg0.ToString();
        }


        [Script(ExternalTarget = "localeCompare")]
        public int CompareTo(string strB)
        {
            return default(int);
        }


        [Script(StringConcatOperator = "+")]
        public static string Concat(object a, object b, object c)
        {
            return default(string);
        }

        #region Concat
        [Script(StringConcatOperator = "+")]
        public static string Concat(object a, object b)
        {
            return default(string);
        }


        [Script(StringConcatOperator = "+")]
        public static string Concat(string a, string b)
        {
            return default(string);
        }

        [Script(StringConcatOperator = "+")]
        public static string Concat(string a, string b, string c)
        {
            return default(string);
        }

        [Script(StringConcatOperator = "+")]
        public static string Concat(string a, string b, string c, string d)
        {
            return default(string);
        }

        [Script(OptimizedCode = "return e.join('');")]
        public static string Concat(string[] e)
        {
            return default(string);
        }

        [Script(OptimizedCode = "return e.join('');")]
        public static string Concat(object[] e)
        {
            return default(string);
        }

        #endregion


        public int Length
        {
            [Script(ExternalTarget = "length")]
            get
            {
                return default(int);
            }
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

        [Script(DefineAsStatic = true)]
        public int IndexOf(char c)
        {
            return ((string)(object)this).IndexOf(new string(c, 1));
        }

        [Script(ExternalTarget = "indexOf")]
        public int IndexOf(string str)
        {
            return default(int);
        }

        [Script(ExternalTarget = "indexOf")]
        public int IndexOf(string str, int start)
        {
            return default(int);
        }

        [Script(ExternalTarget = "lastIndexOf")]
        public int LastIndexOf(string str)
        {
            return default(int);
        }

        [Script(ExternalTarget = "substr")]
        public string Substring(int start)
        {
            return default(string);
        }


        [Script(ExternalTarget = "substr")]
        public string Substring(int start, int length)
        {
            return default(string);
        }

        #region Trim
        static RegExp TrimExpCache;

        static public RegExp TrimExp
        {
            get
            {
                if (TrimExpCache == null)
                    TrimExpCache = new RegExp(@"^\s*|\s*$", "g");

                return TrimExpCache;
            }
        }

        [Script(DefineAsStatic = true)]
        public string Trim()
        {
            return ((ActionScript.String)(object)this).replace(TrimExp, "");
        }
        #endregion


        [Script(DefineAsStatic = true)]
        public char get_Chars(int i)
        {
            return (char)GetCharCodeAt((string)(object)this, i);
        }

        [Script(OptimizedCode = "return e.charCodeAt(o);")]
        public static int GetCharCodeAt(string e, int o)
        {
            return default(int);
        }

        [Script(OptimizedCode = "return String.fromCharCode(i);")]
        public static string FromCharCode(int i)
        {
            // X:\jsc.svn\examples\javascript\test\TestPackageAsApplication\TestPackageAsApplication\Application.cs

            return default(string);
        }

        public static bool IsNullOrEmpty(string e)
        {
            if (e == null)
                return true;

            if (e == "")
                return true;

            return false;
        }

        [Script(DefineAsStatic = true)]
        public string[] Split(params char[] c)
        {
            var z = new string(c);

            return Split(new[] { z }, StringSplitOptions.None);
        }

        [Script(DefineAsStatic = true)]
        public string[] Split(string[] e, StringSplitOptions o)
        {
            if (e.Length != 1)
                throw new NotImplementedException("");


            var x = ((ActionScript.String)(object)this).split(e[0]);

            if (o == StringSplitOptions.None)
                return x;

            var a = new Array();

            foreach (var v in x)
            {
                if (!string.IsNullOrEmpty(v))
                    a.push(v);
            }


            return (string[])(object)a;
        }

        [Script(DefineAsStatic = true)]
        public bool Contains(string a)
        {
            return IndexOf(a) > -1;
        }

        #region equal
        [Script(OptimizedCode = "return a == b")]
        public static bool operator ==(__String a, __String b)
        {
            return default(bool);
        }

        [Script(OptimizedCode = "return a != b")]
        public static bool operator !=(__String a, __String b)
        {
            return default(bool);
        }

        [Script(DefineAsStatic = true)]
        public override bool Equals(object obj)
        {
            return this == (__String)obj;
        }

        [Script(DefineAsStatic = true)]
        public override int GetHashCode()
        {
            throw new NotSupportedException("");
        }
        #endregion

        [Script(DefineAsStatic = true)]
        public bool StartsWith(string value)
        {
            return this.Substring(0, value.Length) == value;
        }

        [Script(DefineAsStatic = true)]
        public bool EndsWith(string value)
        {
            return this.Substring(this.Length - value.Length) == value;
        }



        // X:\jsc.svn\core\ScriptCoreLib\ActionScript\Extensions\CommonExtensions.cs
        [Script(OptimizedCode = @"return a1.join(a0);")]
        static public string Join(string a0, string[] a1)
        {
            // X:\jsc.svn\examples\java\test\JVMCLRStringJoin\JVMCLRStringJoin\Program.cs
            return default(string);
        }
    }
}
