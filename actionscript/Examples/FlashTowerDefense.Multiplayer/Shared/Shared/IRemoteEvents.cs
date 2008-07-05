using System;
using System.Collections.Generic;
using System.Text;
#if !NoAttributes
using ScriptCoreLib;
#endif
namespace FlashTowerDefense.Shared
{
//#if !NoAttributes
//    [Script]
//#endif
//    public interface IRemoteEvents<TWithUserArgumentsRouter>
//    {
//        bool Dispatch(int e, IDispatchHelper h);
//        TWithUserArgumentsRouter Router { get; set; }
//    }

#if !NoAttributes
    [Script]
#endif
    public interface IEventsDispatch
    {
        bool DispatchInt32(int e, IDispatchHelper h);
    }
}
