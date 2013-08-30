using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace TestDictionaryOfTypeAndFunc
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            r.Fill = Brushes.Red;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            var Options = new Dictionary<Type, Func<int>>
			{
				{ typeof(Canvas), 
					() => 78
				},
				{ typeof(ApplicationCanvas), 
					() => 88
				}
            };

            var t = new TextBox { AcceptsReturn = true, IsReadOnly = true }.AttachTo(this);

            t.AppendTextLine(new { First = new { Options.First().Key.Name } }.ToString());
            t.AppendTextLine(new { Last = new { Options.Last().Key.Name } }.ToString());

            t.AppendTextLine(new { FirstKey = new { Options.Keys.First().Name } }.ToString());
            t.AppendTextLine(new { LastKey = new { Options.Keys.Last().Name } }.ToString());


            Options
                //.ForEach(
               .Select(Option => new { Option.Key, Option.Value })
               .WithEachIndex(
               (Option, Index) =>
               {
                   t.AppendTextLine(new { Option.Key.Name }.ToString());

               }
           );
        }

    }
}
