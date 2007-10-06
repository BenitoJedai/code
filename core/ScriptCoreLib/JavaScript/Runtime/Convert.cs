using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;


using ScriptCoreLib.Shared;

namespace ScriptCoreLib.JavaScript.Runtime
{
    using ScriptCoreLib.JavaScript.BCLImplementation.System;

    // http://www.devguru.com/Technologies/ecmascript/quickref/js_property.html

    [Script]
    public static class Convert 
    {
        public static string DateFromMysqlDateFormatString(string e)
        {
            string _ret;

            //Console.WriteLine(":" + e);

            _ret = e.Split(' ')[0];

            //Console.WriteLine(":" + _ret);


            string[] u = _ret.Split('-');

            _ret = u[2] + "." + u[1] + "." + u[0];

            return _ret;
        }

        public static string ToHtml(string e)
        {
            IHTMLElement d = new IHTMLElement();

            d.appendChild(e);

            return d.innerHTML;
        }

        [Script(OptimizedCode = "return String.fromCharCode(c);")]
        public static string ToString(char c)
        {
            return default(string);
        }

        

        public static string ToCurrency(double e)
        {
            string a = Native.Math.round(e * 100) + "";

            if (a.Length > 2)
            {
                return a.Substring(0, a.Length - 2) + "." + a.Substring(a.Length - 2);

            }

            if (a.Length == 2)
                return "0." + a;
            else
                return "0." + a + "0";
        }

        public static string ToRadixString(int value, int radix)
        {
            string r = "";
            string c = "0123456789ABCDEF";

            double n = value;
            double t;
            int i = 0;

            while (n > 0.9)
            {
                i++;
                t = n;
                r = c[(int)t % radix] + r;  
                n = Native.Math.floor(t / radix);
            }

            if (r.Length % 2 == 1)
                return "0" + r;

            return r;
        }

        public static string ToHexString(string value)
        {
            StringWriter w = new StringWriter();

            foreach (char v in value)
            {
                w.Write(ToHexString(v));
            }

            return w.GetString();
        }

        public static string ToHexString(int value)
        {
            return ToRadixString(value, 16);
        }

        public static string ToHexString(uint value)
        {
            return ToRadixString((int)value, 16);
        }

        internal readonly static string Base64Key = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        
        public static string ToBase64String(string input)
        {
            string output = "";

            int chr1, chr2, chr3, enc1, enc2, enc3, enc4;
            int i = 0;

            bool b = true;

            while (b)
            {
                chr1 = __String.GetCharCodeAt(input, i++);
                chr2 = __String.GetCharCodeAt(input, i++);
                chr3 = __String.GetCharCodeAt(input, i++);

                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                enc4 = chr3 & 63;

                if (Native.Window.isNaN(chr2))
                {
                    enc3 = enc4 = 64;
                }
                else if (Native.Window.isNaN(chr3))
                {
                    enc4 = 64;
                }

                output += Base64Key[enc1];
                output += Base64Key[enc2];
                output += Base64Key[enc3];
                output += Base64Key[enc4];

                

                b = i < input.Length;
            }

            return output;
        }

        public static string FromBase64String(string input)
        {
            string output = "";

            int chr1, chr2, chr3, enc1, enc2, enc3, enc4;
            int i = 0;

            bool b = true;

            while(b)
            {
                enc1 = Base64Key.IndexOf(input[i++]);
                enc2 = Base64Key.IndexOf(input[i++]);
                enc3 = Base64Key.IndexOf(input[i++]);
                enc4 = Base64Key.IndexOf(input[i++]);

                chr1 = (enc1 << 2) | (enc2 >> 4);
                chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                chr3 = ((enc3 & 3) << 6) | enc4;

                output += __String.FromCharCode(chr1);

                if (enc3 != 64)
                {
                    output += __String.FromCharCode(chr2);
                }
                if (enc4 != 64)
                {
                    output += __String.FromCharCode(chr3);
                }

                b = i < input.Length;
            }

            return output;
        }

        public static byte ToByte(double value)
        {
            return (byte)(Native.Math.floor(value) % 0x100);
        }

        public static string UrlEncode(string p)
        {
            StringWriter w = new StringWriter();

            string e = p;


            for (int i = 0; i < e.Length; i++)
            {
                int c = __String.GetCharCodeAt(e, i);

                w.Write(@"%" + Convert.ToHexString(c));
            }

            return w.GetString();
        }

        public static int ToInteger(double p)
        {
            return Native.Math.round(p);
        }

        public static T FromJSON<T>(string stream, bool base64)
        {
            return Expando.FromJSON(stream, base64).To<T>();
        }



        public static string ToJSON(object p)
        {
            return Expando.Of(p).ToJSON();
        }

        #if BLOAT
        public static TRet To<TArg, TRet>(TArg e, TRet def, EventHandler<ConvertTo<TArg, TRet>> h)
        {
            var p = new ConvertTo<TArg, TRet>();

            p.TargetIn = e;
            p.TargetOut = def;

            p.Invoke(h);


            return p.TargetOut;
        }
#endif
    }
}
