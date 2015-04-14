using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace FlashAffineTexturedCube
{
    public class ApplicationCanvas : global::AvalonAffineTexturedCube.ApplicationCanvas
    {
        //public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            //r.Fill = Brushes.Red;
            //r.AttachTo(this);
            //r.MoveTo(8, 8);
            //this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);
        }

    }
}


//0001 02000066 AvalonAffineTexturedCube.ApplicationWebService.AndroidActivity::InternalPopupWebView.XWindowCache
//{ Location =
// assembly: W:\staging\clr\AvalonAffineTexturedCube.ApplicationWebService.AndroidActivity.dll
// type: InternalPopupWebView.XWindowFrameLayout, AvalonAffineTexturedCube.ApplicationWebService.AndroidActivity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x007c
//  method:Boolean onInterceptTouchEvent(android.view.MotionEvent) }
//{ Location =
// assembly: W:\staging\clr\AvalonAffineTexturedCube.ApplicationWebService.AndroidActivity.dll
// type: InternalPopupWebView.XWindowFrameLayout, AvalonAffineTexturedCube.ApplicationWebService.AndroidActivity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x0083
//  method:Boolean onInterceptTouchEvent(android.view.MotionEvent) }
//script: error JSC1000: Java : unable to emit and at 'InternalPopupWebView.XWindowFrameLayout.onInterceptTouchEvent'#0085: multiple stack entries instead of one
//   at jsc.ILFlowStackItem.get_SingleStackInstruction() in X:\jsc.internal.git\compiler\jsc\CodeModel\ILFlow.cs:line 138