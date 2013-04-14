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
using Abstractatech.JavaScript.FileStorage.Design;
using Abstractatech.JavaScript.FileStorage.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using System.Collections.Generic;

namespace Abstractatech.JavaScript.FileStorage
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly IApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var content = new ApplicationContent(
                page,

                service
            );

        }

    }

    public static class X
    {
        public static IEnumerable<File> AsEnumerable(this FileList f)
        {
            return Enumerable.Range(0, (int)f.length).Select(k => f[(uint)k]);
        }

    }

    public class ApplicationContent
    {
        public static string Target = "_blank";

        public ApplicationContent(
            IApp page = null,
            IApplicationWebService service = null)
        {
            // need absolute path when docked..
            page.style1.href = page.style1.href;

            // first order of business.
            // enable drop zone.
            var dz = new DropZone();


            dz.Container.AttachToDocument();
            dz.Container.Hide();

            var StayAlertTimer = default(Timer);
            var DoRefresh = default(Action);

            #region StayAlert
            Action<string> StayAlert =
                transaction_id =>
                {
                    StayAlertTimer = new Timer(
                        delegate
                        {
                            service.GetTransactionKeyAsync(
                                id =>
                                {
                                    if (id == transaction_id)
                                        return;

                                    // shot down during flight?
                                    if (!StayAlertTimer.IsAlive)
                                        return;

                                    Console.WriteLine("StayAlert " + new { id, transaction_id });

                                    DoRefresh();
                                }
                            );
                        }
                    );

                    StayAlertTimer.StartInterval(5000);
                };
            #endregion


            DoRefresh =
                delegate
                {
                    if (StayAlertTimer != null)
                        StayAlertTimer.Stop();

                    page.Output.Clear();

                    new FileLoading().Container.AttachTo(page.Output);

                    service.EnumerateFilesAsync(
                        y:
                        (
                            string ContentKey,
                            string ContentValue,
                            string ContentType,
                            string ContentBytesLength
                        ) =>
                        {
                            var e = new FileEntry();

                            #region ContentValue
                            e.ContentValue.value = ContentValue.TakeUntilLastIfAny(".");
                            e.ContentValue.onchange +=
                                delegate
                                {
                                    var ext = ContentValue.SkipUntilLastOrEmpty(".");

                                    if (ext != "")
                                        ext = "." + ext;

                                    ContentValue = e.ContentValue.value + ext;


                                    Console.WriteLine("before update!");

                                    service.UpdateAsync(
                                        ContentKey,
                                        ContentValue,
                                        // null does not really work?
                                        delegate
                                        {
                                            Console.WriteLine("update done!");
                                        }
                                    );

                                    e.open.href = Native.Document.location.href.TakeUntilLastIfAny("/") + "/io/" + ContentKey + "/" + ContentValue;
                                };
                            e.open.href = Native.Document.location.href.TakeUntilLastIfAny("/") + "/io/" + ContentKey + "/" + ContentValue;
                            e.open.target = Target;

                            #endregion

                            e.ContentType.innerText = ContentBytesLength + " bytes " + ContentType;


                            #region Delete
                            e.Delete.WhenClicked(
                                delegate
                                {
                                    //e.ContentValue.style.textDecoration = ""

                                    if (StayAlertTimer != null)
                                        StayAlertTimer.Stop();

                                    e.Container.style.backgroundColor = "red";

                                    service.DeleteAsync(
                                        ContentKey,
                                        delegate
                                        {

                                            DoRefresh();
                                        }
                                    );
                                }
                            );
                            #endregion


                            e.Container.AttachTo(page.Output);



                            Console.WriteLine(
                                new { ContentKey, ContentValue, ContentType, ContentBytesLength }
                            );

                        },

                        done: transaction_id =>
                        {
                            Console.WriteLine(new { transaction_id });
                            new FileLoadingDone().Container.AttachTo(page.Output);

                            StayAlert(transaction_id);
                        }
                    );
                };

            #region ondrop

            var TimerHide = new Timer(
                delegate
                {
                    dz.Container.Hide();
                }
            );

            Native.Document.body.ondragover +=
                evt =>
                {
                    evt.stopPropagation();
                    evt.preventDefault();

                    evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.

                    dz.Container.Show();
                    //Console.WriteLine(" Native.Document.body.ondragover");
                };

            dz.Container.ondragover +=
                evt =>
                {
                    evt.stopPropagation();
                    evt.preventDefault();

                    evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.
                    //Console.WriteLine(" dz.Container.ondragover");

                    TimerHide.Stop();
                };

            dz.Container.ondragleave +=
                 evt =>
                 {
                     //Console.WriteLine(" dz.Container.ondragleave");

                     evt.stopPropagation();
                     evt.preventDefault();

                     TimerHide.StartTimeout(50);
                 };

            dz.Container.ondrop +=
                evt =>
                {
                    TimerHide.StartTimeout(50);

                    evt.stopPropagation();
                    evt.stopImmediatePropagation();

                    evt.preventDefault();

                    Console.WriteLine("ondrop");

                    var xhr = new IXMLHttpRequest();

                    // does not work for chrome?
                    //xhr.setRequestHeader("WebServiceMethod", "FileStorageUpload");

                    // which server?
                    xhr.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/FileStorageUpload");

                    #region send
                    var d = new FormData();

                    evt.dataTransfer.files.AsEnumerable().WithEachIndex(
                        (f, index) =>
                        {
                            d.append("file" + index, f, f.name);
                        }
                    );

                    xhr.InvokeOnComplete(
                        delegate
                        {
                            Console.WriteLine("upload complete!");

                            DoRefresh();
                        }
                    );

                    var upload = new Uploading();

                    upload.Container.AttachTo(page.Output);
                    // http://www.matlus.com/html5-file-upload-with-progress/
                    xhr.upload.onprogress +=
                        e =>
                        {
                            var p = (int)(e.loaded * 100 / e.total) + "%";

                            upload.status = p;

                            Console.WriteLine("upload.onprogress " + new { e.total, e.loaded });
                        };

                    xhr.send(d);
                    #endregion


                    if (StayAlertTimer != null)
                        StayAlertTimer.Stop();



                };
            #endregion




            DoRefresh();
        }
    }
}
