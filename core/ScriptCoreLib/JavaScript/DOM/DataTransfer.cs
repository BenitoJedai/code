using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/DataTransfer.webidl
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/Html/Microsoft/LiveLabs/Html/DataTransfer.cs

    // inferred from Redux
    [Script(HasNoPrototype = true)]
    public class DataTransfer
    {
        public string dropEffect;
        public string effectAllowed;
        public readonly FileList files;
        public readonly DataTransferItemList items;
        public readonly string[] types;

        public DataTransfer() { throw null; } 

        public void addElement(IHTMLElement element) { throw null; }
        public void clearData(string format) { throw null; }
        public string getData(string format) { throw null; }
        public void setData(string format, string data) { throw null; }
        public void setDragImage(IHTMLElement image, int x, int y) { throw null; } 
    }
}
