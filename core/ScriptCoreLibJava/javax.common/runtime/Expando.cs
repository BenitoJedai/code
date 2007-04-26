using ScriptCoreLib;

using java.util;

namespace javax.common.runtime
{
    [Script]
    public sealed class Expando
    {
        HashMap BaseList = new HashMap();

        public int Length
        {
            get
            {
                return BaseList.size();
            }
        }

        public object this[object key]
        {
            get
            {
                return BaseList.get(key);
            }

            set
            {
                BaseList.put(key, value);
            }
        }

        public object[] Keys
        {
            get
            {
                return BaseList.keySet().toArray();
            }
        }

        public object[] SortedKeys
        {
            get
            {
                object[] k = Keys;

                java.util.Arrays.sort(k);

                return k;
            }
        }

        public Expando Clone()
        {
            Expando a = new Expando();

            a.BaseList = (HashMap)this.BaseList.clone();

            return a;
        }
    }

}
