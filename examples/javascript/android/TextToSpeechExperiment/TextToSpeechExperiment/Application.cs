using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextToSpeechExperiment.Design;
using TextToSpeechExperiment.HTML.Pages;

namespace TextToSpeechExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            page.Speak.WhenClicked(
                async delegate
                {
                    @"Hello world".ToDocumentTitle();
                    // Send data from JavaScript to the server tier
                    await TextToSpeechSpeak(page.text.value);
                }
            );

            Native.window.onframe +=
                async frameindex =>
                {
                    var old = page.text.value;

                    //Console.WriteLine("onframe " + new { frameindex } + " before requestAnimationFrameAsync ");
                    await Native.window.requestAnimationFrameAsync;
                    //Console.WriteLine("onframe " + new { frameindex } + " after requestAnimationFrameAsync ");

                    if (old == page.text.value)
                    {
                        //Console.WriteLine("onframe " + new { frameindex } + " no change");

                        return;
                    }

                    if (page.text.value == "")
                    {
                        Console.WriteLine("onframe " + new { frameindex } + " empty");

                        return;
                    }

                    // ther has been a change!

                    page.text.style.backgroundColor = "cyan";

                    if (!page.monkey)
                    {
                        Console.WriteLine("onframe " + new { frameindex } + " not a monkey");

                        return;
                    }

                    page.text.style.backgroundColor = "yellow";

                    var current = page.text.value;

                    Console.WriteLine("onframe " + new { frameindex } + " before delay");
                    await Task.Delay(200);

                    Console.WriteLine("onframe " + new { frameindex } + " after delay");

                    page.text.style.backgroundColor = "red";

                    if (current != page.text.value)
                    {
                        Console.WriteLine("onframe " + new { frameindex, current, page.text.value } + " yet another change!");

                        return;
                    }

                    page.text.style.backgroundColor = "green";

                    page.text.value = "";

                    page.Speak.disabled = true;

                    Console.WriteLine("onframe " + new { frameindex } + " speak!");

                    await TextToSpeechSpeak(current);

                    Console.WriteLine("onframe " + new { frameindex } + " speak! done");

                    page.text.style.backgroundColor = "";
                    page.Speak.disabled = false;
                };


        }

    }
}
