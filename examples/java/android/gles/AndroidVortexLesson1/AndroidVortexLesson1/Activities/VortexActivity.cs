using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
//using android.opengl;
using android.provider;
using android.webkit;
using android.widget;
using AndroidVortexLesson1.Library;
using java.lang;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;

namespace AndroidVortexLesson1.Activities
{
    // A package name must be constitued of two Java identifiers.

    [Script]
    public class VortexActivity : Activity
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141207

        // port from http://www.droidnova.com/android-3d-game-tutorial-part-i,312.html

        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidVortexLesson1.Activities --activity VortexActivity  --target 2  --path Z:\jsc.svn\examples\java\android\AndroidVortexLesson1\AndroidVortexLesson1\staging

        // http://developer.android.com/guide/developing/device.html#setting-up

        // running it in emulator:
        // C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "Z:\jsc.svn\examples\java\android\AndroidVortexLesson1\AndroidVortexLesson1\staging\bin\VortexActivity-debug.apk"

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        // C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        [Script]
        public partial class VortexRenderer : android.opengl.GLSurfaceView.Renderer
        {

            private float _red = 0.9f;
            private float _green = 0.2f;
            private float _blue = 0.2f;


            public  void onSurfaceCreated(GL10 gl, EGLConfig config)
            {
                // Do nothing special.
            }

            public  void onSurfaceChanged(GL10 gl, int w, int h)
            {
                gl.glViewport(0, 0, w, h);
            }

            public  void onDrawFrame(GL10 gl)
            {
                // define the color we want to be displayed as the "clipping wall"
                gl.glClearColor(_red, _green, _blue, 1.0f);
                // clear the color buffer to show the ClearColor we called above...
                gl.glClear(GL10_GL_COLOR_BUFFER_BIT);
            }


            // see also: http://developer.android.com/reference/javax/microedition/khronos/opengles/GL10.html
            // constants in interfaces. yay
            public const int GL10_GL_COLOR_BUFFER_BIT = 0x00004000;
        }

        [Script]
        public partial class VortexView : android.opengl.GLSurfaceView
        {
            public VortexRenderer _renderer;

            public VortexView(Context context) : base(context)
            {
                _renderer = new VortexRenderer();
                setRenderer(_renderer);
            }
        }



        private VortexView _vortexView;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            _vortexView = new VortexView(this);
            setContentView(_vortexView);

        }

        partial class VortexRenderer
        {
            public void setColor(float r, float g, float b)
            {
                _red = r;
                _green = g;
                _blue = b;
            }
        }


        [Script]
        class Handler : Runnable
        {
            public VortexView __this;
            public android.view.MotionEvent __value;

            public void run()
            {
                __this._renderer.setColor(__value.getX() / __this.getWidth(), __value.getY() / __this.getHeight(), 1.0f);
            }
        }

        partial class VortexView
        {


            public override bool onTouchEvent(android.view.MotionEvent value)
            {
                queueEvent(
                    new Handler
                    {
                        __this = this,
                        __value = value
                    }
                );

                return true;
            }
        }




    }
}
