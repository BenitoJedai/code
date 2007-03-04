using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.DOM
{
    using StringArray = IArray<string>;

    [Script(HasNoPrototype=true)]
    public class ILocation
    {
        public string protocol;
        public string host;
        public string href;
        public string search;
        public string pathname;

        public bool IsHTTP
        {
            [Script(DefineAsStatic=true)]
            get { return protocol == "http:"; }
        }

        public void reload()
        {

        }

        public string this[string e]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                string z = null;

                string k = StringArray.Split(this.search, "?")[1];

                if (k != null)
                {
                    StringArray u = StringArray.Split(k, "&");


                    foreach (string x in u.ToArray())
                    {
                        StringArray n = StringArray.Split(x, "=");

                        if (n.length > 1)
                        {
                            if (Native.Window.unescape(n[0]) == e)
                            {
                                z = Native.Window.unescape(n[1]);

                                break;
                            }
                        }
                    }

                }
                    return z;
            }
        }

        public void replace(string e)
        {
        }
    }
}
