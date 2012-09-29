using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RewriteComImportInterface
{
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("51372af4-cae7-11cf-be81-00aa00a2fa25")]
    public interface IGetContextProperties
    {
        object GetParamForMethodIndex();
    }

    class __IGetContextProperties : IGetContextProperties
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.PreserveSig)]
        //[PreserveSig]
        object IGetContextProperties.GetParamForMethodIndex()
        {
            throw new NotImplementedException();
        }
    }
}
