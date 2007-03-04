using ScriptCoreLib;

using ScriptCoreLib.PHP;
using ScriptCoreLib.PHP.IO;

using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.Runtime
{
    [Script]
    public class CacheHelper : global::System.IDisposable
    {
        public CacheHelper()
        {
            Native.API.ob_start();
        }

        public int Length
        {
            get
            {
                return Native.API.ob_get_length();
            }
        }

        bool _clean;

        #region IDisposable Members

        public void Dispose()
        {
            if (_clean)
                Native.API.ob_end_clean();
            else
                Native.API.ob_end_flush();
        }

        #endregion

        public string Text
        {
            get
            {
                return Native.API.ob_get_contents();
            }
        }
        public void Save(string cfile)
        {
            string path = Native.API.dirname(cfile);

            if (!Native.API.is_dir(path))
                Native.API.mkdir(path);

            object f = Native.API.fopen(cfile, "w");

            Native.API.fwrite(f, Text);
            Native.API.fclose(f);
        }

        public static string CacheOutOfDateTimeString(string cachefile, int time)
        {
            if (!Native.API.is_readable(cachefile))
                return null;

            int z = Native.API.filemtime(cachefile);
            int now = Native.API.time();

            return Native.API.date(Native.DateTimeFormat, z + time);
        }

        public static bool IsCacheInvalid(string cachefile, int time)
        {
            if (!Native.API.is_readable(cachefile))
                return true;

            int z = Native.API.filemtime(cachefile);
            int now = Native.API.time();



            return !(now - z < time);
        }

        /// <summary>
        /// returns true, if file exists and is not too old, also echos the file
        /// </summary>
        /// <param name="cfile"></param>
        /// <param name="time">seconds</param>
        /// <returns></returns>
        public static bool StreamCache(string cfile, int time)
        {
            //Native.API.clearstatcache();

            // unix stations require file write permissions

            bool b = !IsCacheInvalid(cfile, time);

            if (b)
            {
                Native.echo(Native.API.file_get_contents(cfile));
            }

            return b;
        }

        public string Clean()
        {
            _clean = true;

            return Text;
        }

        /// <summary>
        /// returns the name based on the given url
        /// </summary>
        /// <param name="cachepath"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string CachedDownloadName(string cachepath, string url)
        {
            FileSystemInfo.EnsureDirectory(cachepath);


            string md5 = Native.API.md5(url);
            string cfile = cachepath + "/" + md5;

            int i = url.LastIndexOf(".");

            string ext = url.Substring(i + 1);

            if (ext.Length < 5)
            {
                cfile += "." + ext;
            }

            return cfile;
        }

        /// <summary>
        /// actually downloads the file, hard cache method
        /// </summary>
        /// <param name="cachepath"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string CachedDownload(string cachepath, string url)
        {
            if (url == null)
                return null;

            string cfile = CachedDownloadName(cachepath, url);


            if (Native.API.is_readable(cfile))
            {
                return Native.API.file_get_contents(cfile);
            }

            Native.Log("cache {" + url + "} to " + cfile);

            Native.API.ob_start();

            string d = Native.API.file_get_contents(url);

            Native.API.ob_end_clean();

            FileSystemInfo.WriteFile(cfile, d);

            return d;
        }

        public static void Stream(string e)
        {
            Native.echo(Native.API.file_get_contents(e));
        }
    }


}
