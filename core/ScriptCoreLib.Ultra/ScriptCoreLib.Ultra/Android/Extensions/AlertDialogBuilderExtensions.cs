extern alias globalandroid;
using globalandroid::android.opengl;
using globalandroid::android.content;
using globalandroid::android.widget;
using globalandroid::java.lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using globalandroid::android.app;

namespace ScriptCoreLib.Android.Extensions
{
    public static class AlertDialogBuilderExtensions
    {
        // X:\jsc.svn\examples\java\android\gles\AndroidOpenGLESLesson6Activity\AndroidOpenGLESLesson6Activity\Shaders\ApplicationSurface.cs

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
    }
}
