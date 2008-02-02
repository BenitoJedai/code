using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.utils
{
    // http://livedocs.adobe.com/flex/201/langref/flash/utils/Timer.html
    [Script(IsNative = true)]
    public class Timer : EventDispatcher
    {
        [method: Script(NotImplementedHere = true)]
        public event Action<TimerEvent> timer;


        /// <summary>
        /// Constructs a new Timer object with the specified delay and repeatCount states.
        /// </summary>
        /// <param name="delay"></param>
        public Timer(double delay, int repeatCount)
        {

        }

        public Timer(double delay)
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

        /// <summary>
        /// The total number of times the timer has fired since it started at zero.
        /// </summary>
        public int currentCount { get; private set; }

    }
}
