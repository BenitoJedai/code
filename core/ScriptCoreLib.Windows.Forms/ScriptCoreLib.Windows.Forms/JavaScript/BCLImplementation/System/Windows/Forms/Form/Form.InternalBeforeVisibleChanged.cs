using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    public partial class __Form
    {
        // allow to target shadow dom.
        // x:\jsc.svn\examples\javascript\test\testshadowdocumentwithforms\testshadowdocumentwithforms\application.cs
        // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionShadowExperiment\ChromeExtensionShadowExperiment\Application.cs


        // allow to create system window and attach to that instead
        // see also: X:\jsc.svn\examples\javascript\chrome\ChromeAppWindowFrameNoneExperiment\ChromeAppWindowFrameNoneExperiment\Application.cs
        public static Action<__Form, Action<bool>> InternalHTMLTargetAttachToDocument =
            (that, yield) =>
            {
                if (that.HTMLTarget.parentNode == null)
                {
                    // are we supposed to add the jsc experience?

                    // we do not want the document scrollbars on top of us?
                    // cannot do that yet. 20131228 as body scrollTop will not work then.

                    that.HTMLTarget.AttachTo(
                        //Native.document.body.parentNode
                        Native.document.documentElement
                    );




                }

                // animate!
                yield(true);
            };

        public override void InternalUpdateBoundsSetLocation(int x, int y)
        {
            // X:\jsc.svn\examples\javascript\Test\TestShadowDocumentWithForms\TestShadowDocumentWithForms\Application.cs

            // or we are part of shadow of document element?

            // what happens if the browser does not understand ShadowRoot?
            //var xShadowRoot = this.HTMLTarget.parentNode is ShadowRoot;
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\ShadowRoot.cs

            var xShadowRoot = this.HTMLTarget.parentNode is ShadowRoot;
            // 13:12213ms InternalUpdateBoundsSetLocation { xShadowRoot = true }
            //Console.WriteLine("InternalUpdateBoundsSetLocation " + new { xShadowRoot });

            var isRoot = this.HTMLTarget.parentNode == Native.document.documentElement;


            if (isRoot || xShadowRoot)
            {
                // could we dock to center?

                this.HTMLTarget.style.position = IStyle.PositionEnum.@fixed;
                this.HTMLTarget.style.left = x + "px";
                this.HTMLTarget.style.top = y + "px";
                return;
            }

            this.HTMLTarget.style.SetLocation(x, y);
        }

        bool InternalBeforeVisibleChangedDone = false;
        public override void InternalBeforeVisibleChanged(Action yield)
        {
            //Console.WriteLine("__Form.InternalBeforeVisibleChanged");

            if (InternalBeforeVisibleChangedDone)
            {
                yield();
                return;
            }

            InternalBeforeVisibleChangedDone = true;




            InternalHTMLTargetAttachToDocument(
                this,
                Animate =>
                {
                    Action DoCenterScreen = delegate
                    {
                        #region CenterScreen
                        if (this.WindowState == FormWindowState.Normal)
                            if (this.StartPosition == FormStartPosition.CenterScreen)
                            {
                                this.Width = Math.Min(InternalHostWidth, this.Width);
                                this.Height = Math.Min(InternalHostHeight, this.Height);

                                //Console.WriteLine(new { this.height, host_Bounds });

                                this.Location = new Point
                                {
                                    X = (InternalHostWidth - this.Width) / 2,
                                    Y = Math.Max(0, (InternalHostHeight - this.Height) / 2)
                                };
                            }
                        #endregion

                    };


                    DoCenterScreen();


                    #region fadein

                    // allow animation to be skipped by custom hosts
                    // like chrom AppWindow
                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs
                    // X:\jsc.svn\examples\javascript\forms\FormsAnimation\FormsAnimation\Application.cs
                    if (Animate)
                    {
                        // http://www.w3schools.com/css3/css3_transitions.asp
                        // X:\jsc.svn\examples\javascript\css\CSSShaderGrayScale\CSSShaderGrayScale\Application.cs

                        outer_css.style.transition = "none";

                        var old_webkitFilter = (outer_css.style as dynamic).webkitFilter;

                        (outer_css.style as dynamic).webkitFilter = " opacity(0.1)";
                        outer_css.style.transform = " scale(0.9)";


                        new ScriptCoreLib.JavaScript.Runtime.Timer(
                            delegate
                            {
                                // InternalBeforeVisibleChanged before requestAnimationFrame { node = [object HTMLDocument], ownerDocument = [object HTMLDocument], same = true }
                                //Console.WriteLine("InternalBeforeVisibleChanged after requestAnimationFrame");

                                outer_css.style.transition = "-webkit-transform 150ms linear, -webkit-filter 150ms linear";

                                (outer_css.style as dynamic).webkitFilter = " opacity(1.0)";
                                outer_css.style.transform = " scale(1.0)";

                                new ScriptCoreLib.JavaScript.Runtime.Timer(
                                    delegate
                                    {
                                        outer_css.style.transition = "none";

                                        (outer_css.style as dynamic).webkitFilter = old_webkitFilter;
                                        outer_css.style.transform = "";
                                    }
                                ).StartTimeout(150);
                            }
                        ).StartTimeout(11);
                    }
                    #endregion

                    Console.WriteLine("about to call InternalRaiseShown");
                    // what if it is never called? no. background window wont call it..



                    // we should have a special test for this!

                    Native.setTimeout(

                    //this.HTMLTarget.requestAnimationFrame +=
                        //    delegate
                        //    {
                        //        this.HTMLTarget.requestAnimationFrame +=
                                 delegate
                                 {
                                     // we were closed before we were shown
                                     if (this.InternalClosed)
                                         return;

                                     this.SizeChanged +=
                                         delegate
                                         {
                                             // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\Application.cs
                                             if (DoCenterScreen != null)
                                                 DoCenterScreen();
                                         };

                                     //Console.WriteLine("InternalRaiseLoad");
                                     InternalRaiseLoad();

                                     DoCenterScreen = null;


                                     InternalUpdateZIndex(HTMLTarget);

                                     Console.WriteLine("InternalRaiseShown");
                                     InternalRaiseShown();

                                     // let child controls know
                                     yield();


                                     #region Focus
                                     var length = this.Controls.Count;

                                     for (int i = 0; i < length; i++)
                                     {
                                         var item = this.Controls[i];

                                         if (item.TabIndex == 0)
                                             item.Focus();
                                     }
                                     #endregion



                                     InternalWindowStateAnimated = true;
                                 },

                                 // allow animations to finish?
                                 2
                        );


                }
            );


        }


    }


}
