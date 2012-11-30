using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
    [Description("guideline: BCL extension filename without suffix, type with suffix")]
    public static class DataGridViewColumnCellExtensions
    {
        public static void AddText(this DataGridViewCellCollection c, string text)
        {
            c.Add(
                new DataGridViewTextBoxCell { Value = text }
            );
        }

        public static void AddTextRange(this DataGridViewCellCollection c, params string[] text)
        {
            foreach (var item in text)
            {
                c.AddText(item);
            }
        }
    }
}
