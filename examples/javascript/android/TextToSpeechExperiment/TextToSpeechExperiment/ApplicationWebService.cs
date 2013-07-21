using android.speech.tts;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace TextToSpeechExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            if (mTts == null)
            {
                var a = new EventWaitHandle(false, EventResetMode.ManualReset);

                mTts = new TextToSpeech(ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext,

                    new XListener
                    {
                        yield = c =>
                        {
                            a.Set();

                        }
                    }

                );

                a.WaitOne();
            }

            Console.WriteLine("speak " + new { e });
            mTts.speak(e, TextToSpeech.QUEUE_FLUSH, null);

            // Send it back to the caller.
            y(e);
        }


        static TextToSpeech mTts;
    }

    class XListener : TextToSpeech.OnInitListener
    {
        public Action<int> yield;

        public void onInit(int value)
        {
            yield(value);
        }
    }
}
