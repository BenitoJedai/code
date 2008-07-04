using System;
using System.Collections.Generic;
using System.Text;
#if !NoAttributes
using ScriptCoreLib;
#endif
namespace FlashTowerDefense.Shared
{
#if !NoAttributes
    [Script]
#endif
    public interface ISendArguments
    {
        int i { get; }
        object[] args { get; }
    }
}
