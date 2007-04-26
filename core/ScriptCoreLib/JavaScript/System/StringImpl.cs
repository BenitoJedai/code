using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM;


namespace ScriptCoreLib.JavaScript.System
{
    [Script(Implements=typeof(global::System.String))]
    internal class StringImpl 
    {
        public static string Format(string format, object a, object b)
        {
            // fast solution 

            return format.Replace("{0}", "" + a).Replace("{1}", "" + b);
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

        [Script(OptimizedCode = "return String.fromCharCode(i);")]
        public static string FromCharCode(int i)
        {
            return default(string);

        }


        [Script(DefineAsStatic = true)]
        public int CompareTo(StringImpl e)
        {
            return Expando.Compare(this, e);
            
        }

        [Script(OptimizedCode="return e.charAt(i);")]
        static internal char InternalCharAt(StringImpl e, int i)
        {
            return default(char);
        }

        

        [Script(OptimizedCode = "return e.length;")]
        static internal int InternalLength(StringImpl e)
        {
            return default(int);
        }

        [Script(OptimizedCode = "return e.indexOf(c);")]
        static internal int InternalIndexOf(StringImpl e, object c)
        {
            return default(int);
        }



        [Script(DefineAsStatic=true)]
        public int IndexOf(char c)
        {
            return InternalIndexOf(this, c);
        }

        [Script(DefineAsStatic = true)]
        public int IndexOf(string c)
        {
            return InternalIndexOf(this, c);
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
            return InternalCharAt(this, i);
        }

        [Script(DefineAsStatic = true)]
        public  bool Contains(string a)
        {
            return InternalIndexOf(this, a) > -1;
        }

        #region concat
        [Script(OptimizedCode = "return a0.join('');")]
        public static StringImpl Concat(object[] a0)
        {
            return default(StringImpl);
        }

        [Script(OptimizedCode = "return {arg0}+'';",
                UseCompilerConstants=true)]
        public static StringImpl Concat(object a0)
        {
            return default(StringImpl);
        }

        [Script(OptimizedCode = "return a0+a1")]
        public static StringImpl Concat(object a0, object a1)
        {
            return default(StringImpl);
        }

        [Script(OptimizedCode = "return a0+a1+a2")]
        public static StringImpl Concat(object a0, object a1, object a2)
        {
            return default(StringImpl);
        }

        [Script(OptimizedCode = "return a0+a1")]
        public static StringImpl Concat(StringImpl a0, StringImpl a1)
        {
            return default(StringImpl);
        }

        [Script(OptimizedCode = "return a0+a1+a2")]
        public static StringImpl Concat(StringImpl a0, StringImpl a1, StringImpl a2)
        {
            return default(StringImpl);
        }

        [Script(OptimizedCode = "return a0+a1+a2+a3")]
        public static StringImpl Concat(StringImpl a0, StringImpl a1, StringImpl a2, StringImpl a3)
        {
            return default(StringImpl);
        }

        #endregion
        [Script(DefineAsStatic = true, OptimizedCode="return a0.split(a1).join(a2)")]
        internal object InternalReplace(object a0, object a1, object a2)
        {
            return default(object);
        }
      
        [Script(DefineAsStatic = true)]
        public StringImpl Replace(StringImpl a0, StringImpl a1)
        {
            return (StringImpl)InternalReplace(this, a0, a1);
        }

        [Script(OptimizedCode=@"return a1.join(a0);")]
        static public StringImpl Join(StringImpl a0, StringImpl[] a1)
        {
            return default(StringImpl);
        }

        [Script(NoDecoration=true)]
        internal StringImpl toLowerCase()
        {
            return default(StringImpl);
        }

        [Script(NoDecoration = true)]
        internal StringImpl toUpperCase()
        {
            return default(StringImpl);
        }

        [Script(DefineAsStatic = true)]
        public StringImpl ToLower()
        {
            return toLowerCase();
        }

        [Script(DefineAsStatic = true)]
        public StringImpl ToUpper()
        {
            return toUpperCase();
        }

        [Script(DefineAsStatic = true)]
        public StringImpl Trim()
        {
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
        internal static StringImpl InternalSubstring(StringImpl a0, int a1)
        {
            return default(StringImpl);
        }

        [Script(OptimizedCode = "return a0.substr(a1, a2);")]
        internal static StringImpl InternalSubstring(StringImpl a0, int a1, int a2)
        {
            return default(StringImpl);
        }

        [Script(DefineAsStatic = true)]
        public StringImpl Substring(int a0)
        {
            return InternalSubstring(this, a0);
        }


        [Script(DefineAsStatic = true)]
        public StringImpl Substring(int a0, int a1)
        {
            return InternalSubstring(this, a0, a1);
        }

        [Script(DefineAsStatic = true)]
        public string[] Split(params char[] e)
        {
            return IArray<string>.Split((string)(object)(this), StringImpl.FromCharCode(e[0]));
        }

        [Script(DefineAsStatic = true)]
        public bool EndsWith(StringImpl a0)
        {
            return InternalSubstring(this, this.Length - a0.Length) == a0;
        }

        [Script(DefineAsStatic = true)]
        public bool StartsWith(StringImpl a0)
        {
            return InternalSubstring(this, 0, a0.Length) == a0;
        }

        #endregion

        #region equal
        [Script(OptimizedCode = "return a == b")]
        public static bool operator ==(StringImpl a, StringImpl b)
        {
            return default(bool);
        }

        [Script(DefineAsStatic = true)]
        public override bool Equals(object obj)
        {
            return this == (StringImpl)obj;
        }

        [Script(OptimizedCode = "return a != b")]
        public static bool operator !=(StringImpl a, StringImpl b)
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
