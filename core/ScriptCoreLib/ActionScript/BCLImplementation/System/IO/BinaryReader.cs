using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
{
    // http://referencesource.microsoft.com/#mscorlib/system/io/binaryreader.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.IO/BinaryReader.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\IO\BinaryReader.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\IO\BinaryReader.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\IO\BinaryReader.cs

    [Script(Implements = typeof(global::System.IO.BinaryReader))]
    internal class __BinaryReader
    {
        //T:\web\ScriptCoreLib\ActionScript\BCLImplementation\System\IO\__BinaryReader.as: Error: Can not resolve a multiname reference unambiguously. ScriptCoreLib.ActionScript.BCLImplementation.System.IO:__Stream (from T:\web\ScriptCoreLib\ActionScript\BCLImplementation\System\IO\__Stream.as) and ScriptCoreLib.Shared.BCLImplementation.System.IO:__Stream (from T:\web\ScriptCoreLib\Shared\BCLImplementation\System\IO\__Stream.as) are available.

        //T:\web\ScriptCoreLib\ActionScript\BCLImplementation\System\IO\__BinaryReader.as(19): col: 46 Error: Type was not found or was not a compile-time constant: __Stream.

        //        public function __BinaryReader(input:__Stream)
        //                                             ^

        //T:\web\ScriptCoreLib\ActionScript\BCLImplementation\System\IO\__BinaryReader.as(32): col: 42 Error: Type was not found or was not a compile-time constant: __Stream.

        private Stream InternalStream;

        private byte[] m_buffer;


        public virtual Stream BaseStream
        {
            get
            {
                return InternalStream;
            }
        }


        public __BinaryReader(Stream input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            this.InternalStream = input;
            this.m_buffer = new byte[0x10];

        }


        public virtual uint ReadUInt32()
        {
            return InternalStream.ToByteArray().readUnsignedInt();
        }

        public virtual byte[] ReadBytes(int length)
        {
            var k = new ByteArray();
            var s = InternalStream.ToByteArray();

            s.readBytes(k, 0, (uint)length);

            return k.ToArray();
        }

        public virtual int Read(byte[] buffer, int index, int count)
        {
            return this.InternalStream.Read(buffer, index, count);
        }

        public virtual int ReadInt32()
        {
            return InternalStream.ToByteArray().readInt();
        }

        public virtual short ReadInt16()
        {
            return InternalStream.ToByteArray().readShort();
        }

        private void FillBuffer(int p)
        {
            InternalStream.Read(m_buffer, 0, p);
        }


        public virtual byte ReadByte()
        {
            if (this.InternalStream == null)
            {
                throw new Exception("FileNotOpen");
            }
            int num = this.InternalStream.ReadByte();
            if (num == -1)
            {
                var ms = this.InternalStream as MemoryStream;


                if (ms != null)
                {

                    throw new Exception("MemoryStreamEndOfFile: " + new { this.InternalStream.Position, this.InternalStream.Length, num, value = ms.ToArray() }.ToString());
                }
                else
                    throw new Exception("EndOfFile: " + new { this.InternalStream.Position, this.InternalStream.Length, num }.ToString());
            }
            return (byte)num;
        }

        public virtual double ReadDouble()
        {
            return InternalStream.ToByteArray().readDouble();

        }

        public virtual float ReadSingle()
        {
            return (float)InternalStream.ToByteArray().readFloat();

        }

        public virtual string ReadString()
        {
            var length = Read7BitEncodedInt();
            //var bytes = ReadBytes(length);

            return InternalStream.ToByteArray().readUTFBytes((uint)length);
        }

        protected internal int Read7BitEncodedInt()
        {
            byte num3;
            int num = 0;
            int num2 = 0;
            bool loop = true;
            while (loop)
            {
                if (num2 == 0x23)
                {
                    throw new Exception("Format_Bad7BitInt32");
                }
                num3 = this.ReadByte();
                num |= (num3 & 0x7f) << num2;
                num2 += 7;

                loop = ((num3 & 0x80) != 0);
            }

            return num;
        }

        public static implicit operator __BinaryReader(BinaryReader r)
        {
            return (__BinaryReader)(object)r;
        }

    }
}
