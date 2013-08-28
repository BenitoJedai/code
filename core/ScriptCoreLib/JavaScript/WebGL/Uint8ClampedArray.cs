using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript
{
    public static partial class Native
    {
        static partial void Uint8ClampedArray()
        {
            // Native cctor shall call us
            // no other cctor shall do byte[] before this
            // how could we make sure Native cctor is ran first?

            // Uncaught TypeError: Cannot use 'in' operator to search for 'Uint8ClampedArray' in null 
            // see also x:\jsc.svn\examples\javascript\Test\TestNewByteArray\TestNewByteArrayViaScriptCoreLib\Class1.cs

            if (ScriptCoreLib.JavaScript.Runtime.Expando.Of(Native.self).Contains("Uint8ClampedArray"))
                return;

            dynamic self = Native.self;

            self.Uint8ClampedArray = self.Array;
        }

    }

    namespace WebGL
    {

        [Script(HasNoPrototype = true, ExternalTarget = "Uint8ClampedArray", Implements = typeof(byte[]))]
        public class Uint8ClampedArray : ArrayBufferView
        {
            public Uint8ClampedArray(params byte[] array)
            {

            }

            public byte this[uint i]
            {
                get { return 0; }
                set { }
            }

            public void set(byte[] array, uint offset) { }
            public void set(Uint8ClampedArray array, uint offset) { }

            public static implicit operator byte[](Uint8ClampedArray e)
            {
                // why no warning?
                return (byte[])(object)e;
            }
        }
    }
}