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
using AndroidVortexLesson3.Library;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;

namespace AndroidVortexLesson3.Activities
{
    // A package name must be constitued of two Java identifiers.

    [Script]
    public class AndroidVortexLesson3 : Activity
    {
        // port from http://www.droidnova.com/android-3d-game-tutorial-part-iii,348.html

        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidVortexLesson3.Activities --activity AndroidVortexLesson3  --target 2  --path Z:\jsc.svn\examples\java\android\AndroidVortexLesson3\AndroidVortexLesson3\staging

        // http://developer.android.com/guide/developing/device.html#setting-up

        // running it in emulator:
        // C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "Z:\jsc.svn\examples\java\android\AndroidVortexLesson3\AndroidVortexLesson3\staging\bin\AndroidVortexLesson3-debug.apk"

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        // C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

        [Script]
        public partial class VortexRenderer : GLSurfaceView.Renderer
        {

            private float _red = 0f;
            private float _green = 0f;
            private float _blue = 0f;

            // a raw buffer to hold indices allowing a reuse of points.
            private ShortBuffer _indexBuffer;
            private ShortBuffer _indexBufferStatic;

            // a raw buffer to hold the vertices
            private FloatBuffer _vertexBuffer;
            private FloatBuffer _vertexBufferStatic;

            private short[] _indicesArray = { 0, 1, 2 };
            private int _nrOfVertices = 3;

            private float _angle;

            public void onSurfaceCreated(GL10 gl, EGLConfig config)
            {
                // preparation
                gl.glEnableClientState(GL10_GL_VERTEX_ARRAY);
                initTriangle();
                initStaticTriangle();
            }

            public void onSurfaceChanged(GL10 gl, int w, int h)
            {
                gl.glViewport(0, 0, w, h);
            }

            public void setAngle(float angle)
            {
                _angle = angle;
            }

            public void onDrawFrame(GL10 gl)
            {
                // define the color we want to be displayed as the "clipping wall"
                gl.glClearColor(_red, _green, _blue, 1.0f);

                // reset the matrix - good to fix the rotation to a static angle
                gl.glLoadIdentity();

                // clear the color buffer to show the ClearColor we called above...
                gl.glClear(GL10_GL_COLOR_BUFFER_BIT);

                // draw the static triangle
                gl.glColor4f(0f, 0.5f, 0f, 0.5f);
                gl.glVertexPointer(3, GL10_GL_FLOAT, 0, _vertexBufferStatic);
                gl.glDrawElements(GL10_GL_TRIANGLES, _nrOfVertices, GL10_GL_UNSIGNED_SHORT, _indexBufferStatic);

                // set rotation for the non-static triangle
                gl.glRotatef(_angle, 0f, 1f, 0f);

                gl.glColor4f(0.5f, 0f, 0f, 0.5f);
                gl.glVertexPointer(3, GL10_GL_FLOAT, 0, _vertexBuffer);
                gl.glDrawElements(GL10_GL_TRIANGLES, _nrOfVertices, GL10_GL_UNSIGNED_SHORT, _indexBuffer);

            }

            private void initTriangle()
            {
                // float has 4 bytes
                ByteBuffer vbb = ByteBuffer.allocateDirect(_nrOfVertices * 3 * 4);
                vbb.order(ByteOrder.nativeOrder());
                _vertexBuffer = vbb.asFloatBuffer();

                // short has 4 bytes
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

            private void initStaticTriangle()
            {
                // float has 4 bytes
                ByteBuffer vbb = ByteBuffer.allocateDirect(_nrOfVertices * 3 * 4);
                vbb.order(ByteOrder.nativeOrder());
                _vertexBufferStatic = vbb.asFloatBuffer();

                // short has 4 bytes
                ByteBuffer ibb = ByteBuffer.allocateDirect(_nrOfVertices * 2);
                ibb.order(ByteOrder.nativeOrder());
                _indexBufferStatic = ibb.asShortBuffer();

                float[] coords = {
                    -0.4f, -0.4f, 0f, // (x1, y1, z1)
                    0.4f, -0.4f, 0f, // (x2, y2, z2)
                    0f, 0.4f, 0f // (x3, y3, z3)
                };

                _vertexBufferStatic.put(coords);

                _indexBufferStatic.put(_indicesArray);

                _vertexBufferStatic.position(0);
                _indexBufferStatic.position(0);
            }

            public void setColor(float r, float g, float b)
            {
                _red = r;
                _green = g;
                _blue = b;
            }


            // see also: http://developer.android.com/reference/javax/microedition/khronos/opengles/GL10.html
            // constants in interfaces. yay
            public const int GL10_GL_UNSIGNED_SHORT = 0x00001403;
            public const int GL10_GL_TRIANGLES = 0x00000004;
            public const int GL10_GL_FLOAT = 0x00001406;
            public const int GL10_GL_COLOR_BUFFER_BIT = 0x00004000;
            public const int GL10_GL_VERTEX_ARRAY = 0x00008074;




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

   

        [Script]
        class Handler : Runnable
        {
            public VortexView __this;
            public android.view.MotionEvent __value;

            public void run()
            {
                __this._renderer.setAngle(__value.getX());
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
