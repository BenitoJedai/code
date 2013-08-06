using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Convert))]
    internal static class __Convert
    {


        public static long ToInt64(double value)
        {
            return (long)global::System.Math.Floor(value);
        }




        public static string ToString(char value)
        {
            return new string(value, 1);
        }


        public static string ToString(bool value)
        {
            if (value)
                return "true";

            return "false";
        }
        public static bool ToBoolean(string value)
        {
            if ("true" == value)
                return true;

            return false;
        }

        public static string ToString(int value)
        {
            return "" + value;
        }

        public static string ToString(long value)
        {
            return "" + value;
        }

        //public static string ToString(short value)
        //{
        //    return "" + value;
        //}

        public static string ToString(double value)
        {
            return "" + value;
        }

        public static string ToString(byte value)
        {
            return "" + value;
        }


        //        Implementation not found for type import :
        //type: System.Convert
        //method: System.String ToString(Int16)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!


        public static int ToInt32(byte e)
        {
            return (int)e;
        }


        public static int ToInt32(long e)
        {
            return (int)e;
        }

        public static int ToInt32(string e)
        {
            return int.Parse(e);
        }


        //public static short ToInt16(string e)
        //{
        //    return short.Parse(e);
        //}

        public static long ToInt64(string e)
        {
            return long.Parse(e);
        }

        // what about this : public static int ToInt32(int e)

        public static int ToInt32(uint e)
        {
            int x = (int)e;

            return x;
        }

        public static int ToInt32(double e)
        {
            return (int)Math.Floor((double)e);
        }

        #region Base64Key
        internal readonly static string Base64Key = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";




        public static string ToBase64String(byte[] input)
        {
            var w = new StringBuilder();

            int chr1, chr2, chr3;
            int enc1, enc2, enc3, enc4;
            int i = 0;
            var length = input.Length;

            bool b = i < length;

            while (b)
            {
                enc3 = 64;
                enc4 = 64;

                chr1 = input[i++];
                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4);

                if (i < length)
                {
                    chr2 = input[i++];
                    enc2 |= (chr2 >> 4);
                    enc3 = ((chr2 & 15) << 2);
                }

                if (i < length)
                {
                    chr3 = input[i++];
                    enc3 |= (chr3 >> 6);
                    enc4 = chr3 & 63;
                }

                w.Append(Base64Key[enc1]);
                w.Append(Base64Key[enc2]);
                w.Append(Base64Key[enc3]);
                w.Append(Base64Key[enc4]);



                b = i < length;
            }

            return w.ToString();
        }

        public static byte[] FromBase64String(string input)
        {
            if (string.IsNullOrEmpty(input))
                return new byte[0];

            var m = new MemoryStream();

            var length = input.Length;

            int chr1, chr2, chr3;
            int enc1, enc2, enc3, enc4;

            int i = 0;

            bool b = true;

            if (i < length)
                while (b)
                {
                    enc1 = 64;
                    enc2 = 64;
                    enc3 = 64;
                    enc4 = 64;

                    if (i < length)
                        enc1 = Base64Key.IndexOf(input[i++]);
                    if (i < length)
                        enc2 = Base64Key.IndexOf(input[i++]);
                    if (i < length)
                        enc3 = Base64Key.IndexOf(input[i++]);
                    if (i < length)
                        enc4 = Base64Key.IndexOf(input[i++]);

                    chr1 = (enc1 << 2) | (enc2 >> 4);
                    chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                    chr3 = ((enc3 & 3) << 6) | enc4;

                    m.WriteByte((byte)chr1);

                    if (enc3 != 64)
                    {
                        m.WriteByte((byte)chr2);
                    }
                    if (enc4 != 64)
                    {
                        m.WriteByte((byte)chr3);
                    }

                    b = i < length;
                }

            return m.ToArray();
        }

        #endregion

        // conflict in java with uint 
        //public static int ToInt32(int value)
        //{
        //    return (int)global::System.Math.Floor((double)value);
        //}


        public static int ToInt32(float value)
        {
            return (int)global::System.Math.Floor(value);
        }

        public static byte ToByte(int value)
        {
            return (byte)(value & 0xff);

        }

        public static byte ToByte(double value)
        {
            return (byte)(((int)global::System.Math.Floor(value)) & 0xff);
        }


        public static double ToDouble(int value)
        {
            return value;
        }


        public static string ToString(object value)
        {
            if (value == null)
                return null;

            var s = value as string;
            if (s != null)
                return s;

            return value.ToString();
        }



        public static double ToDouble(string value)
        {
            return double.Parse(value);

        }

        public static float ToSingle(string value)
        {
            return float.Parse(value);

        }

        public static bool ToBoolean(int value)
        {
            return value != 0;
        }

        public static int ToInt32(bool value)
        {
            if (value)
                return 1;

            return 0;
        }

        // ...



        public static uint ToUInt32(int value)
        {
            return ((uint)value);

        }

        public static uint ToUInt32(long value)
        {
            return ((uint)value & 0xffffffff);

        }


    }

}
