using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AndroidEnvironmentWebActivity.Design;
using AndroidEnvironmentWebActivity.HTML.Pages;

namespace AndroidEnvironmentWebActivity
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            #region f
            Action<string, string, Action<string, Action<string>>> f =
                (text, arg1, c) =>
                {
                    var btn = new IHTMLButton(text).AttachToDocument();
                    var output = new IHTMLDiv().AttachToDocument();

                    btn.onclick +=
                        e => c(arg1,
                                value => new IHTMLPre { innerText = value }.AttachTo(output)
                    );
                };
            #endregion

            f("Environment_getDataDirectory", "", service.Environment_getDataDirectory);
            f("Environment_getDownloadCacheDirectory", "", service.Environment_getDownloadCacheDirectory);
            f("Environment_getExternalStorageDirectory", "", service.Environment_getExternalStorageDirectory);

            service.Environment_DIRECTORY("",
                (
                    string DIRECTORY_MUSIC,
                    string DIRECTORY_PODCASTS,
                    string DIRECTORY_RINGTONES,
                    string DIRECTORY_ALARMS,
                    string DIRECTORY_NOTIFICATIONS,
                    string DIRECTORY_PICTURES,
                    string DIRECTORY_MOVIES,
                    string DIRECTORY_DOWNLOADS,
                    string DIRECTORY_DCIM
                ) =>
                {
                    f("Environment_getExternalStorageDirectory DIRECTORY_MUSIC", DIRECTORY_MUSIC, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStorageDirectory DIRECTORY_PODCASTS", DIRECTORY_PODCASTS, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStorageDirectory DIRECTORY_RINGTONES", DIRECTORY_RINGTONES, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStorageDirectory DIRECTORY_ALARMS", DIRECTORY_ALARMS, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStorageDirectory DIRECTORY_NOTIFICATIONS", DIRECTORY_NOTIFICATIONS, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStorageDirectory DIRECTORY_PICTURES", DIRECTORY_PICTURES, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStorageDirectory DIRECTORY_MOVIES", DIRECTORY_MOVIES, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStorageDirectory DIRECTORY_DOWNLOADS", DIRECTORY_DOWNLOADS, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStorageDirectory DIRECTORY_DCIM", DIRECTORY_DCIM, service.Environment_getExternalStoragePublicDirectory);
                }
            );


            // new IHTMLButton("Environment_getDataDirectory").AttachToDocument().onclick +=
            //     e =>  service.Environment_getDataDirectory("",
            //             value => new IHTMLPre { innerText = value }.AttachToDocument()
            // );

            // new IHTMLButton("Environment_getDownloadCacheDirectory").AttachToDocument().onclick +=
            //    e => service.Environment_getDownloadCacheDirectory("",
            //            value => new IHTMLPre { innerText = value }.AttachToDocument()
            //);
        }

    }
}
