using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ClickOnce.js
{
    [Script, ScriptApplicationEntryPoint(IsClickOnce = true, ScriptedLoading = true)]
    class TipRotatorBrother
    {
        public readonly TipRotator Value;

        public TipRotatorBrother()
        {
            this.Value = new TipRotator(TipRotator.DefaultData);
        }
    }
}
