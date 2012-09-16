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
using ScriptCoreLib.JavaScript.Runtime;

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
            Action<string> pre =
                value =>
                {

                };

            #region f
            Action<string, string, Action<string, Action<string>>> f =
                (text, arg1, c) =>
                {
                    var btn = new IHTMLButton(text).AttachToDocument();
                    var output = new IHTMLDiv().AttachToDocument();

                    btn.onclick +=
                        e =>
                        {
                            btn.style.color = JSColor.Red;

                            c(arg1,
                                value =>
                                {
                                    btn.style.color = JSColor.Blue;

                                    new IHTMLPre { innerText = value }.AttachTo(output);

                                }
                            );
                        }
                    ;
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
                    f("Environment_getExternalStoragePublicDirectory DIRECTORY_MUSIC", DIRECTORY_MUSIC, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStoragePublicDirectory DIRECTORY_PODCASTS", DIRECTORY_PODCASTS, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStoragePublicDirectory DIRECTORY_RINGTONES", DIRECTORY_RINGTONES, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStoragePublicDirectory DIRECTORY_ALARMS", DIRECTORY_ALARMS, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStoragePublicDirectory DIRECTORY_NOTIFICATIONS", DIRECTORY_NOTIFICATIONS, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStoragePublicDirectory DIRECTORY_PICTURES", DIRECTORY_PICTURES, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStoragePublicDirectory DIRECTORY_MOVIES", DIRECTORY_MOVIES, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStoragePublicDirectory DIRECTORY_DOWNLOADS", DIRECTORY_DOWNLOADS, service.Environment_getExternalStoragePublicDirectory);
                    f("Environment_getExternalStoragePublicDirectory DIRECTORY_DCIM", DIRECTORY_DCIM, service.Environment_getExternalStoragePublicDirectory);
                }
            );

            f("Environment_getExternalStorageState", "", service.Environment_getExternalStorageState);
            f("Environment_getRootDirectory", "", service.Environment_getRootDirectory);

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
