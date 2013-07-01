using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class DataTransferItemList
    {
        public readonly uint length;

        public DataTransferItemList() { throw null; } 

        public DataTransferItem this[uint index] { get { throw null; } }

        public DataTransferItem add(File data)  { throw null; }
        public DataTransferItem add(string data, string type) { throw null; } 
        public void clear()  { throw null; } 
        public void deleter(uint index) { throw null; }
    }
}
