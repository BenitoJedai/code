using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/notifications/Notification.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/notifications/NotificationOptions.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/notifications/NotificationPermissionCallback.idl
    // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\android\app\Notification.cs
    // https://notifications.spec.whatwg.org/

    [Script(HasNoPrototype = true, ExternalTarget = "Notification")]
    public class Notification : IEventTarget
    {
        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
        // X:\jsc.svn\examples\javascript\test\TestNotification\TestNotification\Application.cs

        // service worker doesnt yet support it?

        // http://permalink.gmane.org/gmane.comp.web.blink.devel/14696
        // https://github.com/whatwg/notifications/issues/19

        // android Notification vs chrome app Notification
        // didnt we do an example for it with webkit prefix?


        // Uncaught ReferenceError: NotificationCwAABA8QXTe2Tutf1Fl80w is not defined
        public static readonly string permission;

        // [CallWith=ExecutionContext, MeasureAs=NotificationPermissionRequested] static void requestPermission(optional NotificationPermissionCallback callback);
        // Uncaught TypeError: Failed to execute 'requestPermission' on 'Notification': The callback provided as parameter 1 is not a function.
        [Script(ExternalTarget = "Notification.requestPermission")]
        //public static void requestPermission(Action<string> callback) { }
        public static void requestPermission(IFunction callback) { }


        // Constructor(DOMString title, optional NotificationOptions options),
        public Notification(string title, object options)
        {

        }

        public Notification(string title)
        {

        }


        #region event onclick
        public event System.Action<IEvent> onclick
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "click");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "click");
            }
        }
        #endregion

        #region event onclose
        public event System.Action<IEvent> onclose
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "close");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "close");
            }
        }
        #endregion



        #region async
        [Script]
        public new class Tasks
        {
            internal Notification that;

            #region onresize
            public Task<IEvent> onclick
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();

                    // tested by
                    // X:\jsc.svn\examples\javascript\android\TextToSpeechExperiment\TextToSpeechExperiment\Application.cs
                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                    that.onclick += x.SetResult;

                    return x.Task;
                }
            }
            #endregion
        }
        #endregion

        public void close()
        {
        }

        [System.Obsolete("is this the best way to expose events as async?")]
        public new Tasks async
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new Tasks { that = this };
            }
        }
    }

}
