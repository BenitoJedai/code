using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLib;

namespace ScriptCoreLibNative.BCLImplementation.System.IO
{
    [Script(Implements = typeof(global::System.IO.FileStream))]
    internal class __FileStream : __Stream
    {
        internal object InternalHandle;

        public void __Write(byte[] buffer, int offset, int count)
        {
            stdio_h.fwrite(buffer, 1, count, InternalHandle);
        }

        public void __Close()
        {
            stdio_h.fclose(InternalHandle);
        }


        public static void override_Stream_Write(object k, byte[] buffer, int offset, int count)
        {
            ((__FileStream)k).__Write(buffer, offset, count);
        }

        public static void override_Stream_Close(object k )
        {
            ((__FileStream)k).__Close();
        }

        public __FileStream()
        {

            // we are doing manual virtual calls.. feel the pain yet?
            // tested by?
            // wont work with roslyn yet. works with 2012
            // X:\jsc.svn\examples\c\Test\TestAction\TestAction\Program.cs
            // wont work with CTP 14?
            // X:\jsc.svn\examples\c\Test\TestTaskRun\TestTaskRun\Program.cs

            // Error	2	Cannot assign lambda expression to an implicitly-typed variable	X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\IO\FileStream.cs	34	17	ScriptCoreLibNative
            //Action<object, byte[], int, int> __Stream_Write = (k, buffer, offset, count) =>
            //{
            //    ((__FileStream)k).__Write(buffer, offset, count);
            //};

            // jsc does like static delegates from roslyn.  whats the difference?
            this.__Stream_Write = override_Stream_Write;
            this.__Stream_Close = override_Stream_Close;


            // Action<object> __Stream_Close = k =>
            //{
            //    ((__FileStream)k).__Close();
            //};




            //            .ctor()
            //<.ctor > b__0(object, object, byte[], int, int) : void
            //<.ctor > b__2(object, object) : void
            //  Analysis
            //Attributes
            //Signature Types
            //Declaring Module
            //Declaring Type
            //maxstack 3
            //IL Code (27)
            //0x0000.ldarg.0          this[ScriptCoreLibNative] ScriptCoreLibNative.BCLImplementation.System.IO.__FileStream
            //0x0001 call[ScriptCoreLibNative] ScriptCoreLibNative.BCLImplementation.System.IO.__Stream..ctor()
            //0x0006 nop
            //0x0007 nop
            //0x0008.ldarg.0          this[ScriptCoreLibNative] ScriptCoreLibNative.BCLImplementation.System.IO.__FileStream
            //0x0009. .ldsfld[ScriptCoreLibNative] ScriptCoreLibNative.BCLImplementation.System.IO.__FileStream.CS$<> 9__CachedAnonymousMethodDelegate1: (object, byte[], int, int) -> void
            //0x000e. . .dup
            //0x000f. .brtrue.s
            //0x000f-> 0x0011 0x0024
            //       0x000f-> 0x0011 pop
            //0x000f 0x001f-> 0x0024 stfld
            //0x0024 stfld[ScriptCoreLibNative] ScriptCoreLibNative.BCLImplementation.System.IO.__Stream.__Stream_Write : (object, byte[], int, int) -> void
            //0x0029.ldarg.0          this[ScriptCoreLibNative] ScriptCoreLibNative.BCLImplementation.System.IO.__FileStream
            //0x002a. .ldsfld[ScriptCoreLibNative] ScriptCoreLibNative.BCLImplementation.System.IO.__FileStream.CS$<> 9__CachedAnonymousMethodDelegate3: (object) -> void
            //0x002f. . .dup
            //0x0030. .brtrue.s
            //0x0030-> 0x0032 0x0045



        }
    }

}
