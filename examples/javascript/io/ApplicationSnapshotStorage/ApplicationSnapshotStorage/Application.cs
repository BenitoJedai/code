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

                  evt.dataTransfer.types.WithEach(
                      x =>
                      {
                          Console.WriteLine(x);

                          #region text/html
                          if (x == "text/html")
                          {
                              var DocumentText = evt.dataTransfer.getData(x);

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

                                      this.snapshot_Insert(DocumentText,
                                        AppSnapshotKey =>
                                        {
                                            f.Text = ApplicationWebService.prefix + AppSnapshotKey;
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
                          }
                          #endregion

                      }
                  );
              };
            #endregion

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
        }

    }
}
