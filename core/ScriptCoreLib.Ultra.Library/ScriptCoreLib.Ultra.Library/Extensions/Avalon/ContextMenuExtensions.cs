using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Threading;

namespace ScriptCoreLib.Extensions.Avalon
{
    public static class ContextMenuExtensions
    {
        public static MenuItem AddMenuItem(this ContextMenu c, Image Icon, string Header, Func<Func<Action>> HandleAndYieldToBackground)
        {
            var m = new MenuItem
            {
                Icon = Icon,
                Header = Header
            };

            m.Click +=
                delegate
                {
                    HandleAndYieldToBackground().With(
                        ToBackground =>
                            new Thread(
                                delegate()
                                {
                                    ToBackground().With(
                                        ToDispatcher => c.Dispatcher.Invoke(ToDispatcher)
                                            );
                                }
                            )
                            {
                                IsBackground = true,
                            }
                            .Start()
                     );
                };

            c.Items.Add(
                m
            );

            return m;
        }
    }
}
