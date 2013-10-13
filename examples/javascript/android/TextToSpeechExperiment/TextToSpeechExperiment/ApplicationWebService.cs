using android.speech.tts;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextToSpeechExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public Task TextToSpeechSpeak(string e)
        {
            var a = new EventWaitHandle(false, EventResetMode.ManualReset);

            //Handler (android.speech.tts.TextToSpeechService$SynthHandler) {4217c158} sending message to a Handler on a dead thread
            //java.lang.RuntimeException: Handler (android.speech.tts.TextToSpeechService$SynthHandler) {4217c158} sending message to a Handler on a dead thread
            // at android.os.MessageQueue.enqueueMessage(MessageQueue.java:309)
            // at android.os.Handler.enqueueMessage(Handler.java:623)
            // at android.os.Handler.sendMessageAtTime(Handler.java:592)
            // at android.os.Handler.sendMessageDelayed(Handler.java:563)
            // at android.os.Handler.sendMessage(Handler.java:500)
            // at android.speech.tts.TextToSpeechService$SynthHandler.enqueueSpeechItem(TextToSpeechService.java:345)
            // at android.speech.tts.TextToSpeechService$1.speak(TextToSpeechService.java:803)
            // at android.speech.tts.ITextToSpeechService$Stub.onTransact(ITextToSpeechService.java:66)
            // at android.os.Binder.execTransact(Binder.java:388)
            // at dalvik.system.NativeStart.run(Native Method)

            var mTts = new TextToSpeech(ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext,

                new XListener
                {
                    yield = c =>
                    {
                        a.Set();

                    }
                }

            );

            a.WaitOne();

            Console.WriteLine("speak " + new { e });
            mTts.speak(e, TextToSpeech.QUEUE_FLUSH, null);

            // sleep while speaking

            while (mTts.isSpeaking())
            {
                //  unsupported flow detected, try to simplify 'TextToSpeechExperiment.ApplicationWebService.TextToSpeechSpeak'. Try ommiting the return, break or continue instruction.
                Thread.Sleep(111);
            }

            Console.WriteLine("speak done " + new { e });

            mTts.shutdown();

            return Task.FromResult(default(object));
        }


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
