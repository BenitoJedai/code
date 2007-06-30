using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace jsx.Visual
{
    public static class Extensions
    {


        public static int StringRank(this string e, params string[] a)
        {
            int i = 0;

            foreach (var v in a)
            {
                if (e.Contains(v))
                    i++;
            }

            return i;
        }

        public static ToolStripItem Pop(this ToolStripItemCollection e, int offset)
        {
            var u = e[offset];

            e.RemoveAt(offset);

            return u;
        }

        public static T PopAsTag<T>(this ToolStripItemCollection e, int offset)
        {
            return (T)e.Pop(offset).Tag;
        }

        public static void RemoveWhere(this ToolStripItemCollection e, Func<ToolStripItem, bool> a)
        {
            e.RemoveRange(e.ToSequence().Where(a).ToArray());

        }

        public static void RemoveRange(this ToolStripItemCollection e, params ToolStripItem[] a)
        {
            foreach (var v in a)
            {
                e.Remove(v);
            }
        }

        public static IEnumerable<string> ToSequence(this System.Collections.Specialized.StringCollection e)
        {
            foreach (var v in e)
            {
                yield return v;
            }
        }

        public static IEnumerable<ToolStripItem> ToSequence(this ToolStripItemCollection e)
        {
            for (int i = 0; i < e.Count; i++)
            {
                yield return e[i];
            }
        }
    }

}
