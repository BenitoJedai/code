using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.RayCaster
{
    [Script]
    public class ViewInfo
    {
        public double Left;
        public double Right;
        public double Target;

        public bool IsInView;

        public void Update()
        {
            IsInView = true;

            var w = 360.DegreesToRadians();
            

            if (Right < Left)
            {
                Right += w;

                if (Target < Right)
                    if (Left < Target)
                        return;

                Left -= w;
                Right -= w;


            }

            if (Target < Right)
                if (Left < Target)
                    return;

            IsInView = false;

            return;
        }
    }

}
