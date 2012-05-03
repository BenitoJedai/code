using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.opengl;
using android.provider;
using android.webkit;
using android.widget;
using AndroidVortexLesson2.Library;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;

namespace AndroidVortexLesson2.Activities
{
    // A package name must be constitued of two Java identifiers.

    [Script]
    public class AndroidVortexLesson2 : Activity
    {
        // port from http://www.droidnova.com/android-3d-game-tutorial-part-ii,328.html

        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidVortexLesson2.Activities --activity AndroidVortexLesson2  --target 2  --path Z:\jsc.svn\examples\java\android\AndroidVortexLesson2\AndroidVortexLesson2\staging

        // http://developer.android.com/guide/developing/device.html#setting-up

        // running it in emulator:
        // C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "Z:\jsc.svn\examples\java\android\AndroidVortexLesson2\AndroidVortexLesson2\staging\bin\AndroidVortexLesson2-debug.apk"

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        // C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        [Script]
        public partial class VortexRenderer : GLSurfaceView.Renderer
        {

            private float _red = 0.9f;
            private float _green = 0.2f;
            private float _blue = 0.2f;



            // new object variables we need
            // a raw buffer to hold indices
            private ShortBuffer _indexBuffer;

            // a raw buffer to hold the vertices
            private FloatBuffer _vertexBuffer;

            private short[] _indicesArray = { 0, 1, 2 };
            private int _nrOfVertices = 3;





            private void initTriangle()
            {
                // float has 4 bytes
                ByteBuffer vbb = ByteBuffer.allocateDirect(_nrOfVertices * 3 * 4);
                vbb.order(ByteOrder.nativeOrder());
                _vertexBuffer = vbb.asFloatBuffer();

                // short has 2 bytes
                ByteBuffer ibb = ByteBuffer.allocateDirect(_nrOfVertices * 2);
                ibb.order(ByteOrder.nativeOrder());
                _indexBuffer = ibb.asShortBuffer();

                float[] coords = {
                    -0.5f, -0.5f, 0f, // (x1, y1, z1)
                    0.5f, -0.5f, 0f, // (x2, y2, z2)
                    0f, 0.5f, 0f // (x3, y3, z3)
                };

                _vertexBuffer.put(coords);
                _indexBuffer.put(_indicesArray);

                _vertexBuffer.position(0);
                _indexBuffer.position(0);
            }

            public void onSurfaceCreated(GL10 gl, EGLConfig config)
            {
                // preparation
                gl.glEnableClientState(GL10_GL_VERTEX_ARRAY);
                initTriangle();
            }

            public void onSurfaceChanged(GL10 gl, int w, int h)
            {
                gl.glViewport(0, 0, w, h);
            }

            public void onDrawFrame(GL10 gl)
            {
                // define the color we want to be displayed as the "clipping wall"
                gl.glClearColor(_red, _green, _blue, 1.0f);

                // clear the color buffer to show the ClearColor we called above...
                gl.glClear(GL10_GL_COLOR_BUFFER_BIT);


                // set rotation
                gl.glRotatef(_angle, 0f, 1f, 0f);


                // set the color of our element
                gl.glColor4f(0.5f, 0f, 0f, 0.5f);

                // define the vertices we want to draw
                gl.glVertexPointer(3, GL10_GL_FLOAT, 0, _vertexBuffer);

                // finally draw the vertices
                gl.glDrawElements(GL10_GL_TRIANGLES, _nrOfVertices, GL10_GL_UNSIGNED_SHORT, _indexBuffer);
            }


            // see also: http://developer.android.com/reference/javax/microedition/khronos/opengles/GL10.html
            // constants in interfaces. yay
            public const int GL10_GL_UNSIGNED_SHORT = 0x00001403;
            public const int GL10_GL_TRIANGLES = 0x00000004;
            public const int GL10_GL_FLOAT = 0x00001406;
            public const int GL10_GL_COLOR_BUFFER_BIT = 0x00004000;
            public const int GL10_GL_VERTEX_ARRAY = 0x00008074;

            private float _angle;

            public void setAngle(float angle)
            {
                _angle = angle;
            }


        }

        [Script]
        public partial class VortexView : GLSurfaceView
        {
            public VortexRenderer _renderer;

            public VortexView(Context context)
                : base(context)
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
                __this._renderer.setAngle(__value.getX() / 10);
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
