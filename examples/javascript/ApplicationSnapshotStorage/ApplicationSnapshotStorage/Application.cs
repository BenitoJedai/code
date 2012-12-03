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

                  #region dataTransfer
                  evt.dataTransfer.types.WithEach(
                      x =>
                      {
                          Console.WriteLine(x);

                          //SystemSounds.Beep.Play();
                          //Console.Beep();

                          #region text/uri-list
                          if (x == "text/uri-list")
                          {
                              var src = evt.dataTransfer.getData(x);

                              if (src != "about:blank")
                              {
                                  if (src.StartsWith("http://www.youtube.com/watch?v="))
                                      src = "http://www.youtube.com/embed/" + src.SkipUntilIfAny("http://www.youtube.com/watch?v=").TakeUntilIfAny("&");

                                  Console.WriteLine(new { src });

                                  new Form { Text = src }.With(
                                      f =>
                                      {
                                          var w = new WebBrowser { Dock = DockStyle.Fill }.AttachTo(f);

                                          w.Navigate(src);

                                          f.Show();
                                      }
                                  );

                              }
                          }
                          #endregion


                          #region text/plain
                          if (x == "text/plain")
                          {
                              var DocumentText = evt.dataTransfer.getData(x);

                              Console.WriteLine(new { DocumentText });

                              if (DocumentText.StartsWith("javascript:"))
                              {
                                  var host = DocumentText.SkipUntilOrEmpty("href='").TakeUntilOrEmpty("'");

                                  new Form { Text = "Application " + host }.With(
                                      f =>
                                      {
                                          new IHTMLAnchor
                                          {
                                              href = host,
                                              innerText = "Go to " + host
                                          }.AttachTo(f.GetHTMLTargetContainer()).style.display = IStyle.DisplayEnum.block;

                                          new IHTMLAnchor
                                          {
                                              href = DocumentText,
                                              innerText = "Launch " + host
                                          }.AttachTo(f.GetHTMLTargetContainer()).style.display = IStyle.DisplayEnum.block;

                                          f.Show();
                                      }
                                  );
                              }
                              else
                              {

                                  new Form { Text = x }.With(
                                      f =>
                                      {
                                          new IHTMLPre
                                          {
                                              innerText = DocumentText
                                          }.AttachTo(f.GetHTMLTargetContainer()).style.display = IStyle.DisplayEnum.block;


                                          f.Show();
                                      }
                                  );
                              }
                          }
                          #endregion


                          #region text/html
                          if (x == "text/html")
                          {
                              var DocumentText = evt.dataTransfer.getData(x);

                              //Console.WriteLine(new { DocumentText });



                              new Form { Text = x + " " + DocumentText.Length + " bytes" }.With(
                                  f =>
                                  {
                                      var w = new WebBrowser { Dock = DockStyle.Fill }.AttachTo(f);

                                      w.DocumentText = DocumentText;




                                      f.Show();
                                  }
                              );
                          }
                          #endregion

                      }
                  );
                  #endregion
              };
        }

    }
}
