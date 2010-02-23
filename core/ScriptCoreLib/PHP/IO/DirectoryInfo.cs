using ScriptCoreLib;


namespace ScriptCoreLib.PHP.IO
{
	[Script, System.Obsolete]
	internal class DirectoryInfo
    {
        public string FullPath;

        public FileInfo this[string e]
        {
            get
            {
                return GetFileInfo(e);
            }
        }

        public FileInfo[] Files
        {
            get
            {
                return FileSystemInfo.GetFiles(FullPath);
            }
        }

        public string DirectoryName
        {
            get
            {
                return Native.API.dirname(FullPath);
            }
        }

        public bool IsDirectory
        {
            get
            {
                return Native.API.is_dir(FullPath);
            }
        }

        public bool IsReadable
        {
            get
            {
                return Native.API.is_readable(FullPath);
            }
        }

 

        public static DirectoryInfo OfPath(string e)
        {
            DirectoryInfo n = new DirectoryInfo();

            n.FullPath = e;

            return n;
        }



        public FileInfo GetFileInfo(string p)
        {
            return FileInfo.OfPath(FullPath + "/" + p);
        }

        public DirectoryInfo GetDirectoryInfo(string p)
        {
            return DirectoryInfo.OfPath(FullPath + "/" + p);
        }

        public void Ensure()
        {
            FileSystemInfo.EnsureDirectory(FullPath);
        }
    }
}
