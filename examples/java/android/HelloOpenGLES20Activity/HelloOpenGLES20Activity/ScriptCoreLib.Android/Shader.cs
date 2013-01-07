using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using android.content;
using android.opengl;
using android.util;
using android.view;
using android.widget;
using java.lang;
using ScriptCoreLib;
using android.app;
using java.nio;
using ScriptCoreLib.JavaScript.WebGL;
using android.hardware;
using ScriptCoreLibJava.Extensions;

namespace ScriptCoreLib.Android
{
    using gl = __WebGLRenderingContext;
    using opengl = GLES20;



    #region ScriptCoreLib.GLSL.Shader
    [Script(Implements = typeof(ScriptCoreLib.GLSL.Shader))]
    internal class __Shader
    {
        public override string ToString()
        {
            return "/* GLSL shader source */";
        }
    }

    [Script(Implements = typeof(ScriptCoreLib.GLSL.FragmentShader))]
    internal class __FragmentShader : __Shader
    {

    }

    [Script(Implements = typeof(ScriptCoreLib.GLSL.VertexShader))]
    internal class __VertexShader : __Shader
    {

    }
    #endregion



    #region __WebGLRenderingContext

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext))]
    public class __WebGLRenderingContext
    {
        // let's try to mimic WebGL api and see how far we get
        // why is Android ES a static reference?

        // whats the most popular api looking at Y:\jsc.svn\examples\javascript\WebGLLesson16\WebGLLesson16\Application.cs ?
        // jsc analyzer shows how many distinct methods reference the api but not yet how many times what is referenced
        // as such some manual guessing eeds to be done.

        public __WebGLUniformLocation getUniformLocation(__WebGLProgram program, string name)
        {
            return new __WebGLUniformLocation { value = GLES20.glGetUniformLocation(program.value, name) };
        }

        public void useProgram(__WebGLProgram program)
        {
            GLES20.glUseProgram(program.value);
        }

        public void uniform1i(__WebGLUniformLocation u, int p)
        {
            GLES20.glUniform1i(u, p);
        }

        public void uniform1f(__WebGLUniformLocation u, float x)
        {
            GLES20.glUniform1f(u, x);
        }

        public void uniform2f(__WebGLUniformLocation u, float x, float y)
        {
            GLES20.glUniform2f(u, x, y);
        }

        public void uniform3f(__WebGLUniformLocation u, float p1, float p2, float p3)
        {
            GLES20.glUniform3f(u, p1, p2, p3);
        }

        public void uniform3fv(__WebGLUniformLocation u, float[] p1)
        {
            GLES20.glUniform3fv(u, p1.Length * 4, p1, 0);
        }


        public void uniformMatrix4fv(__WebGLUniformLocation location, bool transpose, float[] value)
        {
            // see also: http://www.opengl.org/sdk/docs/man/xhtml/glUniform.xml
            // see also: http://developer.android.com/reference/android/opengl/GLES20.html#glUniformMatrix4fv(int, int, boolean, float[], int)

            //void glUniformMatrix4fv(	GLint  	location,
            //    GLsizei  	count,
            //    GLboolean  	transpose,
            //    const GLfloat * 	value);


            GLES20.glUniformMatrix4fv(location.value, /* count */ 1, transpose, value, /* offset */ 0);
        }


        // shall we also redefine the constants?



        public int getAttribLocation(__WebGLProgram program, string name)
        {
            return GLES20.glGetAttribLocation(program.value, name);
        }

        public void clearColor(float p1, float p2, float p3, float p4)
        {
            GLES20.glClearColor(p1, p2, p3, p4);
        }

        public void viewport(int p1, int p2, int width, int height)
        {
            GLES20.glViewport(p1, p2, width, height);
        }

        internal __WebGLProgram createProgram()
        {
            return new __WebGLProgram { value = GLES20.glCreateProgram() };
        }

        internal void deleteProgram(__WebGLProgram programObject)
        {
            GLES20.glDeleteProgram(programObject);
        }

        internal void linkProgram(__WebGLProgram programObject)
        {
            GLES20.glLinkProgram(programObject);
        }

        internal void clear(uint mask)
        {

            GLES20.glClear((int)mask);
        }

        internal void clearDepth(float f)
        {

            GLES20.glClearDepthf(f);
        }

        internal void enableVertexAttribArray(uint index)
        {
            GLES20.glEnableVertexAttribArray((int)index);
        }

        internal void drawArrays(uint mode, int first, int count)
        {
            GLES20.glDrawArrays((int)mode, first, count);
        }

        internal void drawElements(uint mode, int count, uint type, int offset)
        {
            GLES20.glDrawElements((int)mode, count, (int)type, offset);
        }

        internal __WebGLShader createShader(int shaderType)
        {
            return new __WebGLShader { value = GLES20.glCreateShader(shaderType) };
        }

        internal void shaderSource(__WebGLShader shaderHandle, string shaderSource)
        {
            GLES20.glShaderSource(shaderHandle, shaderSource);
        }

        internal void compileShader(__WebGLShader shaderHandle)
        {
            GLES20.glCompileShader(shaderHandle);
        }

        internal void deleteShader(__WebGLShader shaderHandle)
        {
            GLES20.glDeleteShader(shaderHandle);
        }

        internal void attachShader(__WebGLProgram program, __WebGLShader vertexShader)
        {
            GLES20.glAttachShader(program, vertexShader);
        }


        internal void flush()
        {
            GLES20.glFlush();
        }

        internal void enable(uint cap)
        {
            GLES20.glEnable((int)cap);
        }

        internal void disable(uint cap)
        {
            GLES20.glDisable((int)cap);
        }

        internal void disableVertexAttribArray(int pointPositionHandle)
        {
            GLES20.glDisableVertexAttribArray(pointPositionHandle);
        }

        internal void vertexAttrib3f(int pointPositionHandle, float p1, float p2, float p3)
        {
            GLES20.glVertexAttrib3f(pointPositionHandle, p1, p2, p3);
        }

        internal void texParameteri(uint target, uint pname, int param)
        {
            GLES20.glTexParameteri((int)target, (int)pname, param);
        }

        internal __WebGLTexture createTexture()
        {
            int[] textureHandle = new int[1];

            GLES20.glGenTextures(1, textureHandle, 0);

            return new __WebGLTexture { value = textureHandle[0] };
        }

        internal void bindTexture(uint target, __WebGLTexture textureHandle)
        {
            GLES20.glBindTexture((int)target, textureHandle);
        }

        internal void activeTexture(uint texture)
        {
            GLES20.glActiveTexture((int)texture);
        }



        internal void blendFunc(uint sfactor, uint dfactor)
        {
            GLES20.glBlendFunc((int)sfactor, (int)dfactor);
        }

        internal void generateMipmap(uint target)
        {
            GLES20.glGenerateMipmap((int)target);
        }

        internal void bindAttribLocation(__WebGLProgram programHandle, int i, string p)
        {
            GLES20.glBindAttribLocation(programHandle, i, p);
        }

        internal __WebGLBuffer createBuffer()
        {
            return new __WebGLBuffer();
        }

        internal __WebGLBuffer CurrentBuffer;

        internal void bindBuffer(uint p, __WebGLBuffer buffer)
        {
            CurrentBuffer = buffer;

            if (CurrentBuffer.value > 0)
                opengl.glBindBuffer((int)p, buffer.value);
        }

        internal void bufferData(uint p, __ArrayBufferView v, uint p_2)
        {
            #region f32
            var f32 = v as __Float32Array;
            if (f32 != null)
            {
                if (CurrentBuffer.value == 0)
                {
                    CurrentBuffer.value = f32.InternalFloatArray.Length * 4;

                    bindBuffer(p, CurrentBuffer);
                }

                opengl.glBufferData((int)p, CurrentBuffer.value, f32.InternalFloatBuffer, (int)p_2);
            }
            #endregion

            #region u16
            var u16 = v as __Uint16Array;
            if (u16 != null)
            {
                if (CurrentBuffer.value == 0)
                {
                    CurrentBuffer.value = u16.InternalFloatArray.Length * 2;

                    bindBuffer(p, CurrentBuffer);
                }

                opengl.glBufferData((int)p, CurrentBuffer.value, u16.InternalBuffer, (int)p_2);
            }
            #endregion
        }

        internal void vertexAttribPointer(uint p, int p_2, uint p_3, bool p_4, int p_5, int p_6)
        {
            opengl.glVertexAttribPointer((int)p, p_2, (int)p_3, p_4, p_5, p_6);
        }


    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.ArrayBufferView))]
    public class __ArrayBufferView
    {
    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.Float32Array))]
    public class __Float32Array : __ArrayBufferView
    {
        public float[] InternalFloatArray;
        public FloatBuffer InternalFloatBuffer;

        public __Float32Array(params float[] array)
        {
            InternalFloatArray = array;
            InternalFloatBuffer = FloatBuffer.wrap(
                array
            );
        }
    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.Uint16Array))]
    public class __Uint16Array : __ArrayBufferView
    {
        public ushort[] InternalFloatArray;
        public ShortBuffer InternalBuffer;

        public __Uint16Array(params ushort[] array)
        {
            InternalFloatArray = array;
            InternalBuffer = ShortBuffer.wrap(
                (short[])(object)array
            );
        }
    }


    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLBuffer))]
    public class __WebGLBuffer : __WebGLObject
    {

    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLUniformLocation))]
    public class __WebGLUniformLocation : __WebGLObject
    {
        public static implicit operator __WebGLUniformLocation(ScriptCoreLib.JavaScript.WebGL.WebGLUniformLocation e)
        {
            return (__WebGLUniformLocation)(object)e;
        }
    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLTexture))]
    public class __WebGLTexture : __WebGLObject
    {
    }


    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLProgram))]
    public class __WebGLProgram : __WebGLObject
    {
        public static implicit operator ScriptCoreLib.JavaScript.WebGL.WebGLProgram(__WebGLProgram e)
        {
            return (ScriptCoreLib.JavaScript.WebGL.WebGLProgram)(object)e;
        }
    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLShader))]
    public class __WebGLShader : __WebGLObject
    {
    }

    [Script(Implements = typeof(ScriptCoreLib.JavaScript.WebGL.WebGLObject))]
    public class __WebGLObject
    {
        public int value;

        public static implicit operator int(__WebGLObject e)
        {
            return e.value;
        }
    }

    #endregion



    [Description("ScriptCoreLib.Extensions")]
    public static class MyExtensions
    {

        #region gl
        public static __WebGLShader createShader(this gl gl, ScriptCoreLib.GLSL.FragmentShader fragmentShader)
        {
            return gl.createShader(GLES20.GL_FRAGMENT_SHADER, fragmentShader.ToString());
        }

        public static __WebGLShader createShader(this gl gl, ScriptCoreLib.GLSL.VertexShader fragmentShader)
        {
            return gl.createShader(GLES20.GL_VERTEX_SHADER, fragmentShader.ToString());
        }

        private static __WebGLShader createShader(this gl gl, int shaderType, string shaderSource)
        {
            var shaderHandle = gl.createShader(shaderType);


            // Pass in the shader source.
            gl.shaderSource(shaderHandle, shaderSource);

            // Compile the shader.
            gl.compileShader(shaderHandle);

            // Get the compilation status.
            int[] compileStatus = new int[1];
            GLES20.glGetShaderiv(shaderHandle.value, GLES20.GL_COMPILE_STATUS, compileStatus, 0);

            // If the compilation failed, delete the shader.
            if (compileStatus[0] == 0)
            {
                Log.e("createShader", GLES20.glGetShaderInfoLog(shaderHandle.value));
                gl.deleteShader(shaderHandle);
                shaderHandle = null;
            }

            if (shaderHandle == null)
            {
                //throw new RuntimeException("Error creating shader.");
                throw null;
            }

            return shaderHandle;
        }


        public static __WebGLProgram createAndLinkProgram(this gl gl, ScriptCoreLib.GLSL.VertexShader vertexShaderHandle, ScriptCoreLib.GLSL.FragmentShader fragmentShaderHandle, params string[] attributes)
        {
            return gl.createAndLinkProgram(
                gl.createShader(vertexShaderHandle),
                gl.createShader(fragmentShaderHandle),
                attributes
            );
        }

        public static __WebGLProgram createAndLinkProgram(this gl gl, __WebGLShader vertexShaderHandle, __WebGLShader fragmentShaderHandle, params string[] attributes)
        {
            var programHandle = gl.createProgram();


            // Bind the vertex shader to the program.
            gl.attachShader(programHandle, vertexShaderHandle);

            // Bind the fragment shader to the program.
            gl.attachShader(programHandle, fragmentShaderHandle);

            // Bind attributes
            if (attributes != null)
            {
                int size = attributes.Length;
                for (int i = 0; i < size; i++)
                {
                    gl.bindAttribLocation(programHandle, i, attributes[i]);
                }
            }

            // Link the two shaders together into a program.
            gl.linkProgram(programHandle);

            // Get the link status.
            int[] linkStatus = new int[1];
            GLES20.glGetProgramiv(programHandle.value, GLES20.GL_LINK_STATUS, linkStatus, 0);

            // If the link failed, delete the program.
            if (linkStatus[0] == 0)
            {
                Log.e("createAndLinkProgram", GLES20.glGetProgramInfoLog(programHandle.value));
                gl.deleteProgram(programHandle);
                programHandle = null;
            }

            //if (programHandle == 0)
            //{
            //    throw null;
            //    //throw new RuntimeException("Error creating program.");
            //}

            return programHandle;
        }

        #endregion

        public static bool StringEquals(this string e, string value)
        {
            if (e == null)
            {
                if (value == null)
                    return true;

                return false;
            }

            if (value == null)
                return false;

            return InternalStringEquals(e, value);
        }

        [Script(OptimizedCode = "return e.equals(value);")]
        static bool InternalStringEquals(string e, string value)
        {
            return false;
        }


        class setItems_OnClickListener : DialogInterface_OnClickListener
        {
            public Action<DialogInterface, int> handler;

            public void onClick(DialogInterface dialog, int item)
            {
                handler(dialog, item);
            }
        }

        public static void setItems(this AlertDialog.Builder builder, string[] items, Action<int> handler)
        {
            builder.setItems(items, (_dialog, item) => handler(item));
        }

        public static void setItems(this AlertDialog.Builder builder, string[] items, Action<DialogInterface, int> handler)
        {
            builder.setItems(
                  (CharSequence[])(object)items,
                  new setItems_OnClickListener { handler = handler }
              );
        }




        public static Context ShowLongToast(this Context c, string e)
        {
            if (c == null)
                return c;

            Toast.makeText(
                  c,
                  e,
                  Toast.LENGTH_LONG
              ).show();

            return c;
        }

        class ShowLongToastHandler : Runnable
        {
            public Context c;
            public string e;

            public void run()
            {
                // http://stackoverflow.com/questions/3134683/android-toast-in-a-thread
                c.ShowLongToast(e);
            }
        }

        public static Activity ShowLongToast(this Activity c, string e)
        {

            c.runOnUiThread(
                new ShowLongToastHandler
                {
                    c = c,
                    e = e
                }
            );

            return c;
        }




        public static int HIDE_DELAY_MILLIS = 5000;

        class HideLater : View.OnSystemUiVisibilityChangeListener, Runnable
        {
            public Activity that;
            public View view;

            public void run()
            {
                that.getWindow().getDecorView().setSystemUiVisibility(
                                   View.SYSTEM_UI_FLAG_HIDE_NAVIGATION | View.SYSTEM_UI_FLAG_LOW_PROFILE);
            }

            public void onSystemUiVisibilityChange(int value)
            {
                view.postDelayed(
                    this, HIDE_DELAY_MILLIS
                );
            }
        }


        public static Activity ToFullscreen(this Activity e)
        {
            e.getWindow().requestFeature(Window.FEATURE_ACTION_BAR_OVERLAY);
            e.requestWindowFeature(Window.FEATURE_NO_TITLE);
            e.getWindow().setFlags(WindowManager_LayoutParams.FLAG_FULLSCREEN, WindowManager_LayoutParams.FLAG_FULLSCREEN);


            return e;
        }


        public static void TryHideActionbar(this Activity that, View view)
        {
            try
            {
                //Log.wtf("AndroidGLSpiralActivity", "TryHideActionbar");
                var h = new HideLater { that = that, view = view };
                view.setOnSystemUiVisibilityChangeListener(
                   h
                    );

                h.onSystemUiVisibilityChange(0);
                //Log.wtf("AndroidGLSpiralActivity", "TryHideActionbar done");
            }
            catch
            {
                Log.wtf("AndroidGLSpiralActivity", "TryHideActionbar error");

                //throw;
            }
        }


        public static void ToNotification(this Context that, string Title, string Content, int id, int icon = 0, string uri = "http://www.jsc-solutions.net")
        {
            // Send Notification
            var notificationManager = (NotificationManager)that.getSystemService(Context.NOTIFICATION_SERVICE);

            var w = Title + " ";
            w += Content;

            if (icon == 0)
                icon = android.R.drawable.star_on;

            var myNotification = new Notification(
                //android.R.drawable.star_on,
                icon,
                (CharSequence)(object)w,
                java.lang.System.currentTimeMillis()
            );

            Context context = that.getApplicationContext();

            // ah. c# dynamic for android versions :)

            //#region Notification.largeIcon
            //try
            //{
            //    var largeIcon = AbstractNotifyService.NotificationClass.getField("largeIcon");

            //    if (largeIcon != null)
            //    {
            //        BitmapFactory.Options options = new BitmapFactory.Options();
            //        options.inScaled = false;	// No pre-scaling

            //        // Read in the resource
            //        Bitmap bitmap = BitmapFactory.decodeResource(context.getResources(), R.drawable.white_jsc, options);

            //        largeIcon.set(myNotification, bitmap);
            //    }
            //}
            //catch
            //{ }
            //#endregion


            Intent myIntent = new Intent(Intent.ACTION_VIEW, android.net.Uri.parse(uri));

            var BaseContext = that;

            var IsContextWrapper = that is ContextWrapper;
            if (IsContextWrapper)
                BaseContext = ((ContextWrapper)that).getBaseContext();

            PendingIntent pendingIntent
                //= PendingIntent.getActivity(that.getBaseContext(),
              = PendingIntent.getActivity(BaseContext,
                0, myIntent,
                Intent.FLAG_ACTIVITY_NEW_TASK);
            myNotification.defaults |= Notification.DEFAULT_SOUND;
            myNotification.flags |= Notification.FLAG_AUTO_CANCEL;
            myNotification.setLatestEventInfo(context,
                    (CharSequence)(object)Title,
                    (CharSequence)(object)Content,
               pendingIntent);

            notificationManager.notify(id, myNotification);
        }


        public static void CancelPendingAlarm(this Context that, Class IntentClass)
        {

            // http://stackoverflow.com/questions/6522792/get-list-of-active-pendingintents-in-alarmmanager
            var myIntent = new Intent(that, IntentClass);
            var pendingIntent = PendingIntent.getService(that, 0, myIntent, 0);


            AlarmManager alarmManager = (AlarmManager)that.getSystemService(Context.ALARM_SERVICE);
            alarmManager.cancel(pendingIntent);
        }

        public static void StartPendingAlarm(this Context that, Type IntentClass, long delay = 1000  * 5, long repeat = 1000 * 25)
        {
            StartPendingAlarm(that, IntentClass.ToClass(), delay, repeat);
        }

        public static void StartPendingAlarm(this Context that, Class IntentClass, long delay = 1000  * 5, long repeat = 1000 * 25)
        {
            that.CancelPendingAlarm(IntentClass);

            var myIntent = new Intent(that, IntentClass);
            var pendingIntent = PendingIntent.getService(that, 0, myIntent, 0);

            AlarmManager alarmManager = (AlarmManager)that.getSystemService(Context.ALARM_SERVICE);


            if (repeat > 0)
            {
                alarmManager.setInexactRepeating(
                      AlarmManager.RTC,
                      delay,
                      repeat,
                      pendingIntent
                  );
            }
            else
            {
                alarmManager.set(AlarmManager.RTC, delay, pendingIntent);
            }
        }


        class queueEvent_Handler : Runnable
        {
            public Action h;

            public void run()
            {
                h();
            }
        }

        public static void queueEvent(this GLSurfaceView that, Action h)
        {
            that.queueEvent(
                new queueEvent_Handler { h = h }
            );
        }

        //class AtClickHandler : View.OnClickListener
        //{
        //    public Action h;

        //    public void onClick(android.view.View value)
        //    {
        //        h();
        //    }
        //}


        //public static void AtClick(this Button that, Action h)
        //{
        //    that.setOnClickListener(
        //        new AtClickHandler
        //        {
        //            h = h
        //        }
        //    );
        //}
    }

    public class RenderingContextView : GLSurfaceView, GLSurfaceView.Renderer, ISurface
    {
        WebGLRenderingContext gl;

        public event Action<WebGLRenderingContext> onsurface;
        public event Action onframe;
        public event Action<int, int> onresize;

        Context c;

        public RenderingContextView(Context c)
            : base(c)
        {
            this.c = c;

            // Create an OpenGL ES 2.0 context.
            setEGLContextClientVersion(2);

            setDebugFlags(DEBUG_CHECK_GL_ERROR | DEBUG_LOG_GL_CALLS);

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
            gl = new WebGLRenderingContext();
            if (onsurface != null)
                onsurface(gl);
        }

        #region onaccelerometer
        class MySensorEventListener : SensorEventListener
        {
            public Action<float, float, float> onaccelerometer;

            public void onAccuracyChanged(Sensor sensor, int accuracy)
            {

            }
            public void onSensorChanged(SensorEvent e)
            {

                // check sensor type
                if (e.sensor.getType() == Sensor.TYPE_ACCELEROMETER)
                {

                    // assign directions
                    float x = e.values[0];
                    float y = e.values[1];
                    float z = e.values[2];

                    if (onaccelerometer != null)
                        onaccelerometer(x, y, z);
                }
            }
        }

        public event Action<float, float, float> onaccelerometer
        {
            remove
            {
            }

            add
            {
                SensorManager sensorManager;


                sensorManager = (SensorManager)c.getSystemService(Activity.SENSOR_SERVICE);
                // add listener. The listener will be HelloAndroid (this) class
                sensorManager.registerListener(
                    new MySensorEventListener { onaccelerometer = value }
                    ,
                        sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER),
                        SensorManager.SENSOR_DELAY_GAME);

                /*	More sensor speeds (taken from api docs)
                    SENSOR_DELAY_FASTEST get sensor data as fast as possible
                    SENSOR_DELAY_GAME	rate suitable for games
                    SENSOR_DELAY_NORMAL	rate (default) suitable for screen orientation changes
                */
            }
        }
        #endregion

        public event Action<float, float> ontouchmove;

        // Offsets for touch events	 
        private float mPreviousX;
        private float mPreviousY;

        public float mDensity;

        public override bool onTouchEvent(MotionEvent e)
        {
            if (e != null)
            {
                float x = e.getX();
                float y = e.getY();

                if (e.getAction() == MotionEvent.ACTION_MOVE)
                {

                    float deltaX = (x - mPreviousX) / mDensity / 2f;
                    float deltaY = (y - mPreviousY) / mDensity / 2f;

                    if (ontouchmove != null)
                        ontouchmove(deltaX, deltaY);
                    //mRenderer.mDeltaX += deltaX;
                    //mRenderer.mDeltaY += deltaY;
                }

                mPreviousX = x;
                mPreviousY = y;

                return true;
            }

            return base.onTouchEvent(e);
        }
    }


    #region IntentFilter
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class IntentFilterAttribute : Attribute
    {
        // jsc does not support properties yet? are they even allowed in java?

        public string Action;
    }
    #endregion

}
