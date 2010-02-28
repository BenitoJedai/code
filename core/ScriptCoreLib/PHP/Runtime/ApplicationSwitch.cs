using ScriptCoreLib.Shared;

namespace ScriptCoreLib.PHP.Runtime
{

	[Script, System.Obsolete]
    internal class ApplicationSwitch<TItem> : ApplicationSwitch
    {
        public EventHandler<EventHandlerArgs<TItem>> this[List<TItem> range, EventHandler<Predicate<TItem, string>> convert]
        {
            set
            {
                EventHandler<ApplicationSwitch.EventHandlerArgs> _handler =
                    delegate(ApplicationSwitch.EventHandlerArgs args)
                    {
                        TItem v = range.Find(convert, args.Index);

                        Helper.Invoke(value, new ApplicationSwitch.EventHandlerArgs<TItem>(args, v));

                    };

                this[range.Convert<string>(convert).ToArray()] = _handler;
            }
        }

        public new static ApplicationSwitch<TItem> Of(ApplicationSwitch e)
        {
            ApplicationSwitch<TItem> n = new ApplicationSwitch<TItem>();

            n.QueryString = e.PageQueryString;
            n.Owner = e;

            return n;
        }
    }

    [Script]
    public class ApplicationSwitch : CustomSwitch<ApplicationSwitch, string>
    {

        public bool Run(string e)
        {
            return Run(this, e);
        }



        public string QueryString = Native.QueryString;
        public string Default = "";

        public ApplicationSwitch Owner;

        
        public string UpLocation
        {
            get
            {
                if (Owner == null)
                    return "?";

                return Owner.Location;
            }
        }

        public static ApplicationSwitch Of(ApplicationSwitch e)
        {
            ApplicationSwitch n = new ApplicationSwitch();

            n.QueryString = e.PageQueryString;
            n.Owner = e;

            return n;
        }

        public const string PageSeparator = "?";

        public string PageQueryString
        {
            get
            {
                string n = QueryString;
                int i = n.IndexOf(PageSeparator);

                if (i > -1)
                    return n.Substring(i + 1);

                return "";
            }
        }

        public string PageName
        {
            get
            {
                string n = QueryString;
                int i = n.IndexOf(PageSeparator);

                if (i > -1)
                    return n.Substring(0, i);

                return n;
            }
        }

        public bool Run()
        {

            return Run(QueryString);
        }

        /// <summary>
        /// runs with pagename and default
        /// </summary>
        public void DoPage()
        {
            Run(PageName, Default);
        }

        public void Run(string p, string def)
        {
            if (!Run(p))
                Run(def);
        }

        public string Location
        {
            get
            {
                string n = "";

                ApplicationSwitch s = this;

                if (s.PageName == "")
                    s = s.Owner;

                while (s != null)
                {
                    
                    n = PageSeparator + s.PageName + n;

                    s = s.Owner;
                }

                return n;
            }
        }
        public string GetLocation(string p)
        {
            return Location + PageSeparator + p;
        }

        public void WriteBreadCrumb()
        {
            Native.echo("<p>");

            Native.echo(Location);

            Native.echo("</p>");
        }

    }
}
