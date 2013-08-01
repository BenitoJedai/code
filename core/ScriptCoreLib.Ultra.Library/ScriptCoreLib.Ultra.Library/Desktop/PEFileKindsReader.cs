using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection.Emit;
using System.Reflection;

namespace ScriptCoreLib.Desktop
{
    // http://stackoverflow.com/questions/3111669/how-can-i-determine-the-subsystem-used-by-a-given-net-assembly
    public static class PEFileKindsReader
    {
        public static PEFileKinds GetPEFileKinds(this MemberInfo e)
        {
            return GetPEFileKinds(Path.GetFullPath(e.Module.Assembly.Location));

        }
        /// <summary>
        /// Parses the PE header and determines whether the given assembly is a console application.
        /// </summary>
        /// <param name="assemblyPath">The path of the assembly to check.</param>
        /// <remarks>The magic numbers in this method are extracted from the PE/COFF file
        /// format specification available from http://www.microsoft.com/whdc/system/platform/firmware/pecoff.mspx
        /// </remarks>
        static PEFileKinds GetPEFileKinds(string assemblyPath)
        {
            using (var s = new FileStream(assemblyPath, FileMode.Open, FileAccess.Read))
            {
                return GetPEFileKinds(s);
            }
        }

        private static PEFileKinds GetPEFileKinds(Stream s)
        {
            var rawPeSignatureOffset = new byte[4];
            s.Seek(0x3c, SeekOrigin.Begin);
            s.Read(rawPeSignatureOffset, 0, 4);
            int peSignatureOffset = rawPeSignatureOffset[0];
            peSignatureOffset |= rawPeSignatureOffset[1] << 8;
            peSignatureOffset |= rawPeSignatureOffset[2] << 16;
            peSignatureOffset |= rawPeSignatureOffset[3] << 24;
            var coffHeader = new byte[24];
            s.Seek(peSignatureOffset, SeekOrigin.Begin);
            s.Read(coffHeader, 0, 24);
            byte[] signature = { (byte)'P', (byte)'E', (byte)'\0', (byte)'\0' };
            for (int index = 0; index < 4; index++)
            {
                if (coffHeader[index] != signature[index])
                    throw new InvalidOperationException(
                        "Attempted to check a non PE file for the console subsystem!"
                    );
            }
            var subsystemBytes = new byte[2];
            s.Seek(68, SeekOrigin.Current);
            s.Read(subsystemBytes, 0, 2);
            int subSystem = subsystemBytes[0] | subsystemBytes[1] << 8;


            // http://msdn.microsoft.com/en-us/library/fcc1zstk(v=vs.71).aspx
            return
                // http://support.microsoft.com/kb/90493
                subSystem == 3 ? PEFileKinds.ConsoleApplication :
                subSystem == 2 ? PEFileKinds.WindowApplication :
                PEFileKinds.Dll; /*IMAGE_SUBSYSTEM_WINDOWS_CUI*/
        }
    }
}
