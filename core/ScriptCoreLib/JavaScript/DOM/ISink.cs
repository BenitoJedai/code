using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.System;


namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class ISink 
    {
        [Script(DefineAsStatic = true, NoExeptions = true)]
        public void InternalEvent(bool b, object e, string _EventListener, string _Event)
        {

            object z = ((DelegateImpl)e).InvokePointer;

            if (b)
            {
                if (Expando.InternalIsMember(this, "addEventListener"))
                    addEventListener(_EventListener, z, false);
                if (Expando.InternalIsMember(this, "attachEvent"))
                    attachEvent(_Event, z);
            }
            else
            {
                if (Expando.InternalIsMember(this, "removeEventListener"))
                    removeEventListener(_EventListener, z, false);
                if (Expando.InternalIsMember(this, "detachEvent"))
                    detachEvent(_Event, z);
            }
        }
        
        [Script(DefineAsStatic=true, NoExeptions=true)]
        public void InternalEvent(bool b, object e, string f)
        {
            InternalEvent(b, e, f, "on" + f);

            //object z = ((DelegateImpl)e).InvokePointer;

            //if (b)
            //{
            //    if (Expando.InternalIsMember(this, "addEventListener"))
            //        addEventListener(f, z, false);
            //    if (Expando.InternalIsMember(this, "attachEvent"))
            //        attachEvent("on" + f, z);
            //}
            //else
            //{
            //    if (Expando.InternalIsMember(this, "removeEventListener"))
            //        removeEventListener(f, z, false);
            //    if (Expando.InternalIsMember(this, "detachEvent"))
            //        detachEvent("on" + f, z);
            //}
        }

        internal void attachEvent(string f, object e)
        {

        }

        internal void detachEvent(string f, object e)
        {

        }

        internal void addEventListener(string f, object e, bool c)
        {

        }

        internal void removeEventListener(string f, object e, bool c)
        {

        }
    }
}
