using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;

namespace SpaceInvaders.Library
{
    [Script]
    static class Extensions
    {
        public static bool IsSpaceOrEnterKey(this IEvent e)
        {
            if (e.KeyCode == 13)
                return true;

            if (e.KeyCode == 32)
                return true;


            return false;
        }

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
