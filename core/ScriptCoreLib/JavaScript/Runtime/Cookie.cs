using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.JavaScript;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.Shared;
using System;
using System.Collections.Specialized;

namespace ScriptCoreLib.JavaScript.Runtime
{



    //[Script]
    //public class Cookie<T> : Cookie
    //    where T : class
    //{
    //    public Cookie(string e)
    //        : base(e)
    //    {
    //    }

    //    //System.Action<Predicate<T>> _spawn_helper;

    //    //public Cookie(string e, System.Action<Predicate<T>> def)
    //    //    : base(e)
    //    //{
    //    //    _spawn_helper = def;
    //    //}

    //    public static implicit operator T(Cookie<T> p)
    //    {
    //        return p.Value;
    //    }

    //    /// <summary>
    //    /// this is a time expensive property
    //    /// </summary>
    //    public new T Value
    //    {
    //        get
    //        {
    //            var x = new ObjectStreamHelper<T>();

    //            try
    //            {
    //                x.Stream = this.ValueBase64;
    //            }
    //            catch
    //            {

    //            }

    //            var p = new Predicate<T>();

    //            p.Target = x.Item;

    //            p.Invoke(_spawn_helper);

    //            return p.Target;
    //        }
    //        set
    //        {
    //            var x = new ObjectStreamHelper<T>();

    //            x.Item = value;

    //            this.ValueBase64 = x.Stream;
    //        }
    //    }
    //}

    [Script]
    public class Cookie
    {
        public readonly string Name;

        [Obsolete]
        public static string PHPSession
        {
            get
            {
                return new Cookie("PHPSESSID").Value;
            }
        }



        public Cookie(string name)
        {
            this.Name = name;
        }

        string EscapedName
        {
            get
            {
                return Native.window.escape(Name);
            }
        }


        // dispose?
        public void Delete()
        {
            Native.document.cookie = EscapedName + "=;expires=" + new IDate(0).toGMTString();

        }

        public int IntegerValue
        {
            get
            {
                int z = int.Parse(Value);

                if (Native.window.isNaN(z))
                    return 0;

                return z;
            }
            set
            {
                Value = value.ToString();
            }
        }


        public bool BooleanValue
        {
            get
            {
                return Value == "true";
            }
            set
            {
                if (value)

                    Value = "true";
                else
                    Value = "false";
            }
        }

        public string ValueBase64
        {
            get
            {
                return Convert.FromBase64String(Value);
            }
            set
            {
                Value = Convert.ToBase64String(value);
            }
        }



        public const string Separator = "$";

        public Cookie this[string e]
        {
            get
            {
                return new Cookie(Name + "$" + e);
            }
        }

        public static NameValueCollection GetValues(string value)
        {
            Console.WriteLine("Cookie GetValues " + new { value });

            // 
            var n = new NameValueCollection();

            // _fields=IdentityToken=1235363739&foo=bar;

            if (!string.IsNullOrEmpty(value))
            {
                foreach (var z in value.Split('&'))
                {
                    var i = z.IndexOf("=");

                    if (i > -1)
                    {
                        var v0 = Native.window.unescape(z.Substring(0, i));
                        var v1 = Native.window.unescape(z.Substring(i + 1));

                        n[v0] = v1;
                    }
                }
            }


            return n;
        }

        public NameValueCollection Values
        {
            get
            {
                var value = InternalGetValue();

                return GetValues(value);
            }
        }

        public string InternalGetValue()
        {
            if (Native.document == null)
                return "";

            var cookie = Native.document.cookie;

            // tested by
            // X:\jsc.svn\examples\javascript\IdentityTokenFromWebService\IdentityTokenFromWebService\Application.cs

            // cookie = Password=mypassword; _fields=IdentityToken=1235363739&foo=bar; xx=yy }

            var s = cookie.Split(new[] { "; " }, StringSplitOptions.None);
            var x = "";

            foreach (string z in s)
            {
                var i = z.IndexOf("=");

                if (i > -1)
                {
                    var v0 = z.Substring(0, i);

                    if (v0 == EscapedName)
                    {
                        x = z.Substring(i + 1);

                        break;
                    }
                }
            }

            if (x == null)
                x = "";

            return x;
        }

        public string Value
        {
            get
            {
                var x = InternalGetValue();

                x = Native.window.unescape(x);


                return x.Trim();
            }
            set
            {
                string o = Value;
                string x = value;

                x = Native.window.escape(IArray<string>.SplitLines(x)[0].Trim());

                if (o == x)
                    return;


                string v = EscapedName + "=" + x + ";path=/;";

                Native.Document.cookie = v;


                //if (x != null && x != "")
                //{
                //    if (Value == x)
                //        return;

                //    throw new System.ScriptException(
                //        "Cookie could not be set from \n\t" + o + " (" + o.Length + ") to \n\t"  + x + " (" + x.Length + ")\n\n " + v);

                //}
            }
        }
    }
}
