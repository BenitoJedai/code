using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.utils
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/utils/Timer.html
    // http://livedocs.adobe.com/flex/3/langref/flash/utils/Timer.html
    [Script(IsNative = true)]
    public class Timer : EventDispatcher
    {
        #region Events
        /// <summary>
        /// Dispatched whenever a Timer object reaches an interval specified according to the Timer.delay property.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TimerEvent> timer;

        /// <summary>
        /// Dispatched whenever it has completed the number of requests set by Timer.repeatCount.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TimerEvent> timerComplete;

        #endregion

   


        #region Methods
        /// <summary>
        /// Stops the timer, if it is running, and sets the currentCount property back to 0, like the reset button of a stopwatch.
        /// </summary>
        public void reset()
        {
        }

        /// <summary>
        /// Starts the timer, if it is not already running.
        /// </summary>
        public void start()
        {
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void stop()
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a new Timer object with the specified delay and repeatCount states.
        /// </summary>
        public Timer(double delay, int repeatCount)
        {
        }

        /// <summary>
        /// Constructs a new Timer object with the specified delay and repeatCount states.
        /// </summary>
        public Timer(double delay)
        {
        }

        #endregion


        #region Properties
        /// <summary>
        /// [read-only] The total number of times the timer has fired since it started at zero.
        /// </summary>
        public int currentCount { get; private set; }

        /// <summary>
        /// The delay, in milliseconds, between timer events.
        /// </summary>
        public double delay { get; set; }

        /// <summary>
        /// The total number of times the timer is set to run.
        /// </summary>
        public int repeatCount { get; set; }

        /// <summary>
        /// [read-only] The timer's current state; true if the timer is running, otherwise false.
        /// </summary>
        public bool running { get; private set; }

        #endregion

    }
}
