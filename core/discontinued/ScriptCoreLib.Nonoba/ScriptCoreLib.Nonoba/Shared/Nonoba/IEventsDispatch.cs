using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
namespace ScriptCoreLib.Shared.Nonoba
{
    [Script]
    public interface IEventsDispatch
    {
        bool DispatchInt32(int e, IDispatchHelper h);
    }
}
