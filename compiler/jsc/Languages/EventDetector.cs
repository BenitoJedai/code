using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace jsc.Languages
{
    public class EventDetector
    {
        public EventInfo AddEvent;
        public EventInfo RemoveEvent;

        public EventDetector(MethodBase m)
        {
			if (m.IsInstanceConstructor())
                return;

            var any = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            #region add
            {
                var prefix = "add_";
                if (m.Name.StartsWith(prefix))
                {
                    AddEvent = m.DeclaringType.GetEvent(m.Name.Substring(prefix.Length), any);
                }
            }
            #endregion

            #region remove
            {
                var prefix = "remove_";
                if (m.Name.StartsWith(prefix))
                {
                    RemoveEvent = m.DeclaringType.GetEvent(m.Name.Substring(prefix.Length), any);
                }
            }
            #endregion

        }

        public static bool IsEvent(MethodBase zfn)
        {
            var v = new EventDetector(zfn);

            if (v.AddEvent != null)
                return true;

            if (v.RemoveEvent != null)
                return true;

            return false;
        }

        public static bool IsEvent(FieldInfo zfn)
        {
            if (zfn.DeclaringType.GetEvent(zfn.Name) != null)
                return true;

            return false;
        }
    }
}
