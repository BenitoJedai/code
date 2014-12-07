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
    //using gl = __WebGLRenderingContext;
    using opengl = GLES20;




    [Description("ScriptCoreLib.Extensions")]
    public static class MyExtensions
    {

        ////#region gl
        ////public static __WebGLShader createShader(this gl gl, ScriptCoreLib.GLSL.FragmentShader fragmentShader)
        ////{
        ////    return gl.createShader(GLES20.GL_FRAGMENT_SHADER, fragmentShader.ToString());
        ////}

        ////public static __WebGLShader createShader(this gl gl, ScriptCoreLib.GLSL.VertexShader fragmentShader)
        ////{
        ////    return gl.createShader(GLES20.GL_VERTEX_SHADER, fragmentShader.ToString());
        ////}

        ////private static __WebGLShader createShader(this gl gl, int shaderType, string shaderSource)
        ////{
        ////    var shaderHandle = gl.createShader(shaderType);


        ////    // Pass in the shader source.
        ////    gl.shaderSource(shaderHandle, shaderSource);

        ////    // Compile the shader.
        ////    gl.compileShader(shaderHandle);

        ////    // Get the compilation status.
        ////    int[] compileStatus = new int[1];
        ////    GLES20.glGetShaderiv(shaderHandle.value, GLES20.GL_COMPILE_STATUS, compileStatus, 0);

        ////    // If the compilation failed, delete the shader.
        ////    if (compileStatus[0] == 0)
        ////    {
        ////        Log.e("createShader", GLES20.glGetShaderInfoLog(shaderHandle.value));
        ////        gl.deleteShader(shaderHandle);
        ////        shaderHandle = null;
        ////    }

        ////    if (shaderHandle == null)
        ////    {
        ////        //throw new RuntimeException("Error creating shader.");
        ////        throw null;
        ////    }

        ////    return shaderHandle;
        ////}


        ////public static __WebGLProgram createAndLinkProgram(this gl gl, ScriptCoreLib.GLSL.VertexShader vertexShaderHandle, ScriptCoreLib.GLSL.FragmentShader fragmentShaderHandle, params string[] attributes)
        ////{
        ////    return gl.createAndLinkProgram(
        ////        gl.createShader(vertexShaderHandle),
        ////        gl.createShader(fragmentShaderHandle),
        ////        attributes
        ////    );
        ////}

        ////public static __WebGLProgram createAndLinkProgram(this gl gl, __WebGLShader vertexShaderHandle, __WebGLShader fragmentShaderHandle, params string[] attributes)
        ////{
        ////    var programHandle = gl.createProgram();


        ////    // Bind the vertex shader to the program.
        ////    gl.attachShader(programHandle, vertexShaderHandle);

        ////    // Bind the fragment shader to the program.
        ////    gl.attachShader(programHandle, fragmentShaderHandle);

        ////    // Bind attributes
        ////    if (attributes != null)
        ////    {
        ////        int size = attributes.Length;
        ////        for (int i = 0; i < size; i++)
        ////        {
        ////            gl.bindAttribLocation(programHandle, i, attributes[i]);
        ////        }
        ////    }

        ////    // Link the two shaders together into a program.
        ////    gl.linkProgram(programHandle);

        ////    // Get the link status.
        ////    int[] linkStatus = new int[1];
        ////    GLES20.glGetProgramiv(programHandle.value, GLES20.GL_LINK_STATUS, linkStatus, 0);

        ////    // If the link failed, delete the program.
        ////    if (linkStatus[0] == 0)
        ////    {
        ////        Log.e("createAndLinkProgram", GLES20.glGetProgramInfoLog(programHandle.value));
        ////        gl.deleteProgram(programHandle);
        ////        programHandle = null;
        ////    }

        ////    //if (programHandle == 0)
        ////    //{
        ////    //    throw null;
        ////    //    //throw new RuntimeException("Error creating program.");
        ////    //}

        ////    return programHandle;
        ////}

        ////#endregion

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




}
