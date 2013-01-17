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
using AndroidGLDisturbActivity.Shaders;
using java.lang;
using java.nio;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using android.content.pm;
using java.io;


namespace AndroidGLDisturbActivity.Activities
{
    using gl = WebGLRenderingContext;
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



                    Log.wtf("AndroidGLDisturbActivity", "onsurface");

                    // Create Vertex buffer (2 triangles)

                    var buffer = gl.createBuffer();
                    gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
                    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(-1.0f, -1.0f, 1.0f, -1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f), gl.STATIC_DRAW);



                    // Create Program


                    #region createProgram

                    var program = gl.createProgram(
                        new DisturbVertexShader(),
                        new DisturbFragmentShader()
                    );



                    gl.linkProgram(program);
                    gl.useProgram(program);



                    #endregion

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

                        //GLUtils.texImage2D(
                        //    /*target*/ (int)gl.TEXTURE_2D,
                        //    /*level*/ 0,
                        //    /*internalformat*/(int)gl.RGBA,
                        //    image,
                        //    /*type*/  (int)gl.UNSIGNED_BYTE,
                        //    0
                        //);

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
                        openFileFromAssets("assets/AndroidGLDisturbActivity/disturb.jpg")
                    );
                    var texture = loadTexture(
                        texture__
                    );

                    var vertexPositionLocation = default(long);
                    var textureLocation = default(WebGLUniformLocation);

                    var parameters_time = 0L;
                    var parameters_screenWidth = 0;
                    var parameters_screenHeight = 0;
                    var parameters_aspectX = 0.0f;
                    var parameters_aspectY = 1.0f;

                    #region onresize
                    v.onresize +=
                        (width, height) =>
                        {
                            Log.wtf("AndroidGLDisturbActivity", "onresize");

                            parameters_screenWidth = width;
                            parameters_screenHeight = height;

                            gl.viewport(0, 0, width, height);
                        };
                    #endregion


                    #region onframe
                    var framecount = 0;
                    v.onframe +=
                        delegate
                        {
                            var time = parameters_time / 1000f;

                            if (framecount == 0)
                                Log.wtf("AndroidGLDisturbActivity", "onframe " + ((object)time).ToString());

                            parameters_time += 100;

                            gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                            // Load program into GPU


                            // Get var locations

                            vertexPositionLocation = gl.getAttribLocation(program, "position");
                            textureLocation = gl.getUniformLocation(program, "texture");

                            // Set values to program variables


                            var resolution = new __vec2 { x = parameters_screenWidth, y = parameters_screenHeight };

                            uniforms.time = time;
                            uniforms.resolution = resolution;

                            //gl.uniform1f(gl.getUniformLocation(program, "time"), time);
                            //gl.uniform2f(gl.getUniformLocation(program, "resolution"), parameters_screenWidth, parameters_screenHeight);

                            gl.uniform1i(textureLocation, 0);
                            gl.activeTexture(gl.TEXTURE0);
                            gl.bindTexture(gl.TEXTURE_2D, texture);

                            // Render geometry

                            gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
                            gl.vertexAttribPointer((uint)vertexPositionLocation, 2, gl.FLOAT, false, 0, 0);
                            gl.enableVertexAttribArray((uint)vertexPositionLocation);
                            gl.drawArrays(gl.TRIANGLES, 0, 6);
                            gl.disableVertexAttribArray((uint)vertexPositionLocation);




                            framecount++;
                        };
                    #endregion

                    Log.wtf("AndroidGLDisturbActivity", "onsurface done");

                };


            this.setContentView(v);

            //this.TryHideActionbar(v);

            this.ShowToast("http://my.jsc-solutions.net");
        }

    }


}
