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
    public interface IEventsDispatch
    {
        bool DispatchInt32(int e, IDispatchHelper h);
    }
}
