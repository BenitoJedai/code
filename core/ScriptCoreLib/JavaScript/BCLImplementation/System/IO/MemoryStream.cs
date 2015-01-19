using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.Extensions;
using System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System.IO;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.IO
{
    // http://referencesource.microsoft.com/#mscorlib/system/io/memorystream.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.IO/MemoryStream.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/IO/MemoryStream.cs

    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\IO\MemoryStream.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\IO\MemoryStream.css
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\IO\MemoryStream.cs

    // https://github.com/dotnet/corefx/blob/master/src/System.Reflection.Metadata/src/System/Reflection/Internal/Utilities/ImmutableMemoryStream.cs

    [Script(Implements = typeof(global::System.IO.MemoryStream))]
    internal class __MemoryStream : __Stream
    {
        //20150119
        // can async webethod send back a copy of memory stream just yet?
        // X:\jsc.svn\examples\javascript\test\TestdecodeURIComponent\TestdecodeURIComponent\Application.cs


        // X:\jsc.svn\examples\javascript\io\ZIPDecoderExperiment\ZIPDecoderExperiment\Application.cs




        // X:\jsc.svn\examples\javascript\Test\TestMemoryStreamPerformance\TestMemoryStreamPerformance\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\Experimental\X.cs
        // X:\jsc.svn\examples\javascript\WebCamAvatarsExperiment\WebCamAvatarsExperiment\ApplicationWebService.cs
        // X:\jsc.svn\core\ScriptCoreLib.Avalon\ScriptCoreLib.Avalon\JavaScript\UCLImplementation\AvalonExtensions.cs


        // ByteArrayInputStream 
        // http://www.koders.com/java/fid654B227C95A99C7C2ACA686E7BC6BA584491A6B7.aspx

        // InputStream
        // http://www.koders.com/java/fidF990D954151F15A618183172871A1403F719D971.aspx
        byte[] InternalBuffer = new byte[0];

        long InternalPosition;
        long InternalLength;



        public override void Flush()
        {
            // ?
        }



        // X:\jsc.svn\examples\javascript\io\synergy\PNGEncoderExperiment\PNGEncoderExperiment\Application.cs
        public __MemoryStream(int capacity)
            : this(new byte[capacity])
        {
        }

        #region ctor
        public __MemoryStream()
            : this(null)
        {

        }

        public __MemoryStream(byte[] buffer)
        {
            if (buffer != null)
            {
                this.Write(buffer, 0, buffer.Length);

                this.InternalPosition = 0;
            }
        }

        public __MemoryStream(byte[] buffer, int o, int length)
        {
            if (buffer != null)
            {
                this.Write(buffer, o, length);

                this.InternalPosition = 0;
            }
        }
        #endregion




        public override int Read(byte[] buffer, int offset, int count)
        {
            var a = this.Length - this.InternalPosition;

            if (count > a)
            {
                if (a < 0)
                    return -1;

                count = (int)a;
            }

            Array.Copy(this.InternalBuffer, (int)this.InternalPosition, buffer, offset, count);

            this.InternalPosition += count;

            return count;
        }

        #region Write
        public override long Length
        {
            get
            {
                return InternalLength;
            }
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public virtual int Capacity
        {
            get
            {
                // X:\jsc.svn\examples\java\JVMCLRBase64\JVMCLRBase64\Program.cs

                if (InternalBuffer == null)
                    // so we are fast until 0x1000 then we become really slow?
                    Capacity = 0x1000;

                return InternalBuffer.Length;

            }
            set
            {
                var x = InternalBuffer;

                // workaround:
                // make sure js vm does not give us a double
                //var value32 = ~~value;

                // shall preserve current buffer

                // Web Console( 5281): Uncaught RangeError: Invalid array length at http://192.168.43.1:7362/view-source:33429
                //I/Web Console( 2345): %c0:31780ms InternalEnsureCapacity { newCapacity = 71 } at http://192.168.43.1:14599/view-source:37084
                //I/Web Console( 2345): %c0:31783ms MemoryStream set Capacity { value = 71 } at http://192.168.43.1:14599/view-source:37084
                //I/Web Console( 2345): %c0:31800ms MemoryStream set Capacity { value = 101.33333333333333 } at http://192.168.43.1:14599/view-source:37084
                //E/Web Console( 2345): Uncaught RangeError: Invalid array length at http://192.168.43.1:14599/view-source:33430

                //Console.WriteLine("MemoryStream set Capacity " + new { value, value32 });

                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestAndroidOrderByThenGroupBy\Application.cs
                //Console.WriteLine("MemoryStream set Capacity " + new { value });

                InternalBuffer = new byte[value];

                if (x != null)
                {
                    var y = x.Length;

                    if (InternalBuffer.Length < y)
                        y = InternalBuffer.Length;

                    Array.Copy(x, InternalBuffer, y);
                }
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            InternalEnsureCapacity(InternalPosition + count);

            // java does not support long array copy?

            Array.Copy(buffer, offset, InternalBuffer, (int)InternalPosition, count);
            InternalPosition += count;
            InternalLength += count;
        }

        public override void WriteByte(byte value)
        {
            // X:\jsc.svn\examples\javascript\Test\TestMemoryStreamPerformance\TestMemoryStreamPerformance\Application.cs
            // 2013-01-01 wow this problem was expensive to find
            InternalEnsureCapacity(InternalPosition + 1);

            InternalBuffer[InternalPosition] = value;
            InternalPosition++;
            InternalLength++;
        }

        public virtual void WriteTo(Stream stream)
        {
            stream.Write(InternalBuffer, 0, (int)InternalLength);
        }

        void InternalEnsureCapacity(long TargetCapacity)
        {
            if (Capacity < TargetCapacity)
            {
                var newCapacity = (int)(TargetCapacity + (8 + Length / 2));

                //I/Web Console( 2345): %c0:31780ms InternalEnsureCapacity { newCapacity = 71 } at http://192.168.43.1:14599/view-source:37084
                //I/Web Console( 2345): %c0:31783ms MemoryStream set Capacity { value = 71 } at http://192.168.43.1:14599/view-source:37084
                //I/Web Console( 2345): %c0:31800ms MemoryStream set Capacity { value = 101.33333333333333 } at http://192.168.43.1:14599/view-source:37084
                //E/Web Console( 2345): Uncaught RangeError: Invalid array length at http://192.168.43.1:14599/view-source:33430

                //Console.WriteLine("InternalEnsureCapacity before " + new { newCapacity });
                Capacity = newCapacity;
                //Console.WriteLine("InternalEnsureCapacity after " + new { newCapacity });
            }
        }


        #endregion

        public virtual byte[] ToArray()
        {

            var x = new byte[InternalLength];

            Array.Copy(InternalBuffer, x, (int)InternalLength);

            return x;
        }

        #region Position
        public override long Position
        {
            get
            {
                return this.InternalPosition;
            }
            set
            {
                this.InternalPosition = value;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            // X:\jsc.svn\examples\javascript\io\ZIPDecoderExperiment\ZIPDecoderExperiment\Application.cs

            if (origin == SeekOrigin.Begin)
            {
                this.Position = offset;

                return this.Position;
            }

            throw new InvalidOperationException();
        }
        #endregion



    }
}
