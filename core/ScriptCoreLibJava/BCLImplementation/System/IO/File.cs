using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLibJava.BCLImplementation.System.Net.Sockets;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
    [Script(Implements = typeof(global::System.IO.File))]
    internal class __File
    {
        



        public static Func<string, FileStream> InternalOpenRead =
            path =>
            {
                java.io.FileInputStream fis = null;
                var x =  default(FileStream);

                try
                {
                    fis = new java.io.FileInputStream(path);

                    x=new __FileStream { InternalStream = (__NetworkStream) fis};

                }
                catch
                {

                    throw;
                }

                return x;
                    
            };

        public static FileStream OpenRead(string path)
        {
            return InternalOpenRead(path);
        }

        public static void Delete(string path)
        {
            new java.io.File(path).delete();
        }

        public static bool Exists(string path)
        {
            return new java.io.File(path).exists();
        }

        public static string ReadAllText(string path)
        {
            //return Encoding.ASCII.GetString(ReadAllBytes(path));
            return Encoding.UTF8.GetString(ReadAllBytes(path));
        }


        public static void WriteAllText(string path, string value)
        {
            //WriteAllBytes(path, Encoding.ASCII.GetBytes(value));
            WriteAllBytes(path, Encoding.UTF8.GetBytes(value));
        }

        public static void WriteAllBytes(string path, byte[] value)
        {

            try
            {
                var stream = new global::java.io.RandomAccessFile(path, "rw");

                stream.setLength(0);
                stream.write(InternalByteArrayToSByteArray(value));

                stream.close();
            }
            catch
            {
                throw;
            }
        }


        // tested by ?
        public static Func<string, byte[]> InternalReadAllBytes;

        public static byte[] ReadAllBytes(string path)
        {
            // what if our files are virtual? like android assets.

            if (InternalReadAllBytes != null)
            {
                var z = InternalReadAllBytes(path);
                if (z != null)
                    return z;
            }

            var x = getBytesFromFile(new java.io.File(path));

            return InternalSByteArrayToByteArray(x);
        }

        [Obsolete("Should use (sbyte[])(object)e instead!")]
        [Script(OptimizedCode = @"return e;")]
        public static byte[] InternalSByteArrayToByteArray(sbyte[] e)
        {
            return default(byte[]);
        }

        [Obsolete("Should use (byte[])(object)e instead!")]
        [Script(OptimizedCode = @"return e;")]
        public static sbyte[] InternalByteArrayToSByteArray(byte[] e)
        {
            return default(sbyte[]);
        }

        // http://www.java-tips.org/java-se-tips/java.io/reading-a-file-into-a-byte-array.html
        static sbyte[] getBytesFromFile(global::java.io.File file)
        {
            try
            {

                var istream = new global::java.io.FileInputStream(file);

                // Get the size of the file
                long length = file.length();



                // Create the byte array to hold the data
                var bytes = new sbyte[(int)length];

                // Read in the bytes
                int offset = 0;
                int numRead = istream.read(bytes, offset, bytes.Length - offset);

                if (numRead >= 0)
                    while (offset < bytes.Length)
                    {
                        offset += numRead;
                        numRead = istream.read(bytes, offset, bytes.Length - offset);

                        if (numRead < 0)
                            break;
                    }



                // Close the input stream and return bytes
                istream.close();
                return bytes;
            }
            catch //(Exception e)
            {
                //((java.lang.Throwable)(object)e).printStackTrace();

                // exception mapping must be refactored
                //throw new csharp.RuntimeException("File: " + file.ToString() + "Message: " + e.Message);

                throw;
            }
        }
    }
}
