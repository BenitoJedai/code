//extern alias sc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
    //[TypeForwardedTo(typeof(sc::System.Runtime.CompilerServices.ExtensionAttribute))]
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class ExtensionAttribute : Attribute
    {

    }
}

