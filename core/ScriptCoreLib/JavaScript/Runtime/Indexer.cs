using ScriptCoreLib.Shared;


using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.Runtime
{
    #if BLOAT
    [Script]
    public class Indexer<TValue, TIndex>
    {
        [Script]
        public class EventHandlerArgs
        {
            public TValue Value;
            public TIndex Index;
        }

        EventHandler<EventHandlerArgs> SetEvent;
        EventHandler<EventHandlerArgs> GetEvent;

        public Indexer(EventHandler<EventHandlerArgs> get)
        {
            GetEvent = get;
        }

        public Indexer(EventHandler<EventHandlerArgs> set, EventHandler<EventHandlerArgs> get)
        {
            SetEvent = set;
            GetEvent = get;
        }

        public TValue this[TIndex e]
        {
            get
            {
                if (GetEvent == null)
                    return default(TValue);

                EventHandlerArgs a = new EventHandlerArgs();

                a.Index = e;

                GetEvent(a);

                return a.Value;
            }
            set
            {
                if (SetEvent == null)
                    return;

                EventHandlerArgs a = new EventHandlerArgs();

                a.Index = e;
                a.Value = value;

                SetEvent(a);
            }
        }
    }
#endif
}
