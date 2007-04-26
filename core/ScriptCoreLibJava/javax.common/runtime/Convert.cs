using java.lang;
using java.util;

using ScriptCoreLib;


namespace javax.common.runtime
{
    [Script]
    public sealed class Convert
    {
        public static string BytesToHuman(long bytes)
        {
            string[] u = new string[] {
                "bytes",
                "KB",
                "MB",
                "GB"
            };

            int x = 0;

            long p = bytes;

            bool z = true;


            while (z)
            {
                z = false;

                if (p >= 1024)
                    if (x < u.Length)
                    {
                        p /= 1024;
                        x++;
                        z = true;
                    }
            }

            return p + " " + u[x];
        }

        public static string ToHexString(string e)
        {
            string z = "";

            foreach (char var in e)
            {
                z += ToHexString(var);
            }

            return z;
        }

        public static int[] ToInt32(sbyte[] e)
        {
            int[] i = new int[e.Length];

            for (int x = 0; x < e.Length; x++)
            {
                i[x] = ToInt32(e[x]);
            }

            return i;
        }

        public static int ToInt32(string e)
        {
            int r = 0;

            try
            {
                string v = e.Trim();

                string hex = "0x";

                if (v.StartsWith(hex))
                    r = java.lang.Integer.parseInt(v.Substring(hex.Length), 16);
                else
                    r = int.Parse(v);
            }
            catch (csharp.ThrowableException exc)
            {
                Console.WriteThrowable(exc);
            }

            return r;

        }

        public static int ToInt32(sbyte e)
        {
            int b = 0x00;

            if (e < 0)
                b = 256 + e;
            else
                b = e;

            return b & 0xFF;
        }

        public static string ToHexString(long e)
        {
            return ToHexString(Convert.ToByteArray(e));
        }

        public static string ToHexString(int e, bool LeadingZero)
        {
            string z = Integer.toHexString(e);

            if (LeadingZero)
                if (z.Length % 2 == 1)
                    return "0" + z;

            return z;
        }

        /// <summary>
        /// converts int to a hexstring, and adds padding zeros if uneven number
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ToHexString(int e)
        {
            return ToHexString(e, true);
        }


        public static string ToHexString(long pi, int length)
        {
            string e = ToHexString(pi);

            int z = length - e.Length;

            while (z-- > 0)
                e = "0" + e;

            return e;
        }

        public static string ToHexString(int pi, int length)
        {
            string e = ToHexString(pi);

            int z = length - e.Length;

            while (z-- > 0)
                e = "0" + e;

            return e;
        }

        public static string ToString(char b)
        {
            char[] x = { (char)b };

            return String.valueOf(x);
        }

        public static string ToString(int b)
        {
            sbyte[] x = { (sbyte)b };

            return new java.lang.String(x);
        }

        public static string ToString(sbyte[] bytes, string charset)
        {
            try
            {
                return new java.lang.String(bytes, charset);
            }
            catch
            {
                return null;
            }
        }

        public static sbyte[] FromHexBytes(sbyte[] val)
        {
            return FromHexString(ToString(val));
        }

        public static sbyte[] FromHexString(string val)
        {
            if (val.Length % 2 == 1)
                return FromHexString("0" + val);

            sbyte[] s = new sbyte[val.Length / 2];

            for (int i = 0; i < val.Length - 1; i += 2)
            {

                s[i / 2] = (sbyte)java.lang.Integer.parseInt(val.Substring(i, 2), 16);
            }

            return s;
        }

        public static string ToString(params sbyte[] e)
        {
            return new java.lang.String(e);
        }

        public static string ToString(sbyte[] e, int offset, int len)
        {
            return new java.lang.String(e, offset, len);
        }


        public static string ToString(sbyte[] e, int offset, int len, string enc)
        {
            string u = null;

            try
            {
                u = new java.lang.String(e, offset, len, enc);
            }
            catch
            {
            }

            return u;
        }

        public static string ToHexString(sbyte[] e)
        {
            if (e == null)
                return null;

            return ToHexString(e, 0, e.Length);
        }
        public static string ToHexString(sbyte[] e, int offset, int length)
        {
            string x = "";

            for (int i = offset; i < offset + length; i++)
            {
                x += ToHexString(Convert.ToInt32(e[i]));
            }

            return x;
        }

        public static sbyte[] ToByteArray(params int[] e)
        {
            sbyte[] n = new sbyte[e.Length];

            for (int i = 0; i < e.Length; i++)
            {
                n[i] = Convert.ToByte(e[i]);
            }

            return n;
        }

        private static sbyte ToByte(int p)
        {
            return (sbyte)p;
        }

        public static sbyte[] ToByteArray(long n)
        {
            sbyte[] b = new sbyte[8];
            b[7] = (sbyte)(n);
            n >>= 8;
            b[6] = (sbyte)(n);
            n >>= 8;
            b[5] = (sbyte)(n);
            n >>= 8;
            b[4] = (sbyte)(n);
            n >>= 8;
            b[3] = (sbyte)(n);
            n >>= 8;
            b[2] = (sbyte)(n);
            n >>= 8;
            b[1] = (sbyte)(n);
            n >>= 8;
            b[0] = (sbyte)(n);

            return b;

        }

        public static sbyte[] ToByteArray(string e)
        {
            return new java.lang.String(e).getBytes();
        }

        public static sbyte[] ToByteArray(string e, string charset)
        {
            sbyte[] u = null;

            try { u = new java.lang.String(e).getBytes(charset); }
            catch
            {
            }

            return u;
        }

        public static sbyte[] ToByteArray(sbyte[] FileBytes, int offset, int length)
        {
            sbyte[] n = new sbyte[length];

            for (int i = 0; i < length; i++)
            {
                n[i] = FileBytes[offset + i];
            }

            return n;
        }

        public static int ToInt16(sbyte hi, sbyte lo)
        {
            return ToInt16(Convert.ToInt32(hi), Convert.ToInt32(lo));
        }

        public static int ToInt16(int hi, int lo)
        {
            int x = 0;

            x += hi << 8;
            x += lo << 0;

            return x;
        }

        public static int ToInt16(long p)
        {
            return (int)(p);

        }

        public static long ToLong(string s, int radix)
        {
            long _return = 0;

            try
            {
                _return = java.lang.Long.parseLong(s, radix);
            }
            catch
            {
                _return = 0;
            }

            return _return;
        }

        public static string ReplaceString(string whom, string what, string with)
        {
            int j = -1;
            int i = whom.IndexOf(what);

            if (i == -1)
                return whom;

            StringBuffer b = new StringBuffer();

            b.ensureCapacity(whom.Length);

            

            while (i > -1)
            {
                b.append( whom.Substring(j + what.Length, i - j - what.Length) + with );

                j = i;
                i = whom.IndexOf(what, i + what.Length);
            }

            b.append( whom.Substring(j + what.Length) );

            return b.toString();
        }

        public static string ReplaceWhitespaces(string subject, string e)
        {
            string x = subject;

            x = x.Replace("\t", e);
            x = x.Replace("\n", e);
            x = x.Replace("\r", e);
            x = x.Replace(" ", e);

            return x;
        }

        public static string ReplaceWhitespaces(string DataString)
        {
            return ReplaceWhitespaces(DataString, "_");
        }

        /// <summary>
        /// converts 1 to 4 bytes from an array to an int32
        /// </summary>
        /// <param name="b"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int ToInt32(sbyte[] b, int offset)
        {
            int ret = 0;


            bool seek = true;

            int i = 0;

            while (seek)
            {
                if (i == 4)
                    seek = false;
                else if (offset + i >= b.Length)
                    seek = false;

                if (seek)
                {
                    ret <<= 8;

                    ret += Convert.ToInt32(b[i + offset]);

                    i++;
                }
            }


            return ret;
        }

        public static string BytesToString(sbyte[] p, string charset)
        {
            string u = null;

            try
            {
                u = new String(p, charset);

            }
            catch
            {
            }

            return u;
        }

        /// <summary>
        /// converts object list to int array
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static int[] ToInt32Array(java.util.ArrayList u)
        {
            int[] x = new int[u.size()];

            for (int i = 0; i < u.size(); i++)
            {
                x[i] = (int)u.get(i);
            }

            return x;
        }

        public static bool ToBoolean(string p)
        {
            if (p == null)
                return false;

            if (p.ToLower() == "true")
                return true;

            return false;
        }

        public static string[] SplitStringByChar(string e, char p)
        {
            ArrayList a = new ArrayList();

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

    }
}
