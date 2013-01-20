using android.content;
using android.view;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.opengl
{
    // http://developer.android.com/reference/android/opengl/GLSurfaceView.html
    [Script(IsNative = true)]
    public class GLSurfaceView
    {
        // http://developer.android.com/reference/android/opengl/GLSurfaceView.Renderer.html
        [Script(IsNative = true)]
        public interface Renderer
        {

        }

        public GLSurfaceView(Context context)
        {
        }

        public void setRenderer(GLSurfaceView.Renderer renderer)
        {

        }

        public void setDebugFlags(int debugFlags)
        {
        }
        public void setEGLContextClientVersion(int version)
        {

        }
        public virtual bool onTouchEvent(MotionEvent _event)
        {
            return default(bool);
        }
    }
}
