// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using MultitouchFingerTools.JavaScript;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Shapes;
using MultitouchFingerTools.JavaScript.Avalon.Images;

namespace MultitouchFingerTools.JavaScript
{
    public class ApplicationCanvas : global::MultitouchFingerTools.ApplicationCanvas
    {
        // http://www.kirupa.com/blend_wpf/drag_drop_files_wpf_pg3.htm
        white_jsc Preview;

        public ApplicationCanvas()
        {
            Preview = new white_jsc().AttachTo(this.InfoOverlay);

            //this.AllowDrop = true;
            //this.DragOver += new System.Windows.DragEventHandler(ApplicationCanvas_DragOver);
            //this.DragEnter += new System.Windows.DragEventHandler(ApplicationCanvas_DragEnter);

            this.MouseMove += new System.Windows.Input.MouseEventHandler(ApplicationCanvas_MouseMove);
        }

        void ApplicationCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var t = e.GetPosition(this);

            Preview.MoveTo(t.X, t.Y);
        }

        //void ApplicationCanvas_DragEnter(object sender, System.Windows.DragEventArgs e)
        //{
        //    e.Effects = System.Windows.DragDropEffects.All;
        //}

        //void ApplicationCanvas_DragOver(object sender, System.Windows.DragEventArgs e)
        //{
        //    e.Effects = System.Windows.DragDropEffects.All;
        //}
    }
}
