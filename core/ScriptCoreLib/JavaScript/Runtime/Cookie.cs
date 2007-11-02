using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.JavaScript;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.Shared;

namespace ScriptCoreLib.JavaScript.Runtime
{

        

    [Script]
    public class Cookie<T> : Cookie
        where T: class
    {
        public Cookie(string e)
            : base(e)
        {
        }

        EventHandler<Predicate<T>> _spawn_helper;

        public Cookie(string e,  EventHandler<Predicate<T>> def)
            : base(e)
        {
            _spawn_helper = def;
        }

        public static implicit operator T(Cookie<T> p)
        {
            return p.Value;
        }

        /// <summary>
        /// this is a time expensive property
        /// </summary>
        public new T Value
        {
            get
            {
                var x = new ObjectStreamHelper<T>();

                try
                {
                    x.Stream = this.ValueBase64;
                }
                catch
                {

                }
            
                var p = new Predicate<T>();

                p.Target = x.Item;

                p.Invoke(_spawn_helper);

                return p.Target;
            }
            set
            {
                var x = new ObjectStreamHelper<T>();

                x.Item = value;

                this.ValueBase64 = x.Stream;
            }
        }
    }

    [Script]
    public class Cookie
    {
        public readonly string Name;

        public static string PHPSession
        {
            get{
                return new Cookie("PHPSESSID").Value;
            }
        }

        public const string Separator = "$";

        public Cookie this [string e]
        {
            get
            {
                return new Cookie(Name + "$" + e);
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
                return Native.Window.escape(Name);
            }
        }

        public void Delete()
        {
            Native.Document.cookie = EscapedName + "=;expires=" + new IDate( 0 ).toGMTString();

        }

        public int IntegerValue
        {
            get
            {
                int z = int.Parse(Value);
                
                if (Native.Window.isNaN(z))
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
                Value = value ? "true" : "false";
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

        


        public string Value
        {
            get
            {
                if (Native.Document == null)
                    return "";

                string[] s = IArray<string>.Split(Native.Document.cookie, "; ");

                string x = "";

                foreach (string z in s)
                {
                    string[] v = IArray<string>.Split(z, "=");

                    if (v[0] == EscapedName)
                    {
                        x = v[1];

                        break;
                    }
                }

                if (x == null)
                    x = "";

                x = Native.Window.unescape(x);
              

                return x.Trim();
            }
            set
            {
                string o = Value;
                string x = value;

                x = Native.Window.escape(IArray<string>.SplitLines(x)[0].Trim());

                if (o == x)
                    return;


                string v = EscapedName + "=" + x +";path=/;";

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
