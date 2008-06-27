using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace FlashTowerDefense.Shared
{
    [Script]
    public partial class SharedClass1
    {
        // as the server does not like the attributes the other part 
        // of this class is shared as source to the server side
        // without one :)

        // also members defined over here cannot be used by the server code

        [Script]
        partial interface IMessages
        {

        }

        [Script]
        partial class RemoteMessages
        {

        }
    }
}
