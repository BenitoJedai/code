using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.opengl;
using android.provider;
using android.view;
using android.webkit;
using android.widget;
using java.lang;
using java.nio;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;

namespace AndroidVortexLesson6.Activities
{
    // A package name must be constitued of two Java identifiers.

    public class AndroidVortexLesson6 : Activity
    {
        // port from http://www.droidnova.com/android-3d-game-tutorial-part-vi,436.html

        public partial class VortexRenderer : GLSurfaceView.Renderer
        {

            // a raw buffer to hold indices allowing a reuse of points.
            private ShortBuffer _indexBuffer;

            // a raw buffer to hold the vertices
            private FloatBuffer _vertexBuffer;

            // a raw buffer to hold the colors
            private FloatBuffer _colorBuffer;

            private int _nrOfVertices = 0;

            private float _xAngle;
            private float _yAngle;

            private float _width = 320f;
            private float _height = 480f;


            public void onSurfaceChanged(GL10 gl, int w, int h)
            {
                _width = w;
                _height = h;

                gl.glViewport(0, 0, w, h);
            }

            public void setXAngle(float angle)
            {
                _xAngle = angle;
            }

            public float getXAngle()
            {
                return _xAngle;
            }

            public void setYAngle(float angle)
            {
                _yAngle = angle;
            }

            public float getYAngle()
            {
                return _yAngle;
            }

            private void initTriangle()
            {
                float[] coords = {
                        -0.5f, -0.5f, 0.5f, // 0
                        0.5f, -0.5f, 0.5f, // 1
                        0f, -0.5f, -0.5f, // 2
                        0f, 0.5f, 0f, // 3
                };
                _nrOfVertices = coords.Length;

                float[] colors = {
                        1f, 0f, 0f, 1f, // point 0 red
                        0f, 1f, 0f, 1f, // point 1 green
                        0f, 0f, 1f, 1f, // point 2 blue
                        1f, 1f, 1f, 1f, // point 3 white
                };

                short[] indices = new short[] {
                        0, 1, 3, // rwg
                        0, 2, 1, // rbg
                        0, 3, 2, // rbw
                        1, 2, 3, // bwg
                };

                // float has 4 bytes, coordinate * 4 bytes
                ByteBuffer vbb = ByteBuffer.allocateDirect(coords.Length * 4);
                vbb.order(ByteOrder.nativeOrder());
                _vertexBuffer = vbb.asFloatBuffer();

                // short has 2 bytes, indices * 2 bytes
                ByteBuffer ibb = ByteBuffer.allocateDirect(indices.Length * 2);
                ibb.order(ByteOrder.nativeOrder());
                _indexBuffer = ibb.asShortBuffer();

                // float has 4 bytes, colors (RGBA) * 4 bytes
                ByteBuffer cbb = ByteBuffer.allocateDirect(colors.Length * 4);
                cbb.order(ByteOrder.nativeOrder());
                _colorBuffer = cbb.asFloatBuffer();

                _vertexBuffer.put(coords);
                _indexBuffer.put(indices);
                _colorBuffer.put(colors);

                _vertexBuffer.position(0);
                _indexBuffer.position(0);
                _colorBuffer.position(0);
            }




            public void onSurfaceCreated(GL10 gl, EGLConfig config)
            {
                gl.glMatrixMode(GL10_GL_PROJECTION);
                float size = .01f * (float)java.lang.Math.tan(java.lang.Math.toRadians(45.0) / 2);
                float ratio = _width / _height;
                // perspective:
                gl.glFrustumf(-size, size, -size / ratio, size / ratio, 0.01f, 100.0f);
                // orthographic:
                //gl.glOrthof(-1, 1, -1 / ratio, 1 / ratio, 0.01f, 100.0f);
                gl.glViewport(0, 0, (int)_width, (int)_height);
                gl.glMatrixMode(GL10_GL_MODELVIEW);
                gl.glEnable(GL10_GL_DEPTH_TEST);

                // define the color we want to be displayed as the "clipping wall"
                gl.glClearColor(0f, 0f, 0f, 1.0f);

                // enable the differentiation of which side may be visible 
                gl.glEnable(GL10_GL_CULL_FACE);
                // which is the front? the one which is drawn counter clockwise
                gl.glFrontFace(GL10_GL_CCW);
                // which one should NOT be drawn
                gl.glCullFace(GL10_GL_BACK);

                gl.glEnableClientState(GL10_GL_VERTEX_ARRAY);
                gl.glEnableClientState(GL10_GL_COLOR_ARRAY);

                initTriangle();
            }

            public void onDrawFrame(GL10 gl)
            {
                // clear the color buffer and the depth buffer
                gl.glClear(GL10_GL_COLOR_BUFFER_BIT | GL10_GL_DEPTH_BUFFER_BIT);

                gl.glVertexPointer(3, GL10_GL_FLOAT, 0, _vertexBuffer);
                gl.glColorPointer(4, GL10_GL_FLOAT, 0, _colorBuffer);

                for (int i = 1; i <= 10; i++)
                {
                    gl.glLoadIdentity();
                    gl.glTranslatef(0.0f, -1f, -1.0f + -1.5f * i);
                    // set rotation
                    gl.glRotatef(_xAngle, 1f, 0f, 0f);
                    gl.glRotatef(_yAngle, 0f, 1f, 0f);
                    gl.glDrawElements(GL10_GL_TRIANGLES, _nrOfVertices, GL10_GL_UNSIGNED_SHORT, _indexBuffer);
                }
            }

            // see also: http://developer.android.com/reference/javax/microedition/khronos/opengles/GL10.html
            // constants in interfaces. yay
            public const int GL10_GL_BACK = 0x00000405;
            public const int GL10_GL_CCW = 0x00000901;
            public const int GL10_GL_CULL_FACE = 0x00000b44;
            public const int GL10_GL_UNSIGNED_SHORT = 0x00001403;
            public const int GL10_GL_TRIANGLES = 0x00000004;
            public const int GL10_GL_FLOAT = 0x00001406;
            public const int GL10_GL_COLOR_BUFFER_BIT = 0x00004000;
            public const int GL10_GL_VERTEX_ARRAY = 0x00008074;
            public const int GL10_GL_COLOR_ARRAY = 0x00008076;
            public const int GL10_GL_PROJECTION = 0x00001701;
            public const int GL10_GL_MODELVIEW = 0x00001700;
            public const int GL10_GL_DEPTH_TEST = 0x00000b71;
            public const int GL10_GL_DEPTH_BUFFER_BIT = 0x00000100;




        }

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



        class Handler : Runnable
        {
            public float xdiff;
            public float ydiff;

            public VortexView __this;
            public android.view.MotionEvent __value;

            public void run()
            {
                __this._renderer.setXAngle(__this._renderer.getXAngle() + ydiff);
                __this._renderer.setYAngle(__this._renderer.getYAngle() + xdiff);
            }
        }

        partial class VortexView
        {
            public float _x = 0;
            public float _y = 0;

            public override bool onTouchEvent(android.view.MotionEvent value)
            {
                if (value.getAction() == MotionEvent.ACTION_DOWN)
                {
                    _x = value.getX();
                    _y = value.getY();
                }

                if (value.getAction() == MotionEvent.ACTION_MOVE)
                {
                    queueEvent(
                               new Handler
                               {
                                   __this = this,
                                   __value = value,

                                   xdiff = (_x - value.getX()),
                                   ydiff = (_y - value.getY())

                               }
                           );
                    _x = value.getX();
                    _y = value.getY();
                }

                return true;
            }
        }




    }
}
