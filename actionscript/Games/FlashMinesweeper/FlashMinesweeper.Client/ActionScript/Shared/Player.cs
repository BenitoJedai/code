using System;
using System.Collections.Generic;
using System.Text;
#if !NoAttributes
using ScriptCoreLib;
#endif
namespace FlashMinesweeper.ActionScript.Shared
{
#if !NoAttributes
    [Script]
#endif
    public class Player : Generic.ServerPlayerBase<SharedClass1.IEvents, SharedClass1.IMessages>
    {
        
    }
}
