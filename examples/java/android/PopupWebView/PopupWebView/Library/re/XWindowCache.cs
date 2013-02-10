using android.util;
using java.lang;
using java.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopupWebView.Library
{
    public class XWindowCache
    {
        public Map sWindows;

        public XWindowCache()
        {
            sWindows = new HashMap();
        }

        public virtual XWindow getCache(int id, Class cls)
        {
            SparseArray l2 = (SparseArray)sWindows.get(cls);
            if (l2 == null)
            {
                return null;
            }

            return (XWindow)l2.get(id);
        }
        public virtual Set getCacheIds(Class cls)
        {
            SparseArray l2 = (SparseArray)sWindows.get(cls);
            if (l2 == null)
            {
                return new HashSet();
            }

            Set keys = new HashSet();
            for (int i = 0; i < l2.size(); i++)
            {
                keys.add(l2.keyAt(i));
            }
            return keys;
        }
        public virtual int getCacheSize(Class cls)
        {
            SparseArray l2 = (SparseArray)sWindows.get(cls);
            if (l2 == null)
            {
                return 0;
            }

            return l2.size();
        }
        public virtual bool isCached(int id, Class cls)
        {
            return getCache(id, cls) != null;
        }
        public virtual void putCache(int id, Class cls, XWindow window)
        {
            SparseArray l2 = (SparseArray)sWindows.get(cls);
            if (l2 == null)
            {
                l2 = new SparseArray();
                sWindows.put(cls, l2);
            }

            l2.put(id, window);
        }
        public virtual void removeCache(int id, Class cls)
        {
            SparseArray l2 = (SparseArray)sWindows.get(cls);
            if (l2 != null)
            {
                l2.remove(id);
                if (l2.size() == 0)
                {
                    sWindows.remove(cls);
                }
            }
        }
        public virtual int size()
        {
            return sWindows.size();
        }
    }
}
