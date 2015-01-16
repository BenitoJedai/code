using DiscUtils.Fat;
using DiscUtils.Iso9660;
using DiscUtils.Partitions;
using DiscUtils.Vhd;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestCreateVHD
{
    class Program
    {
        static void Main(string[] args)
        {
            // http://stackoverflow.com/questions/2818179/how-to-force-my-net-app-to-run-as-administrator-on-windows-7

            // "X:\opensource\codeplex\discutils\src\DiscUtils.csproj"
            // https://github.com/perpetual-motion/discutils

            // http://technet.microsoft.com/en-us/magazine/ee872416.aspx
            // http://blogs.technet.com/b/danstolts/archive/2012/11/09/how_2d00_to_2d00_mount_2d00_vhd_2d00_image_2d00_from_2d00_windows_2d00_7_2d00_step_2d00_by_2d00_step_2d00_without_2d00_any_2d00_third_2d00_party_2d00_toolsthe_2d00_easy_2d00_way.aspx

            CDBuilder builder = new CDBuilder();
            builder.UseJoliet = true;
            builder.VolumeIdentifier = "A_SAMPLE_DISK";
            builder.AddFile(@"Folder\Hello.txt", Encoding.ASCII.GetBytes("Hello World!"));

            //Additional information: The process cannot access the file 'X:\jsc.svn\examples\merge\Test\TestCreateVHD\TestCreateVHD\bin\Debug\sample.iso' because it is being used by another process.
            builder.Build(@"sample2.iso");

            //long diskSize = 4 * 1024 * 1024; //30MB
            long diskSize = 5 * 1024 * 1024; //30MB
                                             // Additional information: Requested size is too small for a partition
                                             // 		sectorCount	8143	int

            // Comupter Management/Storage/Disk Management/ shows it unallocated!

            // win7 does not know how to mount vhd?
            // 
            //Additional information: The process cannot access the file 'X:\jsc.svn\examples\merge\Test\TestCreateVHD\TestCreateVHD\bin\Debug\mydisk.vhd' because it is being used by another process.

            string fileName = new FileInfo(@"mydisk2.vhd").FullName;

            using (Stream vhdStream = File.Create(fileName))
            {
                Disk disk = Disk.InitializeDynamic(vhdStream, DiscUtils.Ownership.Dispose, diskSize);

                BiosPartitionTable.Initialize(disk, WellKnownPartitionType.WindowsFat);
                using (FatFileSystem fs = FatFileSystem.FormatPartition(disk, 0, "label1"))
                {
                    fs.CreateDirectory(@"TestDir\CHILD");
                    using (var f = new StreamWriter(fs.OpenFile(@"TestDir\Hello.txt", FileMode.OpenOrCreate, FileAccess.Write)))
                    {
                        f.WriteLine("Hello World!");
                    }

                    // do other things with the file system...
                }
            }

            // http://msdn.microsoft.com/en-us/magazine/dd569754.aspx
            // http://msdn.microsoft.com/en-us/library/windows/desktop/dd323692(v=vs.85).aspx
            // http://www.programmershare.com/1846408/
            // https://www.jmedved.com/2009/05/open-and-attach/


            // http://blogs.msdn.com/b/virtual_pc_guy/archive/2008/01/10/mounting-vhds-from-managed-code.aspx
            // http://support.microsoft.com/kb/981778
            // http://stackoverflow.com/questions/573086/how-to-elevate-privileges-only-when-required
            // This is not true. You can change the Owner of the process and set the DACL and ACL values for the user giving them administrative powers.



            IntPtr handle = IntPtr.Zero;


            // open disk handle
            var openParameters = new OPEN_VIRTUAL_DISK_PARAMETERS();
            openParameters.Version = OPEN_VIRTUAL_DISK_VERSION.OPEN_VIRTUAL_DISK_VERSION_1;
            openParameters.Version1.RWDepth = OPEN_VIRTUAL_DISK_RW_DEPTH_DEFAULT;

            var openStorageType = new VIRTUAL_STORAGE_TYPE();
            openStorageType.DeviceId = VIRTUAL_STORAGE_TYPE_DEVICE_VHD;
            openStorageType.VendorId = VIRTUAL_STORAGE_TYPE_VENDOR_MICROSOFT;

            int openResult = OpenVirtualDisk(ref openStorageType, fileName, VIRTUAL_DISK_ACCESS_MASK.VIRTUAL_DISK_ACCESS_ALL, OPEN_VIRTUAL_DISK_FLAG.OPEN_VIRTUAL_DISK_FLAG_NONE, ref openParameters, ref handle);
            if (openResult != ERROR_SUCCESS)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Native error {0}.", openResult));
            }


            // attach disk - permanently
            var attachParameters = new ATTACH_VIRTUAL_DISK_PARAMETERS();
            attachParameters.Version = ATTACH_VIRTUAL_DISK_VERSION.ATTACH_VIRTUAL_DISK_VERSION_1;
            int attachResult = AttachVirtualDisk(handle, IntPtr.Zero, ATTACH_VIRTUAL_DISK_FLAG.ATTACH_VIRTUAL_DISK_FLAG_PERMANENT_LIFETIME, 0, ref attachParameters, IntPtr.Zero);
            // attachResult = 1314
            // If you get “Native error 1314.” exception, you didn’t run code as user with administrative rights. If you get “Native error 32.”, virtual disk is already attached. 


            if (attachResult != ERROR_SUCCESS)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Native error {0}.", attachResult));
            }


            // close handle to disk
            CloseHandle(handle);


            System.Windows.Forms.MessageBox.Show("Disk is attached.");

        }




        public const Int32 ERROR_SUCCESS = 0;

        public const int OPEN_VIRTUAL_DISK_RW_DEPTH_DEFAULT = 1;

        public const int VIRTUAL_STORAGE_TYPE_DEVICE_VHD = 2;

        public static readonly Guid VIRTUAL_STORAGE_TYPE_VENDOR_MICROSOFT = new Guid("EC984AEC-A0F9-47e9-901F-71415A66345B");


        public enum ATTACH_VIRTUAL_DISK_FLAG : int
        {
            ATTACH_VIRTUAL_DISK_FLAG_NONE = 0x00000000,
            ATTACH_VIRTUAL_DISK_FLAG_READ_ONLY = 0x00000001,
            ATTACH_VIRTUAL_DISK_FLAG_NO_DRIVE_LETTER = 0x00000002,
            ATTACH_VIRTUAL_DISK_FLAG_PERMANENT_LIFETIME = 0x00000004,
            ATTACH_VIRTUAL_DISK_FLAG_NO_LOCAL_HOST = 0x00000008
        }

        public enum ATTACH_VIRTUAL_DISK_VERSION : int
        {
            ATTACH_VIRTUAL_DISK_VERSION_UNSPECIFIED = 0,
            ATTACH_VIRTUAL_DISK_VERSION_1 = 1
        }

        public enum OPEN_VIRTUAL_DISK_FLAG : int
        {
            OPEN_VIRTUAL_DISK_FLAG_NONE = 0x00000000,
            OPEN_VIRTUAL_DISK_FLAG_NO_PARENTS = 0x00000001,
            OPEN_VIRTUAL_DISK_FLAG_BLANK_FILE = 0x00000002,
            OPEN_VIRTUAL_DISK_FLAG_BOOT_DRIVE = 0x00000004
        }

        public enum OPEN_VIRTUAL_DISK_VERSION : int
        {
            OPEN_VIRTUAL_DISK_VERSION_1 = 1
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct ATTACH_VIRTUAL_DISK_PARAMETERS
        {
            public ATTACH_VIRTUAL_DISK_VERSION Version;
            public ATTACH_VIRTUAL_DISK_PARAMETERS_Version1 Version1;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct ATTACH_VIRTUAL_DISK_PARAMETERS_Version1
        {
            public Int32 Reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct OPEN_VIRTUAL_DISK_PARAMETERS
        {
            public OPEN_VIRTUAL_DISK_VERSION Version;
            public OPEN_VIRTUAL_DISK_PARAMETERS_Version1 Version1;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct OPEN_VIRTUAL_DISK_PARAMETERS_Version1
        {
            public Int32 RWDepth;
        }

        public enum VIRTUAL_DISK_ACCESS_MASK : int
        {
            VIRTUAL_DISK_ACCESS_ATTACH_RO = 0x00010000,
            VIRTUAL_DISK_ACCESS_ATTACH_RW = 0x00020000,
            VIRTUAL_DISK_ACCESS_DETACH = 0x00040000,
            VIRTUAL_DISK_ACCESS_GET_INFO = 0x00080000,
            VIRTUAL_DISK_ACCESS_CREATE = 0x00100000,
            VIRTUAL_DISK_ACCESS_METAOPS = 0x00200000,
            VIRTUAL_DISK_ACCESS_READ = 0x000d0000,
            VIRTUAL_DISK_ACCESS_ALL = 0x003f0000,
            VIRTUAL_DISK_ACCESS_WRITABLE = 0x00320000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct VIRTUAL_STORAGE_TYPE
        {
            public Int32 DeviceId;
            public Guid VendorId;
        }


        [DllImport("virtdisk.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 AttachVirtualDisk(IntPtr VirtualDiskHandle, IntPtr SecurityDescriptor, ATTACH_VIRTUAL_DISK_FLAG Flags, Int32 ProviderSpecificFlags, ref ATTACH_VIRTUAL_DISK_PARAMETERS Parameters, IntPtr Overlapped);

        [DllImportAttribute("kernel32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern Boolean CloseHandle(IntPtr hObject);

        [DllImport("virtdisk.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 OpenVirtualDisk(ref VIRTUAL_STORAGE_TYPE VirtualStorageType, String Path, VIRTUAL_DISK_ACCESS_MASK VirtualDiskAccessMask, OPEN_VIRTUAL_DISK_FLAG Flags, ref OPEN_VIRTUAL_DISK_PARAMETERS Parameters, ref IntPtr Handle);
    }
}
