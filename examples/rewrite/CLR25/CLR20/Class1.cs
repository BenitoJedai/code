using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;

namespace CLR20
{
    public class Class1 : Sprite
    {
        public Class1()
        {
            CommonExtensions.InvokeWhenStageIsReady(
                this,
                delegate
                {
                }
           );
        }
    }
}
