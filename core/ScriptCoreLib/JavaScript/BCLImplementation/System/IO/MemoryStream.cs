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
    [Script(Implements = typeof(global::System.IO.MemoryStream))]
    internal class __MemoryStream : __Stream
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\IO\MemoryStream.cs
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\IO\MemoryStream.cs


        // X:\jsc.svn\examples\javascript\Test\TestMemoryStreamPerformance\TestMemoryStreamPerformance\Application.cs
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\Experimental\X.cs
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\IO\MemoryStream.cs
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

                // shall preserve current buffer
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
                Capacity = (int)(TargetCapacity + (8 + Length / 2));
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
            throw new NotImplementedException();
        }
        #endregion



    }
}
