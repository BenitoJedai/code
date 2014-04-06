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
using ApplicationSnapshotStorage.Design;
using ApplicationSnapshotStorage.HTML.Pages;
using System.Windows.Forms;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace ApplicationSnapshotStorage
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //  a[0].asIDisposable._0h0ABmqfyzOLoZ_b8v0KVxw();
            // X:\jsc.svn\examples\javascript\test\TestPackageAsApplication\TestPackageAsApplication\Application.cs

            // X:\jsc.svn\examples\javascript\WebGLSpiral\WebGLSpiral\Application.cs
            // "X:\jsc.svn\examples\javascript\forms\MSVSFormStyle\MSVSFormStyle.sln"
            // X:\jsc.svn\examples\javascript\canvas\CanvasPlasma\CanvasPlasma\Application.cs

            new IHTMLPre { innerText = (Native.Document.location + "") }.AttachToDocument();
            // does it work for android?

            #region ondrop put html into service.snapshot
            Native.Document.body.ondragover +=
              evt =>
              {
                  evt.stopPropagation();
                  evt.preventDefault();
                  evt.dataTransfer.dropEffect = "copy";
              };

            Native.Document.body.ondrop +=
              evt =>
              {
                  evt.preventDefault();
                  evt.stopPropagation();

                  #region AtDocumentText
                  Action<string> AtDocumentText = DocumentText =>
                  {
                      new Form { Text = DocumentText.Length + " bytes" }.With(
                        f =>
                        {
                            var w = new WebBrowser
                            {
                                Dock = DockStyle.Fill,
                                DocumentText = DocumentText
                            }.AttachTo(f);

                            f.Show();
                            f.Opacity = 0.2;

                            Console.WriteLine("service.snapshot_Insert");

                            //03.12.2012 18:25:28 AppSnapshot
                            //AsWithConnection...
                            //AsWithConnection... invoke
                            //03.12.2012 18:27:02 AppSnapshot
                            //AsWithConnection...
                            //AsWithConnection... invoke
                            //snapshot_Insert
                            //03.12.2012 18:27:02 Insert... { Length = 1155962 }
                            //AsWithConnection... invoke
                            //03.12.2012 18:27:02 Insert... Command
                            //03.12.2012 18:27:02 Insert... AddWithValue
                            //03.12.2012 18:27:02 Insert... ExecuteNonQuery
                            //we have InternalParameters for insert into AppSnapshot (AppSnapshotContent) values (@AppSnapshotContent)
                            //03.12.2012 18:27:03 Insert { LastInsertRowId = 4 }

                            //for app engine this might take 2 minutes? is it a problem in our encoder?
                            // time to switch to unencoded post by bytes?

                            //return;

                            // any bytearray shall be sent as file upload?
                            this.snapshot_Insert(DocumentText,
                                AppSnapshotKey =>
                                {
                                    Console.WriteLine("service.snapshot_Insert " + new { AppSnapshotKey });

                                    f.Text = ApplicationWebService.prefix + AppSnapshotKey;

                                    // should we?
                                    w.Navigate(ApplicationWebService.prefix + AppSnapshotKey);
                                    f.Opacity = 1;

                                    f.FormClosing +=
                                        delegate
                                        {
                                            this.snapshot_Delete(AppSnapshotKey);
                                        };
                                }
                            );


                        }
                    );
                  };
                  #endregion


                  #region dataTransfer.files
                  evt.dataTransfer.files.AsEnumerable().WithEach(
                      async f =>
                      {
                          // X:\jsc.svn\examples\javascript\io\DropFileForMD5Experiment\DropFileForMD5Experiment\Application.cs

                          var bytes = await f.readAsBytes();

                          var md5 = bytes.ToMD5Bytes();
                          var md5hex = md5.ToHexString();

                          Console.WriteLine(new { f.name, f.type, f.size, md5hex });
                          // 0:188830ms { name = App.htm, type = text/html, size = 2016045, md5hex = 6de15368b44b9db7e91b5ecfd2ed7140 } 

                          if (f.type == "text/html")
                          {
                              var DocumentText = Encoding.UTF8.GetString(bytes);

                              AtDocumentText(DocumentText);
                          }
                      }
                  );
                  #endregion


                  #region dataTransfer.types
                  evt.dataTransfer.types.WithEach(
                      dataTransferType =>
                      {
                          Console.WriteLine(new { dataTransferType });



                          #region text/html
                          if (dataTransferType == "text/html")
                          {
                              var DocumentText = evt.dataTransfer.getData(dataTransferType);

                              AtDocumentText(DocumentText);
                          }
                          #endregion

                      }
                  );
                  #endregion


              };
            #endregion

            #region snapshot_SelectAll
            var y = 0;
            this.snapshot_SelectAll(
                AppSnapshotKey =>
                {
                    y++;

                    new Form { Text = ApplicationWebService.prefix + AppSnapshotKey }.With(
                        f =>
                        {
                            var w = new WebBrowser
                            {
                                Dock = DockStyle.Fill,
                            }.AttachTo(f);

                            w.Navigate(ApplicationWebService.prefix + AppSnapshotKey);

                            f.StartPosition = FormStartPosition.Manual;
                            f.MoveTo(y * 96, y * 64);

                            f.Show();


                            f.FormClosing +=
                                delegate
                                {
                                    this.snapshot_Delete(AppSnapshotKey);
                                };
                        }
                    );
                }
            );
            #endregion

        }

    }
}
