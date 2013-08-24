using ScriptCoreLib.Shared;


using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;


using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

namespace  ScriptCoreLib.JavaScript.Net
{
    /// <summary>
    /// Provides a way to monitor web resource, and get notified as the resource changes
    /// </summary>
    [Script]
    [System.Obsolete]
    public class UrlMonitor
    {
        public readonly Timer MonitorTimer = new Timer();

        public event System.Action<UrlMonitor> ETagChanged;
        public event System.Action<UrlMonitor> RequestComplete;

        public static System.Action<UrlMonitor> ReloadDocument =
            delegate
            {
                Native.Document.location.reload();
            };



        /// <summary>
        /// monitors an url, and notifies if the etag changes
        /// </summary>
        /// <param name="url"></param>
        /// <param name="interval"></param>
        /// <param name="ETagChanged"></param>
        public UrlMonitor(string url, int interval, System.Action<UrlMonitor> ETagChanged)
            : this(url, interval)
        {
            this.ETagChanged += ETagChanged;

            this.Enabled = true;
        }

        public UrlMonitor(string url, int interval)
        {

            MonitorTimer.Tick += new System.Action<Timer>(t_Tick);

            this.Url = url;
            this.Interval = interval;
        }

        IXMLHttpRequest r;

        public IXMLHttpRequest Request
        {
            get
            {
                return r;
            }
        }

        bool _async = false;

        void t_Tick(Timer e)
        {
            if (_async)
                return;

            _async = true;


            try
            {
                r = new IXMLHttpRequest();
                r.open(HTTPMethodEnum.HEAD, Url);
                r.send();
            }
            catch
            {
                Native.window.alert(" send error ");
            }

            r.InvokeOnComplete(
                delegate(IXMLHttpRequest rr)
                {
                    

                    if (RequestComplete != null)
                        RequestComplete(this);

                    if (rr.status == IXMLHttpRequest.HTTPStatusCodes.OK)
                    {
                        string z = rr.ETag;

                        bool b = (z != ETag) && (ETag != null) && (z != null);

                        ETag = z;

                        if (b)
                        {
                            if (ETagChanged != null)
                                ETagChanged(this);

                            this.Enabled = false;
                            return;
                        }


                    }
                    else
                    {
                        this.Enabled = false;
                        return;
                    }


                    _async = false;
                

            }
            );
        }

        public string ETag;
        public int Interval;
        public string Url;

        public bool Enabled
        {
            get
            {
                return MonitorTimer.IsAlive;
            }
            set
            {
                if (value && !MonitorTimer.IsAlive)
                {
                    MonitorTimer.StartInterval(Interval);
                }
                else
                {
                    if (!value && MonitorTimer.IsAlive)
                    {
                        MonitorTimer.Stop();
                    }
                }

            }
        }



    }

}
