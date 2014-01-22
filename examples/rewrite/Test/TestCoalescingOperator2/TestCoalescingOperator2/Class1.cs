using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCoalescingOperator2
{
    public class Native
    {
        public static Native document;
        public Native body;
        public string innerText;

        static void y(string innerText)
        {
            var x = innerText ?? "server returned null";
            //var x = innerText;

            Native.document.body.innerText = x;
        }

    }
}
