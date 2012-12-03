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
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\WebGLSpiral\WebGLSpiral\Application.cs

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

                                      service.snapshot_Insert(x,
                                        AppSnapshotKey =>
                                        {
                                            f.Text = new { AppSnapshotKey }.ToString();
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

        }

    }
}
