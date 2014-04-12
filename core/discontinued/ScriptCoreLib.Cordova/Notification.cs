using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System;


namespace ScriptCoreLib.Cordova
{
    /// <summary>
    /// Visual, audible, and tactile device notifications
    /// http://docs.phonegap.com/en/1.7.0/cordova_notification_notification.md.html#Notification
    /// </summary>
    [Script(IsNative = true)]
    public class Notification
    {
        #region Constructor

        public Notification()
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Shows a custom alert or dialog box
        /// </summary>
        /// <param name="message"></param>
        /// <param name="alertCallback"></param>
        /// <param name="title"></param>
        /// <param name="buttonName"></param>
        public void alert(string message, Action alertCallback, string title = null, string buttonName = null)
        { }

        /// <summary>
        /// Shows a customizable confirmation dialog box
        /// </summary>
        /// <param name="message"></param>
        /// <param name="confirmCallback"></param>
        /// <param name="title"></param>
        /// <param name="buttonLabels"></param>
        public void confirm(string message, Action<object> confirmCallback, string title = "Confirm", string buttonLabels = "OK,Cancel") { }

        /// <summary>
        /// The device will play a beep sound
        /// </summary>
        public void beep(int times)
        { }

        /// <summary>
        /// Vibrates the device for the specified amount of time.
        /// </summary>
        /// <param name="milliseconds"></param>
        public void vibrate(int milliseconds)
        { }


        #endregion

    }
}
