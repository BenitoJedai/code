using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript
{
    public static partial class Native
    {
        static partial void __Uint8ClampedArray()
        {
            // Native cctor shall call us
            // no other cctor shall do byte[] before this
            // how could we make sure Native cctor is ran first?

            // Uncaught TypeError: Cannot use 'in' operator to search for 'Uint8ClampedArray' in null 
            // see also x:\jsc.svn\examples\javascript\Test\TestNewByteArray\TestNewByteArrayViaScriptCoreLib\Class1.cs

            if (ScriptCoreLib.JavaScript.Runtime.Expando.Of(Native.self).Contains("Uint8ClampedArray"))
                return;

            dynamic self = Native.self;

            if (ScriptCoreLib.JavaScript.Runtime.Expando.Of(Native.self).Contains("Uint8Array"))
            {
                // IE its 2014! where is Uint8ClampedArray ???
                // tested by
                // X:\jsc.svn\examples\javascript\Test\TestUploadValuesAsync\TestUploadValuesAsync\Application.cs
                // http://connect.microsoft.com/IE/feedback/details/781386/typed-array-support-is-incomplete-missing-uint8clampedarray-important-for-canvas

                Console.WriteLine("Uint8ClampedArray not available. while Uint8Array seems to be.");

                self.Uint8ClampedArray = self.Uint8Array;
                return;
            }


            // ArrayBuffer cannot be used with array can it?
            self.Uint8ClampedArray = self.Array;
        }

    }

    namespace WebGL
    {
        //[Script(HasNoPrototype = true, ExternalTarget = "Uint8Array", Implements = typeof(byte[]))]

        // crypto exponend needs to be in Uint8Array while until now we chose Uint8ClampedArray to implement byte[]
        // do we need t switch?

        [Script(HasNoPrototype = true, ExternalTarget = "Uint8Array")]
        public class Uint8Array : ArrayBufferView
        {
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\Native.cs

            public Uint8Array(params byte[] array)
            {

            }

            public static implicit operator byte[](Uint8Array e)
            {
                // x:\jsc.svn\examples\javascript\test\testwebcryptokeyexport\testwebcryptokeyexport\application.cs
                return (byte[])(object)e;
            }
        }


        [Script(HasNoPrototype = true, ExternalTarget = "Uint8ClampedArray", Implements = typeof(byte[]))]
        public class Uint8ClampedArray : ArrayBufferView
        {
            public Uint8ClampedArray(ArrayBuffer array)
            {

            }


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