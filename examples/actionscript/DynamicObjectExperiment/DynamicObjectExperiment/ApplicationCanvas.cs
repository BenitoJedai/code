using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace DynamicObjectExperiment
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

            var t = new TextBox
            {
                AcceptsReturn = true,
                IsReadOnly = true,
            }.AttachTo(this);

            t.AppendTextLine("hello world");

            var foo = new MyDynamic { WriteLine = t.AppendTextLine };
            dynamic bar = foo;

            bar.foo = "foo";

            var goo = bar.goo;

            t.AppendTextLine(new { goo }.ToString());
        }

    }

    class MyDynamic : DynamicObject
    {
        public Action<string> WriteLine;

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            WriteLine("TryInvokeMember");
            result = null;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            WriteLine("TryGetMember");

            result = "MyDynamic" + new { binder.Name };
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            WriteLine("TrySetMember");

            return true;
        }
    }
}
