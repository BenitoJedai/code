using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
    [Description("guideline: BCL extension filename without suffix, type with suffix")]
    public static class DataGridViewColumnCollectionExtensions
    {
        public static void AddText(this DataGridViewColumnCollection c, string text)
        {
            c.Add(text, text);
        }

        public static void AddTextRange(this DataGridViewColumnCollection c, params string[] text)
        {
            foreach (var item in text)
            {
                c.AddText(item);

            }

        }
    }
}
