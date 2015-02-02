using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace CLRLibraryDllExportDefinition
{
    public struct uvec3
    //public sealed class uvec3
    {
        public long x;
        public long y;
        public long z;
    }

    public unsafe struct uvec3ptr
    {
        public uvec3* position;
    }

    static class Test
    {
        public unsafe static void Invoke()
        {
            var yargs = new CLRLibraryDllExportDefinition.uvec3();

            yargs.z = 66;

            var z = yargs.z;

            var yargsptr = &yargs;

            //  num3 = ((long long)uvec3_2.z);
            var pz = yargsptr->z;

            yargsptr->z = pz;
        }
    }

    //// CLRLibraryDllExportDefinition.uvec3
    //typedef struct tag_CLRLibraryDllExportDefinition_uvec3
    //{
    //    long long x;
    //    long long y;
    //    long long z;
    //} CLRLibraryDllExportDefinition_uvec3, *LPCLRLibraryDllExportDefinition_uvec3;
    //#define __new_CLRLibraryDllExportDefinition_uvec3(count) \
    //    (LPCLRLibraryDllExportDefinition_uvec3) malloc(sizeof(CLRLibraryDllExportDefinition_uvec3) * count)
}
