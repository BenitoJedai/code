using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace MultitouchFingerTools
{
    partial class ApplicationCanvas 
    {
        public event Action<double, double> AtNotifyBuildRocket;
        // network sync
        public Action<double, double> NotifyBuildRocket { get; private set; }


        public event Action<double, double> AtNotifyVisualizeTouch;
        // network sync
        public Action<double, double> NotifyVisualizeTouch { get; private set; }

    }

    public static class ApplicationCanvasExtensions
    {
        // this works under .NET debugger :)

        public static void ConnectToSession(this ApplicationCanvas that, IEnumerable<ApplicationCanvas> others)
        {
            that.AtNotifyBuildRocket +=
                (x, y) =>
                {
                    others.WithEach(k => k.NotifyBuildRocket(x, y));
                };

            that.AtNotifyVisualizeTouch +=
               (x, y) =>
               {
                   others.WithEach(k => k.NotifyVisualizeTouch(x, y));
               };
        }
    }



}
