using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.opengl;
using android.os;
using android.util;
using android.view;
using android.widget;
using AndroidGLSphereRayTraceActivity.Shaders;
using java.lang;
using java.nio;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using android.content.pm;


namespace AndroidGLSphereRayTraceActivity.Activities
{

    public class ApplicationActivity : Activity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            //setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);

            this.ToFullscreen();

            var v = new RenderingContextView(this);
            var s = new RaySurface(v);

            this.setContentView(v);

            this.TryHideActionbar(v);

            //this.ShowToast("http://my.jsc-solutions.net");
        }

    }


}
