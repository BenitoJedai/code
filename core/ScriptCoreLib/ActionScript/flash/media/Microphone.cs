using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.media
{
    // http://livedocs.adobe.com/flex/3/langref/flash/media/Microphone.html
    [Script(IsNative=true)]
    public sealed class Microphone : EventDispatcher
    {
        #region Properties
        /// <summary>
        /// [read-only] The amount of sound the microphone is detecting.
        /// </summary>
        public double activityLevel { get; private set; }

        /// <summary>
        /// The microphone gain—that is, the amount by which the microphone should multiply the signal before transmitting it.
        /// </summary>
        public double gain { get; set; }

        /// <summary>
        /// [read-only] The index of the microphone, as reflected in the array returned by Microphone.names.
        /// </summary>
        public int index { get; private set; }

        /// <summary>
        /// [read-only] Specifies whether the user has denied access to the microphone (true) or allowed access (false).
        /// </summary>
        public bool muted { get; private set; }

        /// <summary>
        /// [read-only] The name of the current sound capture device, as returned by the sound capture hardware.
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// [static] [read-only] An array of strings containing the names of all available sound capture devices.
        /// </summary>
        public static string[] names { get; private set; }

        /// <summary>
        /// The rate at which the microphone captures sound, in kHz.
        /// </summary>
        public int rate { get; set; }

        /// <summary>
        /// [read-only] The amount of sound required to activate the microphone and dispatch the activity event.
        /// </summary>
        public double silenceLevel { get; private set; }

        /// <summary>
        /// [read-only] The number of milliseconds between the time the microphone stops detecting sound and the time the activity event is dispatched.
        /// </summary>
        public int silenceTimeout { get; private set; }

        /// <summary>
        /// Controls the sound of this microphone object when it is in loopback mode.
        /// </summary>
        public SoundTransform soundTransform { get; set; }

        /// <summary>
        /// [read-only] Set to true if echo suppression is enabled; false otherwise.
        /// </summary>
        public bool useEchoSuppression { get; private set; }

        #endregion

        #region Methods
        /// <summary>
        /// [static] Returns a reference to a Microphone object for capturing audio.
        /// </summary>
        public static Microphone getMicrophone(int index)
        {
            return default(Microphone);
        }

        /// <summary>
        /// [static] Returns a reference to a Microphone object for capturing audio.
        /// </summary>
        public static Microphone getMicrophone()
        {
            return default(Microphone);
        }

        /// <summary>
        /// Routes audio captured by a microphone to the local speakers.
        /// </summary>
        public void setLoopBack(bool state)
        {
        }

        /// <summary>
        /// Routes audio captured by a microphone to the local speakers.
        /// </summary>
        public void setLoopBack()
        {
        }

        /// <summary>
        /// Sets the minimum input level that should be considered sound and (optionally) the amount of silent time signifying that silence has actually begun.
        /// </summary>
        public void setSilenceLevel(double silenceLevel, int timeout)
        {
        }

        /// <summary>
        /// Sets the minimum input level that should be considered sound and (optionally) the amount of silent time signifying that silence has actually begun.
        /// </summary>
        public void setSilenceLevel(double silenceLevel)
        {
        }

        /// <summary>
        /// Specifies whether to use the echo suppression feature of the audio codec.
        /// </summary>
        public void setUseEchoSuppression(bool useEchoSuppression)
        {
        }

        #endregion

        #region Constructors
        #endregion

        #region Events
        /// <summary>
        /// Dispatched when a microphone begins or ends a session.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<ActivityEvent> activity;

        /// <summary>
        /// Dispatched when a microphone reports its status.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<StatusEvent> status;

        #endregion

    }
}
