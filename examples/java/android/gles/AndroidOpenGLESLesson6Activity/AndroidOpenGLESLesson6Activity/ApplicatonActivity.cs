
using System;
using System.ComponentModel;
using android.app;
using android.content;
using android.content.pm;
using android.opengl;
using android.os;
using android.provider;
using android.util;
using android.view;
using android.webkit;
using android.widget;
using java.lang;
using java.nio;
//using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using java.io;
using AndroidOpenGLESLesson6Activity.Shaders;

namespace AndroidOpenGLESLesson6Activity.Activities
{
    using gl = WebGLRenderingContext;
    using opengl = GLES20;


    public class AndroidOpenGLESLesson6Activity : Activity
    {
        // port from http://www.learnopengles.com/android-lesson-six-an-introduction-to-texture-filtering/
        // Y:\opensource\github\Learn-OpenGLES-Tutorials\android\AndroidOpenGLESLessons\src\com\learnopengles\android\lesson6

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            //this.ToFullscreen();

            var fl = new FrameLayout(this);
            var ll = new LinearLayout(this);
            
            ll.setHorizontalGravity(Gravity.CENTER_HORIZONTAL);
            ll.setVerticalGravity(Gravity.BOTTOM);

            var button_set_min_filter = new Button(this).AttachTo(ll).WithText("Set min. filter");
            var button_set_mag_filter = new Button(this).AttachTo(ll).WithText("Set mag. filter");

            var v = new RenderingContextView(this).AttachTo(fl);
            var s = new ApplicationSurface(v, button_set_min_filter, button_set_mag_filter, this);

            ll.AttachTo(fl);


            #region density
            DisplayMetrics displayMetrics = new DisplayMetrics();
            this.getWindowManager().getDefaultDisplay().getMetrics(displayMetrics);

            v.mDensity = displayMetrics.density;
            #endregion

         

            setContentView(fl);

      


            this.ShowToast("http://my.jsc-solutions.net");
        }

    }


    


    


}
