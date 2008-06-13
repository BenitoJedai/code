using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashNonobaExample.Shared
{
    [Script]
    public partial class SharedClass1
    {
        // as the server does not like the attributes the other part 
        // of this class is shared as source to the server side
        // without one :)

        // also members defined over here cannot be used by the server code
    }
}
