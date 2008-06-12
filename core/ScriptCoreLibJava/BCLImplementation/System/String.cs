using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using javax.common.runtime;

namespace ScriptCoreLibJava.BCLImplementation.System
{

    [Script(
        Implements = typeof(global::System.String),
        ImplementationType = typeof(java.lang.String))]
    internal class __String
    {
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
        public string PadLeft(int totalWidth, char paddingChar)
        {
            string u = (string)(object)this;
            string p = Convert.ToString(paddingChar);

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
            java.lang.String s = (java.lang.String)(object)this;



            return s.substring(start, start + len);
        }

#if JAVA5

        [Script(ExternalTarget = "replace")]
#else
        [Script(DefineAsStatic = true)]
#endif
        public string Replace(string a, string b)
        {
            return Convert.ReplaceString((string)(object)this, a, b);
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
            object a = (object)this;
            return Convert.SplitStringByChar((string)a, e[0]);
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
            java.lang.StringBuffer b = new java.lang.StringBuffer();

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
