using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.Query.Experimental;

namespace TestLINQ
{
    [TestClass]
    public partial class ApplicationWebService
    {
        /// <summary>
        /// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /// </summary>
        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

        //http://blogs.msdn.com/b/windowsazurestorage/archive/2013/09/07/announcing-storage-client-library-2-1-rtm.aspx


 
    }

    public class xTable : QueryExpressionBuilder.xSelect<xRow>
    {
        public xTable()
        {

            Expression<Func<xRow, xRow>> selector = (xTableDefaultSelector) => new xRow
             {
                 Key = xTableDefaultSelector.Key,
                 field1 = xTableDefaultSelector.field1,
                 field2 = xTableDefaultSelector.field2,
                 Timestamp = xTableDefaultSelector.Timestamp,
                 Tag = xTableDefaultSelector.Tag
             };

            this.selector = selector;
        }
    }


    public enum xKey : long { }

    public class xRow
    {
        public xKey Key;

        public int field1;
        public int field2;

        public DateTime Timestamp;
        public string Tag;

    }
}
