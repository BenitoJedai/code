using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SidebarExperiment.Design;
using SidebarExperiment.HTML.Pages;
using System.Windows.Forms;
using SidebarExperiment.Library;
using ScriptCoreLib.JavaScript.Runtime;

namespace SidebarExperiment
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
            FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;
            //FormStyler.AtFormCreated = FormStyler.LikeWindows3;

            var SidebarIdleWidth = 32;

            var f = new Form { Text = "Sidebar" };
            var c = new Sidebar { Dock = DockStyle.Fill }.AttachTo(f);

            f.Show();

            Action<int> SetLeftSidebarWidth =
                 w =>
                 {
                     page.SidebarContainer.style.width = w + "px";
                     page.DocumentContent.style.left = w + "px";
                 };

            Action<int> SetRightSidebarWidth =
               w =>
               {
                   page.RightSidebarContainer.style.width = w + "px";
                   page.DocumentContent.style.right = w + "px";
               };

            var LocationChangedDisabled = false;
            f.LocationChanged +=
                delegate
                {
                    if (LocationChangedDisabled)
                        return;

                    if (f.Left == 0)
                        return;

                    if (f.Right == Native.Window.Width)
                        return;

                    SetLeftSidebarWidth(SidebarIdleWidth);
                    SetRightSidebarWidth(SidebarIdleWidth);

                    if (f.Left < SidebarIdleWidth)
                        page.SidebarContainer.style.backgroundColor = JSColor.Blue;
                    else
                        page.SidebarContainer.style.backgroundColor = JSColor.Gray;

                    if (f.Right > Native.Window.Width - SidebarIdleWidth)
                        page.RightSidebarContainer.style.backgroundColor = JSColor.Blue;
                    else
                        page.RightSidebarContainer.style.backgroundColor = JSColor.Gray;
                };



            #region Capture
            var tt = new ScriptCoreLib.JavaScript.Runtime.Timer();

            Action AtCapture = null;

            tt.Tick +=
                delegate
                {
                    if (LocationChangedDisabled)
                        return;

                    if (f.Left == 0)
                        return;

                    if (f.Right == Native.Window.Width)
                        return;

                    if (f.Capture)
                    {
                        if (AtCapture != null)
                            AtCapture();

                        return;
                    }


                    if (f.Left < SidebarIdleWidth)
                    {
                        var fs = f.Size;

                        SetLeftSidebarWidth(f.ClientSize.Width);
                        f.MoveTo(0, 0);
                        f.SizeTo(f.ClientSize.Width, page.SidebarContainer.clientHeight);
                        f.MinimumSize = new System.Drawing.Size(SidebarIdleWidth, Native.Window.Height);
                        f.MaximumSize = new System.Drawing.Size(Native.Window.Width - page.RightSidebarContainer.clientWidth, Native.Window.Height);


                        var done = false;
                        Action<IEvent> onresize =
                            delegate
                            {
                                if (done)
                                    return;
                                f.SizeTo(f.ClientSize.Width, page.SidebarContainer.clientHeight);
                                f.MinimumSize = new System.Drawing.Size(SidebarIdleWidth, Native.Window.Height);
                                f.MaximumSize = new System.Drawing.Size(Native.Window.Width - page.RightSidebarContainer.clientWidth, Native.Window.Height);


                            };

                        Native.Window.onresize += onresize;

                        AtCapture = delegate
                        {
                            done = true;
                            
                            AtCapture = null;
                            f.MinimumSize = new System.Drawing.Size(100, 100);
                            f.SizeTo(fs.Width, fs.Height);
                        };
                    }
                    else if (f.Right > (Native.Window.Width - SidebarIdleWidth))
                    {
                        var fs = f.Size;

                        SetRightSidebarWidth(f.ClientSize.Width);
                        f.MoveTo(page.RightSidebarContainer.offsetLeft, 0);
                        f.SizeTo(f.ClientSize.Width, page.RightSidebarContainer.clientHeight);
                        f.MinimumSize = new System.Drawing.Size(SidebarIdleWidth, Native.Window.Height);
                        f.MaximumSize = new System.Drawing.Size(Native.Window.Width, Native.Window.Height);

                        var done = false;
                        Action<IEvent> onresize =
                            delegate
                            {
                                if (done)
                                    return;

                                f.MoveTo(page.RightSidebarContainer.offsetLeft, 0);
                                f.SizeTo(f.ClientSize.Width, page.RightSidebarContainer.clientHeight);
                                f.MinimumSize = new System.Drawing.Size(SidebarIdleWidth, Native.Window.Height);
                                f.MaximumSize = new System.Drawing.Size(Native.Window.Width, Native.Window.Height);
                            };

                        Native.Window.onresize += onresize;


                        AtCapture = delegate
                        {
                            done = true;
                            AtCapture = null;
                            f.MinimumSize = new System.Drawing.Size(100, 100);
                            f.SizeTo(fs.Width, fs.Height);
                        };
                    }
                };
            tt.StartInterval(100);
            #endregion

            SetLeftSidebarWidth(SidebarIdleWidth);
            SetRightSidebarWidth(SidebarIdleWidth);

            #region SizeChanged
            var PrevRight = 0;

            f.SizeChanged +=
                delegate
                {
                    if (LocationChangedDisabled)
                        return;

                    if (f.Left == 0)
                    {
                        SetLeftSidebarWidth(f.Width);
                    }
                    else
                    {
                        if (f.Left == page.RightSidebarContainer.offsetLeft)
                        {
                            LocationChangedDisabled = true;
                            f.Left = Native.Window.Width - f.Width;
                            SetRightSidebarWidth(f.Width);
                            LocationChangedDisabled = false;
                        }
                    }

                    PrevRight = f.Right;
                };
            #endregion

            #region button1
            c.button1.Click +=
                delegate
                {
                    c.button1.Enabled = false;
                    c.button2.Enabled = true;

                    var cl = f.Location;
                    var cs = f.ClientSize;


                    var cc = f.GetHTMLTarget();

                    var IsLeft = f.Left < (Native.Window.Width - f.Width) / 2;

                    if (IsLeft)
                    {
                        f.MoveTo(0, 0);
                    }
                    else
                    {
                        f.MoveTo(page.RightSidebarContainer.offsetLeft, 0);
                    }

                    f.FormBorderStyle = FormBorderStyle.None;
                    f.ClientSize = new System.Drawing.Size(f.ClientSize.Width, page.SidebarContainer.clientHeight);

                    c.button2.Click +=
                          delegate
                          {
                              if (cc == null)
                                  return;

                              c.button1.Enabled = true;
                              c.button2.Enabled = false;

                              cc.AttachToDocument();


                              cc = null;

                              f.ClientSize = cs;
                              f.Location = cl;

                              //if (c.checkBox1.Checked)
                              f.FormBorderStyle = FormBorderStyle.Sizable;

                              SetLeftSidebarWidth(SidebarIdleWidth);
                          };

                    cc.AttachTo(page.SidebarContainer);
                };
            #endregion

            c.button2.Enabled = false;

            f.FormClosed +=
                delegate
                {
                    Native.Document.body.Clear();

                    Native.Window.close();
                };


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
