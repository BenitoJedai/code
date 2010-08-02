using ScriptCoreLib;


namespace ScriptCoreLib.PHP.IO
{
    [Script, System.Obsolete]
    class FileInfo
    {
        public void Delete()
        {
            Native.API.unlink(FullPath);
        }

        public int Size
        {
            get
            {
                return Native.API.filesize(FullPath);
            }
        }

        public string FullPath;

        public string FileTitle
        {
            get
            {
                string u = FileName;

                int p = u.LastIndexOf(".");

                if (p > -1)
                    u = u.Substring(0, p);

                return u;
            }
        }

        /// <summary>
        /// returns the base name eg. just file.txt
        /// </summary>
        public string FileName
        {
            get
            {
                return Native.API.basename(FullPath);
            }
        }

        public string DirectoryName
        {
            get
            {
                return Native.API.dirname(FullPath);
            }
        }

        public bool IsFile
        {
            get
            {
                return Native.API.is_file(FullPath);
            }
        }

        public bool IsReadable
        {
            get
            {
                return Native.API.is_readable(FullPath);
            }
        }

        public string[] Lines
        {
            get
            {
                return Text.Split('\n');
            }
        }

        /// <summary>
        /// returns file content as string
        /// </summary>
        public string Text
        {
            get
            {
                if (!IsReadable)
                    return "";

                return Native.API.file_get_contents(FullPath);
            }
        }

        public bool IsDirectory
        {
            get
            {
                return Native.API.is_dir(FullPath);
            }
        }

        public static FileInfo OfPath(string e)
        {
            FileInfo n = new FileInfo();

            n.FullPath = e;

            return n;
        }

        public void Append(string e)
        {
            FileSystemInfo.WriteFile(this.FullPath, e, true);
        }

        public void AppendLine(string e)
        {
            Append(e + "\n");
        }

        public void WriteToStream()
        {
            
            Native.echo(Text);
        }

        public void DumpStatus()
        {
            if (IsReadable)
            {
                Native.Message(FullPath + " is readable");
            }
            else
            {
                Native.Error(FullPath + " is not readable");
            }
        }

        /// <summary>
        /// returns the a fileinfo which will hold an invalid
        /// link to the last directory in list, or to a directory 
        /// where the given file is readable
        /// </summary>
        /// <param name="f"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static FileInfo OfAny(string f, params DirectoryInfo[] e)
        {
            FileInfo p = null;

            foreach (DirectoryInfo v in e)
            {
                p = v[f];

                if (p.IsReadable)
                    break;
            }

            return p;
        }

        public void WriteBinaryToStream()
        {
            object h = Native.API.fopen(FullPath, "rb");

            Native.API.fpassthru(h);
        }
    }
}
