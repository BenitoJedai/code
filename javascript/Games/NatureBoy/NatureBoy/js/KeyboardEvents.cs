using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;

namespace NatureBoy.js
{
    [Script]
    class KeyboardEvents
    {
        public bool Enabled { get; set; }

        void Dispatcher(IEvent evx)
        {
            if (!Enabled)
                return;

            if (Table == null)
                Table = new Dictionary<int, Action<IEvent>>
                            {
                                { 39, ev => right(ev) },
                                { 40, ev => down(ev) },
                                { 37, ev => left(ev) },
                                { 38, ev => up(ev) },
                            };

            if (Table.ContainsKey(evx.KeyCode))
                Table[evx.KeyCode](evx);

        }

        Dictionary<int, Action<IEvent>> Table;

        public event ScriptCoreLib.Shared.EventHandler<IEvent> left;
        public event ScriptCoreLib.Shared.EventHandler<IEvent> right;
        public event ScriptCoreLib.Shared.EventHandler<IEvent> up;
        public event ScriptCoreLib.Shared.EventHandler<IEvent> down;

        public static implicit operator ScriptCoreLib.Shared.EventHandler<IEvent>(KeyboardEvents e)
        {
            return e.Dispatcher;
        }
    }

}
