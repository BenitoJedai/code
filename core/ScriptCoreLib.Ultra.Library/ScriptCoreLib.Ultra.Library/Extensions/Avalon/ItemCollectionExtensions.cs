using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ScriptCoreLib.Extensions.Avalon
{
    public static class ItemCollectionExtensions
    {
        public static IEnumerable<object> AsEnumerable(this ItemCollection e)
        {
            var a = new List<object>();

            foreach (var item in e)
            {
                a.Add(item);    
            }

            return a;
        }
    }
}
