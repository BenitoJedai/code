using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ComplexQueryExperiment
{
    public partial class ApplicationWebService
    {

    }

    interface IQueryStrategy
    {
    }

    interface IQueryStrategy<TElementType> : IQueryStrategy
    {
    }

    class xTable : ComplexQueryExperiment.FrikkingExpressionBuilder.xSelect, IQueryStrategy<xRow>
    {
        public xTable()
        {
            // select all known fields. and then some.
            //Expression<Func<xRow, object>> selector = (x) => new
            //{
            //    x.Key,
            //    x.field1,
            //    x.field2,
            //    x.Timestamp,
            //    x.Tag
            //};

            Expression<Func<xRow, xRow>> selector = (xTableDefaultSelector) => new xRow
            {
                Key = xTableDefaultSelector.Key,
                field1 = xTableDefaultSelector.field1,
                field2 = xTableDefaultSelector.field2,
                Timestamp = xTableDefaultSelector.Timestamp,
                Tag = xTableDefaultSelector.Tag
            };

            // compiler generated
            this.selector = selector;
        }
    }

    enum xKey : long { }

    class xRow
    {
        public xKey Key;

        public int field1;
        public int field2;

        public DateTime Timestamp;
        public string Tag;

    }


}
