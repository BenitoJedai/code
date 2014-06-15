using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ComplexQueryExperiment
{
    public partial class ApplicationWebService
    {

    }

    interface IQueryStrategy<TElementType>
    {
    }

    class xTable : IQueryStrategy<xRow>
    {

    }

    class xRow
    {
        public int field1;
        public int field2;
    }


}
