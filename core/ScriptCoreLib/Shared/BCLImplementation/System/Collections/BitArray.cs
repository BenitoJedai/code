using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections
{
    // http://referencesource.microsoft.com/#mscorlib/system/collections/bitarray.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Collections/BitArray.cs


    [Script(Implements = typeof(global::System.Collections.BitArray))]
    public class __BitArray
    {
        byte[] bytes;

        public __BitArray(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public bool this[int index]
        {
            get
            {
                // X:\jsc.svn\examples\javascript\Test\TestULongShift\TestULongShift\ApplicationControl.cs

                return (bytes[0] & (1 << index)) != 0;
            }
        }
    }
}
