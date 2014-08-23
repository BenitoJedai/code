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
using System.Threading.Tasks;

namespace AndroidEnvironmentWebActivity
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        //public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            var service = this;

            #region pre
            Func<string, IHTMLDiv, IHTMLElement> pre =
                (value, output) =>
                {
                    return new IHTMLPre { innerText = value }.AttachTo(output);
                };
            #endregion

            #region pre
            Func<string, IHTMLDiv, IHTMLElement> browse = null;

            browse =
                (path, output) =>
                {

                    var list = new IHTMLButton { innerText = path }.AttachTo(output);


                    var group = new IHTMLDiv().AttachTo(output);




                    list.onclick +=
                        delegate
                        {
                            group.style.margin = "1em";
                            group.style.paddingLeft = "1em";
                            group.style.border = "1px solid gray";

                            list.disabled = true;

                            service.File_list(path,
                                ydirectory: value =>
                                {
                                    browse(path + "/" + value, group);
                                },

                                yfile: value =>
                                {
                                    var link = new IHTMLAnchor { href = "/io" + path + "/" + value, innerText = value };

                                    link.style.display = IStyle.DisplayEnum.block;
                                    link.AttachTo(group);
                                }
                            );
                        };

                    return group;
                };
            #endregion



            #region f
            Action<string, string, Action<string, Action<string>>, Func<string, IHTMLDiv, IHTMLElement>> f =
                (text, arg1, c, y) =>
                {
                    var btn = new IHTMLButton(text).AttachToDocument();
                    var output = new IHTMLDiv().AttachToDocument();

                    btn.onclick +=
                        e =>
                        {
                            btn.style.color = JSColor.Red;

                            output.Clear();

                            c(arg1,
                                value =>
                                {
                                    btn.style.color = JSColor.Blue;


                                    y(value, output);
                                }
                            );
                        }
                    ;
                };
            #endregion

            #region ff
            Action<string, Func<Task<string>>, Func<string, IHTMLDiv, IHTMLElement>> ff =
                (text, c, y) =>
                {
                    var btn = new IHTMLButton(text).AttachToDocument();
                    var output = new IHTMLDiv().AttachToDocument();

                    btn.WhenClicked(
                        async e =>
                        {
                            btn.style.color = JSColor.Red;

                            output.Clear();

                            var value = await c();

                            btn.style.color = JSColor.Blue;


                            y(value, output);
                        }
                     );
                };
            #endregion


#if CORE_PARTIAL
            ff("Environment_getDataDirectory", () => service.Environment_getDataDirectory(), browse);
            ff("Environment_getDownloadCacheDirectory", () => service.Environment_getDownloadCacheDirectory(), browse);
            ff("Environment_getExternalStorageDirectory", () => service.Environment_getExternalStorageDirectory(), browse);
            ff("Environment_getExternalStorageState", () => service.Environment_getExternalStorageState(), pre);
            ff("Environment_getRootDirectory", () => service.Environment_getRootDirectory(), browse);

            //service.Environment_DIRECTORY("",
            //    (
            //        string DIRECTORY_MUSIC,
            //        string DIRECTORY_PODCASTS,
            //        string DIRECTORY_RINGTONES,
            //        string DIRECTORY_ALARMS,
            //        string DIRECTORY_NOTIFICATIONS,
            //        string DIRECTORY_PICTURES,
            //        string DIRECTORY_MOVIES,
            //        string DIRECTORY_DOWNLOADS,
            //        string DIRECTORY_DCIM
            //    ) =>

            f("Environment_getExternalStoragePublicDirectory DIRECTORY_MUSIC", DIRECTORY_MUSIC, service.Environment_getExternalStoragePublicDirectory, browse);
            f("Environment_getExternalStoragePublicDirectory DIRECTORY_PODCASTS", DIRECTORY_PODCASTS, service.Environment_getExternalStoragePublicDirectory, browse);
            f("Environment_getExternalStoragePublicDirectory DIRECTORY_RINGTONES", DIRECTORY_RINGTONES, service.Environment_getExternalStoragePublicDirectory, browse);
            f("Environment_getExternalStoragePublicDirectory DIRECTORY_ALARMS", DIRECTORY_ALARMS, service.Environment_getExternalStoragePublicDirectory, browse);
            f("Environment_getExternalStoragePublicDirectory DIRECTORY_NOTIFICATIONS", DIRECTORY_NOTIFICATIONS, service.Environment_getExternalStoragePublicDirectory, browse);
            f("Environment_getExternalStoragePublicDirectory DIRECTORY_PICTURES", DIRECTORY_PICTURES, service.Environment_getExternalStoragePublicDirectory, browse);
            f("Environment_getExternalStoragePublicDirectory DIRECTORY_MOVIES", DIRECTORY_MOVIES, service.Environment_getExternalStoragePublicDirectory, browse);
            f("Environment_getExternalStoragePublicDirectory DIRECTORY_DOWNLOADS", DIRECTORY_DOWNLOADS, service.Environment_getExternalStoragePublicDirectory, browse);

#endif

            f("Environment_getExternalStoragePublicDirectory DIRECTORY_DCIM", DIRECTORY_DCIM, service.Environment_getExternalStoragePublicDirectory, browse);



            new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();



            // new IHTMLButton("Environment_getDownloadCacheDirectory").AttachToDocument().onclick +=
            //    e => service.Environment_getDownloadCacheDirectory("",
            //            value => new IHTMLPre { innerText = value }.AttachToDocument()
            //);
        }

    }
}
