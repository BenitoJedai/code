using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared;
using System;
using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.Cordova
{
    [Script(HasNoPrototype = true)]
    public class ICordovaWindow : IWindow
    {

        public Device device;

        public LocalStorage localStorage;

        public CordovaNavigatorInfo navigator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">LocalFileSystem.TEMPORARY, PERSISTENT, RESOURCE or APPLICATION</param>
        /// <param name="size"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onError"></param>
        public void requestFileSystem(int type, int size, Action<LocalFileSystem> onSuccess, Action<FileError> onError)
        { }

        /// <summary>
        /// resolveLocalFileSystemURI: Retrieve a DirectoryEntry or FileEntry using local URI.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onError"></param>
        public void resolveLocalFileSystemURI(int type, int size, Action<LocalFileSystem> onSuccess, Action<FileError> onError)
        { }

        /// <summary>
        /// Returns a new Database object.
        /// </summary>
        /// <param name="database_name"></param>
        /// <param name="database_version"></param>
        /// <param name="database_displayname"></param>
        /// <param name="database_size"></param>
        /// <returns></returns>
        public Database openDatabase(string database_name, string database_version, string database_displayname, int database_size)
        {
            return default(Database);
        }


    }

    [Script]
    public class CordovaNavigatorInfo
    {
        public Accelerometer accelerometer;

        public Camera camera;

        public Capture device;

        public Notification notification;

        public Compass compass;

        public Geolocation geolocation;

        public Connection network;

        public Contacts contacts;

    }
}
