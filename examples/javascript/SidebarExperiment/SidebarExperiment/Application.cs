using CSSShaderGrayScale;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using SidebarExperiment.Design;
using SidebarExperiment.HTML.Pages;
using SidebarExperiment.Library;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

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
            DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();

            FormStyler.AtFormCreated =
                  s =>
                  {

                      FormStyler.LikeVisualStudioMetro(s);

                      s.TargetOuterBorder.style.borderColor = ScriptCoreLib.JavaScript.Runtime.JSColor.FromRGB(0, 127, 0);
                      s.Caption.style.backgroundColor = ScriptCoreLib.JavaScript.Runtime.JSColor.FromRGB(0, 127, 0);
                      s.TargetOuterBorder.style.boxShadow = "rgba(0, 127, 0, 0.3) 0px 0px 6px 3px";
                  };

            //FormStyler.AtFormCreated = FormStyler.LikeWindows3;

            var SidebarIdleWidth = 32;

            var f = new Form { Text = "Sidebar" };

            GrayScaleRule.InitializeGrayScaleFor("CLRForm");
            f.GetHTMLTarget().className = "CLRForm";


            #region WhileDragging


            Action WhileDragging = null;

            WhileDragging = delegate
            {
                if (f.Left == 0)
                {
                    f.GetHTMLTarget().className = "CLRForm_nohover";
                    f.Text = "Sidebar (docked)";
                }
                else if (f.Capture)
                {
                    f.GetHTMLTarget().className = "";
                    f.Text = "Sidebar (dragging)";
                }
                else
                {
                    f.GetHTMLTarget().className = "CLRForm";
                    f.Text = "Sidebar";

                }
                Native.window.requestAnimationFrame += WhileDragging;
            };
            Native.window.requestAnimationFrame += WhileDragging;
            #endregion


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

            #region LocationChanged
            var LocationChangedDisabled = false;
            f.LocationChanged +=
                delegate
                {
                    if (LocationChangedDisabled)
                        return;

                    if (f.Left == 0)
                        return;

                    if (f.Right == Native.window.Width)
                        return;

                    SetLeftSidebarWidth(SidebarIdleWidth);
                    SetRightSidebarWidth(SidebarIdleWidth);

                    if (f.Left < SidebarIdleWidth && c.checkBox1.Checked)
                        page.SidebarContainer.style.backgroundColor = JSColor.Blue;
                    else
                        page.SidebarContainer.style.backgroundColor = JSColor.Gray;

                    if (f.Right > Native.window.Width - SidebarIdleWidth && c.checkBox2.Checked)
                        page.RightSidebarContainer.style.backgroundColor = JSColor.Blue;
                    else
                        page.RightSidebarContainer.style.backgroundColor = JSColor.Gray;
                };
            #endregion



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

                    if (f.Right == Native.window.Width)
                        return;

                    if (f.Capture)
                    {
                        if (AtCapture != null)
                            AtCapture();

                        return;
                    }


                    if (f.Left < SidebarIdleWidth)
                    {
                        if (c.checkBox1.Checked)
                        {
                            var fs = f.Size;

                            SetLeftSidebarWidth(f.ClientSize.Width);
                            f.MoveTo(0, 0);
                            f.SizeTo(f.ClientSize.Width, page.SidebarContainer.clientHeight);
                            f.MinimumSize = new System.Drawing.Size(SidebarIdleWidth, Native.window.Height);
                            f.MaximumSize = new System.Drawing.Size(Native.window.Width - page.RightSidebarContainer.clientWidth, Native.window.Height);


                            var done = false;
                            Action<IEvent> onresize =
                                delegate
                                {
                                    if (done)
                                        return;
                                    f.SizeTo(f.ClientSize.Width, page.SidebarContainer.clientHeight);
                                    f.MinimumSize = new System.Drawing.Size(SidebarIdleWidth, Native.window.Height);
                                    f.MaximumSize = new System.Drawing.Size(Native.window.Width - page.RightSidebarContainer.clientWidth, Native.window.Height);


                                };

                            Native.window.onresize += onresize;

                            AtCapture = delegate
                            {
                                done = true;

                                AtCapture = null;
                                f.MinimumSize = new System.Drawing.Size(100, 100);
                                f.SizeTo(fs.Width, fs.Height);
                            };
                        }
                    }
                    else if (f.Right > (Native.window.Width - SidebarIdleWidth))
                    {
                        if (c.checkBox2.Checked)
                        {

                            var fs = f.Size;

                            SetRightSidebarWidth(f.ClientSize.Width);
                            f.MoveTo(page.RightSidebarContainer.offsetLeft, 0);
                            f.SizeTo(f.ClientSize.Width, page.RightSidebarContainer.clientHeight);
                            f.MinimumSize = new System.Drawing.Size(SidebarIdleWidth, Native.window.Height);
                            f.MaximumSize = new System.Drawing.Size(Native.window.Width, Native.window.Height);

                            var done = false;
                            Action<IEvent> onresize =
                                delegate
                                {
                                    if (done)
                                        return;

                                    f.MoveTo(page.RightSidebarContainer.offsetLeft, 0);
                                    f.SizeTo(f.ClientSize.Width, page.RightSidebarContainer.clientHeight);
                                    f.MinimumSize = new System.Drawing.Size(SidebarIdleWidth, Native.window.Height);
                                    f.MaximumSize = new System.Drawing.Size(Native.window.Width, Native.window.Height);
                                };

                            Native.window.onresize += onresize;


                            AtCapture = delegate
                            {
                                done = true;
                                AtCapture = null;
                                f.MinimumSize = new System.Drawing.Size(100, 100);
                                f.SizeTo(fs.Width, fs.Height);
                            };
                        }
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
                        //if (f.Left == page.RightSidebarContainer.offsetLeft)
                        //{
                        //    LocationChangedDisabled = true;
                        //    f.Left = Native.Window.Width - f.Width;
                        //    SetRightSidebarWidth(f.Width);
                        //    LocationChangedDisabled = false;
                        //}
                    }

                    PrevRight = f.Right;
                };
            #endregion

            #region AtButton1
            Action AtButton1 =
                delegate
                {
                    c.button1.Enabled = false;
                    c.button2.Enabled = true;

                    var cl = f.Location;
                    var cs = f.ClientSize;


                    var cc = f.GetHTMLTarget();

                    var IsLeft = f.Left < (Native.window.Width - f.Width) / 2;

                    if (IsLeft)
                    {
                        f.MoveTo(0, 0);
                    }
                    else
                    {
                        f.MoveTo(page.RightSidebarContainer.offsetLeft, 0);
                    }

                    f.FormBorderStyle = FormBorderStyle.None;

                    Native.window.requestAnimationFrame +=
                        delegate
                        {
                            f.Height = Native.window.Height;
                        };

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

            #region button1
            c.button1.Click +=
                delegate
                {
                    AtButton1();
                };
            #endregion

            c.button2.Enabled = false;

            c.button3.Click +=
                delegate
                {
                    c.checkBox1.Enabled = false;
                    c.checkBox2.Enabled = false;
                    c.button3.Enabled = false;

                    f.PopupInsteadOfClosing(
                        HandleFormClosing: true
                    );
                };

            f.FormClosing +=
                (ss, ee) =>
                {
                    if (c.button3.Enabled)
                    {
                        // not yet popup mode
                        ee.Cancel = true;
                        if (c.button1.Enabled)
                            AtButton1();

                    }

                };

            //f.FormClosed +=
            //    delegate
            //    {
            //        Native.Document.body.Clear();

            //        Native.Window.close();
            //    };


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
