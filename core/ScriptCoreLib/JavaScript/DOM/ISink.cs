using ScriptCoreLib.JavaScript.Runtime;
using System;


namespace ScriptCoreLib.JavaScript.DOM
{

    // http://www.whatwg.org/specs/web-apps/current-work/multipage/infrastructure.html#eventtarget
    [Script(HasNoPrototype = true)]
    //public class ISink
    public class IEventTarget
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
            // http://help.dottoro.com/ljrtxexf.php

            IFunction z = ((BCLImplementation.System.__Delegate)(object)e).InvokePointer;

            // does IE9 define both functions?

            if (bAttach)
            {
                if (Expando.InternalIsMember(this, "addEventListener"))
                {
                    // https://developer.mozilla.org/en-US/docs/DOM/element.addEventListener
                    addEventListener(n.EventListener, z);

                    if (n.EventListenerAlt != null)
                        addEventListener(n.EventListenerAlt, z);
                }
                else if (Expando.InternalIsMember(this, "attachEvent"))
                {
                    attachEvent(n.Event, z);

                    if (n.EventAlt != null)
                    {
                        attachEvent(n.EventAlt, z);
                    }

                }

                return;
            }

            #region remove
            if (Expando.InternalIsMember(this, "removeEventListener"))
            {
                removeEventListener(n.EventListener, z, false);

                if (n.EventListenerAlt != null)
                    removeEventListener(n.EventListenerAlt, z, false);
            }
            else if (Expando.InternalIsMember(this, "detachEvent"))
            {
                detachEvent(n.Event, z);

                if (n.EventAlt != null)
                {
                    detachEvent(n.EventAlt, z);
                }
            }
            #endregion

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
        public void InternalEvent(bool bAttach, global::System.Delegate e, string f)
        {
            InternalEvent(bAttach, e, f, "on" + f);


        }

        internal void attachEvent(string f, IFunction e)
        {

        }

        internal void detachEvent(string f, IFunction e)
        {

        }

        [Script(DefineAsStatic = true)]
        public void addEventListener(string f, Action<IEvent> e, bool c = false)
        {
            this.addEventListener(f, ((BCLImplementation.System.__Delegate)(object)e).InvokePointer, c);
        }

        [Script(DefineAsStatic = true)]
        public void removeEventListener(string f, Action<IEvent> e, bool c = false)
        {
            this.removeEventListener(f, ((BCLImplementation.System.__Delegate)(object)e).InvokePointer, c);
        }

        [Script(DefineAsStatic = true)]
        public void addEventListener(string f, System.Delegate e, bool c = false)
        {
            this.addEventListener(f, ((BCLImplementation.System.__Delegate)(object)e).InvokePointer, c);
        }

        [Script(DefineAsStatic = true)]
        public void removeEventListener(string f, System.Delegate e, bool c = false)
        {
            this.removeEventListener(f, ((BCLImplementation.System.__Delegate)(object)e).InvokePointer, c);
        }


        public void addEventListener(string f, IFunction e, bool c = false)
        {

        }

        internal void removeEventListener(string f, IFunction e, bool c = false)
        {

        }
    }
}
