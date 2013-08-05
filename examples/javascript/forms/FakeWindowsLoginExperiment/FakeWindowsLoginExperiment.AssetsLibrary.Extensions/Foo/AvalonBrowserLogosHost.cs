using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Forms.Integration;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Reflection;
using ScriptCoreLib.Shared.Windows.Forms;

namespace FormsAvalonAnimation
{
    public class AvalonBrowserLogosHost : System.Windows.Forms.UserControl
    {
        //jsc.meta.Library.ScriptResourceWriter ref1;
        //global::jsc.Library.VirtualDictionaryBase ref2;
        //ScriptCoreLib.Ultra.Components.Volatile.Avalon.Images.Google_Chrome ref0 = new ScriptCoreLib.Ultra.Components.Volatile.Avalon.Images.Google_Chrome();


        private System.Windows.Forms.Integration.ElementHost elementHost1 = new ElementHost();


        //        ---------------------------
        //Microsoft Visual Studio Express 2012 for Web
        //---------------------------
        //Code generation for property 'Child' failed.  Error was: 'Object reference not set to an instance of an object.'
        //---------------------------
        //OK   
        //---------------------------


        public System.Windows.Controls.Canvas Child;


        public AvalonBrowserLogosHost()
        {
            ZInitialize();
        }

        private void ZInitialize()
        {
            var label = new TextBox
            {
                Text = "AvalonBrowserLogos " + new { System.ComponentModel.LicenseManager.UsageMode },
                Dock = DockStyle.Top,
                Multiline = true,
                Height = 64
            };

            this.Controls.Add(
            label
          );


            // http://weblogs.asp.net/fmarguerie/archive/2005/03/23/395658.aspx
            //if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            //{

            //    return;
            //}
            //            ---------------------------
            //Microsoft Visual Studio Express 2012 for Web
            //---------------------------
            //The control FormsAvalonAnimation.AnimationControlHost has thrown an unhandled exception in the designer and has been disabled.  
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";

            this.Controls.Add(elementHost1);

            var c = new System.Windows.Controls.Canvas();
            try
            {
                if (Child == null)
                    Child = new AvalonBrowserLogos.ApplicationCanvas();

                c.Children.Add(Child);

                //                        ---------------------------
                //Microsoft Visual Studio Express 2012 for Windows Desktop
                //---------------------------
                //The control System.Windows.Forms.Integration.ElementHost has thrown an unhandled exception in the designer and has been disabled.  



                c.SizeChanged +=
                 delegate
                 {
                     Child.SizeTo(c.ActualWidth, c.ActualHeight);
                 };
            }
            catch (Exception ex)
            {
                label.Text += new { ex.GetType().FullName, ex.Message, StackTrace = new System.Diagnostics.StackTrace(ex, true), ex.InnerException }.ToString();

            }


            this.Load +=
                delegate
                {
                    // should not load new assemblies here??
                    this.elementHost1.Child = c;
                };
        }

    }


}


