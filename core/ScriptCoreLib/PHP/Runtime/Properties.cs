
using ScriptCoreLib.PHP.IO;

namespace ScriptCoreLib.PHP.Runtime
{
    [Script]
    public class Properties
    {
        public string Default;

        IArray<string, string> Items;

        public static Properties Of(FileInfo e)
        {
            return new Properties(e.Text);
        }

        /// <summary>
        /// will not behave with unicode BOM mark
        /// </summary>
        /// <param name="content"></param>
        public Properties(string content)
        {
            IArray<int, string> a = IArray.Split(content, "\n");

            IArray<string, string> i = new IArray<string, string>();

            foreach (string v in a.ToArray())
            {
                string z = v.Trim();

                bool b = !z.StartsWith("#");

                if (b)
                    if (z.Length > 0)
                    {

                        IArray<int, string> x = IArray.Split(z, "=");

                        //Native.Dump(z + " : " + x.Length, x);


                        if (x.Length == 2)
                        {
                            //Native.Message("ok");


                            string key = x[0];
                            string val = x[1];

                            i[key] = val;
                        }
                    }
            }

            Items = i;

        }

        public string this[string e]
        {
            get
            {
                string z = Items[e];

                if (z == null)
                {
                    z = Default;

                    if (z == null)
                        z = "missing : " + e;
                }

                return z;
            }
        }

        /// <summary>
        /// dumps this object
        /// </summary>
        public void Dump()
        {
            Native.Dump("Properties", this);
        }
    }

}
