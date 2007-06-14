using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared;

namespace SpaceInvaders.source.js
{
    [Script]
    static class Extensions
    {
        public static void Trigger(this Func<bool> condition, Action done, int interval)
        {
            Timer t = null;

            t = new Timer(
                delegate
                {
                    if (!condition())
                    {
                        t.Stop();

                        done();
                    }
                }
            );

            t.StartInterval(interval);
        }
    }
}
