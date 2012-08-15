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


namespace AndroidGLEscherDrosteEffectActivity.Activities
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using java.io;
    //using opengl = GLES20;


    public class ApplicationActivity : Activity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);

            this.ToFullscreen();

            var v = new RenderingContextView(this);

            v.onsurface =
                gl =>
                {
                    //var __gl = (ScriptCoreLib.Android.__WebGLRenderingContext)(object)gl;



                    Log.wtf("AndroidGLEscherDrosteEffectActivity", "onsurface");

                    //var buffer = gl.createBuffer();

                    //gl.bindBuffer(gl.ARRAY_BUFFER, buffer);

                    //gl.bufferData(gl.ARRAY_BUFFER,
                    //      new Float32Array(
                    //          -1.0f, -1.0f, 1.0f,
                    //          -1.0f, -1.0f, 1.0f,
                    //          1.0f, -1.0f, 1.0f,
                    //          1.0f, -1.0f, 1.0f), gl.STATIC_DRAW);


                    // Create Program


                    var program = gl.createProgram(
                            new EscherDorsteVertexShader(),
                            new EscherDorsteFragmentShader()
                        );

                    gl.bindAttribLocation(program, 0, "position");

                    gl.linkProgram(program);
                    gl.useProgram(program);



                    #region loadTexture
                    Func<android.graphics.Bitmap, ScriptCoreLib.JavaScript.WebGL.WebGLTexture> loadTexture = (image) =>
                    {
                        var texture_ = gl.createTexture();

                        gl.enable(gl.TEXTURE_2D);
                        gl.bindTexture(gl.TEXTURE_2D, texture_);
                        //gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);
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

                    var escher = android.graphics.BitmapFactory.decodeStream(
                        openFileFromAssets("assets/AndroidGLEscherDrosteEffectActivity/escher.jpg")
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
                    v.onresize =
                        (width, height) =>
                        {
                            Log.wtf("AndroidGLEscherDrosteEffectActivity", "onresize");

                            gl.viewport(0, 0, width, height);

                            var h = height / width;
                            gl.uniform1f(gl.getUniformLocation(program, "h"), h);
                        };
                    #endregion

                    var parameters_time = 0f;

                    #region onframe
                    var framecount = 0;
                    v.onframe =
                        delegate
                        {
                            var t = parameters_time / 1000f;

                            if (framecount == 0)
                                Log.wtf("AndroidGLEscherDrosteEffectActivity", "onframe " + ((object)t).ToString());

                            parameters_time += 100;

                            gl.uniform1f(gl.getUniformLocation(program, "t"), t);
                            gl.drawElements(gl.TRIANGLE_STRIP, 4, gl.UNSIGNED_SHORT, 0);
                            gl.flush();

                            framecount++;
                        };
                    #endregion

                    Log.wtf("AndroidGLEscherDrosteEffectActivity", "onsurface done");

                };


            this.setContentView(v);

            this.TryHideActionbar(v);

            this.ShowToast("http://my.jsc-solutions.net");
        }

    }


}
