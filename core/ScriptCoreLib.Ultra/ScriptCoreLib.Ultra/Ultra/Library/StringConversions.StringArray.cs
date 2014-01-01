using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Data;
using ScriptCoreLib.Extensions;
using System.Diagnostics;

namespace ScriptCoreLib.Library
{
    public static partial class StringConversions
    {
        // used by JVM/CLR 
        // X:\jsc.internal.svn\compiler\jsc.internal\jsc.internal\meta\Library\ILStringConversions.cs

        public static string UTF8FromBase64StringOrDefault(string e)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201401/20140101

            var o = default(string);

            Console.WriteLine("enter UTF8FromBase64StringOrDefault");

            //before call NewGlobalInvokeMethod { Name = Insert }
            //enter { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //before xml parse { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //before ElementsToFields { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //ElementsToFields { Name = Key }
            //ElementsToFields { Name = Avatar640x480 }
            //enter UTF8FromBase64StringOrDefault

            // allow 0 char do be sent
            if (e != null)
            {
                Console.WriteLine("before Convert.FromBase64String");
                var bytes = Convert.FromBase64String(e);

                Console.WriteLine("before Encoding.UTF8.GetString");
                o = Encoding.UTF8.GetString(bytes);
            }

            Console.WriteLine("exit UTF8FromBase64StringOrDefault");

            return o;
        }

        public static string UTF8ToBase64StringOrDefault(string e)
        {
            if (e == null)
                return null;

            var bytes = Encoding.UTF8.GetBytes(e);

            return Convert.ToBase64String(bytes);
        }



        public static byte[] FromBase64StringOrDefault(this string e)
        {
            if (e == null)
                return null;

            return Convert.FromBase64String(e);
        }

        public static string ToBase64StringOrDefault(this byte[] e)
        {
            if (e == null)
                return null;

            return Convert.ToBase64String(e);
        }



        public static int[] Int32ArrayFromBase64StringOrDefault(this string e)
        {
            if (e == null)
                return null;

            var m = new MemoryStream(Convert.FromBase64String(e));
            var r = new BinaryReader(m);

            var length = m.Length / 4;

            var a = new int[length];

            for (int i = 0; i < length; i++)
            {
                a[i] = r.ReadInt32();
            }

            return a;
        }

        public static string Int32ArrayToBase64StringOrDefault(this int[] e)
        {
            if (e == null)
                return null;

            var m = new MemoryStream();
            var w = new BinaryWriter(m);

            foreach (var item in e)
            {
                w.Write(item);
            }

            return Convert.ToBase64String(m.ToArray());
        }

        #region XElement
        public static string ConvertXElementToString(XElement e)
        {
            if (e == null)
                return null;

            return e.ToString();
        }

        public static XElement ConvertStringToXElement(string e)
        {
            if (string.IsNullOrEmpty(e))
                return null;

            return XElement.Parse(e);
        }
        #endregion

        #region FileInfo
        public static string ConvertFileInfoToString(FileInfo e)
        {
            if (e == null)
                return null;

            return e.FullName;
        }

        public static FileInfo ConvertStringToFileInfo(string e)
        {
            if (string.IsNullOrEmpty(e))
                return null;

            return new FileInfo(e);
        }
        #endregion
    }

}
