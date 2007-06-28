using ScriptCoreLib.JavaScript.Runtime;


namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class ISink
    {
        [Script]
        public sealed class EventNames
        {
            // w3c
            public string EventListener;
            public string EventListenerAlt;

            // IE
            public string Event;
            public string EventAlt;
        }

        [Script(DefineAsStatic = true, NoExeptions = true)]
        public void InternalEvent(bool bAttach, global::System.Delegate e, EventNames n)
        {

            IFunction z = ((BCLImplementation.System.__Delegate)(object)e).InvokePointer;

            if (bAttach)
            {
                if (Expando.InternalIsMember(this, "addEventListener"))
                {
                    addEventListener(n.EventListener, z, false);

                    if (n.EventListenerAlt != null)
                        addEventListener(n.EventListenerAlt, z, false);
                }

                if (Expando.InternalIsMember(this, "attachEvent"))
                {
                    attachEvent(n.Event, z);

                    if (n.EventAlt != null)
                    {
                        attachEvent(n.EventAlt, z);
                    }

                }
            }
            else
            {
                if (Expando.InternalIsMember(this, "removeEventListener"))
                {
                    removeEventListener(n.EventListener, z, false);

                    if (n.EventListenerAlt != null)
                        removeEventListener(n.EventListenerAlt, z, false);
                }

                if (Expando.InternalIsMember(this, "detachEvent"))
                {
                    detachEvent(n.Event, z);

                    if (n.EventAlt != null)
                    {
                        detachEvent(n.EventAlt, z);
                    }
                }
            }
        }

        [Script(DefineAsStatic = true, NoExeptions = true)]
        public void InternalEvent(bool bAttach, global::System.Delegate e, string _EventListener, string _Event)
        {
            InternalEvent(bAttach, e, new EventNames { Event = _Event, EventListener = _EventListener });

            //IFunction z = ((BCLImplementation.System.__Delegate)(object)e).InvokePointer;

            //if (bAttach)
            //{
            //    if (Expando.InternalIsMember(this, "addEventListener"))
            //        addEventListener(_EventListener, z, false);
            //    if (Expando.InternalIsMember(this, "attachEvent"))
            //        attachEvent(_Event, z);
            //}
            //else
            //{
            //    if (Expando.InternalIsMember(this, "removeEventListener"))
            //        removeEventListener(_EventListener, z, false);
            //    if (Expando.InternalIsMember(this, "detachEvent"))
            //        detachEvent(_Event, z);
            //}
        }

        [Script(DefineAsStatic = true, NoExeptions = true)]
        public void InternalEvent(bool b, global::System.Delegate e, string f)
        {
            InternalEvent(b, e, f, "on" + f);


        }

        internal void attachEvent(string f, IFunction e)
        {

        }

        internal void detachEvent(string f, IFunction e)
        {

        }

        [Script(DefineAsStatic = true)]
        internal void addEventListener(string f, System.Delegate e, bool c)
        {
            this.addEventListener(f, ((BCLImplementation.System.__Delegate)(object)e).InvokePointer, c);
        }

        [Script(DefineAsStatic = true)]
        internal void removeEventListener(string f, System.Delegate e, bool c)
        {
            this.removeEventListener(f, ((BCLImplementation.System.__Delegate)(object)e).InvokePointer, c);
        }


        internal void addEventListener(string f, IFunction e, bool c)
        {

        }

        internal void removeEventListener(string f, IFunction e, bool c)
        {

        }
    }
}
