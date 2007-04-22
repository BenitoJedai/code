using ScriptCoreLib.Shared;


namespace ScriptCoreLib.PHP.Runtime
{
    [Script]
    public class CustomSwitch<TIndex> : CustomSwitch<CustomSwitch<TIndex>, TIndex>
    {
        public bool Run(TIndex p)
        {
            return this.Run(this, p);
        }
    }

    [Script]
    public class CustomSwitch<TOwner, TIndex>
    {
        public EventHandler<EventHandlerArgs> this[TIndex e]
        {
            get
            {
                return List[e];
            }
            set
            {
                List[e] = value;

                if (IsVerbose)
                    Native.Log("CustomSwitch of " + e);
            }
        }

        public bool IsVerbose;


        public EventHandler<EventHandlerArgs> this[params TIndex[] e]
        {
            set
            {
                foreach (TIndex x in e)
                    this[x] = value;
            }
        }

        [Script]
        public class EventHandlerArgs
        {
            public TIndex Index;
            public TOwner Sender;
            public bool Done;
        }

        [Script]
        public class EventHandlerArgs<TItem> : EventHandlerArgs
        {
            public TItem Item;

            public EventHandlerArgs(EventHandlerArgs e, TItem i)
            {
                this.Done = e.Done;
                this.Index = e.Index;
                this.Sender = e.Sender;

                this.Item = i;
            }
        }

        public readonly IArray<TIndex, EventHandler<EventHandlerArgs>> List = new IArray<TIndex, EventHandler<EventHandlerArgs>>();


        public void Apply(IArray<TIndex, EventHandler<EventHandlerArgs>> e)
        {
            foreach (TIndex v in e.Keys)
            {
                this[v] = e[v];
            }
        }


        /// <summary>
        /// will return false if handler does not exist
        /// or if the handler explicitly sets Done field
        /// to false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        protected bool Run(TOwner sender, TIndex p)
        {

            EventHandler<EventHandlerArgs> h = this[p];

            if (h == null)
            {
                return false;
            }

            EventHandlerArgs a = new EventHandlerArgs();

            a.Index = p;
            a.Sender = sender;
            a.Done = true;


            h(a);


            return a.Done;
        }
    }
}
