using ScriptCoreLib;

using java.lang;
using java.applet;

using reflect = global::java.lang.reflect;

namespace DemoApplet.source.java
{
    partial class DemoApplet
    {
        //[Script(IsDebugCode = true)]
        static void Test()
        {
            throw new global::csharp.ThrowableException();
        }

        //[Script(IsDebugCode = true)]
        static void Test2()
        {
            try
            {
                Test();
            }
            catch (csharp.ThrowableException e)
            {
                throw;
            }
        }

        public static object EvaluateJavaScript(Applet that, string js)
        {
            object r = null;

            try
            {
                // 2047
                // http://www.rgagnon.com/javadetails/java-0172.html

                var c = global::java.lang.Class.forName("netscape.javascript.JSObject");

                reflect.Method getWindow = null;
                reflect.Method eval = null;

                foreach (reflect.Method m in c.getMethods())
                {
                    if (m.getName() == "getWindow") getWindow = m;
                    if (m.getName() == "eval") eval = m;
                }

                var getWindow_Arguments = new object[] { that };

                var jsWindow = getWindow.invoke(c, getWindow_Arguments);

                var eval_Arguments = new object[] { js };

                r = eval.invoke(jsWindow, eval_Arguments);

                //base.getAppletContext().showDocument(new URL("javascript:" + js));
            }
            catch (global::csharp.ThrowableException e)
            {
                throw new global::csharp.RuntimeException("Script is not supported. " + e.Message);
            }


            return r;
        }
    }
}
