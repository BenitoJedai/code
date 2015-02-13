using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    // x:\jsc.svn\core\scriptcorelibnative\scriptcorelibnative\systemheaders\stdio_h.cs

    /// <summary>
    /// http://www.cplusplus.com/ref/cstdio/
    /// </summary>
    [Script(IsNative = true, Header = "stdio.h", IsSystemHeader = true)]
    public static class stdio_h
    {
        // X:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\crt\src\appcrt\_file.cpp
        // #define 	stdout   (__iob[1])
        //public static object[] __iob;

        public static object freopen(string path, string mode, object stream) => default(object);


        // http://msdn.microsoft.com/en-us/library/9yky46tz.aspx
        public static void fflush(object stream = null)
        {
        }

        public static int getchar()
        {
            return default(int);
        }

        /// <summary>
        /// Copies the string to standard output stream (stdout) and appends a new line character (\n).
        /// </summary>
        /// <param name="e">Null-terminated string to be outputed.</param>
        /// <returns>On success, a non-negative value is returned. On error EOF value is returned.</returns>
        public static int puts(string e)
        {
            return default(int);
        }

        public static int putchar(int character)
        {
            return default(int);
        }

        public static object fopen(string filename, string mode)
        {
            return default(object);
        }

        /// <summary>
        /// Close the file associated with the specified stream after flushing all buffers associated with it.
        /// </summary>
        /// <param name="stream">Pointer to FILE structure specifying the stream to be closed.</param>
        /// <returns>If the stream is successfully closed 0 is returned. If any error EOF is returned.</returns>
        public static int fclose(object stream)
        {
            return default(int);
        }


        /// <summary>
        /// The function begins copying from the address specified (string) until it reaches a null character ('\0') that ends the string. The final null-character is not copied to the stream.
        /// </summary>
        /// <param name="_string">Null-terminated string to be written.</param>
        /// <param name="stream">pointer to an open file.</param>
        /// <returns>On success, a non-negative value is returned. On error the function returns EOF.</returns>
        public static int fputs(string _string, object stream)
        {
            // X:\jsc.svn\examples\c\Test\TestConsoleWriteLine\TestConsoleWriteLine\Program.cs

            return default(int);
        }

        public static int fprintf(object stream, string format, __arglist) { return default(int); }


        // X:\jsc.svn\examples\c\Test\TestRoslynStaticDelegate\TestRoslynStaticDelegate\Class1.cs
        // http://en.wikipedia.org/wiki/Printf_format_string
        public static int printf(string format, __arglist) { return default(int); }

        // size_t fwrite ( const void * ptr, size_t size, size_t count, FILE * stream );

        public static int fwrite(object ptr, int size, int count, object stream)
        {
            return default(int);
        }

    }

}
