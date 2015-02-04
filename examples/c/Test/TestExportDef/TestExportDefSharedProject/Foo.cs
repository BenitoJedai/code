using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TestExportDefSharedProject
{
    unsafe struct Foo
    {
        public long value8;


        //.field public valuetype TestExportDefSharedProject.Foo/'<EmailID>e__FixedBuffer' EmailID
        //.custom instance void[mscorlib] System.Runtime.CompilerServices.FixedBufferAttribute

        // http://stackoverflow.com/questions/19152441/create-fixed-size-string-in-a-struct-in-c
        //public fixed char EmailID[20];
        //  unsigned char EmailID[20];
        public fixed byte EmailID[20];
    }

    unsafe struct Foo3
    {
        public Foo x, y, z;
    }

    // x:\jsc.svn\examples\c\test\testswitchtoclr\clrlibrarydllexportdefinition\class1.cs
    //[StructLayout(LayoutKind.Sequential, Pack = 1)]
    unsafe struct FooSignal
    {
        public long value8;

        public Action signal1;
    }

    unsafe struct FooSignal8
    {
        public long value8;

        // by using delegates in a struct, a pointer can no longer taken
        //public Action signal1;
    }
}
