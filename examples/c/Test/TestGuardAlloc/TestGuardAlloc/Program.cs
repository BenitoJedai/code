using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestGuardAlloc
{
    class Program
    {
        #region GuardedAlloc
        [DllImport("kernel32.dll", SetLastError = false)]

        public static extern void GetSystemInfo(out SYSTEM_INFO Info);

        [StructLayout(LayoutKind.Explicit)]
        public struct SYSTEM_INFO_UNION

        {

            [FieldOffset(0)]
            public UInt32 OemId;
            [FieldOffset(0)]
            public UInt16 ProcessorArchitecture;
            [FieldOffset(2)]
            public UInt16 Reserved;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SYSTEM_INFO

        {

            public SYSTEM_INFO_UNION CpuInfo;
            public UInt32 PageSize;
            public UInt32 MinimumApplicationAddress;
            public UInt32 MaximumApplicationAddress;
            public UInt32 ActiveProcessorMask;
            public UInt32 NumberOfProcessors;
            public UInt32 ProcessorType;
            public UInt32 AllocationGranularity;
            public UInt16 ProcessorLevel;
            public UInt16 ProcessorRevision;
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern UIntPtr VirtualAlloc(UIntPtr lpAddress, UIntPtr dwSize,
   AllocationType flAllocationType, MemoryProtection flProtect);

        [Flags()]
        public enum AllocationType : uint
        {
            COMMIT = 0x1000,
            RESERVE = 0x2000,
            RESET = 0x80000,
            LARGE_PAGES = 0x20000000,
            PHYSICAL = 0x400000,
            TOP_DOWN = 0x100000,
            WRITE_WATCH = 0x200000
        }

        [Flags()]
        public enum MemoryProtection : uint
        {
            EXECUTE = 0x10,
            EXECUTE_READ = 0x20,
            EXECUTE_READWRITE = 0x40,
            EXECUTE_WRITECOPY = 0x80,
            NOACCESS = 0x01,
            READONLY = 0x02,
            READWRITE = 0x04,
            WRITECOPY = 0x08,
            GUARD_Modifierflag = 0x100,
            NOCACHE_Modifierflag = 0x200,
            WRITECOMBINE_Modifierflag = 0x400
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll")]
        static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress,
   UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);





        public unsafe static byte* GuardedAlloc(int size)
        {
            var x = default(SYSTEM_INFO);
            GetSystemInfo(out x);

            retry:
            var boundary = 65536;
            var bx = boundary / x.PageSize;

            var n0 = VirtualAlloc(UIntPtr.Zero,

                   (UIntPtr)x.PageSize,
                  AllocationType.RESERVE | AllocationType.COMMIT,
                   MemoryProtection.READONLY | MemoryProtection.GUARD_Modifierflag);

            var n1 = VirtualAlloc(UIntPtr.Zero,

                (UIntPtr)x.AllocationGranularity,
                       AllocationType.RESERVE | AllocationType.COMMIT,
                       MemoryProtection.READWRITE);

            var n2 = (byte*)VirtualAlloc(UIntPtr.Zero, (UIntPtr)x.PageSize,
               AllocationType.RESERVE | AllocationType.COMMIT,
               MemoryProtection.READONLY | MemoryProtection.GUARD_Modifierflag);



            //  Additional information: Attempted to read or write protected memory.This is often an indication that other memory is corrupt.
            //An unhandled exception of type 'System.Runtime.InteropServices.SEHException' occurred in TestGuardAlloc.exe

            //Additional information: External component has thrown an exception.

            // x.PageSize = 4096
            // ndiff = 65536
            var ndiff = (long)n2 - (long)n1;
            // ndiff = 65536

            if (ndiff != boundary)
            {
                //Debugger.Break();
                goto retry;
            }

            // http://stackoverflow.com/questions/874824/contiguous-virtualalloc-behaviour-on-windows-mobile

            return n2 - size;
        }
        #endregion





        unsafe static void Main(string[] args)
        {
            // https://www.corelan.be/index.php/2010/06/16/exploit-writing-tutorial-part-10-chaining-dep-with-rop-the-rubikstm-cube/

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366786(v=vs.85).aspx
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724958(v=vs.85).aspx

            // Show Details	Severity	Code	Description	Project	File	Line
            //Error CS0306  The type 'byte*' may not be used as a type argument TestGuardAlloc Program.cs  95

            var n = GuardedAlloc(4);



            *((byte*)n + 0) = 3;
            *((byte*)n + 1) = 3;
            *((byte*)n + 2) = 3;
            *((byte*)n + 3) = 3;

            // crash here
            *((byte*)n + 4) = 3;


            //// https://msdn.microsoft.com/en-us/library/windows/desktop/aa366549(v=vs.85).aspx
            //var p0 = (byte*)Marshal.AllocHGlobal(32).ToPointer();
            //var p4 = (byte*)Marshal.AllocHGlobal(32).ToPointer();

            //// offset = 440
            //var offset = p4 - p0;
            //// offset = 32

            //*(p4) = 0;

            //*(p0 + 32) = 2;

            //p0 = { 84490840}

        }
    }
}
