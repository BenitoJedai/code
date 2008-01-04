using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Runtime;

namespace ExampleGallery.js
{

    [Script]
    static class Extensions
    {

        public static Action Until(this int i, Func<Timer, bool> h)
        {
            Action done = () => { };

            new Timer(
                t =>
                {
                    if (h(t))
                    {
                        t.Stop();

                        done();
                    }
                }, i, i);

            return done;
        }

        public static int Random(this int i)
        {
            return new Random().Next(i);
        }
    }
}
