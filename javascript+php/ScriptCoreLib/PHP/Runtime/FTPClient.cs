using ScriptCoreLib;

using IDisposable = System.IDisposable;

namespace ScriptCoreLib.PHP.Runtime
{
    [Script]
    public class FTPClient : IDisposable
    {
        [Script]
        private class API
        {
            #region bool ftp_delete ( resource ftp_stream, string path )

            /// <summary>
            /// ftp_delete() deletes the file specified by path from the FTP server. 
            /// </summary>
            /// <param name="_ftp_stream">resource ftp_stream</param>
            /// <param name="_path">string path</param>
            [Script(IsNative = true)]
            public static bool ftp_delete(object _ftp_stream, string _path) { return default(bool); }

            #endregion


            #region bool ftp_rename ( resource ftp_stream, string oldname, string newname )

            /// <summary>
            /// ftp_rename() renames a file or a directory on the FTP server. 
            /// </summary>
            /// <param name="_ftp_stream">resource ftp_stream</param>
            /// <param name="_oldname">string oldname</param>
            /// <param name="_newname">string newname</param>
            [Script(IsNative = true)]
            public static bool ftp_rename(object _ftp_stream, string _oldname, string _newname) { return default(bool); }

            #endregion


            #region int ftp_mdtm ( resource ftp_stream, string remote_file )

            /// <summary>
            /// ftp_mdtm() gets the last modified time for a remote file. 
            /// </summary>
            /// <param name="_ftp_stream">resource ftp_stream</param>
            /// <param name="_remote_file">string remote_file</param>
            [Script(IsNative = true)]
            public static int ftp_mdtm(object _ftp_stream, string _remote_file) { return default(int); }

            #endregion


            #region bool ftp_chdir ( resource ftp_stream, string directory )

            /// <summary>
            /// Changes the current directory to the specified one. 
            /// </summary>
            /// <param name="_ftp_stream">resource ftp_stream</param>
            /// <param name="_directory">string directory</param>
            [Script(IsNative = true)]
            public static bool ftp_chdir(object _ftp_stream, string _directory) { return default(bool); }

            #endregion


            #region resource ftp_connect ( string host [, int port [, int timeout]] )

            /// <summary>
            /// ftp_connect() opens an FTP connection to the specified host. 
            /// </summary>
            /// <param name="_host">string host</param>
            [Script(IsNative = true)]
            public static object ftp_connect(string _host) { return default(object); }

            #endregion

            #region bool ftp_login ( resource ftp_stream, string username, string password )

            /// <summary>
            /// Logs in to the given FTP stream. 
            /// </summary>
            /// <param name="_ftp_stream">resource ftp_stream</param>
            /// <param name="_username">string username</param>
            /// <param name="_password">string password</param>
            [Script(IsNative = true)]
            public static bool ftp_login(object _ftp_stream, string _username, string _password) { return default(bool); }

            #endregion

            #region bool ftp_fput ( resource ftp_stream, string remote_file, resource handle, int mode [, int startpos] )

            /// <summary>
            /// ftp_fput() uploads the data from a file pointer to a remote file on the FTP server. 
            /// </summary>
            /// <param name="_ftp_stream">resource ftp_stream</param>
            /// <param name="_remote_file">string remote_file</param>
            /// <param name="_handle">resource handle</param>
            /// <param name="_mode">int mode</param>
            [Script(IsNative = true)]
            public static bool ftp_fput(object _ftp_stream, string _remote_file, object _handle, int _mode) { return default(bool); }

            #endregion

            #region bool ftp_close ( resource ftp_stream )

            /// <summary>
            /// ftp_close() closes the given link identifier and releases the resource. 
            /// </summary>
            /// <param name="_ftp_stream">resource ftp_stream</param>
            [Script(IsNative = true)]
            public static bool ftp_close(object _ftp_stream) { return default(bool); }

            #endregion

            #region bool ftp_pasv ( resource ftp_stream, bool pasv )

            /// <summary>
            /// ftp_pasv() turns on or off passive mode. In passive mode, data connections are initiated by the client, rather than by the server. It may be needed if the client is behind firewall. 
            /// </summary>
            /// <param name="_ftp_stream">resource ftp_stream</param>
            /// <param name="_pasv">bool pasv</param>
            [Script(IsNative = true)]
            public static bool ftp_pasv(object _ftp_stream, bool _pasv) { return default(bool); }

            #endregion

            #region string ftp_pwd ( resource ftp_stream )

            /// <summary>
            /// 
            /// </summary>
            /// <param name="_ftp_stream">resource ftp_stream</param>
            [Script(IsNative = true)]
            public static string ftp_pwd(object _ftp_stream) { return default(string); }

            #endregion


        }

        object handle;

        string _host;
        string _user;

        string _path;

        private bool _pasv;

        public bool PassiveMode
        {
            get { return _pasv; }
            set { _pasv = value; API.ftp_pasv(handle, value); }
        }
            
        public string Path
        {
            get
            {
                if (_path == null)
                {
                    _path = API.ftp_pwd(handle);
                }

                return _path;
            }

            set
            {
                VerboseLog("ftp chdir " + value);

                _path = value;

                API.ftp_chdir(handle, value);
            }
        }

        public FTPClient()
        {

        }

        public void Connect(string host)
        {
            _host = host;

            VerboseLog("ftp connect to " + _host);


            handle = API.ftp_connect(host);
        }

        public bool LogIn(string user, string pass)
        {
            _user = user;

            VerboseLog("ftp login " + Location);

            return API.ftp_login(handle, user, pass);
        }

        public bool Verbose;

        void VerboseLog(string e)
        {
            if (Verbose)
                Native.Message(Native.DateTime + " " + e);

            Native.Log(e);
        }

        public void Put(string destination, string source)
        {
            if (!Native.API.is_readable(source))
                return;

            int size = Native.API.filesize(source);


            VerboseLog("ftp put " + source + " -> " + Location + "/" + destination + " (" + size + " bytes)");

            int time = Native.API.time();

            object r = Native.API.fopen(source, "r");

            Put(destination, r, 1);

            Native.API.fclose(r);

            int elapse = Native.API.time() - time;
            int size_kB = size / 1024;


            int kBps = size_kB;

            if (elapse > 0)
                kBps /= elapse;

            VerboseLog("ftp put " + source + " -> " + Location + "/" + destination + " done (" + kBps + " kBps)");

        }

        public string Location
        {
            get
            {
                if (_user == null)
                    return "ftp://" + _user + "@" + _host;

                return "ftp://" + _user + "@" + _host + "/" + Path;
            }
        }

        private void Put(string destination, object source, int mode)
        {

            API.ftp_fput(handle, destination, source, mode);


        }

        #region IDisposable Members

        public void Dispose()
        {

            if (handle != null)
            {
                API.ftp_close(handle);

                VerboseLog("ftp disconnect from " + _host);


                handle = null;
            }
        }

        #endregion

        public int LastModifiedTime(string ftpfile)
        {
            return API.ftp_mdtm(handle, ftpfile);
        }



        public void UpdateRemoteDir(string localpath)
        {
            string[] p = Native.API.scandir(localpath);

            foreach (string v in p)
            {
                string f = localpath + "/" + v;

                if (Native.API.is_file(f))
                {
                    Put(v, f, true);
                }
            }
        }

        public bool Delete(string e)
        {
            VerboseLog("ftp delete {" + e + "}");

            return API.ftp_delete(handle, e);
        }

        public bool Rename(string from, string to)
        {
            VerboseLog("ftp rename {" + from + "} -> {" + to + "}");

            return API.ftp_rename(handle, from, to);
        }

        public bool RenameOnUpload = true;

        public void Put(string d, string s, bool keepnewer)
        {
            if (keepnewer)
            {
                int r = LastModifiedTime(d);
                int l = Native.API.filemtime(s);

                if (r >= l)
                {
                    VerboseLog("ftp put [uptodate] " + s + " -> " + Location + "/" + d);

                    return;
                }
            }

            if (RenameOnUpload)
            {
                string t = d + "." + Native.API.time();

                Put(t, s);
                Delete(d);
                Rename(t, d);

                return;
            }

            Put(d, s);
        }
    }
}
