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
using AndroidGLEscherDrosteEffectActivity.Shaders;
using java.lang;
using java.nio;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using android.content.pm;
using java.io;


namespace AndroidGLEscherDrosteEffectActivity.Activities
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using System.Dynamic;
    using ScriptCoreLib.Shared.BCLImplementation.GLSL;
    //using opengl = GLES20;


    public class ApplicationActivity : Activity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);

            //this.ToFullscreen();

            var v = new RenderingContextView(this);

            v.onsurface +=
                gl =>
                {
                    //var __gl = (ScriptCoreLib.Android.__WebGLRenderingContext)(object)gl;

                    Log.wtf("AndroidGLEscherDrosteEffectActivity", "onsurface");

                    var program = gl.createProgram(
                            new EscherDorsteVertexShader(),
                            new EscherDorsteFragmentShader()
                        );

                    gl.bindAttribLocation(program, 0, "position");

                    gl.linkProgram(program);
                    gl.useProgram(program);

                    var uniforms = program.Uniforms(gl);


                    #region loadTexture
                    Func<android.graphics.Bitmap, ScriptCoreLib.JavaScript.WebGL.WebGLTexture> loadTexture = (image) =>
                    {
                        var texture_ = gl.createTexture();

                        gl.enable(gl.TEXTURE_2D);
                        gl.bindTexture(gl.TEXTURE_2D, texture_);

                        //public void texImage2D(uint target, int level, uint internalformat, uint format, uint type, IHTMLCanvas canvas);
                        //public void texImage2D(uint target, int level, uint internalformat, uint format, uint type, IHTMLVideo video);
                        //public void texImage2D(uint target, int level, uint internalformat, uint format, uint type, ImageData pixels);
                        //public void texImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint type, ArrayBufferView pixels);

                        //public void texImage2D(uint target, int level, uint internalformat, uint format, uint type, IHTMLImage image);
                        //gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);

                        //E/AndroidRuntime( 5131): Caused by: java.lang.IllegalArgumentException: invalid Bitmap format
                        //E/AndroidRuntime( 5131):        at android.opengl.GLUtils.texImage2D(GLUtils.java:127)
                        //E/AndroidRuntime( 5131):        at AndroidGLEscherDrosteEffectActivity.Activities.ApplicationActivity___c__DisplayClass5___c__DisplayClass7._onCreate_b__1(ApplicationActivity___c__DisplayClass5___c__DisplayClass7.java:62)

                        //GLUtils.texImage2D(
                        //    /*target*/ (int)gl.TEXTURE_2D,
                        //    /*level*/ 0,
                        //    /*internalformat*/(int)gl.RGBA,
                        //    image,
                        //    /*type*/  (int)gl.UNSIGNED_BYTE,
                        //    0
                        //);

                        // Load the bitmap into the bound texture.
                        GLUtils.texImage2D((int)gl.TEXTURE_2D, 0, image, 0);


                        gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
                        gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR_MIPMAP_LINEAR);
                        gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, (int)gl.REPEAT);
                        gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, (int)gl.REPEAT);
                        gl.generateMipmap(gl.TEXTURE_2D);


                        // Recycle the bitmap, since its data has been loaded into OpenGL.
                        image.recycle();


                        return texture_;
                    };
                    #endregion

                    #region openFileFromAssets
                    Func<string, InputStream> openFileFromAssets = (string spath) =>
                    {
                        InputStream value = null;
                        try
                        {
                            value = this.getResources().getAssets().open(spath);
                        }
                        catch
                        {

                        }
                        return value;

                    };
                    #endregion

                    var texture__ = android.graphics.BitmapFactory.decodeStream(
                        openFileFromAssets("assets/AndroidGLEscherDrosteEffectActivity/escher.jpg")
                    );
                    var texture = loadTexture(
                        texture__
                    );

                    gl.uniform1i(gl.getUniformLocation(program, "texture"), 0);

                    gl.enableVertexAttribArray(0);


                    var vertices = gl.createBuffer();
                    gl.bindBuffer(gl.ARRAY_BUFFER, vertices);
                    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(
                        -1f, -1f, -1f, 1f, 1f, -1f, 1f, 1f
                    ), gl.STATIC_DRAW);
                    gl.vertexAttribPointer(0, 2, gl.FLOAT, false, 0, 0);

                    var indices = gl.createBuffer();
                    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indices);
                    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(
                        0, 1, 2, 3
                    ), gl.STATIC_DRAW);

                    #region onresize
                    v.onresize +=
                        (width, height) =>
                        {
                            Log.wtf("AndroidGLEscherDrosteEffectActivity", "onresize");

                            gl.viewport(0, 0, width, height);

                            var h = (float)height / (float)width;
                            gl.uniform1f(gl.getUniformLocation(program, "h"), h);
                        };
                    #endregion

                    var parameters_time = 0f;

                    #region onframe
                    var framecount = 0;
                    v.onframe +=
                        delegate
                        {
                            var t = parameters_time / 1000f;

                            if (framecount == 0)
                                Log.wtf("AndroidGLEscherDrosteEffectActivity", "onframe " + ((object)t).ToString());

                            parameters_time += 100;

                            uniforms.t = t;

                            //gl.uniform1f(gl.getUniformLocation(program, "t"), t);
                            gl.drawElements(gl.TRIANGLE_STRIP, 4, gl.UNSIGNED_SHORT, 0);
                            gl.flush();

                            framecount++;
                        };
                    #endregion

                    Log.wtf("AndroidGLEscherDrosteEffectActivity", "onsurface done");

                };


            this.setContentView(v);

            //this.TryHideActionbar(v);

            this.ShowToast("http://my.jsc-solutions.net");
        }

    }

}
