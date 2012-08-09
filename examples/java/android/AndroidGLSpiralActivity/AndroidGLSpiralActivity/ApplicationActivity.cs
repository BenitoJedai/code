using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using android.opengl;
using android.content;
using ScriptCoreLib.JavaScript.WebGL;
using android.util;

namespace AndroidGLSpiralActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var v = new MyView(this);

            v.onresize =
                (width, height) =>
                {
                    Log.wtf("AndroidGLSpiralActivity", "onresize");
                };

            v.onframe =
                delegate
                {
                    Log.wtf("AndroidGLSpiralActivity", "onframe");

                };

            Log.wtf("AndroidGLSpiralActivity", "setContentView");
            this.setContentView(v);
        }


        class MyView : GLSurfaceView, GLSurfaceView.Renderer
        {
            WebGLRenderingContext gl = new WebGLRenderingContext();

            public Action onframe;
            public Action<int, int> onresize;

            public MyView(Context c)
                : base(c)
            {
                // Create an OpenGL ES 2.0 context.
                setEGLContextClientVersion(2);

                // set the mRenderer member
                setRenderer(this);
            }



            public void onDrawFrame(javax.microedition.khronos.opengles.GL10 value)
            {
                if (onframe != null)
                    onframe();
            }

            public void onSurfaceChanged(javax.microedition.khronos.opengles.GL10 arg0, int arg1, int arg2)
            {
                if (onresize != null)
                    onresize(arg1, arg2);
            }

            public void onSurfaceCreated(javax.microedition.khronos.opengles.GL10 arg0, javax.microedition.khronos.egl.EGLConfig arg1)
            {
                Log.wtf("AndroidGLSpiralActivity", "onSurfaceCreated");
            }
        }

    }


}
