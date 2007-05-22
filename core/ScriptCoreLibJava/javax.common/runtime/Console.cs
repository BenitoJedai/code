using ScriptCoreLib;

using Exception = global::System.Exception;

using java;
using java.lang;
using java.io;
using java.util;
using java.text;


namespace javax.common.runtime
{
    [Script]
    public sealed class Console
    {
        

        public static bool IsVisibleChar(int p)
        {
            return Helper.IsVisibleChar(p);
        }


        public static void WriteHexDump(sbyte[] read_b)
        {
            WriteHexDump(read_b, 16);
        }

        public static void WriteHexDump(string e, string prefix)
        {
            Console.WriteHexDump(Convert.FromHexString(e), true, prefix);
        }

        public static void WriteHexDump(sbyte[] data, int offset, int length)
        {
            WriteHexDump(data, 16, offset, length, true, "");
        }


        public static void WriteHexDump(sbyte[] read_b, bool show_offset, string prefix)
        {
            if (read_b == null)
                return;

            WriteHexDump(read_b, 16, 0, read_b.Length, show_offset, prefix);
        }

        public static void WriteHexDump(sbyte[] read_b, int bytes_shown)
        {
            if (read_b == null)
                return;

            WriteHexDump(read_b, bytes_shown, 0, read_b.Length, true, "");
        }

        public static void WriteHexDump(sbyte[] read_b, int bytes_shown, int offset, int length, bool show_offset, string prefix)
        {
            //Console.WriteLine(" hex dump offset " + offset + " length " + length);



            try
            {

                int p = 0;

                int pi = offset;
                string Text = "";

                int formatx = 0;

                while (formatx < length)
                {
                   
                    bool b_will_offset = (formatx) % bytes_shown == 0;


                    if (b_will_offset)
                    {

                        if (formatx == 0)
                            Console.Write(prefix);
                        else
                            Console.Write(" ", prefix.Length);

                        if (show_offset)
                        {
                            Console.Write("0x" + Convert.ToHexString(pi, 4) + " : ");
                        }
                    }

                    p = Convert.ToInt32(  read_b[pi] );

                    bool isAlpha = IsVisibleChar(p);

                    if (isAlpha)
                    {

                        sbyte[] xsb = new sbyte[] { (sbyte)p };


                        Text += new java.lang.String(xsb);
                    }
                    else
                        Text += ".";


                    Console.Write(Convert.ToHexString(p & 0xFF) + " ");

                    pi++;
                    formatx++;

                    if (formatx % bytes_shown == 0)
                    {


                        Console.WriteLine(" | " + Text);

                        Text = "";
                    }
                }

                if (Text != "")
                {
                    while (formatx++ % bytes_shown != 0)
                        Console.Write("   ");

                    Console.WriteLine(" | " + Text);
                }
            }
            catch
            {
                Console.WriteLine("hexdump failed");
            }
        }

        private static void Write(string e, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write(e);
            }
        }


        public static void WriteThrowable(object e)
        {
            java.lang.Throwable t = (java.lang.Throwable)(object)e;

            t.printStackTrace();
        }

        internal static void InternalWriteLine(string e)
        {
            JavaSystem.@out.println(e);

        }

        public static void WriteLine(string e)
        {
            InternalWriteLine(e);
        }

        public static sbyte[] ReadAllBytes()
        {
            

            try
            {
                int len = JavaSystem.@in.available();

                sbyte[] b = new sbyte[len];

                JavaSystem.@in.read(b, 0, len);

                return b;
            }
            catch (System.Exception exc)
            {
                Console.WriteThrowable(exc);

                return null;
            }

        }

        public static string ReadAll()
        {
            sbyte[] b = ReadAllBytes();

            return Convert.ToString(b);
        }

        public static string ReadLine()
        {
            try
            {
                InputStreamReader r0 = new InputStreamReader(JavaSystem.@in);

                BufferedReader r1 = new BufferedReader(r0);

                
                return r1.readLine();
            }
            catch(System.Exception exc)
            {
                Console.WriteThrowable(exc);

                return null;
            }
        }

        public static void Write(string p)
        {
            JavaSystem.@out.print(p);
        }

        public static void WriteLine()
        {
            WriteLine("");
        }



        public static void WriteBytes(sbyte[] p)
        {
            try
            {
                JavaSystem.@out.write(p);
            }
            catch
            {

            }
        }

        public static void WriteErrorLine(string p)
        {
            JavaSystem.err.print("*** error: " + p + "\n");
        }

        public static void WriteHexDumpFromFile(string filename)
        {
            try
            {
                File file = new File(filename);

                RandomAccessFile stream = new RandomAccessFile(file, "r");

                sbyte[] bytes = new sbyte[stream.length()];

                stream.read(bytes);

                stream.close();

                Console.WriteLine("dump of [" + file + "]");
                Console.WriteHexDump(bytes);
            }
            catch (Exception exc)
            {

                Console.WriteThrowable(exc);
            }


        }

        /// <summary>
        /// writes bytes to file
        /// </summary>
        /// <param name="cdata"></param>
        /// <param name="p"></param>
        public static void WriteBytes(sbyte[] cdata, string filename, bool utf8)
        {
            try
            {
                File f = new File(filename);

                if (f.exists())
                    f.delete();

                RandomAccessFile stream = new RandomAccessFile(filename, "rw");

                if (utf8)
                {
                    stream.writeByte(0xEF);
                    stream.writeByte(0xBB);
                    stream.writeByte(0xBF);
                }

                stream.write(cdata);

                stream.close();
            }
            catch
            {

            }
        }
    }

}
